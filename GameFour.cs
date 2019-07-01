using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Collections;
using MetroFramework.Forms;

namespace Seminar
{
    
    [JsonObject(MemberSerialization.OptIn)]


    public partial class GameFour : Form
    {
        [JsonProperty]
        public int i = 0;
        public int card_trown = 0;
        public int k1 = 0;
        public int punti = 0;
        public int k2 = 0;
        public static int k_poz = 0;
        public static int max = 39;
        public ArrayList potez1;
        public ArrayList potez2 ;
        private int timer_tick = 0;
        public string slika;
        public ArrayList poz1;
        public ArrayList poz2;
        public String logedUser;
        public String ClientName;
        public JsonSerializer serializer;
        [JsonProperty]
        public Deck d;

        [JsonProperty]
        public Igrac p1;
        [JsonProperty]
        public Igrac p2;
        //karta igraca 1

        [JsonProperty]
        public Cards c1;
        [JsonProperty]
        public Cards c2;
        //karta igraca 2
        [JsonProperty]
        public Cards c3;
        [JsonProperty]
        public Cards c4;
        // za zadnju kartu
        [JsonProperty]
        public Cards c5;

        public bool first_played = true;
        public static string json;

        public GameFour()
        {


            d = new Deck();
            d.shuffle();
           
            
            p1 = new Igrac("player1");
            p1.turn = true;
            poz1 = new ArrayList();
            poz2 = new ArrayList();
            potez1 = new ArrayList();
            potez2 = new ArrayList();
            p2 = new Igrac("player");
            p2.turn = false;
            c1 = new Cards();
            c2 = new Cards();
            c3 = new Cards();
            c4 = new Cards();
            c5 = new Cards();
            for (k_poz = 0; k_poz < 4; k_poz++)
            {
                p1.ruka[k_poz] = (d.getKarte(k_poz));
            }
            int k = 0;
            for (k_poz = 4; k_poz < 8; k_poz++)
            {


                p2.ruka[k] = d.getKarte(k_poz);
                k++;
            }

            InitializeComponent();
            serializer = new JsonSerializer();

        }

        //event kad se izlazi iz forme
        public delegate void FormExitingEventHandler();
        public event FormExitingEventHandler FormExiting;
        public void OnFormExiting()
        {
            if (FormExiting != null)
            {
                FormExiting();
            }
        }


        //kada zavrsi  
        public delegate void GameOverEventHandler(string s);
        public event GameOverEventHandler GameEnded;
        public void OnGameEnded(string s)
        {
            if (GameEnded != null)
            {
                GameEnded(s);

            }
        }
        public delegate void UnsubEventHandler(string s);
        public event UnsubEventHandler FormUnsub;
        public void OnFormUnsub(string s)
        {
            if (FormUnsub != null)
            {
                FormUnsub(s);
            }
        }
        //delegat za pitcurebox1,2,3 klik
        public delegate void MouseClickEventHandler(string s);
        public event MouseClickEventHandler MouseClicked;
        public void OnMOuseClicked(string s)
        {
            if (MouseClicked != null)
            {
                MouseClicked(s);
            }


        }

        public PictureBox GetPicturePlayer_2(string potez)
        {
            if (potez.Equals("pictureBox5"))
            {
                return pictureBox5;
            }
            else if (potez.Equals("pictureBox6"))
            {
                return pictureBox6;
            }
            else if (potez.Equals("pictureBox7"))
            {
                return pictureBox7;
            }
            else if(potez.Equals("pictureBox8"))
            {
                return pictureBox8;
            }
            return null;
        }
        public PictureBox getPictureBoxPlayer_1(string potez)
        {
            if (potez.Equals("pictureBox1"))
            {
                return pictureBox1;
            }
            else if (potez.Equals("pictureBox2"))
            {
                return pictureBox2;
            }
            else if (potez.Equals("pictureBox3"))
            {
                return pictureBox3;
            }
            else if(potez.Equals("pictureBox4"))
            {
                return pictureBox4;
            }
            else
            {
                return null;
            }
        }





