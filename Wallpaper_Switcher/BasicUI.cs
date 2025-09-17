using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallpaper_Switcher
{
    partial class MainForm
    {
        //This part stores minor event handlers and UI logic that doesn't need to change often
        private void Reset_Button_Click(object sender, EventArgs e)
        {
            bg_switcher.Elasped = 0;
        }
        private void Set_Image_Click(object sender, EventArgs e)
        {
            bg_switcher.Change_BG(DemoList.SelectedIndex);
        }
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            PlaySwitchAnimation();
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
    }
}
