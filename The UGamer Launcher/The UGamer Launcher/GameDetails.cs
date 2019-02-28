﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using CefSharp;
using CefSharp.WinForms;
using System.Threading;
using System.ComponentModel;

namespace The_UGamer_Launcher
{
    public partial class GameDetails : Form
    {
        private Stopwatch gameTime = new Stopwatch();
        private ChromiumWebBrowser chromeBrowser;
        Thread timePlaying;
        public bool refresh = false;
        private bool gameRunning = false;
        private bool didPlay = false;
        private bool isPaused = false;
        private bool hasImage = true;
        private string title;
        string newsUrl;
        string wikiUrl;
        private Size browserSize = new Size(659, 88);
        Overlay ingame = new Overlay();
        private bool justTrack = false;
        string[,] links;
        int linkCount = 0;

        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Collection.accdb";
        private OleDbConnection con = new OleDbConnection(connectionString);

        // Starts up a detail form.
        public GameDetails()
        {
            InitializeComponent();
            if (noImageText.Visible == true)
            {
                gamePicture.Visible = false;
            }

            try
            {
                this.BackgroundImage = ThemeAssign("backgroundImageUSING");
            }
            catch (FileNotFoundException e)
            {
                hasImage = false;
            }
        }

        // Displays all the info for the game.
        public void DisplayInfo(string title, string input2, string platform,
            string status, string rating, string hours, string obtained,
            string startDate, string endDate, string notes, string launchString2, bool exePath2,
            string newsString2, string wikiString2, bool hasArgs2)
        {
            this.title = title;
            noImageText.Visible = false;

            // This block of text determines the icon.
            try
            {
                IconAssign(input2);
            }
            catch (FileNotFoundException e) { }

            Image detailedPic = detailedImageAssign(input2);
            Image backgroundPic = backgroundImageAssign(input2, detailedPic);

            if (detailedPic.Width != 1)
            {
                gamePicture.BackgroundImage = detailedPic;
            }
            else
            {
                try
                {
                    if (backgroundPic.Width != 1)
                    {
                        gamePicture.BackgroundImage = backgroundPic;
                        noImageText.Visible = false;
                    }
                }
                catch (FileNotFoundException e) { }
            }

            if (backgroundPic.Width != 1)
            {
                try
                {
                    Bitmap bg = new Bitmap(backgroundPic);
                    var radius = 20;
                    StackBlur.StackBlur.Process(bg, radius);
                    this.BackgroundImage = bg;
                }
                catch (FileNotFoundException f)
                {
                    try
                    {
                        Bitmap bg = new Bitmap(detailedPic);
                        var radius = 20;
                        StackBlur.StackBlur.Process(bg, radius);
                        this.BackgroundImage = bg;
                    }
                    catch (FileNotFoundException g) { }
                }
            }

            nameLabel.UseMnemonic = false;
            nameLabel.Text = title; // Displays the name of the game.
            platformLabel.Text = "Platform: " + platform;

            if (status == "")
                statusLabel.Text = "Status:  ";
            else
                statusLabel.Text = "Status: " + status;

            if (rating == "")
                ratingLabel.Text = "Rating:  ";
            else
                ratingLabel.Text = "Rating: " + rating;

            if (hours == "")
                hoursLabel.Text = "Time Played:  ";
            else
                hoursLabel.Text = "Time Played: " + hours;

            if (obtained == "")
                obtainedLabel.Text = "Obtained:  ";
            else
                obtainedLabel.Text = "Obtained: " + obtained;

            if (startDate == "")
                startDateLabel.Text = "Start Date:  ";
            else
                startDateLabel.Text = "Start Date: " + startDate;

            if (endDate == "")
                endDateLabel.Text = "Last Played:  ";
            else
                endDateLabel.Text = "Last Played: " + endDate;

            if (notes == "")
                notesBox.Text = " ";
            else
                notesBox.Text = notes;

            if (launchString2 != "")
                launchLabel.Text = launchString2;

            if (launchString2 == "" || launchString2 == " ")
            {
                button1.Text = "Track Time";
                TrackTimeButton.Visible = false;
            }

            setURLs(newsString2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrackTime(true);
        }

        private void TrackTime(bool executeLaunch)
        {
            string launchString3 = launchLabel.Text;

            bool exePath3 = isExe(launchString3);
            bool hasArgs3 = hasArgsMethod(launchString3);

            Uri launchUrl;
            if (executeLaunch == true)
            {
                if (exePath3 == true)
                {
                    if (hasArgs3 == true)
                    {
                        int getRidOfQuotes = launchString3.IndexOf("\"");
                        if (getRidOfQuotes == 0)
                        {
                            launchString3.Substring(1);
                            int secondQuote = launchString3.IndexOf("\"");
                            launchString3.Substring(0, secondQuote);
                        }
                        int exeLoc = launchString3.IndexOf(".exe");
                        string fileName = launchString3.Substring(0, exeLoc + 5);
                        string args = launchString3.Substring(exeLoc + 5);
                        ProcessStartInfo procStartInfo = new ProcessStartInfo(fileName, args);

                        // ingame.Show();

                        gameTime.Start();
                        if (procStartInfo.FileName != "" && procStartInfo.FileName != " ")
                        {
                            Process.Start(procStartInfo);
                            // ingame.Show();
                        }
                    }
                    else
                    {
                        ProcessStartInfo procStartInfo = new ProcessStartInfo(launchString3);
                        gameTime.Start();
                        if (procStartInfo.FileName != "" && procStartInfo.FileName != " ")
                        {
                            Process.Start(procStartInfo);
                            // ingame.Show();
                        }
                    }
                }
                else if (launchString3 == "")
                {
                    gameTime.Start();
                }
                else
                {
                    gameTime.Start();
                    launchUrl = new Uri(launchString3);
                    launcher.Url = launchUrl;
                    // ingame.Show();
                }
            }
            else
            {
                gameTime.Start();
            }
            /*
            timePlaying = new Thread(new ThreadStart(DisplaySeconds));
            timePlaying.Start();
            */

            button1.Visible = false;
            TrackTimeButton.Visible = false;
            stopTime.Visible = true;
            PauseTimeButton.Visible = true;
            discardButton.Visible = true;
            didPlay = true;
            gameRunning = true;
            refresh = true;
        }

        private bool isExe(string p)
        {
            if (p.IndexOf(".exe") != -1)
                return true;
            else if (p.IndexOf(".lnk") != -1)
                return true;
            else if (p.IndexOf(".bat") != -1)
                return true;
            else
                return false;
        }

        private bool hasArgsMethod(string p)
        {
            try
            {
                int exeLoc = p.IndexOf(".exe");
                string lookForArgs = p.Substring(exeLoc);

                if (lookForArgs.IndexOf("-") == -1)
                    return false;
                else
                    return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                return false;
            }
        }

        private void DisplaySeconds()
        {
            string secondsString;
            string minutesString;
            string hoursString;
            int seconds;
            int minutes;
            int hours;
            string timePlayingString = "";
            for (; ; )
            {
                seconds = Convert.ToInt32(gameTime.ElapsedMilliseconds / 1000);
                minutes = seconds / 60;
                seconds %= 60;
                hours = minutes / 60;
                minutes %= 60;

                secondsString = Convert.ToString(seconds);
                minutesString = Convert.ToString(minutes);
                hoursString = Convert.ToString(hours);

                if (hours < 10)
                    hoursString = "0" + hours;
                if (minutes < 10)
                    minutesString = "0" + minutes;
                if (seconds < 10)
                    secondsString = "0" + seconds;

                timePlayingString = "Current Play Session: " + hoursString + "h:" + minutesString + 
                    "m:" + secondsString + "s";
                SetText(timePlayingString);
            }
        }

        delegate void StringArgReturningVoidDelegate(string text);

        private void SetText(string text)
        {
            if (this.TimePlayingLabel.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.TimePlayingLabel.Text = text;
            }
        }

        private Image detailedImageAssign(string input2)
        {
            Image background;
            try
            {
                background = Image.FromFile("Resources/Details/" + input2 + ".png");
                return background;
            }
            catch (FileNotFoundException e)
            {
                try
                {
                    background = Image.FromFile("Resources/Details/" + input2 + ".jpg");
                    return background;
                }
                catch (FileNotFoundException f)
                {
                    try
                    {
                        background = Image.FromFile("Resources/Details/" + input2 + ".jpeg");
                        return background;
                    }
                    catch (FileNotFoundException g)
                    {
                        try
                        {
                            background = Image.FromFile("Resources/Details/" + input2 + ".gif");
                            return background;
                        }
                        catch (FileNotFoundException h)
                        {
                            noImageText.Text = "Image \"" + input2 + "\" not found.";
                            noImageText.Visible = true;
                            return background = Image.FromFile("Resources/DONT TOUCH.png");
                        }
                    }
                }
            }

        }

        private Bitmap backgroundImageAssign(string input2, Image backup)
        {
            Image background;
            Bitmap bg;
            try
            {
                background = Image.FromFile("Resources/BG/" + input2 + ".png");
                bg = new Bitmap(background);
                return bg;
            }
            catch (FileNotFoundException e)
            {
                try
                {
                    background = Image.FromFile("Resources/BG/" + input2 + ".jpg");
                    bg = new Bitmap(background);
                    return bg;
                }
                catch (FileNotFoundException f)
                {
                    try
                    {
                        background = Image.FromFile("Resources/BG/" + input2 + ".jpeg");
                        bg = new Bitmap(background);
                        return bg;
                    }
                    catch (FileNotFoundException g)
                    {
                        try
                        {
                            background = Image.FromFile("Resources/BG/" + input2 + ".gif");
                            bg = new Bitmap(background);
                            return bg;
                        }
                        catch (FileNotFoundException h)
                        {
                            bg = new Bitmap(backup);
                            return bg;
                        }
                    }
                }
            }
        }

        private void stopTime_Click(object sender, EventArgs e)
        {
            stopTimeMethod();
        }

        private void stopTimeMethod()
        {
            // timePlaying.Abort();
            // ingame.Close();

            gameTime.Stop();
            gameRunning = false;
            int seconds = Convert.ToInt32(gameTime.ElapsedMilliseconds / 1000);

            int playSeconds = seconds % 60;
            int playMinutes = seconds / 60;
            int playHours = playMinutes / 60;
            playMinutes %= 60;

            gameTime.Restart();

            string message = "";
            if (playHours != 0 && playMinutes != 0 && playSeconds != 0)
            {
                message = "You played " + nameLabel.Text + " for " + playHours + " hours, "
                    + playMinutes + " minutes, and " + playSeconds + " seconds!";
            }
            else if (playHours != 0 && playMinutes != 0 && playSeconds == 0)
            {
                message = "You played " + nameLabel.Text + " for " + playHours + " hours and "
                    + playMinutes + " minutes!";
            }
            else if (playHours != 0 && playMinutes == 0 && playSeconds != 0)
            {
                message = "You played " + nameLabel.Text + " for " + playHours + " hours and "
                    + playSeconds + " seconds!";
            }
            else if (playHours != 0 && playMinutes == 0 && playSeconds == 0)
            {
                message = "You played " + nameLabel.Text + " for " + playHours + " hours!";
            }
            else if (playHours == 0 && playMinutes != 0 && playSeconds != 0)
            {
                message = "You played " + nameLabel.Text + " for " + playMinutes + " minutes and "
                    + playSeconds + " seconds!";
            }
            else if (playHours == 0 && playMinutes != 0 && playSeconds == 0)
            {
                message = "You played " + nameLabel.Text + " for " + playMinutes + " minutes!";
            }
            else
            {
                message = "You played " + nameLabel.Text + " for " + playSeconds + " seconds!";
            }
            string caption = "Play Time";
            
            OleDbCommand delCmd = new OleDbCommand("DELETE FROM Table1 WHERE Title=\"" + nameLabel.Text + "\";", con);
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Table1 (Title, Platform, Status, Rating, PlayTime, Obtained, StartDate, EndDate, Notes, Launch) VALUES (@Title, @Platform, @Status, @Rating, @PlayTime, @Obtained, @StartDate, @EndDate, @Notes, @Launch);", con);

            con.Open();

            delCmd.ExecuteNonQuery();

            string platform = platformLabel.Text.Substring(10);
            string status = statusLabel.Text.Substring(8);
            string rating = ratingLabel.Text.Substring(8);

            string hoursPlayed = hoursLabel.Text.Substring(13);
            string minutesPlayed = hoursLabel.Text.Substring(13);
            string secondsPlayed = hoursLabel.Text.Substring(13);

            int hourIndex = hoursPlayed.IndexOf("h");
            string hPlayed = hoursPlayed.Substring(0, hourIndex);
            int minuteIndex = minutesPlayed.IndexOf("m");
            int minuteLength = minutesPlayed.IndexOf("m") - (hourIndex + 2);
            string mPlayed = minutesPlayed.Substring(hourIndex + 2, minuteLength);
            int secondIndex = secondsPlayed.IndexOf("s");
            int secondLength = secondsPlayed.IndexOf("s") - (minuteIndex + 2);
            string sPlayed = secondsPlayed.Substring(minuteIndex + 2, secondLength);

            int oldHoursPlayed = Convert.ToInt32(hPlayed);
            int oldMinutesPlayed = Convert.ToInt32(mPlayed);
            int oldSecondsPlayed = Convert.ToInt32(sPlayed);

            int newHours = oldHoursPlayed + playHours;
            int newMinutes = oldMinutesPlayed + playMinutes;
            int newSeconds = oldSecondsPlayed + playSeconds;

            newHours += newMinutes / 60;
            newMinutes %= 60;
            newMinutes += newSeconds / 60;
            newSeconds %= 60;

            string newHoursString = newHours.ToString();
            string newMinutesString = newMinutes.ToString();
            string newSecondsString = newSeconds.ToString();

            if (newHours < 10)
                newHoursString = "0" + newHours;
            if (newMinutes < 10)
                newMinutesString = "0" + newMinutes;
            if (newSeconds < 10)
                newSecondsString = "0" + newSeconds;

            string timePlayed = newHoursString + "h:" + newMinutesString + "m:" + newSecondsString + "s";

            string obtained = obtainedLabel.Text.Substring(10);

            DateTime today = DateTime.Now;
            Regex dateFix = new Regex("-");

            string startDate = startDateLabel.Text.Substring(12);
            string startDate2;
            if (startDate == " " || startDate == "")
            {
                string startDate1 = today.ToString("u");
                startDate2 = startDate1.Substring(0, 10);
                startDate2 = dateFix.Replace(startDate2, "/");
            }
            else
            {
                startDate2 = startDate;
            }

            DateTime today2 = DateTime.Now;
            string endDate1 = today2.ToString("u");
            string endDate2 = endDate1.Substring(0, 10);
            endDate2 = dateFix.Replace(endDate2, "/");

            string launchCode = launchLabel.Text;

            cmd.Parameters.AddWithValue("@Title", nameLabel.Text);
            cmd.Parameters.AddWithValue("@Platform", platform);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Rating", rating);
            cmd.Parameters.AddWithValue("@PlayTime", timePlayed);
            cmd.Parameters.AddWithValue("@Obtained", obtained);
            cmd.Parameters.AddWithValue("@StartDate", startDate2);
            cmd.Parameters.AddWithValue("@EndDate", endDate2);
            cmd.Parameters.AddWithValue("@Notes", notesBox.Text);
            cmd.Parameters.AddWithValue("@Launch", launchCode);
            cmd.Parameters.AddWithValue("@News", newsUrl);
            cmd.Parameters.AddWithValue("@Wiki", wikiUrl);
            cmd.ExecuteNonQuery();

            MessageBox.Show(message, caption);
            hoursLabel.Text = "Time Played: " + timePlayed;

            button1.Visible = true;
            if (justTrack == true)
                TrackTimeButton.Visible = true;
            stopTime.Visible = false;
            isPaused = false;
            PauseTimeButton.Text = "Pause Playing";
            PauseTimeButton.Visible = false;
            discardButton.Visible = false;

            con.Close();
        }

        FormWindowState LastWindowState = FormWindowState.Normal;
        private void GameDetails_Resize(object sender, EventArgs e)
        {
            // When window state changes
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;

                if (WindowState == FormWindowState.Maximized)
                {
                    if (LastWindowState == FormWindowState.Minimized)
                        timePlaying.Start();

                    browserDock.Visible = true;
                    BrowserLinksDGV.Visible = true;
                    // Maximized!
                }
                if (WindowState == FormWindowState.Normal)
                {
                    if (LastWindowState == FormWindowState.Minimized)
                        timePlaying.Start();

                    browserDock.Visible = false;
                    BrowserLinksDGV.Visible = false;
                    // Restored!
                }
                if (WindowState == FormWindowState.Minimized)
                {
                    timePlaying.Abort();
                    browserDock.Visible = false;
                    BrowserLinksDGV.Visible = false;
                    // Restored!
                }
            }
        }

