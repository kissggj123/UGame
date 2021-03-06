﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace The_UGamer_Launcher
{
    public partial class AddGame : Form
    {
        public Form1 frm1;
        public bool refresh = false;
        DataTable newTable;
        Regex dateFix = new Regex("-");
        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Collection.accdb";
        private OleDbConnection con = new OleDbConnection(connectionString);
        private string[] linkTitles;
        private string[] linkURLs;
        private string links;
        private string globalLaunch = "";
        AddGameOptions launchDecision;
        AddLinks linkDecision;

        public AddGame(Form1 parent, bool refreshSignal)
        {
            InitializeComponent();
            obtainedDatePicker.Value = DateTime.Now;
            startDatePicker.Value = DateTime.Now;
            endDatePicker.Value = DateTime.Now;
            frm1 = parent;
            try
            {
                IconAssign();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (FileNotFoundException e) { }
#pragma warning restore CS0168 // Variable is declared but never used

            try
            {
                this.BackgroundImage = frm1.ThemeAssign("backgroundImageUSING");
            }
            catch (FileNotFoundException e) { }

            
            FillTable();
        }

        private void FillTable()
        {
            con.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Table1", con);
            
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            newTable = new DataTable();
            da.Fill(newTable);

            con.Close();

            DataTable dt = newTable;
            AutoCompleteStringCollection autoFill = new AutoCompleteStringCollection();
            int columnIndex = 1; // Name column
            string[] table = new string[dt.Rows.Count];
            int index = 0;
            for (index = 0; index < dt.Rows.Count; index++)
            {
                table[index] = dt.Rows[index][columnIndex].ToString();
                autoFill.Add(table[index]);
            }
            titleBox.AutoCompleteCustomSource = autoFill;
        }

        private void addEntryButton_Click(object sender, EventArgs e)
        {
            string title = titleBox.Text;
            string platform = platformBox.Text;
            string status = statusBox.Text;
            string rating = ratingBox.Text;

            string hours = hoursBox.Text;
            string minutes = minutesBox.Text;
            string seconds = secondsBox.Text;

            string obtained;
            string startDate;
            string endDate;

            if (IgnoreObtained.Checked == false)
                obtained = DateFix(obtainedDatePicker.Value.ToString("u"));
            else
                obtained = "";

            if (IgnoreStart.Checked == false)
                startDate = DateFix(startDatePicker.Value.ToString("u"));
            else
                startDate = "";

            if (IgnoreEnd.Checked == false)
                endDate = DateFix(endDatePicker.Value.ToString("u"));
            else
                endDate = "";

            string notes = notesBox.Text;
            string launchCode = launchBox.Text;

            string links = linksBox.Text;
            

            addEntry(title, platform, status, rating, hours, minutes, seconds, obtained, startDate, 
                endDate, launchCode, notes, links);

            refresh = true;
        }

        private void addEntry(string title, string platform, string status,
            string rating, string hours, string minutes, string seconds, string obtained, string startDate,
                string endDate, string launchCode, string notes, string links)
        {
            int hoursInt = 0;
            int minsInt = 0;
            int secsInt = 0;

            string newHoursString = "00";
            string newMinutesString = "00";
            string newSecondsString = "00";

            if (hours != "")
            {
                hoursInt = Convert.ToInt32(hours);
                newHoursString = hours;
            }
            if (minutes != "")
            {
                minsInt = Convert.ToInt32(minutes);
                newMinutesString = minutes;
            }
            if (seconds != "")
            {
                secsInt = Convert.ToInt32(seconds);
                newSecondsString = seconds;
            }

            if (hoursInt < 10 && hoursInt != 0)
                newHoursString = "0" + hours;
            if (minsInt < 10 && minsInt != 0)
                newMinutesString = "0" + minutes;
            if (secsInt < 10 && secsInt != 0)
                newSecondsString = "0" + seconds;
            
            string playTime = newHoursString + "h:" + newMinutesString + "m:" + newSecondsString + "s";
            
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Table1 (Title, Platform, Status, Rating, PlayTime, Obtained, StartDate, EndDate, Notes, Launch, News, Wiki) VALUES (@Title, @Platform, @Status, @Rating, @PlayTime, @Obtained, @StartDate, @EndDate, @Notes, @Launch, @News, @Wiki);", con);

            con.Open();

            title.Trim();
            platform.Trim();
            status.Trim();
            rating.Trim();
            playTime.Trim();
            obtained.Trim();
            startDate.Trim();
            endDate.Trim();
            notes.Trim();
            launchCode.Trim();
            links.Trim();

            cmd.Parameters.AddWithValue("@Title", title);
            if (platform == "")
                cmd.Parameters.AddWithValue("@Platform", "");
            else
                cmd.Parameters.AddWithValue("@Platform", platform);

            if (status == "")
                cmd.Parameters.AddWithValue("@Status", "");
            else
                cmd.Parameters.AddWithValue("@Status", status);

            if (rating == "")
                cmd.Parameters.AddWithValue("@Rating", "0");
            else
                cmd.Parameters.AddWithValue("@Rating", rating);

            if (playTime == "")
                cmd.Parameters.AddWithValue("@PlayTime", "00h:00m:00s");
            else
                cmd.Parameters.AddWithValue("@PlayTime", playTime);

            if (obtained == "")
                cmd.Parameters.AddWithValue("@Obtained", "");
            else
                cmd.Parameters.AddWithValue("@Obtained", obtained);

            if (startDate == "")
                cmd.Parameters.AddWithValue("@StartDate", "");
            else
                cmd.Parameters.AddWithValue("@StartDate", startDate);

            if (endDate == "")
                cmd.Parameters.AddWithValue("@EndDate", "");
            else
                cmd.Parameters.AddWithValue("@EndDate", endDate);

            if (notes == "")
                cmd.Parameters.AddWithValue("@Notes", "");
            else
                cmd.Parameters.AddWithValue("@Notes", notes);

            if (title == "Sonic World")
                cmd.Parameters.AddWithValue("@Launch", "BATs/Sonic World.bat");
            else if (launchCode == "")
                cmd.Parameters.AddWithValue("@Launch", "");
            else
                cmd.Parameters.AddWithValue("@Launch", launchCode);

            if (links == "")
                cmd.Parameters.AddWithValue("@News", "");
            else
                cmd.Parameters.AddWithValue("@News", links);

            string wikiCode = "";

            if (wikiCode == "")
                cmd.Parameters.AddWithValue("@Wiki", "");
            else
                cmd.Parameters.AddWithValue("@Wiki", wikiCode);

            if (title != "")
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    this.Text = "Add an entry... " + title + " added.";
                    con.Close();
                    FillTable();
                    if (launchCode.IndexOf("steam://rungameid/") != -1)
                    {
                        launchBox.Text = "steam://rungameid/";
                    }
                }
                catch (OleDbException e)
                {
                    string caption = "ERROR: Notes/Comments field too long.";
                    string message = "Your notes/comments field is too long. Please reduce to 255 characters.";
                    MessageBox.Show(message, caption);
                    con.Close();
                }
                
            }
            else
            {
                this.Text = "Add an entry... Game not added.";
            }
            return;
        }

        private void editEntry_Click(object sender, EventArgs e)
        {
            int z = 0, y = 0;
            

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Table1", con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable newTable = new DataTable();
            da.Fill(newTable);

            con.Close();

            // DataTable dt = frm1.collectionDataSet4.Table1;
            DataTable dt = newTable;
            int columnIndex = 1; // Name column
            string[] table = new string[dt.Rows.Count];
            int index = 0;
            for (index = 0; index < dt.Rows.Count; index++)
            {
                table[index] = dt.Rows[index][columnIndex].ToString();
            }

            string title = titleBox.Text;

            for (int x = 0; x < dt.Rows.Count; x++)
                if (title == table[x])
                {
                    z = x;
                    y = 1;
                }

            if (y == 1)
            {
                originalTitle.Text = dt.Rows[z][1].ToString();
                platformBox.Text = dt.Rows[z][2].ToString();
                statusBox.Text = dt.Rows[z][3].ToString();

                ratingBox.Text = dt.Rows[z][4].ToString();
                int rating = Convert.ToInt32(ratingBox.Text);
                if (rating == 0)
                    ratingBox.Text = "";

                hoursBox.Text = dt.Rows[z][5].ToString();

                string hoursPlayed = hoursBox.Text;
                string minutesPlayed = hoursBox.Text;
                string secondsPlayed = hoursBox.Text;

                int hourIndex = hoursPlayed.IndexOf("h");
                string hours = hoursPlayed.Substring(0, hourIndex);
                int minuteIndex = minutesPlayed.IndexOf("m");
                int minuteLength = minutesPlayed.IndexOf("m") - (hourIndex + 2);
                string minutes = minutesPlayed.Substring(hourIndex + 2, minuteLength);
                int secondIndex = secondsPlayed.IndexOf("s");
                int secondLength = secondsPlayed.IndexOf("s") - (minuteIndex + 2);
                string seconds = secondsPlayed.Substring(minuteIndex + 2, secondLength);

                int hoursInt = 0;
                int minsInt = 0;
                int secsInt = 0;

                string newHoursString = "00";
                string newMinutesString = "00";
                string newSecondsString = "00";

                if (hours != "")
                {
                    hoursInt = Convert.ToInt32(hours);
                    newHoursString = hours;
                }
                if (minutes != "")
                {
                    minsInt = Convert.ToInt32(minutes);
                    newMinutesString = minutes;
                }
                if (seconds != "")
                {
                    secsInt = Convert.ToInt32(seconds);
                    newSecondsString = seconds;
                }

                if (hoursInt < 10 && hoursInt > 0)
                    newHoursString = "0" + hours;
                if (minsInt < 10 && minsInt > 0)
                    newMinutesString = "0" + minutes;
                if (secsInt < 10 && secsInt > 0)
                    newSecondsString = "0" + seconds;

                string playTime = newHoursString + "h:" + newMinutesString + "m:" + newSecondsString + "s";

                if (hoursInt == 0)
                    hoursBox.Text = "";
                else if (hoursInt < 10)
                    hoursBox.Text = Convert.ToString(hoursInt);
                else
                    hoursBox.Text = newHoursString;

                if (minsInt == 0)
                    minutesBox.Text = "";
                else if (hoursInt < 10)
                    minutesBox.Text = Convert.ToString(minsInt);
                else
                    minutesBox.Text = newMinutesString;

                if (secsInt == 0)
                    secondsBox.Text = "";
                else if (secsInt < 10)
                    secondsBox.Text = Convert.ToString(secsInt);
                else
                    secondsBox.Text = newSecondsString;
                
                string obtainedEntry = dt.Rows[z][6].ToString();
                string startDateEntry = dt.Rows[z][7].ToString();
                string endDateEntry = dt.Rows[z][8].ToString();
                
                try
                {
                    obtainedDatePicker.Text = obtainedEntry;
                    if (obtainedEntry == "" || obtainedEntry == " ")
                        IgnoreObtained.Checked = true;
                    else
                        IgnoreObtained.Checked = false;

                }
                catch (FormatException h)
                {
                    IgnoreObtained.Checked = true;
                }

                try
                {
                    startDatePicker.Text = startDateEntry;
                    if (startDateEntry == "" || startDateEntry == " ")
                        IgnoreStart.Checked = true;
                    else
                        IgnoreStart.Checked = false;
                }
                catch (FormatException f)
                {
                    IgnoreStart.Checked = true;
                }

                try
                {
                    endDatePicker.Text = endDateEntry;
                    if (endDateEntry == "" || endDateEntry == " ")
                        IgnoreEnd.Checked = true;
                    else
                        IgnoreEnd.Checked = false;
                }
                catch (FormatException g)
                {
                    IgnoreEnd.Checked = true;
                }
                
                notesBox.Text = dt.Rows[z][9].ToString();
                launchBox.Text = dt.Rows[z][10].ToString();

                linksBox.Text = dt.Rows[z][11].ToString();

                noGameLabel.Visible = false;
                y = 0;

                this.Text = "Edit an entry... Editing \"" + title + "\"";

                replaceEntry.Visible = true;
                deleteEntryButton.Visible = true;
            }
            // If the entry does not exist, an error message shows.
            else
                noGameLabel.Visible = true;
        }

        private void replaceEntry_Click(object sender, EventArgs e)
        {
            string originalTitleString = originalTitle.Text;
            string title = titleBox.Text;
            string platform = platformBox.Text;
            string status = statusBox.Text;
            string rating = ratingBox.Text;

            string hours = hoursBox.Text;
            string minutes = minutesBox.Text;
            string seconds = secondsBox.Text;

            string obtained;
            string startDate;
            string endDate;

            if (IgnoreObtained.Checked == false)
                obtained = DateFix(obtainedDatePicker.Value.ToString("u"));
            else
                obtained = "";

            if (IgnoreStart.Checked == false)
                startDate = DateFix(startDatePicker.Value.ToString("u"));
            else
                startDate = "";

            if (IgnoreEnd.Checked == false)
                endDate = DateFix(endDatePicker.Value.ToString("u"));
            else
                endDate = "";

            string launchCode = launchBox.Text;
            string notes = notesBox.Text;

            string links = linksBox.Text;
            

            replaceEntryMethod(originalTitleString, title, platform, status, rating, 
                hours, minutes, seconds, obtained, startDate,
                endDate, launchCode, notes, links);

            FillTable();

            refresh = true;
        }

        private void replaceEntryMethod(string originalTitleString, string title, string platform, string status,
            string rating, string hours, string minutes,
            string seconds, string obtained, string startDate,
                string endDate, string launchCode, string notes, string links)
        {

            OleDbCommand delCmd = new OleDbCommand("DELETE FROM Table1 WHERE Title=\"" + originalTitleString + "\";", con);
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Table1 (Title, Platform, Status, Rating, PlayTime, Obtained, StartDate, EndDate, Notes, Launch, News, Wiki) VALUES (@Title, @Platform, @Status, @Rating, @PlayTime, @Obtained, @StartDate, @EndDate, @Notes, @Launch, @News, @Wiki);", con);

            string message = "Are you sure you want to edit entry \"" + originalTitleString + "\"?";
            string caption = "Editing entry \"" + originalTitleString + "\"";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);

            int hoursInt = 0;
            int minsInt = 0;
            int secsInt = 0;

            string newHoursString = "00";
            string newMinutesString = "00";
            string newSecondsString = "00";

            if (hours != "")
            {
                hoursInt = Convert.ToInt32(hours);
                newHoursString = hours;
            }
            if (minutes != "")
            {
                minsInt = Convert.ToInt32(minutes);
                newMinutesString = minutes;
            }
            if (seconds != "")
            {
                secsInt = Convert.ToInt32(seconds);
                newSecondsString = seconds;
            }

            if (hoursInt < 10 && hoursInt > 0)
                newHoursString = "0" + hours;
            if (minsInt < 10 && minsInt > 0)
                newMinutesString = "0" + minutes;
            if (secsInt < 10 && secsInt > 0)
                newSecondsString = "0" + seconds;

            string playTime = newHoursString + "h:" + newMinutesString + "m:" + newSecondsString + "s";

            title.Trim();
            platform.Trim();
            status.Trim();
            rating.Trim();
            playTime.Trim();
            obtained.Trim();
            startDate.Trim();
            endDate.Trim();
            notes.Trim();
            launchCode.Trim();
            links.Trim();

            if (result == DialogResult.Yes)
            {
                con.Open();

                delCmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@Title", title);
                if (platform == "")
                    cmd.Parameters.AddWithValue("@Platform", "");
                else
                    cmd.Parameters.AddWithValue("@Platform", platform);

                if (status == "")
                    cmd.Parameters.AddWithValue("@Status", "");
                else
                    cmd.Parameters.AddWithValue("@Status", status);

                if (rating == "")
                    cmd.Parameters.AddWithValue("@Rating", "0");
                else
                    cmd.Parameters.AddWithValue("@Rating", rating);

                if (playTime == "")
                    cmd.Parameters.AddWithValue("@PlayTime", "00h:00m:00s");
                else
                    cmd.Parameters.AddWithValue("@PlayTime", playTime);

                if (obtained == "")
                    cmd.Parameters.AddWithValue("@Obtained", "");
                else
                    cmd.Parameters.AddWithValue("@Obtained", obtained);

                if (startDate == "")
                    cmd.Parameters.AddWithValue("@StartDate", "");
                else
                    cmd.Parameters.AddWithValue("@StartDate", startDate);

                if (endDate == "")
                    cmd.Parameters.AddWithValue("@EndDate", "");
                else
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                if (notes == "")
                    cmd.Parameters.AddWithValue("@Notes", "");
                else
                    cmd.Parameters.AddWithValue("@Notes", notes);

                if (title == "Sonic World")
                    cmd.Parameters.AddWithValue("@Launch", "BATs/Sonic World.bat");
                else if (launchCode == "")
                    cmd.Parameters.AddWithValue("@Launch", "");
                else
                    cmd.Parameters.AddWithValue("@Launch", launchCode);

                if (links == "")
                    cmd.Parameters.AddWithValue("@News", "");
                else
                    cmd.Parameters.AddWithValue("@News", links);

                string wikiCode = "";

                if (wikiCode == "")
                    cmd.Parameters.AddWithValue("@Wiki", "");
                else
                    cmd.Parameters.AddWithValue("@Wiki", wikiCode);

                try
                {
                    cmd.ExecuteNonQuery();
                    this.Text = "Edit an entry... Game edited.";
                    con.Close();
                    FillTable();
                }
                catch (OleDbException e)
                {
                    caption = "ERROR: Notes/Comments field too long.";
                    message = "Your notes/comments field is too long. Please reduce to 255 characters.";
                    MessageBox.Show(message, caption);
                    con.Close();
                }
                replaceEntry.Visible = false;
                deleteEntryButton.Visible = false;

            }
            else
            {
                return;
            }
            return;
        }

        private void deleteEntryButton_Click(object sender, EventArgs e)
        {
            string originalTitleString = originalTitle.Text;
            deleteEntry(originalTitleString);
            FillTable();
            refresh = true;
        }

        private void deleteEntry(string originalTitleString)
        {
            OleDbCommand delCmd = new OleDbCommand("DELETE FROM Table1 WHERE Title=\"" + originalTitleString + "\";", con);

            this.Text = "Remove an entry... Removing \"" + originalTitleString + "\"";

            string message = "Are you sure you want to delete entry \"" + originalTitleString + "\"?";
            string title = "Deleting entry \"" + originalTitleString + "\"";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                con.Open();

                delCmd.ExecuteNonQuery();

                this.Text = "Entries... removed \"" + originalTitleString + "\"";
                replaceEntry.Visible = false;
                deleteEntryButton.Visible = false;

                con.Close();
                FillTable();

                

                return;
            }
            else
            {
                this.Text = "Edit an entry... Editing \"" + title + "\"";
                return;
            }
        }

        private void minutesBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                secondsBox.Focus();
        }

        private void secondsBox_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Tab)
                obtainedBox.Focus();
                */
        }

        private void hoursBox_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Tab)
                minutesBox.Focus();
        }

        public void IconAssign()
        {
            Icon windowIcon;
            try
            {
                windowIcon = new Icon("Resources/Theme/icon.ico");
                this.Icon = windowIcon;
            }
            catch (FileNotFoundException e)
            {

            }
        }

        private void clearFieldsButton_Click(object sender, EventArgs e)
        {
            Regex dateFix = new Regex("-");

            DateTime today2 = DateTime.Now;
            string todayDate = today2.ToString("u");
            string todayString = todayDate.Substring(0, 10);
            todayString = dateFix.Replace(todayString, "/");

            titleBox.Text = "";
            platformBox.Text = "";
            statusBox.Text = "";
            ratingBox.Text = "";
            hoursBox.Text = "";
            minutesBox.Text = "";
            secondsBox.Text = "";
            IgnoreObtained.Checked = false;
            IgnoreStart.Checked = false;
            IgnoreEnd.Checked = false;
            obtainedDatePicker.Value = today2;
            startDatePicker.Value = today2;
            endDatePicker.Value = today2;
            launchBox.Text = "";
            notesBox.Text = "";
            linksBox.Text = "";

            this.Text = "Entries";
        }

        public void DisplayInfo(string title, string platform, string status, string rating,
                    string hours, string obtained, string startDate, string endDate, string notes, 
                    string launchString, string newsString, string wikiString)
        {
            originalTitle.Text = title;
            titleBox.Text = title;
            platformBox.Text = platform;
            statusBox.Text = status;

            if (rating == "0")
                ratingBox.Text = "";
            else
                ratingBox.Text = rating;

            hoursBox.Text = hours;
            string hoursPlayed = hoursBox.Text;
            string minutesPlayed = hoursBox.Text;
            string secondsPlayed = hoursBox.Text;

            int hourIndex = hoursPlayed.IndexOf("h");
            hours = hoursPlayed.Substring(0, hourIndex);
            int minuteIndex = minutesPlayed.IndexOf("m");
            int minuteLength = minutesPlayed.IndexOf("m") - (hourIndex + 2);
            string minutes = minutesPlayed.Substring(hourIndex + 2, minuteLength);
            int secondIndex = secondsPlayed.IndexOf("s");
            int secondLength = secondsPlayed.IndexOf("s") - (minuteIndex + 2);
            string seconds = secondsPlayed.Substring(minuteIndex + 2, secondLength);

            int hoursInt = 0;
            int minsInt = 0;
            int secsInt = 0;

            string newHoursString = "00";
            string newMinutesString = "00";
            string newSecondsString = "00";

            if (hours != "")
            {
                hoursInt = Convert.ToInt32(hours);
                newHoursString = hours;
            }
            if (minutes != "")
            {
                minsInt = Convert.ToInt32(minutes);
                newMinutesString = minutes;
            }
            if (seconds != "")
            {
                secsInt = Convert.ToInt32(seconds);
                newSecondsString = seconds;
            }

            if (hoursInt < 10 && hoursInt > 0)
                newHoursString = "0" + hours;
            if (minsInt < 10 && minsInt > 0)
                newMinutesString = "0" + minutes;
            if (secsInt < 10 && secsInt > 0)
                newSecondsString = "0" + seconds;

            string playTime = newHoursString + "h:" + newMinutesString + "m:" + newSecondsString + "s";

            if (hoursInt == 0)
                hoursBox.Text = "";
            else if (hoursInt < 10)
                hoursBox.Text = Convert.ToString(hoursInt);
            else
                hoursBox.Text = newHoursString;

            if (minsInt == 0)
                minutesBox.Text = "";
            else if (minsInt < 10)
                minutesBox.Text = Convert.ToString(minsInt);
            else
                minutesBox.Text = newMinutesString;

            if (secsInt == 0)
                secondsBox.Text = "";
            else if (secsInt < 10)
                secondsBox.Text = Convert.ToString(secsInt);
            else
                secondsBox.Text = newSecondsString;

            try
            {
                obtainedDatePicker.Text = obtained;
            }
            catch
            {
                obtainedDatePicker.Text = "";
            }

            try
            {
                startDatePicker.Text = startDate;
            }
            catch
            {
                startDatePicker.Text = "";
            }

            try
            {
                endDatePicker.Text = endDate;
            }
            catch
            {
                endDatePicker.Text = "";
            }



            notesBox.Text = notes;
            launchBox.Text = launchString;

            linksBox.Text = newsString;

            replaceEntry.Visible = true;
            deleteEntryButton.Visible = true;
        }

        public string DateFix(string oldDate)
        {
            string newDate = oldDate.Substring(0, 10);
            newDate = dateFix.Replace(newDate, "/");
            return newDate;
        }

        private void AddGameButton_Click(object sender, EventArgs e)
        {
            launchDecision = new AddGameOptions();
            launchDecision.Show();
            launchDecision.FormClosed += new FormClosedEventHandler(launchDecision_FormClosed);
        }

        private void AddLinksButton_Click(object sender, EventArgs e)
        {
            linkDecision = new AddLinks();
            linkDecision.Show();
            linkDecision.FormClosed += new FormClosedEventHandler(linkDecision_FormClosed);
        }

        private void launchDecision_FormClosed(object sender, FormClosedEventArgs e)
        {
            globalLaunch = launchDecision.finalReturn;
            launchBox.Text = globalLaunch;
        }

        private void linkDecision_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.linkTitles = linkDecision.linkTitles;
            this.linkURLs = linkDecision.linkURLs;
            string links = "";

            for (int index = 0; index < linkTitles.Length; index++)
            {
                links += "[Title]" + linkTitles[index] + "[URL]" + linkURLs[index];
            }

            linksBox.Text = links;
        }
    }
}