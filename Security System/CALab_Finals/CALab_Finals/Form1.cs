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
using System.Net.NetworkInformation;

namespace CALab_Finals
{
    public partial class Form1 : Form
    {
        public SerialPort port;
        public Form1()
        {
            port = new SerialPort();
            port.PortName = "COM4";
            port.BaudRate = 9600;
            port.Open();
            InitializeComponent();
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            textBox1.PasswordChar = '*';

            button4.Click += TogglePasswordVisibility;
        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            // Toggle the PasswordChar property between '*' and '\0' (empty character)
            textBox1.PasswordChar = (textBox1.PasswordChar == '\0') ? '*' : '\0';

            // Update the button text based on the current state
            button4.Text = (textBox1.PasswordChar == '\0') ? "Hide" : "Show";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text; // Get password from the TextBox
            SendPasswordToArduino(password);
        }

        private void SendPasswordToArduino(string password)
        {
            if (port.IsOpen)
            {
                try
                {
                    port.WriteLine(password); // Send the password to Arduino
                    // You may want to add some delay here to give Arduino time to process the input.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending password: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Serial port is not open.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendCommandToArduino("TURN_OFF_RELAY");

            // Close the serial port
            if (port != null && port.IsOpen)
            {
                port.Close();
            }

            // Close the form
            
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SendCommandToArduino(string command)
        {
            if (port.IsOpen)
            {
                try
                {
                    port.WriteLine(command);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending command to Arduino: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Serial port is not open.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the serial port when the form is closing
            if (port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();

            if (indata.Contains("Correct Password"))
            {
                this.Invoke(new Action(() => ShowPasswordCorrectPopup()));
                this.Invoke(new Action(() => dateTimePicker1.Value = DateTime.Now));
                this.Invoke(new Action(() => listBox1.Items.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"))));
            }
            if (indata.Contains("Incorrect Password"))
            {
                this.Invoke(new Action(() => ShowPasswordIncorrectPopup()));
                this.Invoke(new Action(() => dateTimePicker1.Value = DateTime.Now));
                this.Invoke(new Action(() => listBox1.Items.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"))));
            }

            if (indata.Contains("Fire/Smoke Detected"))
            {
                this.Invoke(new Action(() => ShowFireSmokePopup()));
            }

        }

        private void ShowFireSmokePopup()
        {
            MessageBox.Show("Fire/Smoke Detected!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowPasswordCorrectPopup()
        {
            MessageBox.Show("Password is correct!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowPasswordIncorrectPopup()
        {
            MessageBox.Show("Password is incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