        private void setURLs(string news)
        {
            string allLinks = news;
            int URLIndex1;

            for (int index = 0; index != 1;)
            {
                URLIndex1 = allLinks.IndexOf("[URL]");
                if (URLIndex1 != -1)
                {
                    linkCount++;
                    allLinks = allLinks.Substring(URLIndex1 + 5);
                }
                else
                {
                    index = 1;
                }
            }

            string allLinks2 = news;
            links = new string[2,linkCount];

            for (int index = 0; index < linkCount; index++)
            {
                try
                {
                    int titleIndex = allLinks2.IndexOf("[Title]");
                    int URLIndex = allLinks2.IndexOf("[URL]");
                    string tempLink = allLinks2.Substring(titleIndex + 7);
                    int titleLength = URLIndex - (titleIndex + 7);
                    links[0, index] = allLinks2.Substring(titleIndex + 7, titleLength);
                    int nextTitleIndex = tempLink.IndexOf("[Title]");
                    if (nextTitleIndex != -1)
                    {
                        int urlLength = nextTitleIndex - URLIndex + 2;
                        links[1, index] = allLinks2.Substring(URLIndex + 5, urlLength);
                    }
                    else
                    {
                        links[1, index] = allLinks2.Substring(URLIndex + 5);
                    }
                        
                    allLinks2 = allLinks2.Substring(nextTitleIndex);
                }
                catch (ArgumentOutOfRangeException e)
                {

                }
            }

            OleDbCommand clearTable = new OleDbCommand("DELETE FROM TempTable", con);
            con.Open();
            clearTable.ExecuteNonQuery();
            con.Close();

            OleDbCommand insertTemp = new OleDbCommand("INSERT INTO TempTable (Title) VALUES (@Title);", con);

            for (int index = 0; index < linkCount; index++)
            {
                insertTemp.Parameters.AddWithValue("@Title", links[0, index]);
                con.Open();
                insertTemp.ExecuteNonQuery();
                con.Close();
                insertTemp.Parameters.RemoveAt("@Title");
            }

            BrowserLinksDGV.DataSource = null;
            BrowserLinksDGV.Update();
            BrowserLinksDGV.Refresh();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM TempTable", con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable newTable = new DataTable();
            da.Fill(newTable);
            BrowserLinksDGV.DataSource = newTable;
            con.Close();

            if (news == " " || news == "")
            {
                this.Controls.Remove(BrowserLinksDGV);
                this.Controls.Remove(browserDock);
            }
            else if (news != " " || news != "")
            {
                // Create a browser component
                chromeBrowser = new ChromiumWebBrowser(links[1, 0]);
                // Add it to the form and fill it to the form window.
                chromeBrowser.Size = browserSize;
                chromeBrowser.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                this.browserDock.Controls.Add(chromeBrowser);
            }
        }

