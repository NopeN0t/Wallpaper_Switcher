using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BG_Lib
{
    public class Shuffler
    {
        public string FolderPath { get; set; }
        private static readonly string LAST_RNG = "latest_RNG.txt";
        private static readonly string[] IgnoreList = { "desktop.ini", LAST_RNG };
        private static readonly Random Rng = new Random();

        /// <summary>
        /// Shuffles the contents of the folder at the specified path or the current folder path.
        /// </summary>
        /// <param name="path">The path to the folder to shuffle. If null, the method uses the current value of FolderPath.</param>
        /// <exception cref="ArgumentNullException">Thrown if no folder path is specified and FolderPath is null.</exception>
        public void Shuffle(string path = null)
        {
            if (path != null) FolderPath = path;
            if (FolderPath == null) throw new ArgumentNullException(nameof(FolderPath));

            string lastRngPath = Path.Combine(FolderPath, LAST_RNG);
            if (File.Exists(lastRngPath)) Undo();
            Randomize();
        }

        /// <summary>
        /// Resets the current state to its initial configuration if a previous state exists.
        /// </summary>
        /// <remarks>If a previous state file is detected, this method reverts the state to the last saved
        /// configuration. Otherwise, no action is taken. This method is typically used to undo changes and restore the
        /// original state.</remarks>
        public void Reset()
        {
            if (FolderPath == null) throw new ArgumentNullException(nameof(FolderPath));
            string lastRngPath = Path.Combine(FolderPath, LAST_RNG);
            if (File.Exists(lastRngPath)) Undo();
        }

        /// <summary>
        /// Randomizes the file names within the specified folder, excluding files listed in the ignore list, and
        /// records the changes to a log file.
        /// </summary>
        /// <remarks>This method renames all files in the target folder (except those in the ignore list)
        /// to randomized, sequentially numbered names. The original and new file names are recorded in a log file for
        /// reference. The method is intended for internal use and may prompt for user input before
        /// completion.</remarks>
        private void Randomize()
        {
            // get all files in the folder, excluding ignore list
            List<string> files = Directory.GetFiles(FolderPath).Where(f => !IgnoreList.Contains(Path.GetFileName(f))).ToList();
            if (files.Count == 0) return;

            // compute digits for zero-padded numbering
            int digits = (int)Math.Floor(Math.Log10(files.Count)) + 1;

            // temporarily rename the files (use temp prefix)
            string tempPrefix = ".temp_";
            for (int i = 0; i < files.Count; i++)
            {
                string origName = Path.GetFileName(files[i]);
                string tempName = Path.Combine(FolderPath, tempPrefix + i + "_" + origName);
                File.Move(files[i], tempName);
                files[i] = tempName;
            }

            // generate shuffled indices
            int[] randomNumbers = Enumerable.Range(1, files.Count).ToArray();
            for (int i = randomNumbers.Length - 1; i > 0; i--)
            {
                int j = Rng.Next(i + 1);
                (randomNumbers[j], randomNumbers[i]) = (randomNumbers[i], randomNumbers[j]);
            }

            string lastRngPath = Path.Combine(FolderPath, LAST_RNG);
            using (var sw = new StreamWriter(lastRngPath))
            {
                for (int i = 0; i < files.Count; i++)
                {
                    string tempPath = files[i];
                    string origFileName = Path.GetFileName(tempPath);
                    // remove the temp prefix and index
                    string originalName = origFileName;
                    if (originalName.StartsWith(tempPrefix))
                    {
                        // tempPrefix + index + "_" + origFileName
                        int underscoreIndex = originalName.IndexOf('_', tempPrefix.Length);
                        if (underscoreIndex >= 0 && underscoreIndex + 1 < originalName.Length)
                            originalName = originalName.Substring(underscoreIndex + 1);
                    }

                    string newFileNameOnly = randomNumbers[i].ToString($"D{digits}") + Path.GetExtension(tempPath);
                    string newFilePath = Path.Combine(FolderPath, newFileNameOnly);

                    // log original and new names (filenames only)
                    sw.WriteLine($"{originalName}|||{newFileNameOnly}");

                    // perform final rename
                    File.Move(tempPath, newFilePath);
                }
            }
        }

        /// <summary>
        /// Reverts the most recent file renaming operation by restoring original file names based on the recorded
        /// changes.
        /// </summary>
        /// <remarks>This method reads a log file containing the details of the last renaming operation
        /// and attempts to undo each change. If any file cannot be restored, the method continues processing the
        /// remaining entries. The log file is deleted after the undo operation completes. This method is intended for
        /// internal use and is not thread-safe.</remarks>
        private void Undo()
        {
            string lastRngPath = Path.Combine(FolderPath, LAST_RNG);
            if (!File.Exists(lastRngPath)) return;

            string[] lines = File.ReadAllLines(lastRngPath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { "|||" }, StringSplitOptions.None);
                if (parts.Length != 2) continue;
                string original = Path.Combine(FolderPath, parts[0]);
                string shuffled = Path.Combine(FolderPath, parts[1]);
                string tempRestore = shuffled + ".restore_tmp";
                try
                {
                    if (File.Exists(shuffled))
                    {
                        File.Move(shuffled, tempRestore);
                        File.Move(tempRestore, original);
                    }
                }
                catch
                {
                    // best-effort: continue restoring others
                }
            }
            try
            {
                File.Delete(lastRngPath);
            }
            catch { }
        }
    }
}
