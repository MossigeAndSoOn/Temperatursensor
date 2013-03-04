﻿namespace MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnMoreData = new System.Windows.Forms.Button();
            this.btnLessData = new System.Windows.Forms.Button();
            this.grBoxSettings = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDefaults = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnContacts = new System.Windows.Forms.Button();
            this.checkSMS = new System.Windows.Forms.CheckBox();
            this.checkMail = new System.Windows.Forms.CheckBox();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.grBoxSettings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(13, 13);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(358, 156);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // btnMoreData
            // 
            this.btnMoreData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoreData.Location = new System.Drawing.Point(377, 12);
            this.btnMoreData.Name = "btnMoreData";
            this.btnMoreData.Size = new System.Drawing.Size(97, 23);
            this.btnMoreData.TabIndex = 1;
            this.btnMoreData.Text = "More datapoints";
            this.btnMoreData.UseVisualStyleBackColor = true;
            this.btnMoreData.Click += new System.EventHandler(this.btnMoreData_Click);
            // 
            // btnLessData
            // 
            this.btnLessData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLessData.Location = new System.Drawing.Point(377, 42);
            this.btnLessData.Name = "btnLessData";
            this.btnLessData.Size = new System.Drawing.Size(97, 23);
            this.btnLessData.TabIndex = 1;
            this.btnLessData.Text = "Less datapoints";
            this.btnLessData.UseVisualStyleBackColor = true;
            this.btnLessData.Click += new System.EventHandler(this.btnLessData_Click);
            // 
            // grBoxSettings
            // 
            this.grBoxSettings.Controls.Add(this.groupBox3);
            this.grBoxSettings.Controls.Add(this.btnDefaults);
            this.grBoxSettings.Controls.Add(this.btnSave);
            this.grBoxSettings.Controls.Add(this.groupBox2);
            this.grBoxSettings.Controls.Add(this.groupBox1);
            this.grBoxSettings.Location = new System.Drawing.Point(13, 175);
            this.grBoxSettings.Name = "grBoxSettings";
            this.grBoxSettings.Size = new System.Drawing.Size(468, 156);
            this.grBoxSettings.TabIndex = 2;
            this.grBoxSettings.TabStop = false;
            this.grBoxSettings.Text = "Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMin);
            this.groupBox3.Controls.Add(this.txtMax);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(176, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(180, 76);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Temperature limits:";
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(52, 45);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(59, 20);
            this.txtMin.TabIndex = 2;
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(52, 19);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(59, 20);
            this.txtMax.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Min:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Max:";
            // 
            // btnDefaults
            // 
            this.btnDefaults.Location = new System.Drawing.Point(362, 124);
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.Size = new System.Drawing.Size(102, 23);
            this.btnDefaults.TabIndex = 12;
            this.btnDefaults.Text = "Reset to defaults";
            this.btnDefaults.UseVisualStyleBackColor = true;
            this.btnDefaults.Click += new System.EventHandler(this.btnDefaults_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(362, 95);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtInterval);
            this.groupBox2.Location = new System.Drawing.Point(176, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 51);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensor readout interval:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Minutes";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(6, 19);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(44, 20);
            this.txtInterval.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnContacts);
            this.groupBox1.Controls.Add(this.checkSMS);
            this.groupBox1.Controls.Add(this.checkMail);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 133);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages to clients using:";
            // 
            // btnContacts
            // 
            this.btnContacts.Location = new System.Drawing.Point(6, 76);
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Size = new System.Drawing.Size(91, 23);
            this.btnContacts.TabIndex = 14;
            this.btnContacts.Text = "Edit contact list";
            this.btnContacts.UseVisualStyleBackColor = true;
            this.btnContacts.Click += new System.EventHandler(this.btnContacts_Click);
            // 
            // checkSMS
            // 
            this.checkSMS.AutoSize = true;
            this.checkSMS.Location = new System.Drawing.Point(6, 43);
            this.checkSMS.Name = "checkSMS";
            this.checkSMS.Size = new System.Drawing.Size(49, 17);
            this.checkSMS.TabIndex = 3;
            this.checkSMS.Text = "SMS";
            this.checkSMS.UseVisualStyleBackColor = true;
            // 
            // checkMail
            // 
            this.checkMail.AutoSize = true;
            this.checkMail.Location = new System.Drawing.Point(6, 19);
            this.checkMail.Name = "checkMail";
            this.checkMail.Size = new System.Drawing.Size(45, 17);
            this.checkMail.TabIndex = 2;
            this.checkMail.Text = "Mail";
            this.checkMail.UseVisualStyleBackColor = true;
            // 
            // MainTimer
            // 
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(386, 122);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(45, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "20";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 334);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.grBoxSettings);
            this.Controls.Add(this.btnLessData);
            this.Controls.Add(this.btnMoreData);
            this.Controls.Add(this.chart1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.grBoxSettings.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnMoreData;
        private System.Windows.Forms.Button btnLessData;
        private System.Windows.Forms.GroupBox grBoxSettings;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDefaults;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkSMS;
        private System.Windows.Forms.CheckBox checkMail;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.Button btnContacts;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

