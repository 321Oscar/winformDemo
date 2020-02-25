using System;
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
    public partial class ProgressForm : Form
    {
        private BackgroundWorker backgroundWorker;

        public ProgressForm(BackgroundWorker ba)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.backgroundWorker = ba;
            this.backgroundWorker.WorkerReportsProgress = true;//允许 backgroundWorker 报告进度
            this.backgroundWorker.WorkerSupportsCancellation = true;//backgroundWorker 支持取消异步操作
            this.backgroundWorker.ProgressChanged += backgroundWorkerProgressChanged;
            this.backgroundWorker.RunWorkerCompleted += backgroundWorkerRunWorkerCompleted;
            if (this.backgroundWorker.IsBusy)
            {
                return;
            }
            this.backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show("Cancelled");
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Done");
            }
            this.Close();
        }

        private void backgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pgbMain.Value = e.ProgressPercentage;
            this.lblProcessStatus.Text = e.UserState.ToString();
        }

        public ProgressForm()
        {
            InitializeComponent();
        }

        private void btnProcessStop_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }
    }
}
