using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label4.Text = DateTime.Now.ToLongDateString();
            label3.Text = DateTime.Now.ToLongTimeString();
            txtpass.PasswordChar = '*';

        }

        public class RootObject
        {
            
            public string username { get; set; }
            public string password { get; set; }
            public int admin { get; set; }
        }
        RootObject obj;
        private void button1_Click(object sender, EventArgs e)
        {
            string password = txtpass.Text;
            string html = string.Empty;
            string url = @"https://localhost:8080/searchu?username=" + txtuser.Text;
            try

            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                Console.WriteLine(html);

                if (html == "Database offline") { MessageBox.Show("Database offline"); }
                else
                {
                    try
                    {



                        var resultscan = JsonConvert.DeserializeObject<List<RootObject>>(html);

                        try
                        {
                            obj = resultscan[0];
                            Console.WriteLine(obj.password);
                            if (txtuser.Text == obj.username && txtpass.Text == obj.password)
                            {
                                
                                if (obj.admin == 0)
                                {
                                    MessageBox.Show("Logged in as User");
                                    using (Form3 f3 = new Form3())
                                    {
                                        this.Hide();
                                        f3.ShowDialog(this);
                                        this.Close();

                                    }
                                }
                                else 
                                {
                                    MessageBox.Show("Logged in as Admin");
                                    using (Form2 f2 = new Form2())
                                    {
                                        this.Hide();
                                        f2.ShowDialog(this);
                                        this.Close();
                                    }

                                }

                            }
                            else { MessageBox.Show("Boss"); }
                            MessageBox.Show(password);
                            Console.WriteLine(obj.password);
                        }
                        catch { MessageBox.Show("Nume de utilizator sau parola incorecta."); }

                    }
                    catch { MessageBox.Show("Error"); }
                }
            }
            catch { MessageBox.Show("Error"); }

 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://denisrugby.wixsite.com/mysite");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
