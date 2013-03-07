using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MainForm
{
    public partial class MainForm : Form
    {
        public static decimal sensorValue = 20;
        private NotifyIcon m_notifyicon = new NotifyIcon();
        private ContextMenu m_menu = new ContextMenu();        
        int dataPointsInChart = 15;
        public MainForm()
        {
            InitializeComponent();
            makeTrayIcon();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Error.exclamationGet();
                populateSettingsDefault();
                // display graphics
                populateChart(dataPointsInChart);
                // remove unnessesary legend on top right of chart
                this.chart1.Legends.RemoveAt(0);
                // start timer 
                MainTimer.Interval = 1000 * Properties.Settings.Default.sensorReadInterval;
                MainTimer.Enabled = true;
                // disallow resizing
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
            }
            catch (Exception ex)
            {
                Error.WriteLog("main form load", ex.Message, "");
            }
        }
// ----------------TRAY ICON RELATED----------------------------------------------
        private void makeTrayIcon()
        {
            try
            {
                this.m_menu.MenuItems.Add(0,
                    new MenuItem("Show", new System.EventHandler(Show_Click)));
                this.m_menu.MenuItems.Add(1,
                    new MenuItem("Hide", new System.EventHandler(Hide_Click)));
                this.m_menu.MenuItems.Add(2,
                    new MenuItem("Exit", new System.EventHandler(Exit_Click)));

                this.m_notifyicon.Text = "Right click for context menu";
                this.m_notifyicon.Visible = true;
                this.m_notifyicon.Icon = new Icon("3highres.ico");
                this.m_notifyicon.ContextMenu = m_menu;
                this.m_notifyicon.DoubleClick += new EventHandler(notifyIconDoubbleClicked);
                this.SizeChanged += new System.EventHandler(this.MainFormSizeChanged);
            }
            catch (Exception ex)
            {
                Error.WriteLog("makeTrayIcon", ex.Message, "");
            }
        }

        private void MainFormSizeChanged(Object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                this.ShowInTaskbar = false;
            }
        }
        protected void notifyIconDoubbleClicked(Object sender, System.EventArgs e)
        {
            if (this.Visible != true)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
            }
            else if (this.Visible)
            {
                Hide();
            }
        }
        
        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Close();
            //m_notifyicon.Dispose();
        }
        protected void Hide_Click(Object sender, System.EventArgs e)
        {
            Hide();
        }
        protected void Show_Click(Object sender, System.EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //if (e.CloseReason = CloseReason.WindowsShutDown)
                //{
                //    MessageBox.Show("Noooo, dont go!");
                //}
                //if(
                //e.Cancel = true;
                //Hide();
                m_notifyicon.Dispose();
            }
            catch (Exception ex)
            {
                Error.WriteLog("form-closing", ex.Message, "");
            }
        }

