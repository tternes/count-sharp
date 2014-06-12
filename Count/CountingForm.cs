using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Count
{
    public partial class CountingForm : Form
    {
        public static int s_instances = 0;

        private Random rand = new Random(DateTime.Now.Millisecond);
        private Timer formCounter = null;
        private DateTime startTime = DateTime.Now;
        public CountingForm()
        {
            InitializeComponent();
            s_instances++;

            startTime = DateTime.Now;

            formCounter = new Timer();
            formCounter.Tick += this.TimerTicked;
            formCounter.Interval = 1000;
            formCounter.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            formCounter.Stop();

            base.OnClosed(e);
            if (--s_instances == 0)
                Application.Exit();
        }

        public void TimerTicked(object sender, EventArgs e)
        {
            TimeSpan duration = DateTime.Now - startTime;
            this.counterLabel.Text = string.Format("{0}:{1}:{2}", 
                duration.Hours.ToString("0"),
                duration.Minutes.ToString("00"),
                duration.Seconds.ToString("00"));
            this.BackColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CountingForm().Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
