using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TheaterTickets
{
    public partial class Manager : Form
    {
      
        ASCIIEncoding encoding = new ASCIIEncoding();
        
       
        public Manager()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Εισαγωγή: πχ. Δευτέρα,14-16  για πολλαπλες παραστάσεις: πχ: Δευτερα,10-12,Τρίτη,13-17");
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (playtxt.Text == "" || daytimestxt.Text == "" || shortinfotxt.Text == "" || pricetxt.Text == "")
            {
                MessageBox.Show("Παρακαλώ συμπληρώστε όλα τα στοιχεία πριν καταχωρήσετε την παράσταση.");
            }else
            {
                string[] words=daytimestxt.Text.Split(',');
                for (int i = 0; i < words.Count(); i += 2)
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://83.212.118.71/theaters/Add.php");
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";

                    string forupload;
                    string[] linesfortext;

                    string postData = "name="+playtxt.Text+"&shortinfo="+shortinfotxt.Text+"&day_time="+ words[i]+"_"+words[i+1]+"&seatratio=1&price="+pricetxt.Text;

                    //byte[] data = encoding.GetBytes(postData);
                    byte[] data = Encoding.UTF8.GetBytes(postData);
                    req.ContentLength = data.Length;

                    using (Stream stream = req.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if (responseString == "1")
                    {
                        MessageBox.Show("Η καταχώρηση ολοκληρώθηκε.");
                        Form1 newform1 = new Form1();
                        newform1.Show();

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("H καταχώρηση δεν ολοκληρώθηκε,δοκιμάστε ξανά η ενημερώστε τον υπέυθυνο για το θέμα.");
                        Form1 newform1 = new Form1();
                        newform1.Show();

                        Close();
                    }

                }
               
            }
        }
    }
}