        public Image ThemeAssign(string input2)
        {
            Image background;
            try
            {
                background = Image.FromFile("Resources/Theme/" + input2 + ".png");
                return background;
            }
            catch (FileNotFoundException e)
            {
                try
                {
                    background = Image.FromFile("Resources/Theme/" + input2 + ".jpg");
                    return background;
                }
                catch (FileNotFoundException f)
                {
                    try
                    {
                        background = Image.FromFile("Resources/Theme/" + input2 + ".jpeg");
                        return background;
                    }
                    catch (FileNotFoundException g)
                    {
                        try
                        {
                            background = Image.FromFile("Resources/Theme/" + input2 + ".gif");
                            return background;
                        }
                        catch (FileNotFoundException h)
                        {
                            return background = Image.FromFile("Resources/DONT TOUCH.png");
                        }
                    }
                }
            }
        }

        public void IconAssign(string input2)
        {
            Icon windowIcon;
            Image image;
            Bitmap bitmap;
            try
            {
                windowIcon = new Icon("Resources/Icons/" + input2 + ".ico");
                this.Icon = windowIcon;
            }
            catch (FileNotFoundException e)
            {
                try
                {
                    image = Image.FromFile("Resources/Icons/" + input2 + ".png");
                    bitmap = new Bitmap(image);
                    IntPtr Hicon = bitmap.GetHicon();
                    windowIcon = Icon.FromHandle(Hicon);
                    this.Icon = windowIcon;
                }
                catch (FileNotFoundException f)
                {
                    try
                    {
                        image = Image.FromFile("Resources/Icons/" + input2 + ".jpg");
                        bitmap = new Bitmap(image);
                        IntPtr Hicon = bitmap.GetHicon();
                        windowIcon = Icon.FromHandle(Hicon);
                        this.Icon = windowIcon;
                    }
                    catch (FileNotFoundException g)
                    {
                        try
                        {
                            image = Image.FromFile("Resources/Icons/" + input2 + ".jpeg");
                            bitmap = new Bitmap(image);
                            IntPtr Hicon = bitmap.GetHicon();
                            windowIcon = Icon.FromHandle(Hicon);
                            this.Icon = windowIcon;
                        }
                        catch (FileNotFoundException h)
                        {
                            try
                            {
                                image = Image.FromFile("Resources/Icons/" + input2 + ".gif");
                                bitmap = new Bitmap(image);
                                IntPtr Hicon = bitmap.GetHicon();
                                windowIcon = Icon.FromHandle(Hicon);
                                this.Icon = windowIcon;
                            }
                            catch (FileNotFoundException k)
                            {
                                try
                                {
                                    windowIcon = new Icon("Resources/Theme/icon.ico");
                                    this.Icon = windowIcon;
                                }
                                catch (FileNotFoundException i)
                                {
                                    try
                                    {
                                        windowIcon = new Icon("Resources/Theme/icon.ico");
                                        this.Icon = windowIcon;
                                    }
                                    catch (FileNotFoundException j)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GameDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (didPlay == true)
            {
                if (gameRunning == true)
                {
                    gameTime.Stop();
                    string message = "You're still keeping track of \" " + title + " \". Are you sure you want to stop the time and quit?";
                    string caption = "Are you sure?";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, caption, buttons);
                    if (result == DialogResult.Yes)
                    {
                        stopTimeMethod();
                        gameTime.Restart();
                        didPlay = false;
                        this.Close();
                    }
                    else
                    {
                        gameTime.Start();
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    gameTime.Restart();
                    didPlay = false;
                    this.Close();
                }
            }
            else
            {

            }
        }

        private void PauseTimeButton_Click(object sender, EventArgs e)
        {
            if (isPaused == false)
            {
                gameTime.Stop();
                isPaused = true;
                PauseTimeButton.Text = "Resume Playing";
            }
            else
            {
                gameTime.Start();
                isPaused = false;
                PauseTimeButton.Text = "Pause Playing";
            }
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            gameTime.Stop();
            string message = "Are you sure you want to discard your \"" + title + "\" session?";
            string caption = "Discard Session?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                gameTime.Restart();
                didPlay = false;
                stopTime.Visible = false;
                PauseTimeButton.Visible = false;
                button1.Visible = true;
                isPaused = false;
                PauseTimeButton.Text = "Pause Playing";
                discardButton.Visible = false;
            }
            else
            {
                gameTime.Start();
                return;
            }
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            BrowserButton.Visible = false;
            BackButton.Visible = true;

            TrackTimeButton.Visible = false;
            gamePicture.Visible = false;
            noImageText.Visible = false;
            nameLabel.Visible = false;
            panel1.Visible = false;
            notesLabel.Visible = false;
            notesBox.Visible = false;

            // Size fullBrowserSize = new Size(835, 304);
            Size fullBrowserSize = new Size(1280, 720);
            Point fullBrowserLocation = new Point(8, 40);

            browserDock.Size = fullBrowserSize;
            browserDock.Location = fullBrowserLocation;

            browserDock.Visible = true;
            BrowserLinksDGV.Visible = true;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            BrowserButton.Visible = true;
            BackButton.Visible = false;

            TrackTimeButton.Visible = true;
            gamePicture.Visible = true;

            if (hasImage == false)
                noImageText.Visible = true;

            nameLabel.Visible = true;
            panel1.Visible = true;
            notesLabel.Visible = true;
            notesBox.Visible = true;

            browserDock.Visible = false;
            BrowserLinksDGV.Visible = false;

            browserDock.Size = browserSize;
        }

        private void TrackTimeButton_Click(object sender, EventArgs e)
        {
            justTrack = true;
            TrackTime(false);
        }

        private void GameDetails_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tempTableDataSet.TempTable' table. You can move, or remove it, as needed.
            this.tempTableTableAdapter.Fill(this.tempTableDataSet.TempTable);

        }

        private void BrowserLinksDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string titleValue;
            string urlToPass = "";
            try
            {
                object value = BrowserLinksDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                titleValue = value.ToString();
                

                for (int index = 0; index < linkCount; index++)
                {
                    if (links[0, index] == titleValue)
                        urlToPass = links[1, index];
                }
                ChangeURL(urlToPass);
            }
            catch (ArgumentOutOfRangeException f) { }
        }

        private void ChangeURL(string url)
        {
            chromeBrowser.Load(url);
        }
    }
}