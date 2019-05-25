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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (PlayersEntities1 player = new PlayersEntities1())
            {
                Player p = player.Players.FirstOrDefault(r => r.Username == textBox1.Text);
                if(p!=null)
                {

                    if(p.Password.Equals(textBox2.Text))
                    {
                        if (!p.Status.Equals("Online"))
                        {
                            ServerClient s = new ServerClient();

                            p.Status = "Online";
                            player.SaveChanges();

                            s.logedUser = p.Username;

                            s.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("You are already logged in");
                            textBox1.Text = "";
                            textBox2.Text = "";
                        }
                    }

                    else
                    {
                        MessageBox.Show("wrong password");
                    }
                }
                else
                {
                    MessageBox.Show("User dont exist");
                }

            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
    }
}     