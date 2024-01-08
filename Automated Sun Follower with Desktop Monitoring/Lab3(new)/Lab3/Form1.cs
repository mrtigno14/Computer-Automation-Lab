using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public SerialPort port;
        sbyte indexOfA, indexOfB, indexOfC;
        string lightValue1, lightValue2, servoAngle; 
        public Form1()
        {
            InitializeComponent();
            InitializeSerialPort();
            //trackBar1.Maximum = 180;
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void InitializeSerialPort()
        {
            port = new SerialPort();
            port.PortName = "COM4";
            port.BaudRate = 9600;
            port.Open();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string value = port.ReadExisting();


            //label7.Text = value.ToString();
            try
            {
                indexOfA = Convert.ToSByte(value.IndexOf("A"));
                indexOfB = Convert.ToSByte(value.IndexOf("B"));
                indexOfC = Convert.ToSByte(value.IndexOf("C"));

                lightValue1 = value.Substring(0, indexOfA);
                lightValue2 = value.Substring(indexOfA + 1, (indexOfB - indexOfA) -1);
                servoAngle = value.Substring(indexOfB + 1, (indexOfC - indexOfB) - 1);

                label7.Text = lightValue1;
                label8.Text = lightValue2;
                trackBar1.Value = Convert.ToInt16(servoAngle);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
