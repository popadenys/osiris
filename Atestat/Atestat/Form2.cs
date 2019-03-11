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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            populateDGV();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public class RootObject
        {

            public string username { get; set; }
            public string password { get; set; }
            public int admin { get; set; }
        }

        public void populateDGV()
        {

            string html = string.Empty;
            string url = @"https://localhost:8080/users";
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
                if (html == "Database offline") { MessageBox.Show("Database offline"); }
                else
                {
                    var result = JsonConvert.DeserializeObject<List<RootObject>>(html);
                    dataGridView1.DataSource = result;
                }

             }
            catch
            {
                MessageBox.Show("Error");
            }

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        int counter;
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (counter == 0)
            {
                int admin;
                string username;
                string password;


                admin = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["admin"].Value.ToString());
                username = dataGridView1.Rows[e.RowIndex].Cells["username"].Value.ToString();
                password = dataGridView1.Rows[e.RowIndex].Cells["password"].Value.ToString();


                string html2 = string.Empty;
                string url2 = @"https://localhost:8080/updategriduser?username=" + username + "&admin=" + admin + "&password=" + password;
                //https://localhost:8080/updategriduser?username=test&password=newpassword&admin=5

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html2 = reader.ReadToEnd();
                }


                if (html2 == "Good to go")
                {

                }
                else if (html2 == "Database offline")
                {
                    MessageBox.Show("Database offline", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                counter = 0;
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            counter = 1;
            // Don't throw an exception when we're done.
            e.ThrowException = false;

            // Display an error message.
            string txt = "Error with " +
                dataGridView1.Columns[e.ColumnIndex].HeaderText +
                "\n\n" + e.Exception.Message;
            MessageBox.Show(txt, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            // If this is true, then the user is trapped in this cell.
            e.Cancel = false;
        }
    }
}
