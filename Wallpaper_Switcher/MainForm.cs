using Microsoft.Win32;
using System;
using System.Diagnostics;
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

        private static readonly string PROGRAMNAME = "Wallpaper Switcher";
        private static readonly FileVersionInfo PROGRAM_VERSION = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

        public MainForm()
        {
            InitializeComponent();
            InitializeProgram();
        }
        private void InitializeProgram()
        {
            //Setup More Menu
            MorePage = new Control[] { Timer_Text, Timer_Box, Reset_Button, Total_Text, Selected_Image ,Set_Image,
                                       Elapsed_CFG, Elapsed_box, Startup};
            this.Width = 440;
            foreach (var ctrl in MorePage) ctrl.Enabled = false;

            //Setup NotifyIcon
            NotifyIcon.ContextMenuStrip = IconMenu;
            NotifyIcon.Icon = frames[0];
            NotifyIcon.Visible = true;
            Preview.SizeMode = PictureBoxSizeMode.Zoom;

            //Load previous state
            bg_switcher.Load_State();
            Source_Box.Text = bg_switcher.BG_Source;
            Timer_Box.Text = bg_switcher.Change_Interval.ToString();
            Timer.Text = "Timer      :  " + SecondsToString(bg_switcher.Change_Interval);
            Elapsed.Text = "Elapsed :  " + SecondsToString(bg_switcher.Elasped);
            foreach (var file in bg_switcher.GetImages())
                DemoList.Items.Add(Path.GetFileName(file));
            if (bg_switcher.Image_Index != 0)
                DemoList.SelectedIndex = bg_switcher.Image_Index;
            else
                DemoList.SelectedIndex = 0;
            Total_Text.Text = "Total Image : " + bg_switcher.GetImages().Count.ToString();

            //Setup events
            this.FormClosing += (s, e) => { bg_switcher.Save_State(); bg_switcher.Stop(); };
            bg_switcher.OnBackgroundChanged += (s, e) => PlaySwitchAnimation();
            bg_switcher.TimerTick += (s, e) => HandleTick();

            //Auto start
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Startup.Checked = rk?.GetValue(PROGRAMNAME) != null;
                //Check if auto start is valid (program location, version check)
                if (Startup.Checked)
                {
                    FileVersionInfo Registry_version = FileVersionInfo.GetVersionInfo(rk.GetValue(PROGRAMNAME).ToString());
                    if (rk.GetValue(PROGRAMNAME).ToString() != Application.ExecutablePath ||    //Path check
                        PROGRAM_VERSION.FileVersion.CompareTo(Registry_version.FileVersion) > 0)//Version check
                    {
                        rk.DeleteValue(PROGRAMNAME, false);
                        rk.SetValue(PROGRAMNAME, Application.ExecutablePath);
                    }
                    StartTimer();
                }
            }
            catch (Exception e)
            { MessageBox.Show("Error loading startup settings\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            PlaySwitchAnimation();
        }
    }
}
