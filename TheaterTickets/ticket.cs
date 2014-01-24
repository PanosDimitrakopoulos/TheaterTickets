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
    public partial class ticket : Form
    {
        ASCIIEncoding encoding = new ASCIIEncoding();
            
        string idd;
        string dayntime;
        string cplay;
        string cseat;
        public ticket(string currplay,string day_time,string id,string currseat,string baseprice)
        {
            InitializeComponent();
            idd = id;
            dayntime = day_time;
            cseat = currseat;

            releasedate.Text = "Ημερομηνία έκδοσης" + DateTime.Now;
            playtitle.Text = currplay;
            daytime.Text = day_time;
            seatnumber.Text = currseat; 
            if (Convert.ToInt32(currseat) <= 10)
            {
                double ma = 0.4;
                double extra=(Convert.ToDouble(baseprice) * ma);
                price.Text = (Convert.ToDouble(baseprice) +extra ).ToString() + "€";
            }else
              if (Convert.ToInt32(currseat) <= 20)
                {
                    double ma = 0.35;
                    double extra = (Convert.ToDouble(baseprice) * ma);
                    price.Text = (Convert.ToDouble(baseprice) + extra).ToString() + "€";
                 }
            else
                  if (Convert.ToInt32(currseat) <= 30)
                  {
                      double ma = 0.30;
                      double extra = (Convert.ToDouble(baseprice) * ma);
                      price.Text = (Convert.ToDouble(baseprice) + extra).ToString() + "€";
                  }
                  else
                      if (Convert.ToInt32(currseat) <= 40)
                      {
                          double ma = 0.25;
                          double extra = (Convert.ToDouble(baseprice) * ma);
                          price.Text = (Convert.ToDouble(baseprice) + extra).ToString() + "€";
                      }
                      else
                          if (Convert.ToInt32(currseat) <= 50)
                          {
                              double ma = 0.20;
                              double extra = (Convert.ToDouble(baseprice) * ma);
                              price.Text = (Convert.ToDouble(baseprice) + extra).ToString() + "€";
                          }
                          else
                              if (Convert.ToInt32(currseat) <= 60)
                              {
                                  double ma = 0.15;
                                  double extra = (Convert.ToDouble(baseprice) * ma);
                                  price.Text = (Convert.ToDouble(baseprice) + extra).ToString() + "€";
                              }
                              else
                          if (Convert.ToInt32(currseat) <= 72)
                          {
                              double ma = 0.10;
                              double extra = (Convert.ToDouble(baseprice) * ma);
                              price.Text = (Convert.ToDouble(baseprice) + extra).ToString() + "€";
                          }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://83.212.118.71/theaters/Addseats.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            string forupload;
            string[] linesfortext;
            
            string postData = "id=" + idd + "&number=" +cseat;

            byte[] data = encoding.GetBytes(postData);
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
            // textBox1.Text = responseString;
         

           

        }
    }
}
