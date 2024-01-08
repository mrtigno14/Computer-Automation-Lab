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

namespace lab5
{
    public partial class Form1 : Form
    {
        public SerialPort port;
        int totalSeconds;
        bool isPaused = false;
        public Form1()
        {
            InitializeComponent();
            port = new SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 9600;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            port.Open();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
            label1.Text = timeSpan.ToString(@"hh\:mm\:ss");
            

            if (totalSeconds > 0)
            {
                totalSeconds--;
                
                
            }
            else
            {
                port.WriteLine("2");
                timer1.Stop();
                textBox1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TryParseTime(textBox1.Text, out totalSeconds))
            {
                // Update the label to show the new time without starting the timer
                TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
                label1.Text = timeSpan.ToString(@"hh\:mm\:ss");

                //timer1.Start();
                //listBox1.Items.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));


                textBox1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid time in 'hh:mm:ss' format.");
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();

            if (indata.Contains("Correct Password"))
            {
                // Start the timer when "correct password" is received from Arduino
                this.Invoke(new Action(() => timer1.Start()));
                this.Invoke(new Action(() => dateTimePicker1.Value = DateTime.Now));
                this.Invoke(new Action(() => listBox1.Items.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"))));
            }
            if (indata.Contains("Incorrect Password"))
            {
                this.Invoke(new Action(() => dateTimePicker1.Value = DateTime.Now));
                this.Invoke(new Action(() => listBox1.Items.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"))));
            }
        }


        private bool TryParseTime(string input, out int seconds)
        {
            seconds = 0;
            var timeComponents = input.Split(':');

            if (timeComponents.Length == 3 &&
                int.TryParse(timeComponents[0], out int hours) &&
                int.TryParse(timeComponents[1], out int minutes) &&
                int.TryParse(timeComponents[2], out int secs))
            {
                seconds = hours * 3600 + minutes * 60 + secs;
                return true;
            }

            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Toggle the isPaused variable to pause/resume the timer
            button2.Enabled = false;
            button3.Enabled = true;
            isPaused = !isPaused;

            if (isPaused)
            {
                // If paused, stop the timer
                timer1.Stop();
            }
            else
            {
                // If resumed, start the timer
                timer1.Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = false;
            if (isPaused)
            {
                isPaused = false;
                timer1.Start();
            }
        }
    }
}
