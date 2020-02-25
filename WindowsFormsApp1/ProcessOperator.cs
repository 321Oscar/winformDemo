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
        private processForm processForm;

        public ProcessOperator()
        {
            backgroundWorker = new BackgroundWorker();
            processForm = new processForm();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
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
