﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class progressForm : Form
    {
        private BackgroundWorker backgroundWorker;

        public progressForm(BackgroundWorker ba)
        {
            InitializeComponent();
            //GlobalData
            this.backgroundWorker = ba;
            this.backgroundWorker.ProgressChanged += backgroundWorkerProgressChanged;
            this.backgroundWorker.RunWorkerCompleted += backgroundWorkerRunWorkerCompleted;
        }

        private void backgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            MessageBox.Show("Done");
            this.Close();
        }

        private void backgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pgbMain.Value = e.ProgressPercentage;
            this.lblProcessStatus.Text = e.UserState.ToString();
        }

        /// <summary>
        /// 进度信息
        /// </summary>
        public string MessageInfo
        {
            set { this.lblProcessStatus.Text = value; }
        }

        /// <summary>
        /// 进度条值
        /// </summary>
        public int ProcessValue
        {
            set { this.pgbMain.Value = value; }
        }

        public progressForm()
        {
            InitializeComponent();
        }

        private void btnProcessStop_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }
    }
}
