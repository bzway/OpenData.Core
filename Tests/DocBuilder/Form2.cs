using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocBuilder
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var url = this.textBox1.Text.Trim();
                var data = this.textBox2.Text.Trim();
                System.Net.WebClient client = new System.Net.WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                this.textBox3.Text = System.Text.Encoding.Default.GetString(client.UploadData(url, "POST", System.Text.Encoding.Default.GetBytes(data)));
            }
            catch (Exception ex)
            {
                this.textBox3.Text = ex.Message;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var url = this.textBox1.Text.Trim();
                var data = this.textBox2.Text.Trim();
                System.Net.WebClient client = new System.Net.WebClient();
                client.Credentials = CredentialCache.DefaultCredentials;
                this.textBox3.Text = client.DownloadString(url);
            }
            catch (Exception ex)
            {

                this.textBox3.Text = ex.Message;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.textBox3.Text);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
