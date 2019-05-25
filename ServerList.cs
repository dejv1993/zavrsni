using SimpleTCP;
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

namespace Seminar
{
    public partial class ServerList : Form
    {
        public IPAddress address;
        Client client;
        public String logedUser;
        public ServerList()
        {
            client = new Client();
            InitializeComponent();
        }

        public void clientClosing(object sender,FormClosingEventArgs e)
        {


            if (client != null)
            {
                client.client.Disconnect();
                client.Dispose();
            }
            this.Show();

        }

        //refresh button
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            listView3.Clear();
            using (PlayersEntities1 play = new PlayersEntities1())
            {
                var games = play.Games.ToList();
                var p = play.Players.ToList();
                if (p != null)
                {

                    foreach (var server in p)
                    {
                        var playerGames = server.Games.Where(r => r.Status != "Closed");

                        string s =server.IpAddress.ToString();
                        if (!s.Equals(""))
                        {
                            foreach (var g in playerGames)
                            {
                                if (g.Status!="Closed")
                                {
                                    ListViewItem item1 = new ListViewItem();
                                    /*/
                                    item1.SubItems.Add(server.Username);
                                    item1.SubItems.Add(server.Win_Rate.ToString());

                                    listView2.Items.Add(item1);
                                    /*/

                                    item1.Tag = server.IpAddress;
                                    item1.Text = server.Username;

                                    
                                    item1.SubItems.Add(g.Players_In_Game.ToString());
                                    item1.SubItems.Add(g.Player_Game.ToString());
                                    item1.SubItems.Add("Card Number" + g.Game_Type.ToString());
                                    item1.SubItems.Add(server.Win_Rate.ToString());

                                    listView2.Items.Add(item1);
                                }
                            }
                        }
                        if (server.Status.Equals("Online"))
                        {
                            if (!server.Username.Contains(logedUser))
                            {
                                ListViewItem item2 = new ListViewItem();
                                item2.Text = server.Username;
                                item2.Tag = server;
                                item2.ImageIndex = 1;
                                listView3.Items.Add(item2);
                            
                            }
                        }

                    }
                }
            }

        }

      

        private void listView2_DoubleClick(object sender, EventArgs e)
        {

            int poz = listView2.SelectedIndices[0];
            int playersInServer = 0;
            int maxNumber = 0;
            
            if(int.TryParse(listView2.Items[poz].SubItems[1].Text,out int i))
            {
                playersInServer = i;
            }
            if(int.TryParse(listView2.Items[poz].SubItems[2].Text,out int i1))
            {
                maxNumber = i1;
            }
            
            if (i!=i1)
            {


                if (!client.IsDisposed)
                {
                    client.Show();
                    client.FormClosing += this.clientClosing;

                    this.Hide();
                    client.Join(listView2.Items[poz].Tag.ToString());
                    if(int.TryParse(listView2.Items[poz].SubItems[3].Text,out int numb))
                        {
                        client.gameType = numb;

                        }



                }
                else
                {
                    client = new Client();
                    client.FormClosing += this.clientClosing;

                    this.client.logedUser = logedUser;

                    client.Show();

                    client.Join(listView2.Items[poz].Tag.ToString());
                    if (int.TryParse(listView2.Items[poz].SubItems[3].Text, out int numb))
                    {
                        client.gameType = numb;

                    }
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("Server is full");
            }
        }

        private void ServerList_Load(object sender, EventArgs e)
        {

            listView3.View = View.LargeIcon;
            this.listView3.LargeImageList = this.imageList1;

            imageList1.Images.Add("play", Properties.Resources.dot);
            this.client.logedUser = logedUser;
            using (PlayersEntities1 players= new PlayersEntities1())
            {
                var PlayersOnline = players.Players.ToList();

                foreach (var online in PlayersOnline)
                    {
                    var playerGames = online.Games.Where(r => r.Status != "Closed");

                    string s = online.IpAddress;
                    if (!s.Equals(""))
                    {
                        foreach (var g in playerGames)
                        {
                            ListViewItem item3 = new ListViewItem();




                            item3.Tag = online.IpAddress;
                                item3.Text = online.Username;

                                item3.SubItems.Add(g.Players_In_Game.ToString());
                            item3.SubItems.Add(g.Player_Game.ToString());
                                item3.SubItems.Add(g.Game_Type.ToString());
                                item3.SubItems.Add(online.Win_Rate.ToString());

                                listView2.Items.Add(item3);
                            }
                        };
                    

                    // lista online igraca
                    if (online.Status.Equals("Online"))
                    {

                        if (!online.Username.Equals(logedUser))
                        {
                            ListViewItem item2 = new ListViewItem();
                            item2.ImageIndex = 1;
                            item2.Text = online.Username;
                            listView3.Items.Add(item2);
                          

                        }
                        

                        
                    }
                        
                    }
                }
            }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PlayersEntities1 player = new PlayersEntities1())
            {
                Player p = player.Players.FirstOrDefault(r => r.Username == logedUser);
                p.Status = "Offline";
                player.SaveChanges();
            }
            this.Dispose();
            ServerClient.ActiveForm.Dispose();
            Application.Restart();
           
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ServerList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
    }

