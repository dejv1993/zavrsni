using MetroFramework.Forms;
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
    public partial class GameType : MetroForm
    {
       

        public GameType()
        {

            InitializeComponent();
        }

    

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
               ServerClient.playerNumber = 2;
            }
        }

      


        private void GameType_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();

        }

     

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                ServerClient.gameType = 3;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                ServerClient.gameType = 4;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


      
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            ServerClient.playerNumber = 4;

        }

        private void GameType_Load_1(object sender, EventArgs e)
        {
            this.radioButton1.Select();
            this.radioButton3.Select();
           if( int.TryParse((string)radioButton3.Tag,out int radio1))
            {
                ServerClient.gameType = radio1;

            }
            if (int.TryParse((string)radioButton1.Tag, out int radio2))
            {
                ServerClient.playerNumber = radio2;

            }


    

        }
    }
}
