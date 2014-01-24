using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TheaterTickets.classes;
using System.Net;
using System.IO;

namespace TheaterTickets
{
    public partial class seat_map : Form
    {
        RootPlay rootplay = new RootPlay();
        string day_time = "";
        string currplay = "";
        ASCIIEncoding encoding = new ASCIIEncoding();
           
       RootSeat currseats = new RootSeat();
        public seat_map(string play,string daytime,RootSeat seats,RootPlay plays)
        {  
            InitializeComponent();
            //this.Text = this.Tag.ToString();
            label1.Text = play;
            foreach (var x in seats.seats.seat)
            {
                PictureBox currbox = this.Controls.Find("seat"+x.number, true).FirstOrDefault() as PictureBox;
                currbox.Image = Properties.Resources.seattaken;
                currbox.Tag = "taken";
            }
            rootplay = plays;
            currseats = seats;
            day_time = daytime;
            currplay = play;

            
        }

        private void seat_click(object sender, EventArgs e)
        {

            PictureBox curr = sender as PictureBox;
            if (curr.Tag == null)
            {
                if (MessageBox.Show("Ειστε σίγουροι οτι θέλετε να καταχωρίσετε την θέση: "+curr.Name.Remove(0,4)+" και να προχωρήσετε σε Πώληση/Εκτύπωση;", "Καταχωρηση;", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //string numseat = curr.Name.Remove(0, 4);
                    //PictureBox currbox = this.Controls.Find(curr.Name, true).FirstOrDefault() as PictureBox;
                    //currbox.Image = Properties.Resources.seattaken;
                    //currbox.Tag = "taken";
                    ////currbox.Image = Properties.Resources.seatreserved;
                    var pid = rootplay.plays.play.Where(x => x.name == currplay);
                    string postid = pid.Where(y => y.day_time == day_time).First().id;
                    ticket ticket = new ticket(currplay, day_time,postid, curr.Name.Remove(0, 4),pid.Where(y => y.day_time == day_time).First().price);
                    ticket.Text = currplay + " " + day_time + " " + curr.Name.Remove(0, 4);
                    //ticket.Tag = listBox1.SelectedItem.ToString();

                    ticket.Show();
                    Hide();

                }
            }
            //else if (curr.Tag == "taken")
            //{
            //    string numseat = curr.Name.Remove(0, 4);
            //    PictureBox currbox = this.Controls.Find(curr.Name, true).FirstOrDefault() as PictureBox;
            //    currbox.Image = Properties.Resources.seatfree;
            //    currbox.Tag = "";
            //}



         
        }
    }
}
