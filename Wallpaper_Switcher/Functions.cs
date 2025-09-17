using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wallpaper_Switcher
{
    partial class MainForm
    {
        //This class stores custom functions OOP style
        private void PlaySwitchAnimation()
        {
            Task.Run(() =>
            {
                foreach (var frame in frames)
                {
                    NotifyIcon.Icon = frame;
                    this.Invoke(new Action(() => { this.Icon = frame; }));
                    Thread.Sleep(100);
                }
                NotifyIcon.Icon = frames[0];
            });
        }
        private string SecondsToString(int time)
        {
            int sec = time % 60;
            int min = time / 60;
            int hrs = min / 60;
            return $"{hrs}hr {min}min  {sec}sec";
        }
        private void HandleTick()
        {
            if (!this.Visible && !IconMenu.Visible) return;
            if (this.Visible)
            {
                this.Invoke(new Action(() =>
                {
                Elapsed.Text = "Elapsed :  " + SecondsToString(bg_switcher.Elasped);
                }));
            }
            if (IconMenu.Visible)
            {
                this.Invoke(new Action(() =>
                {
                
                }));
            }
        }
    }
}
