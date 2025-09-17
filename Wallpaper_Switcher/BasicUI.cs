using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Wallpaper_Switcher
{
    partial class MainForm
    {
        //This part stores minor event handlers and UI logic that doesn't need to change often or done implementing
        //It is recommend to move all of this back to MainForm.cs if you want to view code by double clicking
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
            bg_switcher.Image_Index = DemoList.SelectedIndex;
        }
        private void NextImage_Button_Click(object sender, EventArgs e)
        {
            bg_switcher.Change_BG(++bg_switcher.Image_Index);
            DemoList.SelectedIndex = bg_switcher.Image_Index;
        }
        private void LastImage_Button_Click(object sender, EventArgs e)
        {
            bg_switcher.Change_BG(--bg_switcher.Image_Index);
            DemoList.SelectedIndex = bg_switcher.Image_Index;
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
        private void On_Visiblity_Change(object sender, EventArgs e)
        {
            if (!this.Visible)
                Preview.Image?.Dispose();
            else
            {
                try { Preview.Image = Image.FromFile(bg_switcher.GetImages()[DemoList.SelectedIndex]); }
                catch { MessageBox.Show($"ImageBox doesn't like\nThis file : {bg_switcher.GetImages()[DemoList.SelectedIndex]}", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        private void Set_Button_Click(object sender, EventArgs e)
        {
            try
            {
                int parsed = int.Parse(Timer_Box.Text);
                if (parsed <= 0) throw new Exception("Interval must be greater than 0");
                bg_switcher.Change_Interval = parsed;
                Timer.Text = "Timer      =  " + SecondsToString(bg_switcher.Change_Interval);

                if (!string.IsNullOrWhiteSpace(Elapsed_box.Text))
                {
                    parsed = int.Parse(Elapsed_box.Text);
                    if (parsed >= bg_switcher.Change_Interval)
                        bg_switcher.Elasped = bg_switcher.Change_Interval;
                    bg_switcher.Elasped = parsed;
                }
            }
            catch (Exception er) { MessageBox.Show("Error: " + er.Message, "Invaild Input", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