// --------------end trayicon related------------------------------------------
        private void populateSettingsDefault()
        {
            try
            {
                // populate form with defaults
                // check if it's the program's first run (should be moved to ProjectMainForm before shipping)
                Properties.Settings.Default.isFirstRun = false;

                // populate form with saved settings
                checkMail.Checked = Properties.Settings.Default.warnMail;
                checkSMS.Checked = Properties.Settings.Default.warnSMS;
                txtInterval.Text = Convert.ToString(Properties.Settings.Default.sensorReadInterval);
                MainTimer.Interval = Properties.Settings.Default.sensorReadInterval * 1000;
                txtMax.Text = Convert.ToString(Properties.Settings.Default.tempMax);
                txtMin.Text = Convert.ToString(Properties.Settings.Default.tempMin);
            }
            catch (Exception ex)
            {
                // this error will never happen
                string errorMessage = "Could not set data to form: " + ex.Message;
                MessageBox.Show(errorMessage);
                Error.WriteLog("main form", ex.Message, "settings");
            }
        }

        private void populateChart(int numPoints) // populates Chart1 with the last temperature readings from the MDB
        {
            try
            {
                // remove the default values
                this.chart1.Series.Clear();

                Series series1 = this.chart1.Series.Add("Temperature");
                // make a spline type chart (can be set to show different types)
                series1.ChartType = SeriesChartType.Spline;
                // add axis labels
                chart1.ChartAreas[0].AxisY.Title = "Temperature (celsius)";
                chart1.ChartAreas[0].AxisX.Title = "Readings";
                // change marker thickness
                series1.BorderWidth = 5;

                // add data points to the chart
                this.chart1.Series[0].XValueType = ChartValueType.Auto;
                DataTable points = Database.generatePlotPoints(numPoints);
                foreach (DataRow row in points.Rows)
                {
                    series1.Points.AddXY(Convert.ToInt32(row["ID"].ToString()),
                        Convert.ToInt32(row["Temperature"].ToString()));
                }
                // add a nice heading
                Font headingFont = new Font(FontFamily.GenericSansSerif, 14);
                this.chart1.Titles.Clear();
                this.chart1.Titles.Add("Last " + numPoints + " readings:").Font = headingFont;


                // Methodes for desplaying lowest, hightst or avarage temperature in text fields.








            }
            catch (Exception ex)
            {
                Error.WriteLog("Main form", ex.Message, "chart");
            }
        }

        private void btnMoreData_Click(object sender, EventArgs e)
        {
            try
            {
                dataPointsInChart += 5;
                populateChart(dataPointsInChart);
            }
            catch (Exception ex)
            {
                Error.WriteLog("btnMoreData-click", ex.Message, "");
            }
        }

        private void btnLessData_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataPointsInChart >= 10)
                {
                    dataPointsInChart -= 5;
                }
                populateChart(dataPointsInChart);
            }
            catch (Exception ex)
            {
                Error.WriteLog("btnLessData-click", ex.Message, "");
            }
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // read from Sensor
                // using textBox to test
                //sensorValue = Convert.ToInt16(textBox1.Text);
                sensorValue = Sensor.GetTemp();

                // save sensor data to database
                Database.addSensorDataToMDB(sensorValue);
                // update chart
                populateChart(dataPointsInChart);

                // update status image 
                pictureBox1.Image = Error.exclamationGet();

                // check if values are outside limits
                if (sensorValue > Properties.Settings.Default.tempMax)
                {
                    // temperature too high
                    if (Properties.Settings.Default.warnMail == true) // send mail
                    {
                        Mail.sendMailToEntireContactsList("Temperature warning", "Temperature has exceeded the limit of: " +
                            Properties.Settings.Default.tempMax + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                    if (Properties.Settings.Default.warnSMS == true) // send SMS
                    {
                        SMS.sendSMSToEntireContactsList("Temperature warning", "Temperature has exceeded the limit of: " +
                            Properties.Settings.Default.tempMax + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                }
                if (sensorValue < Properties.Settings.Default.tempMin)
                {
                    // temperature too low
                    if (Properties.Settings.Default.warnMail == true) // send mail
                    {
                        Mail.sendMailToEntireContactsList("Temperature warning", "Temperature is below the limit of: " +
                            Properties.Settings.Default.tempMin + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                    if (Properties.Settings.Default.warnSMS == true) // send SMS
                    {
                        SMS.sendSMSToEntireContactsList("Temperature warning", "Temperature is below the limit of: " +
                            Properties.Settings.Default.tempMin + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Error.WriteLog("Timer1-tick", ex.Message, "");
            }


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // max temperature must be higher than minimum temperature
                // and interval must be 1 or higher
                if (Convert.ToInt16(txtMax.Text) > Convert.ToInt16(txtMin.Text) &&
                    Convert.ToInt16(txtInterval.Text) >= 1)
                {
                    // read settings from form and save them to application settings
                    Properties.Settings.Default.warnMail = checkMail.Checked;
                    Properties.Settings.Default.warnSMS = checkSMS.Checked;
                    Properties.Settings.Default.sensorReadInterval = Convert.ToInt16(txtInterval.Text);
                    MainTimer.Interval = Properties.Settings.Default.sensorReadInterval * 1000;
                    Properties.Settings.Default.tempMax = Convert.ToDecimal(txtMax.Text);
                    Properties.Settings.Default.tempMin = Convert.ToDecimal(txtMin.Text);
                    Properties.Settings.Default.Save();
                }
                //ID-Ten-T Errors, also seen as ID10T and ID107 ("idiot")
                else if (Convert.ToInt16(txtMin.Text) >= Convert.ToInt16(txtMax.Text))
                { MessageBox.Show("Temperature Minimum must be lower than the Maximum", "User error:"); }
                else if (Convert.ToInt16(txtInterval.Text) < 1)
                { MessageBox.Show("Interval set to low", "User error:"); }
            }
            catch (Exception ex)
            {
                // this will probably fail due to user error (datatype mismatch)
                string errorMessage = "Could not save settings: " + ex.Message;
                MessageBox.Show(errorMessage);
                Error.WriteLog("Main form", ex.Message, "settings");
            }
        }

        private void btnDefaults_Click(object sender, EventArgs e)
        {
            try
            {
                // populate form with defaults
                checkMail.Checked = true;
                checkSMS.Checked = false;
                txtInterval.Text = "1";
                txtMax.Text = "25";
                txtMin.Text = "15";
                // read settings from form and save them to application settings
                Properties.Settings.Default.warnMail = checkMail.Checked;
                Properties.Settings.Default.warnSMS = checkSMS.Checked;
                Properties.Settings.Default.sensorReadInterval = Convert.ToInt16(txtInterval.Text);
                MainTimer.Interval = Properties.Settings.Default.sensorReadInterval * 1000;
                Properties.Settings.Default.tempMax = Convert.ToDecimal(txtMax.Text);
                Properties.Settings.Default.tempMin = Convert.ToDecimal(txtMin.Text);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                // this error will never happen
                string errorMessage = "Could not set data to form: " + ex.Message;
                MessageBox.Show(errorMessage);
                // write error to log
                //errorLog_class.WriteTextFile.WriteError(errorMessage);
            }
        }

        private void btnContacts_Click(object sender, EventArgs e)
        {
            MDB_readerClass.Contacts_list_form contacts = new MDB_readerClass.Contacts_list_form();
            contacts.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.addSensorDataToMDB(Convert.ToInt16(textBox1.Text));
            populateChart(dataPointsInChart);
            //Error.WriteLog("main form", "sample error", "n comment");
        }

        private void lblAver_Click(object sender, EventArgs e)
        {

        }
    }
}
