namespace Wallpaper_Switcher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.IconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OK_Button = new System.Windows.Forms.Button();
            this.Reset_Button = new System.Windows.Forms.Button();
            this.Elapsed = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Label();
            this.Timer_Box = new System.Windows.Forms.TextBox();
            this.Source_Box = new System.Windows.Forms.TextBox();
            this.Path_text = new System.Windows.Forms.Label();
            this.Browse = new System.Windows.Forms.Button();
            this.DemoList = new System.Windows.Forms.ListBox();
            this.Preview = new System.Windows.Forms.PictureBox();
            this.Timer_Text = new System.Windows.Forms.Label();
            this.More_Button = new System.Windows.Forms.Button();
            this.Total_Text = new System.Windows.Forms.Label();
            this.Selected_Image = new System.Windows.Forms.Label();
            this.Set_Image = new System.Windows.Forms.Button();
            this.Elapsed_CFG = new System.Windows.Forms.Label();
            this.Elapsed_box = new System.Windows.Forms.TextBox();
            this.Startup = new System.Windows.Forms.CheckBox();
            this.NextImage_Button = new System.Windows.Forms.Button();
            this.LastImage_Button = new System.Windows.Forms.Button();
            this.Set_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.Text = "Wallpaper Switcher";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // IconMenu
            // 
            this.IconMenu.Name = "IconMenu";
            this.IconMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // OK_Button
            // 
            this.OK_Button.Location = new System.Drawing.Point(304, 295);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(104, 28);
            this.OK_Button.TabIndex = 4;
            this.OK_Button.Text = "OK";
            this.OK_Button.UseVisualStyleBackColor = true;
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Reset_Button
            // 
            this.Reset_Button.Location = new System.Drawing.Point(637, 294);
            this.Reset_Button.Name = "Reset_Button";
            this.Reset_Button.Size = new System.Drawing.Size(105, 28);
            this.Reset_Button.TabIndex = 12;
            this.Reset_Button.Text = "Reset Timer";
            this.Reset_Button.UseVisualStyleBackColor = true;
            this.Reset_Button.Click += new System.EventHandler(this.Reset_Button_Click);
            // 
            // Elapsed
            // 
            this.Elapsed.AutoSize = true;
            this.Elapsed.Location = new System.Drawing.Point(12, 294);
            this.Elapsed.Name = "Elapsed";
            this.Elapsed.Size = new System.Drawing.Size(93, 20);
            this.Elapsed.TabIndex = 3;
            this.Elapsed.Text = "Elapsed = 0";
            // 
            // Timer
            // 
            this.Timer.AutoSize = true;
            this.Timer.Location = new System.Drawing.Point(12, 265);
            this.Timer.Name = "Timer";
            this.Timer.Size = new System.Drawing.Size(81, 20);
            this.Timer.TabIndex = 4;
            this.Timer.Text = "Timer      =";
            // 
            // Timer_Box
            // 
            this.Timer_Box.Location = new System.Drawing.Point(526, 12);
            this.Timer_Box.Name = "Timer_Box";
            this.Timer_Box.Size = new System.Drawing.Size(216, 26);
            this.Timer_Box.TabIndex = 5;
            // 
            // Source_Box
            // 
            this.Source_Box.Location = new System.Drawing.Point(60, 12);
            this.Source_Box.Name = "Source_Box";
            this.Source_Box.Size = new System.Drawing.Size(245, 26);
            this.Source_Box.TabIndex = 0;
            // 
            // Path_text
            // 
            this.Path_text.AutoSize = true;
            this.Path_text.Location = new System.Drawing.Point(12, 15);
            this.Path_text.Name = "Path_text";
            this.Path_text.Size = new System.Drawing.Size(42, 20);
            this.Path_text.TabIndex = 7;
            this.Path_text.Text = "Path";
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(314, 12);
            this.Browse.Margin = new System.Windows.Forms.Padding(2);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(95, 28);
            this.Browse.TabIndex = 1;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // DemoList
            // 
            this.DemoList.FormattingEnabled = true;
            this.DemoList.ItemHeight = 20;
            this.DemoList.Location = new System.Drawing.Point(16, 44);
            this.DemoList.Name = "DemoList";
            this.DemoList.Size = new System.Drawing.Size(186, 204);
            this.DemoList.TabIndex = 2;
            this.DemoList.SelectedIndexChanged += new System.EventHandler(this.DemoList_SelectedIndexChanged);
            // 
            // Preview
            // 
            this.Preview.Location = new System.Drawing.Point(208, 44);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(200, 200);
            this.Preview.TabIndex = 10;
            this.Preview.TabStop = false;
            // 
            // Timer_Text
            // 
            this.Timer_Text.AutoSize = true;
            this.Timer_Text.Location = new System.Drawing.Point(425, 15);
            this.Timer_Text.Name = "Timer_Text";
            this.Timer_Text.Size = new System.Drawing.Size(95, 20);
            this.Timer_Text.TabIndex = 11;
            this.Timer_Text.Text = "Timer (sec) :";
            // 
            // More_Button
            // 
            this.More_Button.Location = new System.Drawing.Point(304, 261);
            this.More_Button.Name = "More_Button";
            this.More_Button.Size = new System.Drawing.Size(104, 28);
            this.More_Button.TabIndex = 3;
            this.More_Button.Text = "More >>";
            this.More_Button.UseVisualStyleBackColor = true;
            this.More_Button.Click += new System.EventHandler(this.More_Button_Click);
            // 
            // Total_Text
            // 
            this.Total_Text.AutoSize = true;
            this.Total_Text.Location = new System.Drawing.Point(425, 84);
            this.Total_Text.Name = "Total_Text";
            this.Total_Text.Size = new System.Drawing.Size(105, 20);
            this.Total_Text.TabIndex = 14;
            this.Total_Text.Text = "Total Image : ";
            // 
            // Selected_Image
            // 
            this.Selected_Image.AutoSize = true;
            this.Selected_Image.Location = new System.Drawing.Point(425, 119);
            this.Selected_Image.Name = "Selected_Image";
            this.Selected_Image.Size = new System.Drawing.Size(124, 20);
            this.Selected_Image.TabIndex = 15;
            this.Selected_Image.Text = "Slected Image : ";
            // 
            // Set_Image
            // 
            this.Set_Image.Location = new System.Drawing.Point(637, 80);
            this.Set_Image.Name = "Set_Image";
            this.Set_Image.Size = new System.Drawing.Size(105, 28);
            this.Set_Image.TabIndex = 7;
            this.Set_Image.Text = "Set Image";
            this.Set_Image.UseVisualStyleBackColor = true;
            this.Set_Image.Click += new System.EventHandler(this.Set_Image_Click);
            // 
            // Elapsed_CFG
            // 
            this.Elapsed_CFG.AutoSize = true;
            this.Elapsed_CFG.Location = new System.Drawing.Point(425, 47);
            this.Elapsed_CFG.Name = "Elapsed_CFG";
            this.Elapsed_CFG.Size = new System.Drawing.Size(114, 20);
            this.Elapsed_CFG.TabIndex = 16;
            this.Elapsed_CFG.Text = "Elapsed (sec) :";
            // 
            // Elapsed_box
            // 
            this.Elapsed_box.Location = new System.Drawing.Point(545, 44);
            this.Elapsed_box.Name = "Elapsed_box";
            this.Elapsed_box.Size = new System.Drawing.Size(197, 26);
            this.Elapsed_box.TabIndex = 6;
            // 
            // Startup
            // 
            this.Startup.AutoSize = true;
            this.Startup.Location = new System.Drawing.Point(429, 152);
            this.Startup.Name = "Startup";
            this.Startup.Size = new System.Drawing.Size(134, 24);
            this.Startup.TabIndex = 10;
            this.Startup.Text = "Run on startup";
            this.Startup.UseVisualStyleBackColor = true;
            this.Startup.CheckedChanged += new System.EventHandler(this.Startup_CheckedChanged);
            // 
            // NextImage_Button
            // 
            this.NextImage_Button.Location = new System.Drawing.Point(637, 114);
            this.NextImage_Button.Name = "NextImage_Button";
            this.NextImage_Button.Size = new System.Drawing.Size(105, 28);
            this.NextImage_Button.TabIndex = 8;
            this.NextImage_Button.Text = "Next Image";
            this.NextImage_Button.UseVisualStyleBackColor = true;
            this.NextImage_Button.Click += new System.EventHandler(this.NextImage_Button_Click);
            // 
            // LastImage_Button
            // 
            this.LastImage_Button.Location = new System.Drawing.Point(637, 148);
            this.LastImage_Button.Name = "LastImage_Button";
            this.LastImage_Button.Size = new System.Drawing.Size(105, 28);
            this.LastImage_Button.TabIndex = 9;
            this.LastImage_Button.Text = "Last Image";
            this.LastImage_Button.UseVisualStyleBackColor = true;
            this.LastImage_Button.Click += new System.EventHandler(this.LastImage_Button_Click);
            // 
            // Set_Button
            // 
            this.Set_Button.Location = new System.Drawing.Point(526, 294);
            this.Set_Button.Name = "Set_Button";
            this.Set_Button.Size = new System.Drawing.Size(105, 28);
            this.Set_Button.TabIndex = 11;
            this.Set_Button.Text = "Set Timer";
            this.Set_Button.UseVisualStyleBackColor = true;
            this.Set_Button.Click += new System.EventHandler(this.Set_Button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(754, 331);
            this.Controls.Add(this.Set_Button);
            this.Controls.Add(this.LastImage_Button);
            this.Controls.Add(this.NextImage_Button);
            this.Controls.Add(this.Startup);
            this.Controls.Add(this.Elapsed_box);
            this.Controls.Add(this.Elapsed_CFG);
            this.Controls.Add(this.Set_Image);
            this.Controls.Add(this.Selected_Image);
            this.Controls.Add(this.Total_Text);
            this.Controls.Add(this.More_Button);
            this.Controls.Add(this.Timer_Text);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.DemoList);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.Path_text);
            this.Controls.Add(this.Source_Box);
            this.Controls.Add(this.Timer_Box);
            this.Controls.Add(this.Timer);
            this.Controls.Add(this.Elapsed);
            this.Controls.Add(this.Reset_Button);
            this.Controls.Add(this.OK_Button);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Wallpaper Switcher";
            this.VisibleChanged += new System.EventHandler(this.On_Visiblity_Change);
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip IconMenu;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.Button Reset_Button;
        private System.Windows.Forms.Label Elapsed;
        private System.Windows.Forms.Label Timer;
        private System.Windows.Forms.TextBox Timer_Box;
        private System.Windows.Forms.TextBox Source_Box;
        private System.Windows.Forms.Label Path_text;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.ListBox DemoList;
        private System.Windows.Forms.PictureBox Preview;
        private System.Windows.Forms.Label Timer_Text;
        private System.Windows.Forms.Button More_Button;
        private System.Windows.Forms.Label Total_Text;
        private System.Windows.Forms.Label Selected_Image;
        private System.Windows.Forms.Button Set_Image;
        private System.Windows.Forms.Label Elapsed_CFG;
        private System.Windows.Forms.TextBox Elapsed_box;
        private System.Windows.Forms.CheckBox Startup;
        private System.Windows.Forms.Button NextImage_Button;
        private System.Windows.Forms.Button LastImage_Button;
        private System.Windows.Forms.Button Set_Button;
    }
}

