using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Net.NetworkInformation;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using SimpleTCP;
namespace Seminar
{
    public partial class Server : Form
    {
        //form close posiva formclosing event
        public delegate void PlayersNumberDecrease(object sender, EventArgs e);
        public event PlayersNumberDecrease NumberDecrease;
        public delegate void PlayersNumberIncrase(object sender, EventArgs e);
        public event PlayersNumberIncrase NumberIncrease;
        public delegate void MoveMadeEventHandler(string s);
        public event MoveMadeEventHandler MoveMade;

        public delegate void OnServerNewGame(string s);
        public event OnServerNewGame NewServerGame;
        public static SimpleTcpServer tcpServer;
        public  TcpClient tcpClient;



        public int port;
        public String logedUser;
        public IPAddress address;
        public StreamReader reader;
        private Button Send;
        private ListView listView1;
        private TextBox textBox1;
        private Button StartGame;
        public StreamWriter writer;
        public int win = 0;
        public int lost = 0;
        public int draw = 0;
        public string poruka = "";
        public string potez = "";
        public string json;
        public int gameType=3;
        public int usercnt = 0;
        public string clientUsername = "";
        private Label label1;
        private ListView listView2;
        private Label label2;
        public WinnerForm winForm;
        private Button button1;
        public int playerCount=1;
        public bool Game_Running = false;
        public int GameId;
        public bool local = false;
        public int maxPlayers;
        Form1 f;
        GameFour g;
        public Server()
        {
            InitializeComponent();
            tcpServer = new SimpleTcpServer();
            tcpServer.Delimiter = 0x13;
            tcpServer.StringEncoder = Encoding.ASCII;
            tcpServer.DataReceived += Server_Recived;
        
            //klijen ulazi u server 
            tcpServer.ClientConnected += Client_Connected;
            //klijent napusta server
            tcpServer.ClientDisconnected += Client_Disconnect;
            if (gameType == 3)
            {
                f = new Form1();
                f.Text = "server";
            }
            if(gameType==3)
            {
                g = new GameFour();
                g.Text = "server";
            }
            winForm = new WinnerForm();
            


        }

        //change number of players  in db
        public void OnNumberDecrease(object sender, EventArgs e)
        {
            if (NumberDecrease != null)
            {
                NumberDecrease(this, EventArgs.Empty);
            }
        }
        public void OnNumberIncrease(object sender, EventArgs e)

        {
            if(NumberIncrease!=null)
            {
                NumberIncrease(this, EventArgs.Empty);
            }
        }



