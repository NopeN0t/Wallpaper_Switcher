using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Wallpaper_Switcher
{
    partial class MainForm
    {
        //This part stores minor event handlers and UI logic that doesn't need to change often or done implementing
        private void OK_Button_Click(object sender, EventArgs e)
        {
            StartTimer();
        }
        private void Reset_Button_Click(object sender, EventArgs e)
        {
            bg_switcher.Elasped = 0;
        }
        private void Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Source_Box.Text = fbd.SelectedPath;
                bg_switcher.BG_Source = fbd.SelectedPath;
            }
        }
        private void Set_Image_Click(object sender, EventArgs e)
        {
            bg_switcher.Change_BG(DemoList.SelectedIndex);
        }
        private void More_Button_Click(object sender, EventArgs e)
        {
            if (this.Width == 770)
            {
                this.Width = 440;
                More_Button.Text = "More >>";
                foreach (var ctrl in MorePage) ctrl.Enabled = false;
            }
            else
            {
                this.Width = 770;
                More_Button.Text = "Less <<";
                foreach (var ctrl in MorePage) ctrl.Enabled = true;
            }
        }
        private void Startup_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (Startup.Checked)
                rk.SetValue(PROGRAMNAME, Application.ExecutablePath);
            else
                rk.DeleteValue(PROGRAMNAME, false);

        }
        private void DemoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preview.Image?.Dispose();
            try { Preview.Image = Image.FromFile(bg_switcher.GetImages()[DemoList.SelectedIndex]); }
            catch { MessageBox.Show($"ImageBox doesn't like\nThis file : {bg_switcher.GetImages()[DemoList.SelectedIndex]}", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            Selected_Image.Text = $"Slected Image : {DemoList.SelectedIndex}";
        }
    }
}
