using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_test_
{
    public partial class Form1 : Form
    {
        string value;
        sbyte indexOfA;
        string output_value;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;

            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                progressBar1.Value = 0;

            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                serialPort1.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            value = serialPort1.ReadLine();
            this.BeginInvoke(new EventHandler(ProcessData));
        }

        private void ProcessData(object sender, EventArgs e)
        {
            try
            {
                indexOfA = Convert.ToSByte(value.IndexOf("A"));
                //indexOfB = Convert.ToSByte(value.IndexOf("B"));
                //indexOfC = Convert.ToSByte(value.IndexOf("C"));

                output_value = value.Substring(0, indexOfA);
                //lightValue2 = value.Substring(indexOfA + 1, (indexOfB - indexOfA) - 1);
                //servoAngle = value.Substring(indexOfB + 1, (indexOfC - indexOfB) - 1);

                //label7.Text = lightValue1;
                //label8.Text = lightValue2;
                progressBar1.Value = Convert.ToInt16(output_value);
            }

            catch (Exception error)
            {
             MessageBox.Show(error.Message);
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("1");
        }
    }
}
