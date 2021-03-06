﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace The_UGamer_Launcher
{
    public partial class ScreenshotViewer : Form
    {
        string titleFriendly;

        string[] files;
        Panel[] picturePanel;
        PictureBox[] pictureBox;
        Label[] pictureLabel;
        int currentImage = 0;

        int xPosPanel = 3;
        int yPosPanel = 0;
        Point location;

        public ScreenshotViewer(string titleFriendly)
        {
            this.titleFriendly = titleFriendly;
            InitializeComponent();
            LockButton.BackgroundImage = Image.FromFile("Resources\\Theme\\Unlock.png");
            InitializeImages();
            InitializeDesign();
        }

        private void InitializeImages()
        {
            NoScreenshotsLabel.Visible = false;

            if (!Directory.Exists("Screenshots\\" + titleFriendly))
                NoScreenshotsLabel.Visible = true;
            else
            {
                files = Directory.GetFiles("Screenshots\\" + titleFriendly);
                FocusedPictureBox.BackgroundImage = Image.FromFile(files[0]);

                int gameIndex = files[0].IndexOf("Screenshots\\");

                string fileName = files[0];
                string gameName = files[0];

                gameName = gameName.Substring(gameIndex + 12);

                int gameLength = gameName.IndexOf("\\");
                gameName = gameName.Substring(0, gameLength);

                fileName = fileName.Substring(gameIndex + gameName.Length + 13);
                string type = ".jpg";
                int getRidOfExtension = fileName.IndexOf(".jpg");

                if (getRidOfExtension == -1)
                {
                    getRidOfExtension = fileName.IndexOf(".png");
                    type = ".png";
                }

                if (getRidOfExtension == -1)
                    getRidOfExtension = fileName.IndexOf(".jpeg");
                if (getRidOfExtension == -1)
                    getRidOfExtension = fileName.IndexOf(".gif");

                string extension = fileName;
                int length = fileName.Length;
                int extensionIndex = extension.IndexOf(type);
                extension = extension.Substring(0, type.Length);
                length -= extension.Length;
                fileName = fileName.Substring(0, length);

                string timeTaken = File.GetCreationTime(files[0]).ToString();

                FileNameLabel.Text = fileName;
                GameNameLabel.Text = gameName;
                TimeTakenLabel.Text = timeTaken;

                picturePanel = new Panel[files.Length];
                pictureBox = new PictureBox[files.Length];
                pictureLabel = new Label[files.Length];

                Size picturePanelSize = new Size(202, 163);
                Size pictureBoxSize = new Size(194, 114);
                Size pictureLabelSize = new Size(51,20);

                Point pictureBoxLocation = new Point(4, 5);
                Point pictureLabelLocation = new Point(4, 129);

                for (int index = 0; index < files.Length; index++)
                {
                    picturePanel[index] = new Panel();
                    pictureBox[index] = new PictureBox();
                    pictureLabel[index] = new Label();

                    ImagesPanel.Controls.Add(picturePanel[index]);
                    picturePanel[index].Controls.Add(pictureBox[index]);
                    picturePanel[index].Controls.Add(pictureLabel[index]);

                    location = new Point(xPosPanel, yPosPanel);
                    picturePanel[index].Location = location;
                    picturePanel[index].Size = picturePanelSize;
                    xPosPanel += 210;

                    pictureBox[index].Location = pictureBoxLocation;
                    pictureBox[index].Size = pictureBoxSize;
                    pictureBox[index].BackgroundImage = Image.FromFile(files[index]);
                    pictureBox[index].BackgroundImageLayout = ImageLayout.Zoom;
                    pictureBox[index].Tag = index;
                    pictureBox[index].Click += new EventHandler(pictureBox_Click);

                    pictureLabel[index].Location = pictureLabelLocation;
                    pictureLabel[index].Size = pictureLabelSize;
                    pictureLabel[index].Text = files[index];
                }
            }
        }

        private void InitializeDesign()
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox tempPic = (PictureBox)sender;
            string tagString = tempPic.Tag.ToString();
            int index = Convert.ToInt32(tagString);
            FocusedPictureBox.BackgroundImage = Image.FromFile(files[index]);
            currentImage = index;
            FocusedPictureBox.Tag = tagString;

            int gameIndex = files[index].IndexOf("Screenshots\\");

            string fileName = files[index];
            string gameName = files[index];

            gameName = gameName.Substring(gameIndex + 12);

            int gameLength = gameName.IndexOf("\\");
            gameName = gameName.Substring(0, gameLength);
            
            fileName = fileName.Substring(gameIndex + gameName.Length + 13);
            string type = ".jpg";
            int getRidOfExtension = fileName.IndexOf(".jpg");
            
            if (getRidOfExtension == -1)
            {
                getRidOfExtension = fileName.IndexOf(".png");
                    type = ".png";
            }
                
            if (getRidOfExtension == -1)
                getRidOfExtension = fileName.IndexOf(".jpeg");
            if (getRidOfExtension == -1)
                getRidOfExtension = fileName.IndexOf(".gif");

            string extension = fileName;
            int length = fileName.Length;
            int extensionIndex = extension.IndexOf(type);
            extension = extension.Substring(0, type.Length);
            length -= extension.Length;
            fileName = fileName.Substring(0, length);

            string timeTaken = File.GetCreationTime(files[index]).ToString();

            FileNameLabel.Text = fileName;
            GameNameLabel.Text = gameName;
            TimeTakenLabel.Text = timeTaken;
            
        }

        private void ExpandButton_Click(object sender, EventArgs e)
        {
            ImagesPanel.Visible = false;
            ChooseGameCombo.Visible = false;
            DetailsPanel.Visible = false;

            BackButton.Visible = true;

            Size newFormSize = new Size(816, 535);
            Size newSize = new Size(775, 436);
            Point newPoint = new Point(13, 14);

            this.Size = newFormSize;
            FocusedPictureBox.Location = newPoint;
            FocusedPictureBox.Size = newSize;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ImagesPanel.Visible = true;
            ChooseGameCombo.Visible = true;
            DetailsPanel.Visible = true;

            BackButton.Visible = false;

            Size newFormSize = new Size(816, 597);
            Size newSize = new Size(539, 302);
            Point newPoint = new Point(13, 41);

            this.Size = newFormSize;
            FocusedPictureBox.Location = newPoint;
            FocusedPictureBox.Size = newSize;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to delete this picture?";
            string caption = "Are you sure?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                int index;
                try
                {
                    FocusedPictureBox.BackgroundImage = Image.FromFile(files[currentImage + 1]);

                    index = currentImage + 1;

                    int gameIndex = files[index].IndexOf("Screenshots\\");

                    string fileName = files[index];
                    string gameName = files[index];

                    gameName = gameName.Substring(gameIndex + 12);

                    int gameLength = gameName.IndexOf("\\");
                    gameName = gameName.Substring(0, gameLength);

                    fileName = fileName.Substring(gameIndex + gameName.Length + 13);
                    string type = ".jpg";
                    int getRidOfExtension = fileName.IndexOf(".jpg");

                    if (getRidOfExtension == -1)
                    {
                        getRidOfExtension = fileName.IndexOf(".png");
                        type = ".png";
                    }

                    if (getRidOfExtension == -1)
                        getRidOfExtension = fileName.IndexOf(".jpeg");
                    if (getRidOfExtension == -1)
                        getRidOfExtension = fileName.IndexOf(".gif");

                    string extension = fileName;
                    int length = fileName.Length;
                    int extensionIndex = extension.IndexOf(type);
                    extension = extension.Substring(0, type.Length);
                    length -= extension.Length;
                    fileName = fileName.Substring(0, length);

                    string timeTaken = File.GetCreationTime(files[index]).ToString();

                    FileNameLabel.Text = fileName;
                    GameNameLabel.Text = gameName;
                    TimeTakenLabel.Text = timeTaken;

                    File.Delete(files[currentImage]);
                    Controls.Remove(picturePanel[currentImage]);
                    FocusedPictureBox.Tag = index;
                }
                catch
                {
                    try
                    {
                        FocusedPictureBox.BackgroundImage = Image.FromFile(files[currentImage - 1]);
                        index = currentImage - 1;

                        int gameIndex = files[index].IndexOf("Screenshots\\");

                        string fileName = files[index];
                        string gameName = files[index];

                        gameName = gameName.Substring(gameIndex + 12);

                        int gameLength = gameName.IndexOf("\\");
                        gameName = gameName.Substring(0, gameLength);

                        fileName = fileName.Substring(gameIndex + gameName.Length + 13);
                        string type = ".jpg";
                        int getRidOfExtension = fileName.IndexOf(".jpg");

                        if (getRidOfExtension == -1)
                        {
                            getRidOfExtension = fileName.IndexOf(".png");
                            type = ".png";
                        }

                        if (getRidOfExtension == -1)
                            getRidOfExtension = fileName.IndexOf(".jpeg");
                        if (getRidOfExtension == -1)
                            getRidOfExtension = fileName.IndexOf(".gif");

                        string extension = fileName;
                        int length = fileName.Length;
                        int extensionIndex = extension.IndexOf(type);
                        extension = extension.Substring(0, type.Length);
                        length -= extension.Length;
                        fileName = fileName.Substring(0, length);

                        string timeTaken = File.GetCreationTime(files[index]).ToString();

                        FileNameLabel.Text = fileName;
                        GameNameLabel.Text = gameName;
                        TimeTakenLabel.Text = timeTaken;

                        File.Delete(files[currentImage]);
                        Controls.Remove(picturePanel[currentImage]);
                        FocusedPictureBox.Tag = index;
                    }
                    catch
                    {
                        FocusedPictureBox.BackgroundImage = Image.FromFile("Screenshots\\Minecraft (Windows 10)\\Super Mario Party.jpg");
                        
                        FileNameLabel.Text = "";
                        GameNameLabel.Text = "";
                        TimeTakenLabel.Text = "";

                        Controls.Remove(picturePanel[currentImage]);
                        File.Delete(files[currentImage]);
                    }
                }
                
                
            }
            else
            {

            }
        }

        public bool locked = false;

        private void LockButton_Click(object sender, EventArgs e)
        {
            if (locked == false)
            {
                locked = true;
                LockButton.BackgroundImage = Image.FromFile("Resources\\Theme\\Lock.png");
            }
            else
            {
                locked = false;
                LockButton.BackgroundImage = Image.FromFile("Resources\\Theme\\Unlock.png");
            }
        }

        private void OpacityBar_ValueChanged(object sender, EventArgs e)
        {
            double opacityValue = Convert.ToDouble(OpacityBar.Value);
            opacityValue /= 100;
            this.Opacity = opacityValue;
        }
    }
}