using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Pacsoft_Auto
{
    public partial class Form1 : Form
    {
        
        
        public Form1()
        {
            InitializeComponent();
            CloseChromeProcesses();
            
        }

        
        private async void CloseChromeProcesses()
        {
            try
            {
                Process[] proc = Process.GetProcessesByName("chromedriver.exe");
                foreach (var item in proc)
                {
                    item.Kill();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        

        private void refBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (PacsoftDriver.IsRunning)
            {
                MessageBox.Show("Printing already in progress");
            }
            else
            {
                Thread threadprint = new Thread(() => PacsoftDriver.printLabel(refBox.Text, AmntBox.Value));
                threadprint.Start();

                Thread progressupdate = new Thread(() => StepIncrement());
                refBox.Text = "";

            }










        }
        

        private void StepIncrement()
        {
            bool finished = false;

            while (!finished)
            {

                if (PacsoftDriver.progress == 5)
                {
                    finished = true;
                    printBar.BeginInvoke(new Action(() =>
                    {
                        printBar.Value = 0;

                    }));
                }



                printBar.BeginInvoke(new Action(() =>
                {
                    printBar.Value = PacsoftDriver.progress;
                }));

                
                

            }
            
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            ChromeDriverInstaller updateInstance = new(this);
            await updateInstance.Install();
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
