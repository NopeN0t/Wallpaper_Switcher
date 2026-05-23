using System.Diagnostics;

namespace BG_Lib.Managers.linux
{
    public partial class Wallpaper_Linux
    {

        private bool Try_Set_GNOME(string filepath)
        {
            try
            {
                return RunProcess("gsettings",
                     $"set org.gnome.desktop.background picture-uri \"file://{filepath}\"");
            }
            catch { return false; }
        }
        private bool Try_Set_KDE(string filepath)
        {
            try { return RunProcess("plasma-apply-wallpaperimage", $"\"{filepath}\""); }
            catch { return false; }
        }
        private bool RunProcess(string filepath, string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = filepath,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    process.WaitForExit(10000);
                    return process.ExitCode == 0;
                }
            }
            catch { return false; }
        }
    }
}
