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
namespace Seminar
{
  
    public partial class ServerClient : Form
    {

        public delegate void ClientAddedHandler(string s);
        GameType gameOption;
        public  static int playerNumber=2;
        public static int gameType=3;

        public string ServerIP;
        public Server s;
        public Client c;
        public String logedUser;
        ServerList serverLista;
        private int GameId;
        public ServerClient()
        {
            InitializeComponent();
       
           
            serverLista = new ServerList();
      
         
            c = new Client();

        }
        public void OnNumberIncrease(object sender,EventArgs e)
        {
           
            using (PlayersEntities1 players = new PlayersEntities1())
            {
                Game g = players.Games.FirstOrDefault(r => r.Id == this.GameId);
                g.Players_In_Game=2;
           
                players.SaveChanges();
            }
            

        }
        public void OnNumberDecrease(object sender,EventArgs e)
        {
           
            using (PlayersEntities1 players = new PlayersEntities1())

            {
                if (s.Game_Running.Equals(false))
                {
                    Game g = players.Games.FirstOrDefault(r => r.Id == this.GameId);
                    if (g.Status.Equals("Open"))
                    {
                        g.Players_In_Game = 1;

                        players.SaveChanges();
                    }
                }
              
            }
            

        }
        public static int openPort()
        {

            int PortStartIndex = 1000;
            int PortEndIndex = 2000;
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();

            List<int> usedPorts = tcpEndPoints.Select(p => p.Port).ToList<int>();
            int unusedPort = 0;

            for (int port = PortStartIndex; port < PortEndIndex; port++)
            {
                if (!usedPorts.Contains(port))
                {
                    unusedPort = port;
                    break;
                }
            }
            return unusedPort;
        }

