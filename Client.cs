using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SimpleTCP;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Seminar
{
    public partial class Client : Form
    {
        public delegate void ClientMoveEventHandler(string s);
        public event ClientMoveEventHandler ClientMove;
       
        public IPAddress address;
        public  SimpleTcpClient client;
        public int port;
        public const string left = "kicked";
        public String logedUser;
        public int gstart = 1;
        public string start = "";
        public string potez = "";
        public Form1 game;
        public int gameType;
        public WinnerForm winForm;
        public int clientWin = 0;
         public int serverWin = 0;
        public TcpClient tcpClient;
        Form1 f;
        GameFour g;
        private bool gameRunning = false;

        public Client()
        {
            InitializeComponent();
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_Received;
          
            winForm = new WinnerForm();

        }
     

  

     
        public void Join(String Ip)
        {

            IPAddress.TryParse(Ip, out address);
            try
            {
                client.Connect(Ip, 2000);
              
                   client.WriteLine(logedUser);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message.ToString());


                this.Dispose();               
             
            }

        }


        //form1 closed while game is running so the event is raised  and client will left the server
        public void Game_Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (gameRunning)
            {
                button2_Click(null, new EventArgs());
            }
        }
        
        public void LocalHost()
        {
            try
            {
                client.Connect("127.0.0.1", 2000);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToLower());
            }

        }

        private void Client_Load(object sender, EventArgs e)
        {
          

        }
        public void Client_Received(object sender, SimpleTCP.Message m)
        {
              
            string end = m.MessageString.Substring(0, m.MessageString.Length - 1);
           string kicked=m.MessageString.Substring(0, m.MessageString.Length - 1);
            if(kicked.Equals(left))
            {
                button2_Click(this,EventArgs.Empty);
            }
                if (end.Equals("e") || end.Equals("e!!"))
                {
                    this.serverWin++;

                    if (this.serverWin > 0)
                    {

                        MessageBox.Show("you lost");

                        winForm.pictureBox1.Image = Properties.Resources.lost;
                        winForm.ShowDialog();
                        DialogResult dialog = MessageBox.Show("New Game", "exit", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {
                        if (gameType == 3)
                        {
                            this.serverWin = 0;
                            this.clientWin = 0;
                            f.MouseClicked -= this.OnMOuseClicked;
                            this.ClientMove -= f.OnClientMove;
                            f.Dispose();
                        }
                        else
                        {
                            this.serverWin = 0;
                            this.clientWin = 0;
                            g.MouseClicked -= this.OnMOuseClicked;
                            this.ClientMove -= g.OnClientMove;
                            g.Dispose();
                        }
                    }

                    //klijent ne zeli novu igru te izlazi iz servera
                    else if (dialog == DialogResult.No)
                    {
                        try
                        {
                            button2_Click(null, new EventArgs());
                            //this.Client_FormClosing(null, new FormClosingEventArgs(CloseReason.None,false));
                            if (gameType == 3)
                            {
                                if (!f.IsDisposed)
                                {
                                    f.Dispose();
                                }
                            }
                            else
                            {
                                if (!g.IsDisposed)
                                {
                                    g.Dispose();
                                }
                            }
                            
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message.ToString());
                        }
                    }
                }

                    //server dobio partiju u malim
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Continue", "exit", MessageBoxButtons.YesNo);

                        if (dialog == DialogResult.Yes)
                        {
                        if (gameType == 3)
                        {
                            f.MouseClicked -= this.OnMOuseClicked;
                            this.ClientMove -= f.OnClientMove;

                            f.Dispose();
                        }
                        else
                        {
                            g.MouseClicked -= this.OnMOuseClicked;
                            this.ClientMove -= g.OnClientMove;

                            g.Dispose();

                        }



                        }
                        else if (dialog == DialogResult.No)
                        {
                        button2_Click(null, new EventArgs());
                        if (gameType == 3)
                        {
                            if (!f.IsDisposed)
                            {
                                f.Dispose();
                            }
                        }
                        else
                        {
                            if (!g.IsDisposed)
                            {
                                g.Dispose();
                            }
                        }
                        }

                    }
                }
                else if (end.Equals("c") || end.Equals("c!!"))
                {
                this.gameRunning = false;
                    this.clientWin++;
                    if (this.clientWin > 0)
                    {
                        MessageBox.Show("you won");

                        winForm.pictureBox1.Image = Properties.Resources.winner;
                        winForm.ShowDialog();
                        DialogResult dialog = MessageBox.Show("New Game", "exit", MessageBoxButtons.YesNo);

                        if (dialog == DialogResult.Yes)
                        {
                            if (gameType == 3)
                            {
                                this.serverWin = 0;
                                this.clientWin = 0;
                                f.MouseClicked -= this.OnMOuseClicked;
                                this.ClientMove -= f.OnClientMove;
                                f.Dispose();
                            }
                            else
                            {
                                this.serverWin = 0;
                                this.clientWin = 0;
                                g.MouseClicked -= this.OnMOuseClicked;
                                this.ClientMove -= g.OnClientMove;
                                g.Dispose();
                            }
                       
                        }
                        else if (dialog == DialogResult.No)
                        {
                        button2_Click(null, new EventArgs());

                        if (gameType == 3)
                        {
                            if (!f.IsDisposed)
                            {
                                f.Dispose();
                            }
                        }
                        else
                        {
                            if (!g.IsDisposed)
                            {
                                g.Dispose();
                            }
                        }
                           
                        }


                    }


                    else
                    {

                        MessageBox.Show("client won");
                        DialogResult dialog = MessageBox.Show("Continue", "exit", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            if (gameType == 3)
                            {
                                f.MouseClicked -= this.OnMOuseClicked;
                                this.ClientMove -= f.OnClientMove;

                                f.Dispose();
                            }
                            else
                            {
                                g.MouseClicked -= this.OnMOuseClicked;
                                this.ClientMove -= g.OnClientMove;

                                g.Dispose();
                            }

                        }
                        else if(dialog==DialogResult.No)
                            {
                        button2_Click(null, new EventArgs());
                        if (gameType == 3)
                        {
                            if (!f.IsDisposed)
                            {
                                f.Dispose();
                            }
                        }
                        else
                        {
                            if (!g.IsDisposed)
                            {
                                g.Dispose();
                            }
                        }
                           
                        }

                        
                    }
                }
                else if (end.Equals("t") || end.Equals("t!!"))
                {
                    MessageBox.Show("Game draw");
                    DialogResult dialog = MessageBox.Show("Continue", "exit", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {

                        if (gameType == 3)
                        {
                            f.MouseClicked -= this.OnMOuseClicked;
                            this.ClientMove -= f.OnClientMove;

                            f.Dispose();
                        }
                        else
                        {
                            g.MouseClicked -= this.OnMOuseClicked;
                            this.ClientMove -= g.OnClientMove;

                            g.Dispose();

                        }



                    }
                    else if (dialog == DialogResult.No)
                    {
                    button2_Click(null, new EventArgs());
                    if (gameType == 3)
                    {
                        if (!f.IsDisposed)
                        {
                            f.Dispose();
                        }
                    }
                    else
                    {
                        if (!g.IsDisposed)
                        {
                            g.Dispose();
                        }
                    }
                       
                    }
                }
                textBox1.Invoke((MethodInvoker)delegate ()
                {
                //da ne postavlja pitcureboxove u listview
                string m1 = m.MessageString.Substring(0, m.MessageString.Length - 1);
                    if ((!m1.Equals("pictureBox1")) && (!m1.Equals("pictureBox2")) && (!m1.Equals("pictureBox3")) && (!m1.Equals("pictureBox4")))
                    {
                        if (m1.Length > 800)
                        {
                            listView1.Items.Add("game started" + "\n");
                        }
                        else
                        {
                            listView1.Items.Add(m.MessageString + "\n");
                        }
                    }
                    potez = m.MessageString;

                });
                start = m.MessageString;

                if (start.Length > 800)
                {


                    BeginInvoke((MethodInvoker)delegate
                        {



                            string s = start.Substring(0, start.Length - 1);
                        //bez ovoga ne uspije ucitat cili string 
                        try
                            {
                                if (gameType == 3)
                                {
                                    f = JsonConvert.DeserializeObject<Form1>(s);
                                }
                                else
                                {
                                    g = JsonConvert.DeserializeObject<GameFour>(s);

                                }

                                Thread.Sleep(1);
                            }

                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message.ToString());
                            }


                            if (gameType == 3)
                            {
                                var p1 = f.p1;
                                var p2 = f.p2;
                                f.p1 = p2;
                                f.p2 = p1;

                                f.Text = "client";
                                f.MouseClicked += this.OnMOuseClicked;
                                this.ClientMove += f.OnClientMove;
                                this.gameRunning = true;
                                f.FormClosing += this.Game_Form_Closing;

                                f.Show();
                            }
                            else
                            {
                                var p1 = g.p1;
                                var p2 = g.p2;
                                g.p1 = p2;
                                g.p2 = p1;


                                g.Text = "client";
                                g.MouseClicked += this.OnMOuseClicked;
                                this.ClientMove += g.OnClientMove;
                                this.gameRunning = true;
                                g.FormClosing += this.Game_Form_Closing;

                                g.Show();
                            }
                            
                        });



                   

                }




                string mess = potez.Substring(0, potez.Length - 1);

                if ((mess.Equals("pictureBox1")) || (mess.Equals("pictureBox2")) || (mess.Equals("pictureBox3")) || mess.Equals("pictureBox4"))
                {
                    ClientMove(mess);

                }

         
        }


        public void OnMOuseClicked(string s)
        {
            
            client.WriteLine(s);

        }

        //potez event
        public void OnClientMove(string s)
        {
            if (ClientMove != null)
            {
                ClientMove(s);
            }
        }
        //client send
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                client.WriteLine(logedUser+": " + textBox1.Text);
                textBox1.Text = "";
            }

        }

      
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Ready_Click(object sender, EventArgs e)
        {
           
            client.WriteLine("ready");
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            // this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gameType == 3)
            {
                if (f == null)
                {

                    client.Disconnect();
                    this.Close();
                }
                else if (!f.IsDisposed)
                {

                    f.Close();
                    client.Disconnect();
                    this.Close();
                }
                else
                {
                    client.Disconnect();
                    this.Close();
                }
            }
            else
            {
                if (g == null)
                {

                    client.Disconnect();
                    this.Close();
                }
                else if (!g.IsDisposed)
                {

                    g.Close();
                    client.Disconnect();
                    this.Close();
                }
                else
                {
                    client.Disconnect();
                    this.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
        
        }

        private void Client_Load_1(object sender, EventArgs e)
        {

        }
    }

}
