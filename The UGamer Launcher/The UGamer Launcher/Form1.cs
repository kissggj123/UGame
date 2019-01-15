﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace The_UGamer_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void collectionButto(object sender, EventArgs e)
        {
            Uri url = new Uri("https://ugamer.github.io/Library/collection.html");
            webBrowser1.Url = url;
            launcherButton.Visible = true;
            collectionButton.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void launcherButto(object sender, EventArgs e)
        {
            Uri url = new Uri("E:/Projects/UGame Launcher/launcher/launcher.html");
            webBrowser1.Url = url;
            collectionButton.Visible = true;
            launcherButton.Visible = false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Uri url = new Uri("E:/Projects/UGame Launcher/launcher/launcher.html");
            Uri sonicWorld = new Uri("E:/Projects/UGame Launcher/launcher/sonicWorld.html");

            if (webBrowser1.Url == sonicWorld)
            {
                Process.Start("E:/Games/Sonic World/launch.bat");
                webBrowser1.Url = url;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'collectionDataSet.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.collectionDataSet.Table1);

        }

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.table1TableAdapter.Fill(this.collectionDataSet.Table1);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
