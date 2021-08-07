using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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

        private async void button1_Click_1(object sender, EventArgs e)
        {
            PacsoftDriver driver = new PacsoftDriver();
            //Thread threadprint = new Thread(() => driver.printLabel(refBox.Text, AmntBox.Value));
            //threadprint.Start();

            //Thread progressupdate = new Thread(() => StepIncrement());
            refBox.Text = "";

            Task print = driver.printLabelAsync(refBox.Text, AmntBox.Value);
            Task stepIncr = StepIncrementAsync();

            await print;






        }
        

        private async Task StepIncrementAsync()
        {
            bool finished = false;

            while (!finished)
            {

                if(PacsoftDriver.progress == 5)
                {
                    finished = true;
                }

                progressBar1.Value = PacsoftDriver.progress;

                /*Thread.Sleep(50);
                progressBar1.BeginInvoke(
                    new Action(() => {
                        progressBar1.Value = PacsoftDriver.progress;
                    }
                    ));*/
            }

            /*progressBar1.Invoke(new Action(() => {

                Thread.Sleep(1000);
                progressBar1.Value = 0; }));*/

            await Task.Delay(1000);
            progressBar1.Value = 0;
            
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            ChromeDriverInstaller updateInstance = new(this);
            updateInstance.Install();
            UpdateButton.Enabled = false; 
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }


    

}
