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
using Newtonsoft.Json;
using TheaterTickets.classes;

namespace TheaterTickets
{
    public partial class Form1 : Form
    {
        public static RootSeat rootseat = new RootSeat();
        public static RootPlay rootplay = new RootPlay();
        List<String> temp = new List<String>();
        string id;
     
       
        public Form1()
        {
            InitializeComponent();
            //listBox1.SelectionMode =SelectionMode.One;
            //listBox1.Items.Add("JANIS JOPLIN:ΔΩΜΑΤΙΟ 105");
            //listBox1.Items.Add("4.48 ΕΝΑ ΤΡΑΓΟΥΔΙ");
            //listBox1.Items.Add("Ο ΘΕΟΣ ΒΑΡΙΕΤΑΙ ΤΩΡΑ ΤΕΛΕΥΤΑΙΑ");
            //listBox1.Items.Add("ΒΡΥΚΌΛΑΚΕΣ");
            //listBox1.Items.Add("BEDTIME STORIES");
            //listBox1.Items.Add("Η ΑΥΞΗΣΗ");

            //listBox2.SelectionMode = SelectionMode.One;
            //listBox2.Items.Add("10-12");
            //listBox2.Items.Add("12-2");
            //listBox2.Items.Add("2-4");
            //listBox2.Items.Add("4-6");
            //listBox2.Items.Add("6-8");
            //listBox2.Items.Add("9-11");
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://83.212.118.71/theaters/get.php");
            req.Method = "GET";
            if (req.Headers == null)
            {
                req.Headers = new WebHeaderCollection();
            }
           // req.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();

            req.BeginGetResponse(new AsyncCallback(getresponseforplays), req);






        }


        void getresponseforplays(IAsyncResult MyResultAsync)
        {
            HttpWebResponse response;
            do
            {
                HttpWebRequest request = (HttpWebRequest)MyResultAsync.AsyncState;

                response = (HttpWebResponse)request.EndGetResponse(MyResultAsync);
            }
            while (response.StatusCode != HttpStatusCode.OK);

            if (response.StatusCode == HttpStatusCode.OK && response.ContentLength > 0)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();

                    rootplay = JsonConvert.DeserializeObject<RootPlay>(result);
                BeginInvoke((Action)(() =>
                         {
                            
                    for (int i = 0; i < rootplay.plays.play.Count; i++)
                    {
                        if (temp.Contains(rootplay.plays.play[i].name) )continue;
                       temp.Add(rootplay.plays.play[i].name);
                    }


                            
                    temp.Distinct().ToList();
                    foreach (string th in temp)
                    {
                        if (listBox1.Items.Contains(th)) continue;
                       
                        listBox1.Items.Add(th);
                    }
                  
                    for (int i = 0; i < rootplay.plays.play.Count; i++)
                    {
                        if(rootplay.plays.play[i].name==rootplay.plays.play[0].name)listBox2.Items.Add(rootplay.plays.play[i].day_time);
                    }
                   // listBox1.Items.Add(temp);
                         }));

                }
            }




        }


        private void button1_Click(object sender, EventArgs e)
        {
            seat_map seatmap = new seat_map(listBox1.SelectedItem.ToString(), listBox2.SelectedItem.ToString(),rootseat,rootplay);
            seatmap.Text = listBox1.SelectedItem.ToString();
            seatmap.Tag = listBox1.SelectedItem.ToString();
            
            seatmap.Show();
            Hide();
        }

        private void play_changed(object sender, EventArgs e)
        {
              int index = listBox1.SelectedIndex;
            //desc.Text = rootplay.plays.play.Where(x => x.name == temp[index]).First().shortinfo;
            // id = rootplay.plays.play.Where(x => x.id == temp[index]).First().id;
            listBox2.Items.Clear();
            for (int i = 0; i < rootplay.plays.play.Count; i++)
            {
                if (rootplay.plays.play[i].name == temp[index]) listBox2.Items.Add(rootplay.plays.play[i].day_time);
            }


           
        }


        void getresponseforseats(IAsyncResult MyResultAsync)
        {
            HttpWebResponse response;
            do
            {
                HttpWebRequest request = (HttpWebRequest)MyResultAsync.AsyncState;

                response = (HttpWebResponse)request.EndGetResponse(MyResultAsync);
            }
            while (response.StatusCode != HttpStatusCode.OK);

            if (response.StatusCode == HttpStatusCode.OK && response.ContentLength > 0)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string result = sr.ReadToEnd();

                    rootseat = JsonConvert.DeserializeObject<RootSeat>(result);
                    BeginInvoke((Action)(() =>
                    {
                        rootseat.seats.seat = rootseat.seats.seat.Where(x => x.id == id).ToList();
                        availableseats.Text = (122 - rootseat.seats.seat.Count).ToString()+"/122";

                    }));

                }
            }




        }

        private void day_time_changed(object sender, EventArgs e)
        {
            id = rootplay.plays.play.Where(x => x.day_time == listBox2.SelectedItem).First().id;
            desc.Text = rootplay.plays.play.Where(x => x.day_time == listBox2.SelectedItem).First().shortinfo;


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://83.212.118.71/theaters/getseats.php");
            req.Method = "GET";
            if (req.Headers == null)
            {
                req.Headers = new WebHeaderCollection();
            }
            // req.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();

            req.BeginGetResponse(new AsyncCallback(getresponseforseats), req);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            manager.Show();
            this.Hide();
        }
    }
}