        //subscribe za move handlera-client
        //client move
        //event hand
        public void OnMoveMade(string s)
        {
            if (s.Equals("pictureBox1"))
            {


                pictureBox5_Click(this, EventArgs.Empty);

                MessageBox.Show("card thrown");


            }
            if (s.Equals("pictureBox2"))
            {

                pictureBox6_Click(this, EventArgs.Empty);
                MessageBox.Show("card thrown");

            }
            if (s.Equals("pictureBox3"))
            {

                pictureBox7_Click(this, EventArgs.Empty);
                MessageBox.Show("card thrown");
            }
            if(s.Equals("pictureBox4"))
            {
                pictureBox8_Click(this, EventArgs.Empty);
                MessageBox.Show("card thrown");
            }

        }
        // client move
        //event hand
        public void OnClientMove(string s)
        {

            if (s.Equals("pictureBox1"))
            {

                pictureBox5_Click(this, EventArgs.Empty);
                
                MessageBox.Show("card thrown");

            }
            if (s.Equals("pictureBox2"))
            {

                pictureBox6_Click(this, EventArgs.Empty);
                MessageBox.Show("card thrown");
            }
            if (s.Equals("pictureBox3"))
            {

                pictureBox7_Click(this, EventArgs.Empty);
                MessageBox.Show("card thrown");
            }
            if (s.Equals("pictureBox4"))
            {

                pictureBox8_Click(this, EventArgs.Empty);
                MessageBox.Show("card thrown");
            }
        }

