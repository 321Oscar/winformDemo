using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ProcessOperator
    {
        private BackgroundWorker backgroundWorker;
        private ProgressForm processForm;

        public ProcessOperator()
        {
            backgroundWorker = new BackgroundWorker();
            processForm = new ProgressForm();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (processForm.Visible)
            {
                processForm.Close();
            }
            //if (this.Back)
            //{

            //}
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        
    }
}
