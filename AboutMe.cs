using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank_Video
{
    public partial class AboutMe : Form
    {
        public AboutMe()
        {
            InitializeComponent();
        }

        private void AboutMe_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/maggy.gygy/");// ไดนอสติก
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/tnk__maxx/");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://line.me/ti/p/IJHpoI24ag"); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/#sent?compose=GTvVlcSBpRbLrmMlhqNPNsFNJnMHdTQVflKDcBQjPsWmMrcRlwVGnMGQSQTNZKVxTBhVjRjwNZcxB");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