        private void GameFour_Load(object sender, EventArgs e)
        {

            label1.Text = p2.name;
            label2.Text = p1.name;
            label10.Text = p2.pobjede.ToString();
            label11.Text = p1.pobjede.ToString();
            //Postavlja slike za prvog igraca
            for (int p = 0; p < 1; p++)
            {

                slika = p1.ruka[i].ToString();
                pictureBox1.Tag = p1.getRuka(i);

                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                i++;
                k1++;
                slika = p1.ruka[i].ToString();
                pictureBox2.Tag = p1.getRuka(i);
                pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                i++;
                k1++;
                slika = p1.ruka[i].ToString();
                pictureBox3.Tag = p1.getRuka(i);
                pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                i++;
                k1++;
                slika = p1.ruka[i].ToString();
                pictureBox4.Tag = p1.getRuka(i);
                pictureBox4.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                i++;
            }
            // za protivnika 
            for (int p = 0; p < 1; p++)
            {
                pictureBox5.Tag = p2.getRuka(k2);
                pictureBox5.Image = Properties.Resources.pozadina;
                i++;
                k2++;
                pictureBox6.Tag = p2.getRuka(k2);
                pictureBox6.Image = Properties.Resources.pozadina;
                i++;
                k2++;
                pictureBox7.Tag = p2.getRuka(k2);
                pictureBox7.Image = Properties.Resources.pozadina;
                i++;
                k2++;
                pictureBox8.Tag = p2.getRuka(k2);
                pictureBox8.Image = Properties.Resources.pozadina;
                i++;
            }


            label3.Text = (max - i).ToString();
            label4.Text = "Opponent TURN";
            label5.Text = "YOUR TURN";

            label7.Text = p1.punti.ToString();
            label8.Text = p2.punti.ToString();
            label8.Visible = false;
            string img = d.getKarte(max).ToString();
            pictureBox12.Image = (Image)Properties.Resources.ResourceManager.GetObject(img);
            pictureBox12.Tag = d.getKarte(max);

            c5 = (Cards)pictureBox12.Tag;
            panel1.Controls.Add(pictureBox1);

            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox3);
            panel2.Controls.Add(pictureBox5);
            panel2.Controls.Add(pictureBox6);
            panel2.Controls.Add(pictureBox7);
            panel2.Controls.Add(pictureBox8);
            if (p1.turn == true)
            {
                label4.Visible = false;
                label5.Visible = true;
                panel1.Enabled = true;
            }
            if (p2.turn == true)
            {
                label4.Visible = true;
                label5.Visible = false;
                panel1.Enabled = true;
            }

        }
        public bool BiggerTwo(int broj,int broj1)
        {
            if(broj>broj1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Bigger(int broj,int broj1,int broj2,int broj3)
        {
            if(broj>broj1)
            {
                  if(broj2>broj3)
                {
                  if(broj>broj2)
                    {
                        return true;
                    }
                  else
                    {
                        return false;
                    }
                }
                  else
                {
                    if(broj>broj3)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (broj2 > broj3)
                {
                    if (broj1 > broj2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (broj1 > broj3)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
                
        }
        public bool VrstaType(string igra,string thrown_card)
        {
            if(igra.Equals(thrown_card))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void GameRules(string igra, bool card1, bool card2, bool card3, bool card4, int broj1, int broj2, int broj3, int broj4)
        {




            if (card1 && card2 && card3 && card4)
            {
                MessageBox.Show("sve od igre");
                if (Bigger(broj1, broj2, broj3, broj4))
                {

                    p1.turn = true;
                    p2.turn = false;
                    label5.Visible = true;
                    label4.Visible = false;
                    p1.punti += Punti(broj1, broj2, broj3, broj4);
                    label7.Text = p1.punti.ToString();


                }
                else
                {
                    p1.turn = false;
                    p2.turn = true;
                    label5.Visible = false;
                    label4.Visible = true;
                    p2.punti += Punti(broj1, broj2, broj3, broj4);
                    label8.Text = p2.punti.ToString();

                }


            }

            //nisu od igre nijedna
            else if (!card1 && !card2 && !card3 && !card4)
            {
                MessageBox.Show("nisu od igre nijedna");
                //sve su iste vrste ali nisu od igre

                
                if (c1.v == c2.v && c2.v==c3.v && c2.v==c4.v)
                {
                    MessageBox.Show("1))2 i 2==3 i 2==4");

                    if (Bigger(broj1, broj2, broj3, broj4))
                    {
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.turn = true;
                        p2.turn = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                    else
                    {

                        label5.Visible = false;
                        label4.Visible = true;
                        p2.turn = true;
                        p1.turn = false;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }
                }
                //ako nisu  sve iste
               
                else
                {
                    //ako je prva ista kao 3 ili kao 4

                    if (first_played)
                    {
                        if (c1.v == c2.v && (c1.v == c3.v || c1.v == c4.v))
                        {
                            MessageBox.Show("1==2,1==3ili1==4");
                            if (c1.v.Equals(c3.v))
                            {
                                if (BiggerTwo(broj1, broj2))
                                {
                                    //pl1win
                                    if (BiggerTwo(broj1, broj3))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.turn = true;
                                        p1.turn = false;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                }
                                else
                                {
                                    //pl1 win
                                    if (BiggerTwo(broj2, broj3))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.turn = true;
                                        p1.turn = false;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                }

                            }
                            else if (c1.v.Equals(c4))
                            {
                                if (BiggerTwo(broj1, broj2))
                                {
                                    //pl1win
                                    if (BiggerTwo(broj1, broj4))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.turn = true;
                                        p1.turn = false;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                }
                                else
                                {
                                    //pl1 win
                                    if (BiggerTwo(broj2, broj4))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.turn = true;
                                        p1.turn = false;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                }

                            }
                        }



                        else if ((c1.v == c2.v && (c1.v != c3.v && c1.v != c4.v)))
                        {
                            MessageBox.Show("1==2 i 1!=3 i 1!=4");
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }

                        else if ((c1.v != c2.v) && ((c1.v == c3.v) || c1.v == c4.v))
                        {
                            MessageBox.Show("1!=2 i 1==3 ili 1==4");
                            if (c1.v == c3.v && c1.v == c4.v)
                            {
                                if (BiggerTwo(broj3, broj4))
                                {
                                    //pl1
                                    if (BiggerTwo(broj1, broj3))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                }
                                else
                                {
                                    //pl1
                                    if (BiggerTwo(broj1, broj4))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();

                                    }
                                }
                            }
                            else if (c1.v == c3.v)
                            {
                                //pl1
                                if (BiggerTwo(broj1, broj3))
                                {
                                    p1.turn = true;
                                    p2.turn = false;
                                    label5.Visible = true;
                                    label4.Visible = false;
                                    p1.punti += Punti(broj1, broj2, broj3, broj4);
                                    label7.Text = p1.punti.ToString();
                                }
                                else
                                {
                                    p1.turn = false;
                                    p2.turn = true;
                                    label5.Visible = false;
                                    label4.Visible = true;
                                    p2.punti += Punti(broj1, broj2, broj3, broj4);
                                    label8.Text = p2.punti.ToString();
                                }
                            }
                            else if(c1.v==c4.v)
                            {
                                //pl1
                                if (BiggerTwo(broj1, broj4))

                                {
                                    p1.turn = true;
                                    p2.turn = false;
                                    label5.Visible = true;
                                    label4.Visible = false;
                                    p1.punti += Punti(broj1, broj2, broj3, broj4);
                                    label7.Text = p1.punti.ToString();
                                }
                                else
                                {
                                    p1.turn = false;
                                    p2.turn = true;
                                    label5.Visible = false;
                                    label4.Visible = true;
                                    p2.punti += Punti(broj1, broj2, broj3, broj4);
                                    label8.Text = p2.punti.ToString();
                                }
                            }

                        }
                        else if (c1.v != c2.v && c1.v != c3.v && c1.v != c4.v)
                        {
                            MessageBox.Show("1 razlicit od svih");
                            //pl1
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();

                        }
                        else
                        {
                            if(c1.v==c2.v)
                            {
                                if(BiggerTwo(broj1,broj2))
                                {
                                    if (c1.v == c3.v)
                                    {
                                        if (BiggerTwo(broj1, broj3))
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }

                                    }
                                    if(c1.v==c4.v)
                                    {
                                        if (BiggerTwo(broj1, broj4))
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                    }

                                }
                                else
                                {
                                    if (c1.v == c3.v)
                                    {
                                        if (BiggerTwo(broj2, broj3))
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }

                                    }
                                    if (c1.v == c4.v)
                                    {
                                        if (BiggerTwo(broj2, broj4))
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if(c1.v==c3.v)
                                {
                                    if (BiggerTwo(broj1, broj3))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }

                                }
                                if(c1.v==c4.v)
                                {
                                    if(BiggerTwo(broj1,broj4))
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                }
                                if(c1.v==c4.v && c1.v==c3.v)
                                {
                                   if(BiggerTwo(broj4,broj3))
                                    {
                                        if(BiggerTwo(broj1,broj4))
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                    }
                                   else
                                    {
                                        if (BiggerTwo(broj1, broj3))
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    { 
                     if (c3.v != c4.v && (c3.v == c1.v || c3.v == c2.v))
                        {
                            MessageBox.Show("Test1");
                            if (c3.v == c1.v && c3.v == c2.v)
                            {
                                if (BiggerTwo(broj1, broj2))
                                {
                                    //pl2
                                    if (BiggerTwo(broj3, broj1))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                }
                                else
                                {
                                    if (BiggerTwo(broj3, broj2))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    //pl1
                                    else
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                }

                            }
                            else if (c3.v == c2.v)
                            {
                                if (BiggerTwo(broj3, broj2))
                                {
                                    p1.turn = false;
                                    p2.turn = true;
                                    label5.Visible = false;
                                    label4.Visible = true;
                                    p2.punti += Punti(broj1, broj2, broj3, broj4);
                                    label8.Text = p2.punti.ToString();
                                }
                                //pl1
                                else
                                {
                                    p1.turn = true;
                                    p2.turn = false;
                                    label5.Visible = true;
                                    label4.Visible = false;
                                    p1.punti += Punti(broj1, broj2, broj3, broj4);
                                    label7.Text = p1.punti.ToString();

                                }
                            }
                            else if (c3.v == c1.v)
                            {
                                if (BiggerTwo(broj3, broj1))
                                {
                                    p1.turn = false;
                                    p2.turn = true;
                                    label5.Visible = false;
                                    label4.Visible = true;
                                    p2.punti += Punti(broj1, broj2, broj3, broj4);
                                    label8.Text = p2.punti.ToString();
                                }
                                //pl1
                                else
                                {
                                    p1.turn = true;
                                    p2.turn = false;
                                    label5.Visible = true;
                                    label4.Visible = false;
                                    p1.punti += Punti(broj1, broj2, broj3, broj4);
                                    label7.Text = p1.punti.ToString();
                                }
                            }


                        }
                        else if (c3.v == c4.v && (c3.v != c1.v && c3.v != c2.v))
                        {
                            MessageBox.Show("Test2");
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();
                        }
                        else if (c3.v == c4.v && (c3.v == c2.v || c3.v == c1.v))
                        {
                            MessageBox.Show("Test3");
                            if (c3.v == c2.v)
                            {
                                if (BiggerTwo(broj3, broj4))
                                {
                                    //pl2
                                    if (BiggerTwo(broj3, broj2))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                }
                                else
                                {//pl2
                                    if (BiggerTwo(broj4, broj2))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                }
                            }
                            else if (c3.v == c1.v)
                            {
                                if (BiggerTwo(broj3, broj4))
                                {
                                    //pl2
                                    if (BiggerTwo(broj3, broj1))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                }
                                else
                                {//pl2
                                    if (BiggerTwo(broj4, broj1))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    else
                                    {
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                }
                            }

                        }
                        else if ((c3.v != c2.v) && (c3.v != c4.v) && (c3.v != c1.v))
                        {
                            MessageBox.Show("Test4");
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();

                        }
                        else
                        {
                            if (c3.v == c4.v)
                            {
                                if (BiggerTwo(broj3, broj4))
                                {
                                    if (c3.v == c1.v)
                                    {
                                        if (BiggerTwo(broj3, broj1))
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                           
                                        }

                                    }
                                    if (c3.v == c2.v)
                                    {
                                        if (BiggerTwo(broj3, broj2))
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                        else
                                        {
                                            
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                    }

                                }
                                else
                                {
                                    if (c1.v == c3.v)
                                    {
                                        if (BiggerTwo(broj4, broj1))
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                        else
                                        {
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }

                                    }
                                   else if (c4.v == c1.v)
                                    {
                                        if (BiggerTwo(broj4, broj1))
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                        else
                                        {
                                            
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (c3.v == c1.v)
                                {
                                    if (BiggerTwo(broj3, broj1))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    else
                                    {
                                        
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }

                                }
                               else  if (c1.v == c4.v)
                                {
                                    if (BiggerTwo(broj4, broj1))
                                    {
                                        p1.turn = false;
                                        p2.turn = true;
                                        label5.Visible = false;
                                        label4.Visible = true;
                                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                                        label8.Text = p2.punti.ToString();
                                    }
                                    else
                                    {
                                        
                                        p1.turn = true;
                                        p2.turn = false;
                                        label5.Visible = true;
                                        label4.Visible = false;
                                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                                        label7.Text = p1.punti.ToString();
                                    }
                                }
                                if (c3.v == c1.v && c3.v == c2.v)
                                {
                                    if (BiggerTwo(broj1, broj2))
                                    {
                                        if (BiggerTwo(broj4, broj1))
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                        else
                                        {
                                            
                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                    }
                                    else
                                    {
                                        if (BiggerTwo(broj3, broj1))
                                        {
                                            p1.turn = false;
                                            p2.turn = true;
                                            label5.Visible = false;
                                            label4.Visible = true;
                                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                                            label8.Text = p2.punti.ToString();
                                        }
                                        else
                                        {
                                           

                                            p1.turn = true;
                                            p2.turn = false;
                                            label5.Visible = true;
                                            label4.Visible = false;
                                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                                            label7.Text = p1.punti.ToString();
                                        }
                                    }
                                }

                            }

                        }
                    }
                    }
                    
                
            }

            else if (card1 && !card2 && (card3 || card4))
            {

                if (card3 && card4)
                {
                    if (BiggerTwo(broj3, broj4))
                    {
                        //pl2
                        if (BiggerTwo(broj3, broj1))
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();


                        }
                        else
                        {
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }
                    }
                    else
                    {
                        if (BiggerTwo(broj4, broj1))
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();
                        }
                        else
                        {

                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }

                    }
                }
                else if (card3)
                {
                    //pl2
                    if (BiggerTwo(broj3, broj1))
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }
                    else
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                }
                else if (card4)
                {
                    //pl2
                    if (BiggerTwo(broj4, broj1))
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }
                    else
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }

                }
            }
            else if ((!card1 && card2) && (card3 || card4))
            {

                if (card3 && card4)
                {
                    if (BiggerTwo(broj3, broj4))
                    {
                        if (BiggerTwo(broj3, broj2))
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();


                        }
                        else
                        {
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }
                    }
                    else
                    {
                        if (BiggerTwo(broj4, broj2))
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();
                        }
                        else
                        {

                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }

                    }
                }
                else if (card3)
                {
                    //pl2
                    if (BiggerTwo(broj3, broj2))
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }
                    else
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                }
                else if (card4)
                {
                    //pl2
                    if (BiggerTwo(broj4, broj2))
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }
                    else
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                }
            }
            else if ((card1 || card2) && (card3 && !card4))
            {

                if (card1 && card2)
                {
                    if (BiggerTwo(broj1, broj2))
                    {
                        if (BiggerTwo(broj1, broj3))
                        {
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }
                        else
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();
                        }
                    }
                    else
                    {
                        if (BiggerTwo(broj2, broj3))
                        {
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }
                        else
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();
                        }

                    }
                }
                else if (card1)
                {
                    //pl1
                    if (BiggerTwo(broj1, broj3))
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                    else
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }
                }
                else
                {
                    if (BiggerTwo(broj2, broj3))
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                    else
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }

                }
            }
            else if ((card1 || card2) && (!card3 && card4))
            {

                if (card1 && card2)
                {
                    if (BiggerTwo(broj1, broj2))
                    {
                        if (BiggerTwo(broj1, broj4))
                        {
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }
                        else
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();
                        }
                    }
                    else
                    {
                        if (BiggerTwo(broj2, broj4))
                        {
                            p1.turn = true;
                            p2.turn = false;
                            label5.Visible = true;
                            label4.Visible = false;
                            p1.punti += Punti(broj1, broj2, broj3, broj4);
                            label7.Text = p1.punti.ToString();
                        }
                        else
                        {
                            p1.turn = false;
                            p2.turn = true;
                            label5.Visible = false;
                            label4.Visible = true;
                            p2.punti += Punti(broj1, broj2, broj3, broj4);
                            label8.Text = p2.punti.ToString();
                        }

                    }
                }
                else if (card1)
                {
                    //pl1
                    if (BiggerTwo(broj1, broj4))
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                    else
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }
                }
                else
                {
                    if (BiggerTwo(broj2, broj4))
                    {
                        p1.turn = true;
                        p2.turn = false;
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.punti += Punti(broj1, broj2, broj3, broj4);
                        label7.Text = p1.punti.ToString();
                    }
                    else
                    {
                        p1.turn = false;
                        p2.turn = true;
                        label5.Visible = false;
                        label4.Visible = true;
                        p2.punti += Punti(broj1, broj2, broj3, broj4);
                        label8.Text = p2.punti.ToString();
                    }

                }

            }
            else if ((card1 || card2) && (!card3 && !card4))
            {
                p1.turn = true;
                p2.turn = false;
                label5.Visible = true;
                label4.Visible = false;
                p1.punti += Punti(broj1, broj2, broj3, broj4);
                label7.Text = p1.punti.ToString();

            }
            else if ((!card1 && !card2) && (card3 || card4))
            {
                p1.turn = false;
                p2.turn = true;
                label5.Visible = false;
                label4.Visible = true;
                p2.punti += Punti(broj1, broj2, broj3, broj4);
                label8.Text = p2.punti.ToString();


            }

                }
        

            
        
        public void DrawCard()
        {
            PictureBox pbp1_1 = getPictureBoxPlayer_1((string)potez1[0]);
            PictureBox pbp1_2 = getPictureBoxPlayer_1((string)potez1[1]);
            PictureBox pbp2_1 = GetPicturePlayer_2((string)potez2[0]);
            PictureBox pbp2_2 = GetPicturePlayer_2((string)potez2[1]);
            if (p1.turn == true)
            {
                if (i < max)
                {
                    p1.ruka[(int)poz1[0]] = d.getKarte(i);
                    string slika = p1.ruka[(int)poz1[0]].ToString();
                    pbp1_1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    i++;
                    label3.Text = (max - i).ToString();
                  
                    p2.ruka[(int)poz2[0]] = d.getKarte(i);
                    pbp2_1.Image = Properties.Resources.pozadina;
                    i++;
                    if (i >= max)
                    {
                        pictureBox12.Dispose();
                        label3.Text = 0.ToString();
                    }
                    p1.ruka[(int)poz1[1]] = d.getKarte(i);
                    slika = p1.ruka[(int)poz1[1]].ToString();
                    pbp1_2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    i++;
                    p2.ruka[(int)poz2[1]] = d.getKarte(i);

                    pbp2_2.Image = Properties.Resources.pozadina;
                    i++;
                    label3.Text = (max - i).ToString();
                    if (i >= max)
                    {
                        pictureBox12.Dispose();
                        label3.Text = 0.ToString();
                    }

                }

            }
            else
            {
                if (i < max)
                {
                    p2.ruka[(int)poz2[0]] = d.getKarte(i);

                    pbp2_1.Image = Properties.Resources.pozadina;


                    i++;

                    p1.ruka[(int)poz1[0]] = d.getKarte(i);
                    string slika = p1.ruka[(int)poz1[0]].ToString();
                    pbp1_1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                    i++;
                    p2.ruka[(int)poz2[1]] = d.getKarte(i);
                    pbp2_2.Image = Properties.Resources.pozadina;
                    if (i >= max)
                    {
                        pictureBox12.Dispose();
                        label3.Text = 0.ToString();
                    }

                    i++;
                    p1.ruka[(int)poz1[1]] = d.getKarte(i);
                    slika = p1.ruka[(int)poz1[1]].ToString();
                    pbp1_2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    i++;
                    label3.Text = (max - i).ToString();
                    if (i >= max)
                    {
                        pictureBox12.Dispose();
                        label3.Text = 0.ToString();
                    }
                   
                }





            }
        }
        
        public void pokupi()
        {
            int broj1,broj2,broj3,broj4;
            string prvi1 = c1.vr.ToString();
            string prvi2 = c2.vr.ToString();
            string drugi1 = c3.vr.ToString();
            string drugi2 = c4.vr.ToString();

            string igra = c5.v.ToString();

            broj1= (int)Enum.Parse(typeof(Vrijednost), prvi1);
            broj2 = (int)Enum.Parse(typeof(Vrijednost), prvi2);
            broj3 = (int)Enum.Parse(typeof(Vrijednost), drugi1);
            broj4 = (int)Enum.Parse(typeof(Vrijednost), drugi2);
            //sve od igre bacene
           


            

            bool card1=VrstaType(igra,c1.v.ToString());
            bool card2= VrstaType(igra, c2.v.ToString());
            bool card3= VrstaType(igra, c3.v.ToString());
            bool card4= VrstaType(igra, c4.v.ToString());
            GameRules(igra,card1,card2,card3,card4,broj1,broj2,broj3,broj4);
            DrawCard();
            this.potez1.Clear();
            this.potez2.Clear();
            this.poz1.Clear();
            this.poz2.Clear();

            if (p1.punti > 60)
            { 
                OnGameEnded("e");
            }
            else if (p2.punti > 60)
            {
                OnGameEnded("c");

            }
            else if (p1.punti == 60 && p2.punti == 60)
            {
                OnGameEnded("t");


            }
            timer1_Tick(null, null);



        }
        public int CardValue(int vr)
        {
                if(vr==9)
                {
                return 11;
                }
                else if(vr==8)
                {
                return 10;
                }
                else if(vr==7)
                {
                return 4;
                }
                else if(vr==6)
                {
                return 3;
                }
                else if(vr==5)
                {
                return 2;
                }

            return 0;
            
        }   


        public int Punti(int vr1, int vr2,int vr3,int vr4)
        {
            punti = 0;
            punti+= CardValue(vr1);
            punti+=CardValue(vr2);
            punti += CardValue(vr3);
            punti += CardValue(vr4);

            return punti;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Tag != null)
            {
                p1.turn = false;
                p2.turn = true;
                label4.Visible = true;
                label5.Visible = false;
                card_trown++;


                OnMOuseClicked(pictureBox1.Name.ToString());
                this.potez1.Add(pictureBox1.Name.ToString());
                this.poz1.Add(0);
                string slika = p1.ruka[0].ToString();
                if (pictureBox14.Tag == null)
                {
                    first_played = true;
                }

                if (i < max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[0];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[0];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox1.Tag = null;
                    pictureBox1.Image = null;

                }
                else if (i == max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[0];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[0];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox1.Tag = null;
                    pictureBox1.Image = null;



                }

                //kada dode za podilit kartu za igru(zadnju) 
                else
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[0];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[0];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox1.Tag = null;
                    pictureBox1.Image = null;
                }
                if (card_trown == 4)
                {
                    card_trown = 0;
                    pokupi();
                }
                if (p1.turn == false)
                {
                    panel1.Enabled = false;

                }



            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Tag != null)
            {
                p1.turn = false;
                p2.turn = true;
                label4.Visible = true;
                label5.Visible = false;
                this.potez1.Add(pictureBox2.Name.ToString());
                this.poz1.Add(1);
                OnMOuseClicked(pictureBox2.Name.ToString());
                card_trown++;
                string slika = p1.ruka[1].ToString();
                if (pictureBox14.Tag == null)
                {
                    first_played = true;
                }
                if (i < max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[1];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[1];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox2.Tag = null;
                    pictureBox2.Image = null;
                }
                else if (i == max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[1];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[1];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox2.Tag = null;
                    pictureBox2.Image = null;




                }
                else
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[1];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[1];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox2.Tag = null;
                    pictureBox2.Image = null;

                }
                if (card_trown == 4)
                {
                    card_trown = 0;
                    pokupi();
                }
                if (p1.turn == false)
                {
                    panel1.Enabled = false;

                }

            }
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Tag != null)
            {
                p1.turn = false;
                p2.turn = true;
                label4.Visible = true;
                label5.Visible = false;
                card_trown++;

                this.potez1.Add(pictureBox3.Name.ToString());
                this.poz1.Add(2);
                OnMOuseClicked(pictureBox3.Name.ToString());

                string slika = p1.ruka[2].ToString();
                if (pictureBox14.Tag == null)
                {
                    first_played = true;
                }
                if (i < max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[2];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[2];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox3.Tag = null;
                    pictureBox3.Image = null;
                }
                else if (i == max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[2];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[2];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox3.Tag = null;
                    pictureBox3.Image = null;




                }
                else
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[2];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[2];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox3.Tag = null;
                    pictureBox3.Image = null;

                }
                if (card_trown == 4)
                {
                    card_trown = 0;
                    pokupi();
                }
                if (p1.turn == false)
                {
                    panel1.Enabled = false;

                }
            }

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (pictureBox4.Tag != null)
            {
                p1.turn = false;
                p2.turn = true;
                label4.Visible = true;
                label5.Visible = false;
                card_trown++;

                this.potez1.Add(pictureBox4.Name.ToString());
                this.poz1.Add(3);
                OnMOuseClicked(pictureBox4.Name.ToString());

                string slika = p1.ruka[3].ToString();
                if (pictureBox14.Tag == null)
                {
                    first_played = true;
                }

                if (i < max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[3];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[3];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox4.Tag = null;
                    pictureBox4.Image = null;
                }
                else if (i == max)
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[3];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[3];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox4.Tag = null;
                    pictureBox4.Image = null;




                }
                else
                {
                    if (pictureBox10.Tag == null)
                    {
                        pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox10.Tag = (Cards)p1.ruka[3];
                        c1 = (Cards)pictureBox10.Tag;
                    }
                    else
                    {
                        pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                        pictureBox11.Tag = (Cards)p1.ruka[3];
                        c2 = (Cards)pictureBox11.Tag;
                    }
                    pictureBox4.Tag = null;
                    pictureBox4.Image = null;

                }
                if (card_trown == 4)
                {
                    card_trown = 0;
                    pokupi();
                }
                if (p1.turn == false)
                {
                    panel1.Enabled = false;

                }
            }

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
           
            string slika = p2.ruka[2].ToString();
            this.potez2.Add(pictureBox7.Name.ToString());
            this.poz2.Add(2);
            card_trown++;

            p1.turn = true;
            p2.turn = false;
            label5.Visible = true;
            label4.Visible = false;
            if (pictureBox10.Tag == null)
            {
                first_played = false;
            }
            if (i < max)
            {

                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[2];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[2];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox7.Tag = null;
                pictureBox7.Image = null;

            }
            else if (i == max)
            {
                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[2];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[2];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox7.Tag = null;
                pictureBox7.Image = null;

            }
            else
            {
                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[2];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[2];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox7.Tag = null;
                pictureBox7.Image = null;

            }
            if (card_trown == 4)
            {
                card_trown = 0;
                pokupi();
            }
            if (p2.turn == false)
            {
                panel1.Enabled = true;
            }




        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            string slika = p2.ruka[1].ToString();
            p1.turn = true;
            p2.turn = false;
            label5.Visible = true;
            label4.Visible = false;
            card_trown++;

            this.potez2.Add(pictureBox6.Name.ToString());
            this.poz2.Add(1);
            if (pictureBox10.Tag == null)
            {
                first_played = false;
            }
            if (i < max)
            {

                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[1];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[1];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox6.Tag = null;
                pictureBox6.Image = null;
            }
            else if (i == max)
            {

                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[1];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[1];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox6.Tag = null;
                pictureBox6.Image = null;


            }
            else
            {
                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[1];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                    pictureBox15.Tag = (Cards)p2.ruka[1];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox6.Tag = null;
                pictureBox6.Image = null;


            }
            if (card_trown==4)
            {
                card_trown = 0;
                pokupi();
            }
            if (p2.turn == false)
            {
                panel1.Enabled = true;
            }



        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
          
            string slika = p2.ruka[0].ToString();
            p1.turn = true;
            p2.turn = false;
            this.label5.Visible = true;
            this.label4.Visible = false;
            card_trown++;

            this.potez2.Add(pictureBox5.Name.ToString());
            this.poz2.Add(0);
            if (pictureBox10.Tag == null)
            {
                first_played = false;
            }
            if (i < max)
            {

                if (pictureBox14.Tag== null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[0];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[0];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox5.Tag = null;
                pictureBox5.Image = null;
            }
            else if (i == max)
            {
                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[0];
                    c3= (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[0];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox5.Tag = null;
                pictureBox5.Image = null;


            }
            else
            {
                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = p2.ruka[0];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[0];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox5.Tag = null;
                pictureBox5.Image = null;


            }
            if (card_trown == 4)
            {
                card_trown = 0;
                pokupi();
            }

            if (p2.turn == false)
            {
                panel1.Enabled = true;
            }

        }
       

        private void pictureBox8_Click(object sender, EventArgs e)
        {
          
            string slika = p2.ruka[3].ToString();
            p1.turn = true;
            p2.turn = false;
            this.label5.Visible = true;
            this.label4.Visible = false;
            card_trown++;

            this.potez2.Add(pictureBox8.Name.ToString());
            this.poz2.Add(3);
            if (pictureBox10.Tag == null)
            {
                first_played = false;
            }
            if (i < max)
            {

                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[3];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[3];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox8.Tag = null;
                pictureBox8.Image = null;
            }
            else if (i == max)
            {
                if (pictureBox14 == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = (Cards)p2.ruka[3];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[3];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox8.Tag = null;
                pictureBox8.Image = null;


            }
            else
            {
                if (pictureBox14.Tag == null)
                {
                    pictureBox14.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox14.Tag = p2.ruka[3];
                    c3 = (Cards)pictureBox14.Tag;
                }
                else
                {
                    pictureBox15.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                    pictureBox15.Tag = (Cards)p2.ruka[3];
                    c4 = (Cards)pictureBox15.Tag;
                }
                pictureBox8.Tag = null;
                pictureBox8.Image = null;


            }
            if (card_trown == 4)
            {
                card_trown = 0;
                pokupi();
            }

            if (p2.turn == false)
            {
                panel1.Enabled = true;
            }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            timer_tick++;

            if (timer_tick == 3)
            {

                pictureBox10.Tag = null;

                pictureBox10.Image = null;
                pictureBox11.Tag = null;
                pictureBox11.Image = null;
                pictureBox14.Tag = null;
                pictureBox14.Image = null;
                pictureBox15.Tag = null;
                pictureBox15.Image = null;
                timer1.Stop();
                timer_tick = 0;
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void GameRecived(object sender, SimpleTCP.Message e)
        {
            if (e.MessageString.Contains(ClientName + ":"))
                listView2.Items.Add(e.MessageString);
        }
        private void GameFour_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnFormExiting();
            this.Dispose();

        }


        private void listView1_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        private void GameFour_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Dispose();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pictureBox15.Tag.ToString());

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pictureBox14.Tag.ToString());

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pictureBox11.Tag.ToString());

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pictureBox10.Tag.ToString());
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
