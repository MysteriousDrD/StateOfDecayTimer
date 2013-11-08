using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StateOfDecayTimeChanger
{
    public partial class Form1 : Form
    {
        public int restore = 0;

        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("Kernel32.dll")]
        private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("Kernel32.dll")]
        private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

        private void GetTime()
        {
            // Call the native GetSystemTime method 
            // with the defined structure.
            SYSTEMTIME stime = new SYSTEMTIME();
            GetSystemTime(ref stime);

            // Show the current time.           
            MessageBox.Show("Current Time: " +
                stime.wHour.ToString() + ":"
                + stime.wMinute.ToString());
        }

        private void SetTime()
        {
            // Call the native GetSystemTime method 
            // with the defined structure.
            SYSTEMTIME systime = new SYSTEMTIME();
            GetSystemTime(ref systime);

            // Set the system clock ahead one hour.
            systime.wHour = (short)((systime.wHour - 1) % 24);
            restore++;
            SetSystemTime(ref systime);
            MessageBox.Show("New time: " + systime.wHour.ToString() + ":"
                + systime.wMinute.ToString());
        }

        private void restoreTime()
        {
            SYSTEMTIME systime = new SYSTEMTIME();
            GetSystemTime(ref systime);

            // Set the system clock ahead one hour.
            systime.wHour = (short)((systime.wHour + restore) % 24);
            restore = 0;
            SetSystemTime(ref systime);
            MessageBox.Show("New time: " + systime.wHour.ToString() + ":"
                + systime.wMinute.ToString());
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetTime();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetTime();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            restoreTime();
        }
    }
}