        public void NewGame(string s)
        {
            if (s.Equals("e"))
            {
                this.win++;

                MessageBox.Show("you won with");

            }
            else if (s.Equals("c"))
            {
                this.lost++;

                MessageBox.Show("you lost");


            }
            else if (s.Equals("t"))
            {
                MessageBox.Show("Draw");

            }

            if (win > 0)

            {
                this.Game_Running = false;

                using (PlayersEntities1 player = new PlayersEntities1())
                {

                    DateTime foo = DateTime.UtcNow;
                    long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
                    Player p = player.Players.FirstOrDefault(r => r.Username==logedUser);
                    Player p2 = player.Players.FirstOrDefault(r => r.Username ==clientUsername);
                    Game game = player.Games.Find(this.GameId);
                    game.WinnerId = p.Id;
                    if (game.Player_Game.HasValue)
                    {
                        maxPlayers =game.Player_Game.Value;
                    }
                    game.Status = "Closed";
     //ovo je nepotrebno jer vec postoji jos treba testirat               player.Games.Add(game);
                    p.Games.Add(game);
                    p2.Games.Add(game);
                    player.SaveChanges();
                }


                    winForm.pictureBox1.Image = Properties.Resources.winner;

                winForm.ShowDialog();
                DialogResult dialog = MessageBox.Show("New Game", "Exit", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {

                        this.win = 0;
                        this.lost = 0;
                    

                    OnFormUnsub("Form");
                    using (PlayersEntities1 p = new PlayersEntities1())
                    {
                        playerCount+=tcpServer.ConnectedClientsCount;
                        Game game = new Game { Status="Open",Players_In_Game=playerCount,Player_Game=maxPlayers, Game_Type = gameType };
                      
                        p.Games.Add(game);

                        Player player = p.Players.FirstOrDefault(r => r.Username == logedUser);
                        player.Games.Add(game);
                        p.SaveChanges();
                        this.GameId = game.Id;
                    }

                    MessageBox.Show(this.GameId.ToString());
                }
                else  if(dialog==DialogResult.No)
                {
                    OnFormUnsub("Form");
                  
                        tcpServer.BroadcastLine("kicked");
                        Client_Disconnect(null, tcpClient);
                    
                    //  this.OnFormClosing(new FormClosingEventArgs(CloseReason.UserClosing, false));

                }

            }

            else if (lost > 1)
            {
                this.Game_Running = false;

                using (PlayersEntities1 player = new PlayersEntities1())
                {
                    Player p1 = player.Players.FirstOrDefault(r => r.Username == logedUser);
                    Player p2 = player.Players.FirstOrDefault(r => r.Username == clientUsername);
                    Game game = player.Games.Find(this.GameId);
                    game.WinnerId = p2.Id;
                    if (game.Player_Game.HasValue)
                    {
                        maxPlayers = game.Player_Game.Value;
                    }

                    game.Status = "Closed";
                  //visak testirat  player.Games.Add(game);
                    p1.Games.Add(game);
                    p2.Games.Add(game);
                    player.SaveChanges();
                }



                winForm.pictureBox1.Image = Properties.Resources.lost;
                winForm.ShowDialog();
                DialogResult dialog = MessageBox.Show("New Game", "exit", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    this.win = 0;
                    this.lost = 0;
                    OnFormUnsub("Form");
                    using (PlayersEntities1 p = new PlayersEntities1())
                    {
                        playerCount += tcpServer.ConnectedClientsCount;
                        Game game = new Game { Status = "Open", Players_In_Game = playerCount, Player_Game = maxPlayers, Game_Type = gameType };

                        p.Games.Add(game);

                        Player player = p.Players.FirstOrDefault(r => r.Username == logedUser);
                        player.Games.Add(game);
                        p.SaveChanges();
                        this.GameId = game.Id;
                    }

                }
                else if (dialog == DialogResult.No)
                {
                    OnFormUnsub("Form");

                    tcpServer.BroadcastLine("kicked");
                    Client_Disconnect(null, tcpClient);


                }
            }

            else
            {
                DialogResult dialog = MessageBox.Show("Continue", "exit", MessageBoxButtons.YesNo);


              
                if (dialog == DialogResult.Yes)
                {

                    OnFormUnsub("Form");


                }
                //need to add lost for server couse he left
                if (dialog==DialogResult.No)
                {
                    OnFormUnsub("Form");
                    tcpServer.BroadcastLine("kicked");
                    Client_Disconnect(null, tcpClient);
                }
                
            }
        }

        // subscribe to form1 closing
        public void OnFormClosing()
        {
            if(this.Game_Running)
            {
                MessageBox.Show("server left so he lost the game");

                //server left the game so he lost the game
                using (PlayersEntities1 player = new PlayersEntities1())
                {
                    Player p1 = player.Players.FirstOrDefault(r => r.Username == logedUser);
                    Player p2 = player.Players.FirstOrDefault(r => r.Username == clientUsername);
                    Game game = player.Games.Find(this.GameId);
                    game.WinnerId = p2.Id;
                   
                    game.Status = "Closed";
                   //visak testirat player.Games.Add(game);
                    p1.Games.Add(game);
                    p2.Games.Add(game);
                    player.SaveChanges();
                }
                this.Game_Running = false;
                button1_Click(this, EventArgs.Empty);
            }
            else
            {
                OnFormUnsub("f");
            }
        }
       public void Client_Disconnect(object sender,TcpClient e)
        {
            if(tcpClient==e)
            {
                if (listView2.Items.Count > 0)
                {
                    StartGame.Enabled=false;
                    listView2.Items.RemoveAt(0);

                    usercnt--;
                    OnNumberDecrease(this, null);
                    tcpClient.Close();
                    tcpClient = null;
                }
                //player left while game is running so he lost the game
                if(Game_Running)
                {
                    using (PlayersEntities1 players = new PlayersEntities1())
                    {
                        Game game = players.Games.Find(this.GameId);
                       

                        Player player1 = players.Players.FirstOrDefault(r => r.Username == this.logedUser);
                        Player player2 = players.Players.FirstOrDefault(r => r.Username == this.clientUsername);
                        game.WinnerId = player1.Id;
                        game.Status = "Closed";
                   //visak testirat     players.Games.Add(game);
                        player1.Games.Add(game);
                        player2.Games.Add(game);
                        players.SaveChanges();
                    }
                    this.Game_Running = false;
                    OnFormUnsub("Form");

                }
                else
                {
                    
                }
                MessageBox.Show(this.clientUsername + " left the server");

                
            }
        }

       public void Client_Connected(object sender,TcpClient e)
        {
                if(tcpClient==null)
            {

                tcpClient = e;
                
            }

        }



        public void Server_Recived(object sender, SimpleTCP.Message e)
        {
            
            if(usercnt==0)
            {
                
                    clientUsername = e.MessageString.Substring(0, e.MessageString.Length - 1);
                
                if (listView2.InvokeRequired)
                {
                    listView2.Invoke((MethodInvoker)delegate ()
                    {
                        listView2.Items.Add(clientUsername);
                    });
                    }

                if (gameType == 3)
                {
                    f.ClientName = clientUsername;

                    f.logedUser = logedUser;
                }
                else
                {
                    g.ClientName = clientUsername;

                    g.logedUser = logedUser;
                }
                usercnt = tcpServer.ConnectedClientsCount;

                OnNumberIncrease(this, null);

                
            }
         
            textBox1.Invoke((MethodInvoker)delegate ()
            {
                
                
                string m1 = e.MessageString.Substring(0, e.MessageString.Length - 1);
                if ((!m1.Equals("pictureBox1")) && (!m1.Equals("pictureBox2")) && (!m1.Equals("pictureBox3")) && (!m1.Equals("pictureBox4")) && (!m1.Equals(clientUsername)) && (!m1.Equals("exiting")) && (!m1.Equals("NewGame")) && (!m1.Equals("ready")))
                {
                    
                        listView1.Items.Add(e.MessageString + "\n");
                                        

                }
                //poruka je ready button
                poruka = e.MessageString.Substring(0, e.MessageString.Length - 1);
                if(poruka.Equals("ready"))
                {
                    StartGame.Enabled = true;
                }
                potez = e.MessageString;
            });
           
             string m = potez.Substring(0, potez.Length - 1);
            if ((m.Equals("pictureBox1")) || (m.Equals("pictureBox2")) || (m.Equals("pictureBox3")) || (m.Equals("pictureBox4")))
            {
                OnMoveMade(m);

            }
           


        }
        //event za potez
        public  void OnMoveMade(string s)
        {
            if(MoveMade!=null)
            {
              
               MoveMade(s);

            }
        }

        public void OnNewServerGame(string s)
        {
            if(NewServerGame!=null)
            {

            }
        }

        public void OnGameEnded(string s)
        {
            
                tcpServer.BroadcastLine(s);
             
        
            NewGame(s);
        }


        public void Start(string Ip,int port)
        {
            IPAddress.TryParse(Ip, out address);
            try
            {

                tcpServer.Start(address, port);


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToLower());
                this.Dispose();
            }

        }
        //localhost
        public void Local_Start()
        {
            IPAddress.TryParse("127.0.0.1", out address);
            try
            {

                tcpServer.Start(2000);
                local = true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToLower());
            }

        }


