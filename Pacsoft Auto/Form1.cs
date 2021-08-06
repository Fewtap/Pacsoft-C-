using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Pacsoft_Auto
{
    public partial class Form1 : Form
    {
        BackgroundWorker bgw = new BackgroundWorker();
        public Form1()
        {
            InitializeComponent();
        }

        

        

        private void refBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PacsoftDriver driver = new PacsoftDriver();
            Thread threadprint = new Thread(() => driver.printLabel(refBox.Text, AmntBox.Value));
            threadprint.Start();
            Thread progressupdate = new Thread(() => StepIncrement());

            progressupdate.Start();

            




        }
        

        private void StepIncrement()
        {
            bool finished = false;

            while (!finished)
            {

                if(PacsoftDriver.progress == 5)
                {
                    finished = true;
                }
                Thread.Sleep(50);
                progressBar1.BeginInvoke(
                    new Action(() => {
                        progressBar1.Value = PacsoftDriver.progress;
                    }
                    ));
            }

            progressBar1.Invoke(new Action(() => {

                Thread.Sleep(1000);
                progressBar1.Value = 0; }));
            
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            ChromeDriverInstaller updateInstance = new();
            updateInstance.Install();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }


    

}
