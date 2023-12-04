using YourFlightInstructor.controller;
using System;
using System.Threading;
using System.Windows.Forms;
using YourFlightInstructor.Service;
using System.Drawing;

namespace YourFlightInstructor
{
    public partial class MainUI : Form
    {
        Thread thread;
        
        public MainUI()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            btnStop.Enabled = false;
        }

        public void UpdateData(SimData data)
        {
            if (textOutput.InvokeRequired)
            {
                textOutput.Invoke(new Action<SimData>(UpdateData), data);
            }
            else
            {
                textOutput.SelectionStart = 0;
                textOutput.SelectedText = data.RadioAltitude + " feet (" + Math.Floor(data.RadioAltitude / 3.3) + "米), " + (data.VerticalSpeed * 60) + " feet/min" + Environment.NewLine;
                lableAircraft.Text = "Aircraft: " + data.AircraftTitle;

                textSimData.Text = "";
                textSimData.Text = data.ToValueString();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            thread = new Thread(new Controller(this).start);
            thread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (thread != null && thread.ThreadState != ThreadState.Stopped)
            {
                thread.Abort();
            }
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textOutput.Text = "";
        }

        private void btnTop_Click(object sender, EventArgs e)
        {

            if (this.TopMost)
            {
                this.TopMost = false;
                btnTop.BackColor = SystemColors.Control;
            }
            else
            {
                this.TopMost = true;
                btnTop.BackColor = SystemColors.ActiveCaption;
            }
        }

        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnStop_Click(sender, e);
        }
    }
}
