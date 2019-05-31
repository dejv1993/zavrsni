using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seminar
{
    public partial class PlayerList : Form
    {
        public int player_id=-1;
        public PlayerList()
        {
            InitializeComponent();
        }

        private void PlayerList_Load(object sender, EventArgs e)
        {
            using (PlayersEntities1 players = new PlayersEntities1())
            {
                var player = players.Players.ToList();
                foreach (Player p in player)
                {
                    var role = p.PlayerRoles.FirstOrDefault(r1 => r1.PlayerId == p.Id);
                    if (role.Role.Id == 2)
                    {
                        ListViewItem item1 = new ListViewItem();
                        item1.Tag = p.Id;
                        item1.Text = p.Id.ToString();
                        item1.SubItems.Add(p.Username);
                        listView1.Items.Add(item1);
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        public void refresh_list()
        {
            if(listView1.Items.Count>0)
            {
                listView1.Items.Clear();
            }
            using (PlayersEntities1 players = new PlayersEntities1())
            {
                var player = players.Players.ToList();
                foreach (Player p in player)
                {
                    var role = p.PlayerRoles.FirstOrDefault(r1 => r1.PlayerId == p.Id);
                    if (role.Role.Id == 2)
                    {
                        ListViewItem item1 = new ListViewItem();
                        /*/
                        item1.SubItems.Add(server.Username);
                        item1.SubItems.Add(server.Win_Rate.ToString());

                        listView2.Items.Add(item1);
                        /*/

                        item1.Tag = p.Id;
                        item1.Text = p.Id.ToString();
                        item1.SubItems.Add(p.Username);
                        listView1.Items.Add(item1);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


          
          if(listView1.SelectedItems.Count>0)
            { 
              int selected = listView1.SelectedIndices[0];

                if (int.TryParse(listView1.Items[selected].Text, out int numb))
                {
                    player_id=numb;

                }
                
                using (PlayersEntities1 players = new PlayersEntities1())
                {
                    Player player = players.Players.Find(player_id);

                    var games = player.Games.ToList();
                    var roles = player.PlayerRoles.ToList();
                    foreach (var game in games)
                    {
                        player.Games.Remove(game);

                    }
                    var role = player.PlayerRoles.FirstOrDefault(r1 => r1.PlayerId == player.Id);
                    if (role.Role.Id == 2)
                    {
                        players.PlayerRoles.Remove(role);

                    }


                    players.Players.Remove(player);

                    players.SaveChanges();
                }
                MessageBox.Show("User deleted sucesfull");
            }
            else
            {
                MessageBox.Show("Select user first");
            }
            refresh_list();
          
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int selected = listView1.SelectedIndices[0];
            // player_id= listView1.SelectedItems[selected].Text
            MessageBox.Show(listView1.Items[selected].Text);
        }
    }
}
