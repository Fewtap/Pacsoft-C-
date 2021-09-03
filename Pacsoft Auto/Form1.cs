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
            if(usersettings.Default.FirstRun == true)
            {
                usersettings.Default.FirstRun = false;
            }
            else
            {
                delayBar.Value = (decimal)usersettings.Default.delay;
            }
        }

        
        
        

        private void refBox_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {


            int val = ReturnDelayValue();

            if(val == -1)
            {
                return;
            }

            if (PacsoftDriver.IsRunning)
            {
                MessageBox.Show("Printing already in progress");
            }
            else
            {
                PacsoftDriver driver = new PacsoftDriver(val);
                string reference = ReferenceBox.Text;
                decimal amnt = AmntBox.Value;
                try
                {
                    Task print = Task.Run(() => driver.printLabel(reference, amnt));
                    await print;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                
                


                //Task incr = Task.Run(() => StepIncrement());

                
                ReferenceBox.Text = "";

            }

        }
       
        int ReturnDelayValue()
        {
            int value;

            try
            {
                value = (int)delayBar.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }



            if (value != usersettings.Default.delay && (int)delayBar.Value != 0)
            {
                
                usersettings.Default.delay = value;
                usersettings.Default.Save();
            }
            else if (value == 0)
            {
                MessageBox.Show("Du måste ha något värde i 'Delay Rutan'");
                return -1;
            }

            return value;

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

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }


    

}