        public string getIpv4()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return "";
        }
        public String getIP()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
                return localIP;
            }
        }
        
        private void start_Click(object sender, EventArgs e)
        {
            if(s==null)
            {
                s = new Server();
                s.NumberDecrease += OnNumberDecrease;
                s.NumberIncrease += OnNumberIncrease;
            }
            else if(s.IsDisposed)
            {
                s = new Server();
                s.NumberDecrease += OnNumberDecrease;
                s.NumberIncrease += OnNumberIncrease;
            }
            using (PlayersEntities1 p = new PlayersEntities1())
            {
                Player play = p.Players.FirstOrDefault(r => r.Username==logedUser);
                play.IpAddress = getIpv4();
                Game game = new Game {Status="Open",Players_In_Game=1,Game_Type=gameType,Player_Game=playerNumber};
                play.Games.Add(game);
              
                p.SaveChanges();

                var games = play.Games.ToList();
                foreach(var g in games)
                {
                    if(g.Status!="Closed")
                    {
                        this.GameId = g.Id;
                        s.GameId = g.Id;

                    }
                }
                s.Start(play.IpAddress);
                s.logedUser = play.Username;
                this.Hide();
                s.FormClosing += this.formClosed;
                s.gameType = gameType;
                s.Show();
                
                
            }

        }
        

        //server closing
        public void formClosed(object sender,FormClosingEventArgs e)
        {
            if (s != null)
            {
                s.Dispose();
            }
            this.Show();
           
        }
        

        private void join_Click(object sender, EventArgs e)
        {
            //prosljecuje serverIp adresu join metodi u client
            //c.Join(ServerIP);
            if(serverLista.IsDisposed)
            {
                serverLista = new ServerList();
            }
            serverLista.logedUser = this.logedUser;
            serverLista.FormClosing += this.formClosed;
            serverLista.Show();
            this.Hide();
            
           

        }

        private void ServerClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (PlayersEntities1 player = new PlayersEntities1())
            {

                Player logoff = player.Players.FirstOrDefault(r=>r.Username==this.logedUser);
                logoff.Status = "Offline";
                player.SaveChanges();

            }
                this.Dispose();
                Application.Exit();
            
           
        }

        private void LocalHost_Click(object sender, EventArgs e)
        {   
            if(s.IsDisposed)
            {
                s = new Server();
            }
                
                s.Local_Start();
            s.local = true;

            s.FormClosing += this.formClosed;
            this.Hide();
                s.ShowDialog();
            
        }

        private void Join_Local_Click(object sender, EventArgs e)
        {
            if(c.IsDisposed)
            {
                c = new Client();
            }
            c.LocalHost();
            c.FormClosing += this.formClosed;
            c.ShowDialog();
        }

        private void ServerClient_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (PlayersEntities1 players = new PlayersEntities1())
            {
                
                var games = players.Games.ToList();
                var pl = players.Players.ToList();

                for(int i=0;i<games.Count;i++)
                {
                    if(games[i].Id>=4)
                    {
                        for(int j=0;j<pl.Count;j++)
                        {
                            for(int k=0; k<pl[j].Games.Count;k++)
                            {
                                pl[j].Games.Remove(games[i]);
                            }
                        }
                        players.SaveChanges();

                    }
                }
                for(int j=0;j<games.Count;j++)
                {
                    if (games[j].Id > 4)
                    {
                        players.Games.Remove(games[j]);
                    }
                }
               
                players.SaveChanges();
            }

        }

        private void MyStats_Click(object sender, EventArgs e)
        {
            int wins = 0;
            int lost = 0;
            int total = 0;
            decimal percent = 0;
            using (PlayersEntities1 players = new PlayersEntities1())
            {
                Player player = players.Players.FirstOrDefault(r => r.Username ==logedUser);
                
                var games = player.Games.ToList();
                total = games.Count;
                foreach(var g in games)
                {
                    
                    if(g.WinnerId==player.Id)
                    {
                        wins++;
                    }
                    else
                    {
                        lost++;
                    }
                }
            }
            try
            {
                percent = (decimal)wins /total  * 100;
                DialogResult d = MessageBox.Show("User:"+logedUser+"\n"+"Win:" + wins + "\n" + "Lost:"+lost+"\n"+"Total Played:"+total+"\n"+"Percent:"+percent,"Statistic", MessageBoxButtons.OK);
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.StackTrace);
            }
        }

        private void TopPlayers_Click(object sender, EventArgs e)
        {
            int wins = 0;
            using (PlayersEntities1 players = new PlayersEntities1())
            {
                var player = players.Players.ToList();

                foreach (var pl in player)
                {
                    var games = pl.Games.ToList();
                    foreach (var g in games)
                    {
                        if (pl.Id == g.WinnerId)
                        {
                            wins++;
                        }


                    }
                    try
                    {
                        if (games.Count> 0)
                        {
                            pl.Win_Rate= (decimal)(100 * wins/games.Count);
                            wins = 0;
                            
                        }
                        else
                        {
                            pl.Win_Rate = (decimal)0;
                        }

                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(e2.Message);
                    }

                }
                players.SaveChanges();

                var p = players.Players.OrderByDescending(r => r.Win_Rate).OrderByDescending(r => r.Games.Count).Take(3).ToList();
                StringBuilder builder = new StringBuilder();


                foreach(var str in p)
                {
                    builder.Append("User:"+str.Username+"  "+"Win rate:"+str.Win_Rate.ToString()+"%"+" Total Games:"+str.Games.Count.ToString()).AppendLine();
                }


                MessageBox.Show(builder.ToString());
            }
        }

        private void HomePage_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://support.microsoft.com/en-us/help/305703/how-to-start-the-default-internet-browser-programmatically-by-using-vi");
            }
            catch
        (
         System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
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
            Application.Restart();
        }

    public void RadioChanges(object sender,EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gameOption == null)
            {
                gameOption = new GameType();
                gameOption.Show();
              
            }
            else if (gameOption.IsDisposed)
            {
                gameOption = new GameType();
                gameOption.Show();


            }
            else
            {
                gameOption.Show();
              
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
