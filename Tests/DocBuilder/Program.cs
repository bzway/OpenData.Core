using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocBuilder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                foreach (var item in args)
                {
                    try
                    {
                        var json = JsonConvert.DeserializeObject<Webpost>(File.ReadAllText(item));
                        var form = new Form2();
                        form.Setting(json.Url, json.Data);
                        form.ShowDialog();
                    }
                    catch { }
                }
            }
            Application.Run(new Form2());
        }
    }
    public class Webpost
    {
        public string Url { get; set; }
        public string Data { get; set; }
    }
}