        public void OnFormUnsub(string s)
        {
            if (gameType == 3)
            {
                this.MoveMade -= f.OnMoveMade;
                f.GameEnded -= this.OnGameEnded;
                f.MouseClicked -= this.OnMOuseClicked;
                f.FormUnsub -= this.OnFormUnsub;
              
                f.FormExiting -= this.OnFormClosing;
                f.Close();
            }
            else
            {
                this.MoveMade -= g.OnMoveMade;
                g.GameEnded -= this.OnGameEnded;
                g.MouseClicked -= this.OnMOuseClicked;
                g.FormUnsub -= this.OnFormUnsub;
                g.FormExiting -= this.OnFormClosing;
                g.Close();

            }
        }

        private void StartGame_Click(object sender, EventArgs e)
        {

            if (gameType == 3)
            {
                f.logedUser = this.logedUser;


               

                    this.Game_Running = true;

                // za ponovno pokretanje nove partije
                StartGame.Enabled = false;
                    if (f.IsDisposed)
                    {
                        f = new Form1();
                        f.Text = "f1";

                        f.p1.pobjede = win;

                        f.p2.pobjede = lost;
                        f.ClientName = clientUsername;
                        f.logedUser = logedUser;
                        json = JsonConvert.SerializeObject(f);


                        tcpServer.BroadcastLine(json);
                        //server subscribe
                        f.MouseClicked += this.OnMOuseClicked;

                        //form sub
                        f.ClientName = this.clientUsername;

                        f.GameEnded += this.OnGameEnded;

                        this.MoveMade += f.OnMoveMade;
                        f.FormExiting += this.OnFormClosing;
                    tcpServer.DataReceived += f.GameRecived;
                        f.FormUnsub -= this.OnFormUnsub;


                        f.Show();
                    }

                    else
                    {
                        json = JsonConvert.SerializeObject(f);




                        tcpServer.BroadcastLine(json);
                        //pretp
                        f.ClientName = this.clientUsername;

                        f.MouseClicked += this.OnMOuseClicked;
                        //f.onmovemade rfferenca na metodu
                        f.GameEnded += this.OnGameEnded;
                        
                        this.MoveMade += f.OnMoveMade;
                        f.FormUnsub += this.OnFormUnsub;
                        f.FormExiting += this.OnFormClosing;
                    tcpServer.DataReceived += f.GameRecived;

                    f.Show();
                    }
                  
                    

            }
            else
            {
                g.logedUser = this.logedUser;




              

                

                    this.Game_Running = true;
                StartGame.Enabled = false;
                    // za ponovno pokretanje nove partije

                    if (g.IsDisposed)
                    {
                        g = new GameFour();
                        g.Text = "gamefour";

                        g.p1.pobjede = win;

                        g.p2.pobjede = lost;
                        g.ClientName = clientUsername;
                        g.logedUser = logedUser;
                        json = JsonConvert.SerializeObject(g);


                        tcpServer.BroadcastLine(json);
                        //server subscribe
                        g.MouseClicked += this.OnMOuseClicked;

                        //form sub
                        g.ClientName = this.clientUsername;

                        g.GameEnded += this.OnGameEnded;
                    tcpServer.DataReceived += g.GameRecived;
                    this.MoveMade += g.OnMoveMade;
                        g.FormExiting += this.OnFormClosing;

                        g.FormUnsub -= this.OnFormUnsub;


                        g.Show();
                    }

                    else
                    {
                        json = JsonConvert.SerializeObject(g);




                        tcpServer.BroadcastLine(json);
                        //pretp
                        g.ClientName = this.clientUsername;

                        g.MouseClicked += this.OnMOuseClicked;
                        //f.onmovemade rfferenca na metodu
                        g.GameEnded += this.OnGameEnded;
                    tcpServer.DataReceived += g.GameRecived;
                    this.MoveMade += g.OnMoveMade;
                        g.FormUnsub += this.OnFormUnsub;
                        g.FormExiting += this.OnFormClosing;

                        g.Show();
                    }


                    MessageBox.Show(this.Game_Running.ToString());

                }
            
        }

