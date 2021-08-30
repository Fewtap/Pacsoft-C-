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
            
            this.AcceptButton = button1;
            
        }

        
        
        

        private void refBox_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (PacsoftDriver.IsRunning)
            {
                MessageBox.Show("Printing already in progress");
            }
            else
            {
                string reference = ReferenceBox.Text;
                decimal amnt = AmntBox.Value;
                Task print = Task.Run(() => PacsoftDriver.printLabel(reference, amnt));


                Task incr = Task.Run(() => StepIncrement());

                
                ReferenceBox.Text = "";

            }

        }
        

        private Task StepIncrement()
        {
            

            while (PacsoftDriver.IsRunning)
            {

                printBar.BeginInvoke(new Action(() =>
                {
                    printBar.Value = PacsoftDriver.progress;
                }));

            }

            printBar.BeginInvoke(new Action(() =>
            {
                printBar.Value = 0;
            }));
            return null;
            
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CloseChromeProcesses();

            base.OnFormClosing(e);
        }
    }


    

}
