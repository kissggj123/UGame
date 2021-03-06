﻿namespace The_UGamer_Launcher
{
    partial class BrowserWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LockButton = new System.Windows.Forms.Button();
            this.FavoriteButton = new System.Windows.Forms.Button();
            this.NewTabButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.LinksBar = new System.Windows.Forms.ToolStrip();
            this.BrowserDock = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.OpacityBar = new System.Windows.Forms.TrackBar();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.OpacityBar);
            this.panel1.Controls.Add(this.LockButton);
            this.panel1.Controls.Add(this.FavoriteButton);
            this.panel1.Controls.Add(this.NewTabButton);
            this.panel1.Controls.Add(this.RefreshButton);
            this.panel1.Controls.Add(this.SearchButton);
            this.panel1.Controls.Add(this.AddressBox);
            this.panel1.Controls.Add(this.ForwardButton);
            this.panel1.Controls.Add(this.BackButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 32);
            this.panel1.TabIndex = 3;
            // 
            // LockButton
            // 
            this.LockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LockButton.Location = new System.Drawing.Point(886, 6);
            this.LockButton.Name = "LockButton";
            this.LockButton.Size = new System.Drawing.Size(27, 23);
            this.LockButton.TabIndex = 9;
            this.LockButton.UseVisualStyleBackColor = true;
            this.LockButton.Click += new System.EventHandler(this.LockButton_Click);
            // 
            // FavoriteButton
            // 
            this.FavoriteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FavoriteButton.Location = new System.Drawing.Point(856, 6);
            this.FavoriteButton.Name = "FavoriteButton";
            this.FavoriteButton.Size = new System.Drawing.Size(24, 22);
            this.FavoriteButton.TabIndex = 22;
            this.FavoriteButton.Text = "⭐";
            this.FavoriteButton.UseVisualStyleBackColor = true;
            this.FavoriteButton.Click += new System.EventHandler(this.FavoriteButton_Click);
            // 
            // NewTabButton
            // 
            this.NewTabButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewTabButton.Location = new System.Drawing.Point(795, 6);
            this.NewTabButton.Name = "NewTabButton";
            this.NewTabButton.Size = new System.Drawing.Size(55, 22);
            this.NewTabButton.TabIndex = 21;
            this.NewTabButton.Text = "New Tab";
            this.NewTabButton.UseVisualStyleBackColor = true;
            this.NewTabButton.Click += new System.EventHandler(this.NewTabButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(70, 0);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(25, 32);
            this.RefreshButton.TabIndex = 20;
            this.RefreshButton.Text = "↺";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(646, 5);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(24, 22);
            this.SearchButton.TabIndex = 19;
            this.SearchButton.Text = "🔍";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // AddressBox
            // 
            this.AddressBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressBox.Location = new System.Drawing.Point(101, 6);
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(551, 20);
            this.AddressBox.TabIndex = 5;
            this.AddressBox.Enter += new System.EventHandler(this.AddressBox_Enter);
            this.AddressBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddressBox_KeyDown);
            // 
            // ForwardButton
            // 
            this.ForwardButton.Location = new System.Drawing.Point(31, 0);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(25, 32);
            this.ForwardButton.TabIndex = 4;
            this.ForwardButton.Text = "->";
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(0, 0);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(25, 32);
            this.BackButton.TabIndex = 3;
            this.BackButton.Text = "<-";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // LinksBar
            // 
            this.LinksBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinksBar.AutoSize = false;
            this.LinksBar.Dock = System.Windows.Forms.DockStyle.None;
            this.LinksBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.LinksBar.Location = new System.Drawing.Point(0, 34);
            this.LinksBar.Name = "LinksBar";
            this.LinksBar.Size = new System.Drawing.Size(924, 25);
            this.LinksBar.TabIndex = 4;
            this.LinksBar.Text = "toolStrip1";
            // 
            // BrowserDock
            // 
            this.BrowserDock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowserDock.Location = new System.Drawing.Point(195, 97);
            this.BrowserDock.Name = "BrowserDock";
            this.BrowserDock.Size = new System.Drawing.Size(924, 389);
            this.BrowserDock.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 58);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(920, 391);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // TabPage1
            // 
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.TabPage1.Size = new System.Drawing.Size(912, 365);
            this.TabPage1.TabIndex = 1;
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // OpacityBar
            // 
            this.OpacityBar.Location = new System.Drawing.Point(685, 8);
            this.OpacityBar.Maximum = 100;
            this.OpacityBar.Minimum = 10;
            this.OpacityBar.Name = "OpacityBar";
            this.OpacityBar.Size = new System.Drawing.Size(104, 45);
            this.OpacityBar.TabIndex = 23;
            this.OpacityBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.OpacityBar.Value = 100;
            this.OpacityBar.ValueChanged += new System.EventHandler(this.OpacityBar_ValueChanged);
            // 
            // BrowserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.BrowserDock);
            this.Controls.Add(this.LinksBar);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrowserWindow";
            this.Text = "Browser";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrowserWindow_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OpacityBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ForwardButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ToolStrip LinksBar;
        private System.Windows.Forms.Panel BrowserDock;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPage1;
        private System.Windows.Forms.Button NewTabButton;
        private System.Windows.Forms.Button FavoriteButton;
        private System.Windows.Forms.Button LockButton;
        private System.Windows.Forms.TrackBar OpacityBar;
    }
}