        //event handler
        public void OnMOuseClicked(string s)
        {

            try {
                tcpServer.BroadcastLine(s);
                Thread.Sleep(100);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

        }
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpServer.IsStarted) 
            {
                //server  se gasi te se uklanja igra koja je stvorena prilikom pokretanja servera a nije se odigrala
                using (PlayersEntities1 players = new PlayersEntities1())
                {
                    
                    Player p = players.Players.FirstOrDefault(r => r.Username == this.logedUser);
                    p.IpAddress = "";
                  
                    Game game = players.Games.FirstOrDefault(r => r.Id == this.GameId);
                    if (game.Status.Equals("Open"))
                    {
                        
                        p.Games.Remove(game);
                        players.Games.Remove(game);
                        players.SaveChanges();
                    }
                    tcpServer.Stop();
                    if (gameType == 3)
                    {
                        if (!f.IsDisposed)
                        {
                            OnFormUnsub("f");
                        }
                    }
                    else
                    {
                        if (!g.IsDisposed)
                        {
                            OnFormUnsub("g");
                        }
                    }


                }
            }
        }
     


        private void Send_Click(object sender, EventArgs e)
        {
            int clientsConnected = tcpServer.ConnectedClientsCount;
            if (clientsConnected > 0)
            {


                if (textBox1.Text != "")
                {
                    tcpServer.BroadcastLine("server:"+textBox1.Text);
                    textBox1.Text = "";
                }
                textBox1.Text = "";
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.Send = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.StartGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(27, 244);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 0;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(382, 70);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(223, 140);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 244);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(611, 52);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // StartGame
            // 
            this.StartGame.Enabled = false;
            this.StartGame.Location = new System.Drawing.Point(255, 70);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(121, 23);
            this.StartGame.TabIndex = 3;
            this.StartGame.Text = "StartGame";
            this.StartGame.UseVisualStyleBackColor = true;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(379, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ServerNotifications";
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(676, 70);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(95, 140);
            this.listView2.TabIndex = 5;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(676, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Players in server";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(60, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Server
            // 
            this.BackgroundImage = global::Seminar.Properties.Resources.Server;
            this.ClientSize = new System.Drawing.Size(815, 310);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartGame);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Send);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Server";
            this.Text = "SERVER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tcpServer.BroadcastLine("kicked");
            Client_Disconnect(null,tcpClient);

        }

        private void Server_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
