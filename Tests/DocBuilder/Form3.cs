using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DocBuilder
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        WebClient client = new WebClient();

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var url = this.textBox1.Text.Trim();
                List<StockPrice> list = new List<StockPrice>();
                foreach (var line in client.DownloadString(url).Split('\r', '\n'))
                {
                    if (line.Length < 100)
                    {
                        continue;
                    }
                    list.Add(StockPrice.NewStockPrice(line));
                }
                this.dataGridView1.DataSource = list;
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<DailyPrice> list = new List<DailyPrice>();
            foreach (var path in Directory.GetFiles(@"C:\Users\zhumingwu\Desktop\stock", "*.txt", SearchOption.TopDirectoryOnly))
            {
                FileInfo fi = new FileInfo(path);
                var lines = File.ReadAllLines(path, Encoding.Default);

                for (int i = 2; i < lines.Length - 1; i++)
                {
                    var item = DailyPrice.NewDailyPrice(lines[i]);
                    if (item == null)
                    {
                        continue;
                    }
                    item.Code = fi.Name.Replace(fi.Extension, "");
                    list.Add(item);
                }

            }
            this.dataGridView1.DataSource = list;
        }
    }
}