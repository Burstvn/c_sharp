using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace httprequest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void AddCookie(HttpRequest http, string cookie)
        {
            var temp = cookie.Split(';');
            foreach (var item in temp)
            {
                var temp2 = item.Split('=');
                if (temp2.Count() > 1)
                {
                    http.Cookies.Add(temp2[0], temp2[1]);
                }
            }
        }
        string GetData(string url, string cookie = null)
        {
            HttpRequest http = new HttpRequest();
            http.Cookies = new CookieDictionary();


            if (!string.IsNullOrEmpty(cookie))
            {
                AddCookie(http, cookie);
            }

            http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
            string html = http.Get(url).ToString();
            return html;
        }
        void Getdata(int i)
        {
            Random number = new Random();

            HttpRequest http = new HttpRequest();
            http.AddHeader("Referer", textBox1.Text);
            http.Cookies = new CookieDictionary();
            http.Cookies.Add("sref", textBox1.Text.Split('=')[1]);
            string url = "https://nethermix.com/core.php";
            string usename = "adhfb" + number.Next(0, 9999).ToString();
            string data = "signup=1&login=undefined&password=Hoangbitcoin@1&password1=Hoangbitcoin@1&email=" + usename + i + "@gmail.com";
            http.Post(url, data, "application/x-www-form-urlencoded; charset=UTF-8");

            xuatDuLieu(usename, i);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Thread> threads = new List<Thread>();
            for(var j =0; j< numericUpDown2.Value; j++)
            {
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    int val = i;
                    Thread.Sleep(i * 1000);
                    Thread t = new Thread(() => { Getdata(val); });
                    t.Start();
                    t.Join(3000);
                    threads.Add(t);
                }
                for(var i = 0; i < threads.Count; i++)
                {
                    if (threads[i].IsAlive == false)
                    {
                        threads.Remove(threads[i]);
                    }
                }
            }
            


        }
           


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void xuatDuLieu(string users, int index)
        {

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("nethermix" + index.ToString() + ".txt", true))
                {
                    file.WriteLine(users);
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("lost :", ex);
            }



        }
    }
}
