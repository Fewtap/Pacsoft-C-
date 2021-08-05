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
            Thread thread = new Thread(() => driver.printLabel(refBox.Text, AmntBox.Value));
            thread.Start();
        }
    }


    

}
