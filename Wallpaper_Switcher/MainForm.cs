using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Wallpaper_Switcher.InternalLibs.BG_Switcher;

namespace Wallpaper_Switcher
{
    public partial class MainForm : Form
    {
        private Control[] MorePage;
        private readonly Icon[] frames = { 
                                  Properties.Resources.refresh_1, Properties.Resources.refresh_2, Properties.Resources.refresh_3,
                                  Properties.Resources.refresh_4, Properties.Resources.refresh_5, Properties.Resources.refresh_6,
                                  Properties.Resources.refresh_7, Properties.Resources.refresh_8};
        private readonly BG_Switcher bg_switcher = new BG_Switcher();
        
        public MainForm()
        {
            InitializeComponent();
            InitializeProgram();
        }
        private void InitializeProgram()
        {
            MorePage = new Control[] { Timer_Text, Timer_Box, Reset_Button, Total_Text, Selected_Image ,Set_Image,
                                       Elapsed_CFG, Elapsed_box};
            this.Width = 440;
            NotifyIcon.ContextMenuStrip = IconMenu;
            NotifyIcon.Icon = frames[0];
            NotifyIcon.Visible = true;
            Preview.SizeMode = PictureBoxSizeMode.Zoom;

            //Load previous state
            bg_switcher.Load_State();
            Timer_Box.Text = bg_switcher.Change_Interval.ToString();
            Timer.Text = "Timer      :  " + SecondsToString(bg_switcher.Change_Interval);
            Source_Box.Text = bg_switcher.BG_Source;
            Elapsed.Text = "Elapsed :  " + SecondsToString(bg_switcher.Elasped);
            foreach (var file in bg_switcher.GetImages())
                DemoList.Items.Add(Path.GetFileName(file));
            if (DemoList.Items.Count > 0) DemoList.SelectedIndex = 0;
            Total_Text.Text = "Total Image : " + bg_switcher.GetImages().Count.ToString();

            //Setup events
            this.FormClosing += (s, e) => bg_switcher.Save_State();
            bg_switcher.OnBackgroundChanged += (s, e) => PlaySwitchAnimation();
            bg_switcher.TimerTick += (s, e) => HandleTick();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            try { bg_switcher.Start(); }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Hide();
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

        private void DemoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preview.Image?.Dispose();
            try { Preview.Image = Image.FromFile(bg_switcher.GetImages()[DemoList.SelectedIndex]); }
            catch { MessageBox.Show($"ImageBox doesn't like\nThis file : {bg_switcher.GetImages()[DemoList.SelectedIndex]}", "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            Selected_Image.Text = $"Slected Image : {DemoList.SelectedIndex}";
        }
    }
}
