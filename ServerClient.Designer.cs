namespace Seminar
{
    partial class ServerClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerClient));
            this.start = new System.Windows.Forms.Button();
            this.join = new System.Windows.Forms.Button();
            this.LocalHost = new System.Windows.Forms.Button();
            this.Join_Local = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyStats = new System.Windows.Forms.ToolStripTextBox();
            this.TopPlayers = new System.Windows.Forms.ToolStripTextBox();
            this.HomePage = new System.Windows.Forms.ToolStripTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.start.BackColor = System.Drawing.Color.OldLace;
            this.start.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.start.Location = new System.Drawing.Point(349, 200);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(201, 27);
            this.start.TabIndex = 0;
            this.start.Text = "start_server";
            this.start.UseVisualStyleBackColor = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // join
            // 
            this.join.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.join.BackColor = System.Drawing.Color.OldLace;
            this.join.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.join.Location = new System.Drawing.Point(349, 333);
            this.join.Name = "join";
            this.join.Size = new System.Drawing.Size(201, 23);
            this.join.TabIndex = 1;
            this.join.Text = "Server_List";
            this.join.UseVisualStyleBackColor = false;
            this.join.Click += new System.EventHandler(this.join_Click);
            // 
            // LocalHost
            // 
            this.LocalHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalHost.BackColor = System.Drawing.Color.OldLace;
            this.LocalHost.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LocalHost.Location = new System.Drawing.Point(349, 244);
            this.LocalHost.Name = "LocalHost";
            this.LocalHost.Size = new System.Drawing.Size(201, 23);
            this.LocalHost.TabIndex = 2;
            this.LocalHost.Text = "Local_host";
            this.LocalHost.UseVisualStyleBackColor = false;
            this.LocalHost.Click += new System.EventHandler(this.LocalHost_Click);
            // 
            // Join_Local
            // 
            this.Join_Local.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Join_Local.BackColor = System.Drawing.Color.OldLace;
            this.Join_Local.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Join_Local.Location = new System.Drawing.Point(349, 381);
            this.Join_Local.Name = "Join_Local";
            this.Join_Local.Size = new System.Drawing.Size(201, 23);
            this.Join_Local.TabIndex = 3;
            this.Join_Local.Text = "Join_Local";
            this.Join_Local.UseVisualStyleBackColor = false;
            this.Join_Local.Click += new System.EventHandler(this.Join_Local_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.MyStats,
            this.TopPlayers,
            this.HomePage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(896, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logOutToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::Seminar.Properties.Resources.log_out;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "LogOut";
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.logOutToolStripMenuItem.Text = "LogOut";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // MyStats
            // 
            this.MyStats.BackColor = System.Drawing.SystemColors.MenuBar;
            this.MyStats.Name = "MyStats";
            this.MyStats.ReadOnly = true;
            this.MyStats.Size = new System.Drawing.Size(100, 25);
            this.MyStats.Text = "MyStats";
            this.MyStats.Click += new System.EventHandler(this.MyStats_Click);
            // 
            // TopPlayers
            // 
            this.TopPlayers.BackColor = System.Drawing.SystemColors.MenuBar;
            this.TopPlayers.Name = "TopPlayers";
            this.TopPlayers.ReadOnly = true;
            this.TopPlayers.Size = new System.Drawing.Size(100, 25);
            this.TopPlayers.Text = "TopPlayers";
            this.TopPlayers.Click += new System.EventHandler(this.TopPlayers_Click);
            // 
            // HomePage
            // 
            this.HomePage.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.HomePage.BackColor = System.Drawing.SystemColors.Menu;
            this.HomePage.Name = "HomePage";
            this.HomePage.ReadOnly = true;
            this.HomePage.Size = new System.Drawing.Size(100, 25);
            this.HomePage.Text = "HomePage";
            this.HomePage.Click += new System.EventHandler(this.HomePage_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(349, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.OldLace;
            this.button3.Location = new System.Drawing.Point(349, 290);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(201, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Server Options";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ServerClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(896, 431);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Join_Local);
            this.Controls.Add(this.LocalHost);
            this.Controls.Add(this.join);
            this.Controls.Add(this.start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerClient";
            this.Text = "Main Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerClient_FormClosing);
            this.Load += new System.EventHandler(this.ServerClient_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button join;
        private System.Windows.Forms.Button LocalHost;
        private System.Windows.Forms.Button Join_Local;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox MyStats;
        private System.Windows.Forms.ToolStripTextBox TopPlayers;
        private System.Windows.Forms.ToolStripTextBox HomePage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}