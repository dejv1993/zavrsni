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

namespace Seminar
{
    [JsonObject(MemberSerialization.OptIn)]

    public partial class Form1 : Form
    {
        [JsonProperty]
        public int i = 0;
        public int card_trown = 0;
        public int k1 = 0;
        public int k2 = 0;
        public static int k_poz = 0;
        public static int max = 39;
        public string potez1="";
        public string potez2="";
        private int timer_tick = 0;
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
        //karta igraca 2
        [JsonProperty]
        public Cards c2;
        // za zadnju kartu
        [JsonProperty]
        public Cards c3;


        public static string json;

        public Form1()
        {


            d = new Deck();
            d.shuffle();
            p1 = new Igrac("player1");
            p1.turn = true;
          
            p2 = new Igrac("player");
            p2.turn = false;
            c1 = new Cards();
            c2 = new Cards();
            c3 = new Cards();
            for (k_poz = 0; k_poz < 3; k_poz++)
            {
                p1.ruka[k_poz] = (d.getKarte(k_poz));
            }
            int k = 0;
            for (k_poz = 3; k_poz < 6; k_poz++)
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
            if(FormExiting!=null)
            {
                FormExiting();
            }
        }


        //kada zavrsi  
        public delegate void GameOverEventHandler(string s);
        public event GameOverEventHandler GameEnded;
        public void OnGameEnded(string s)
        {
            if(GameEnded!=null)
            {
                GameEnded(s);
               
            }
        }
        public delegate void UnsubEventHandler(string s);
        public event UnsubEventHandler FormUnsub;
        public void  OnFormUnsub(string s)
        {
            if(FormUnsub!=null)
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            label1.Text = p2.name;
            label2.Text = p1.name;
            label10.Text = p2.pobjede.ToString();
            label11.Text = p1.pobjede.ToString();
            //Postavlja slike za prvog igraca
            for (int p = 0; p < 1; p++)
            {
                
                string slika = p1.ruka[i].ToString();
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

            c3 = (Cards)pictureBox12.Tag;
            panel1.Controls.Add(pictureBox1);
            
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox3);
            panel2.Controls.Add(pictureBox5);
            panel2.Controls.Add(pictureBox6);
            panel2.Controls.Add(pictureBox7);
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


        public void pokupi()
        {

            int broj;
            int broj1;
           

            string prvi = c1.vr.ToString();
            string drugi = c2.vr.ToString();
            string igra = c3.v.ToString();

            broj = (int)Enum.Parse(typeof(Vrijednost), prvi);
            broj1 = (int)Enum.Parse(typeof(Vrijednost), drugi);
            if (igra == c1.v.ToString() && igra == c2.v.ToString())
            {
                if (broj > broj1)
                {

                    p1.turn = true;
                    p2.turn = false;
                    label5.Visible = true;
                    label4.Visible = false;
                    p1.punti += Punti(broj, broj1);
                    label7.Text = p1.punti.ToString();
                 

                }
                else
                {
                    p1.turn = false;
                    p2.turn = true;
                    label5.Visible = false;
                    label4.Visible = true;
                    p2.punti += Punti(broj, broj1);
                    label8.Text = p2.punti.ToString();

                }



            }
            //ako je p1 baci od igre a p2 nije
            else if (igra == c1.v.ToString() && igra != c2.v.ToString())
            {

                label5.Visible = true;
                label4.Visible = false;
                p1.punti += Punti(broj, broj1);
                p1.turn = true;
                p2.turn = false;
                label7.Text = p1.punti.ToString();
            }
            //obrnuto
            else if (igra != c1.v.ToString() && igra == c2.v.ToString())
            {

                label5.Visible = false;
                label4.Visible = true;
                p2.punti += Punti(broj, broj1);
                p2.turn = true;
                p1.turn = false;
                label8.Text = p2.punti.ToString();
            }

            else if (igra != c1.v.ToString() && igra != c2.v.ToString())
            {
                if (c1.v.ToString() == c2.v.ToString())
                {
                    if (broj > broj1)
                    {
                        label5.Visible = true;
                        label4.Visible = false;
                        p1.turn = true;
                        p2.turn = false;
                        p1.punti += Punti(broj, broj1);
                        label7.Text = p1.punti.ToString();
                    }
                    else if (broj < broj1)
                    {

                        label5.Visible = false;
                        label4.Visible = true;
                        p2.turn = true;
                        p1.turn = false;
                        p2.punti += Punti(broj, broj1);
                        label8.Text = p2.punti.ToString();
                    }
                }
                else
                {
                    if (p1.turn == true)
                    {

                        p1.turn = true;
                        p2.turn = false;
                        p1.punti += Punti(broj, broj1);
                        label7.Text = p1.punti.ToString();
                    }
                    else
                    {

                        p2.turn = true;
                        p1.turn = false;
                        p2.punti += Punti(broj, broj1);
                        label8.Text = p2.punti.ToString();
                    }

                }
                

            }


            if (potez1.Equals("pictureBox1") && potez2.Equals("pictureBox5"))
            {
                if (p1.turn == true)
                {
                    if (i < max)
                    {
                        p1.ruka[0] = d.getKarte(i);
                        string slika = p1.ruka[0].ToString();
                        pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();
                       
                        p2.ruka[0] = d.getKarte(i);

                        pictureBox5.Image = Properties.Resources.pozadina;


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
                        p2.ruka[0] = d.getKarte(i);

                        pictureBox5.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();
                        
                        p1.ruka[0] = d.getKarte(i);
                        string slika = p1.ruka[0].ToString();
                        pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        
                        label3.Text = (max - i).ToString();
                        if(i>=max)
                        {
                            pictureBox12.Dispose();
                            label3.Text = 0.ToString();
                        }
                    }
                
                  

                }

            }
            else if (potez1.Equals("pictureBox1") && potez2.Equals("pictureBox6"))
            {

                if (p1.turn == true)
                {

                    if (i < max)
                    {
                        p1.ruka[0] = d.getKarte(i);
                        string slika = p1.ruka[0].ToString();
                        pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[1] = d.getKarte(i);

                        pictureBox6.Image = Properties.Resources.pozadina;


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
                        p2.ruka[1] = d.getKarte(i);

                        pictureBox6.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[0] = d.getKarte(i);
                        string slika = p1.ruka[0].ToString();
                        pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();
                    }
                   
                }
            }
            else if (potez1.Equals("pictureBox1") && potez2.Equals("pictureBox7"))
            {
               
                    if (p1.turn == true)
                    {
                    if (i < max)
                    {
                        p1.ruka[0] = d.getKarte(i);
                        string slika = p1.ruka[0].ToString();
                        pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[2] = d.getKarte(i);

                        pictureBox7.Image = Properties.Resources.pozadina;


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
                        p2.ruka[2] = d.getKarte(i);


                        pictureBox7.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[0] = d.getKarte(i);
                        string slika = p1.ruka[0].ToString();
                        pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

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

            if (potez1.Equals("pictureBox2") && potez2.Equals("pictureBox5"))
            {
                if (p1.turn == true)
                {
                    if (i < max)
                    {
                        p1.ruka[1] = d.getKarte(i);
                        string slika = p1.ruka[1].ToString();
                        pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[0] = d.getKarte(i);

                        pictureBox5.Image = Properties.Resources.pozadina;


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
                        p2.ruka[0] = d.getKarte(i);

                        pictureBox5.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[1] = d.getKarte(i);
                        string slika = p1.ruka[1].ToString();
                        pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

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
            else if (potez1.Equals("pictureBox2") && potez2.Equals("pictureBox6"))
            {
                if (p1.turn == true)
                {
                    if (i < max)
                    {
                        p1.ruka[1] = d.getKarte(i);
                        string slika = p1.ruka[1].ToString();
                        pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[1] = d.getKarte(i);

                        pictureBox6.Image = Properties.Resources.pozadina;


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
                        p2.ruka[1] = d.getKarte(i);

                        pictureBox6.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[1] = d.getKarte(i);
                        string slika = p1.ruka[1].ToString();
                        pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

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
            else if (potez1.Equals("pictureBox2") && potez2.Equals("pictureBox7"))
            {

                if (p1.turn == true)
                {
                    if (i < max)
                    {
                        p1.ruka[1] = d.getKarte(i);
                        string slika = p1.ruka[1].ToString();
                        pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[2] = d.getKarte(i);

                        pictureBox7.Image = Properties.Resources.pozadina;


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
                        p2.ruka[2] = d.getKarte(i);

                        pictureBox7.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[1] = d.getKarte(i);
                        string slika = p1.ruka[1].ToString();
                        pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

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
         
            if (potez1.Equals("pictureBox3") && potez2.Equals("pictureBox5"))
            {
                if (p1.turn == true)
                {
                    if (i < max)
                    {
                        p1.ruka[2] = d.getKarte(i);
                        string slika = p1.ruka[2].ToString();
                        pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[0] = d.getKarte(i);

                        pictureBox5.Image = Properties.Resources.pozadina;


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
                        p2.ruka[0] = d.getKarte(i);

                        pictureBox5.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[2] = d.getKarte(i);
                        string slika = p1.ruka[2].ToString();
                        pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

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
            else if (potez1.Equals("pictureBox3") && potez2.Equals("pictureBox6"))
            {
                if (p1.turn == true)
                {
                    if (i < max)
                    {
                        p1.ruka[2] = d.getKarte(i);
                        string slika = p1.ruka[2].ToString();
                        pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[1] = d.getKarte(i);

                        pictureBox6.Image = Properties.Resources.pozadina;


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
                        p2.ruka[1] = d.getKarte(i);

                        pictureBox6.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[2] = d.getKarte(i);
                        string slika = p1.ruka[2].ToString();
                        pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

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
            else if (potez1.Equals("pictureBox3") && potez2.Equals("pictureBox7"))
            {
                if (p1.turn == true)
                {
                    if (i < max)
                    {
                        p1.ruka[2] = d.getKarte(i);
                        string slika = p1.ruka[2].ToString();
                        pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

                        i++;
                        label3.Text = (max - i).ToString();


                        p2.ruka[2] = d.getKarte(i);

                        pictureBox7.Image = Properties.Resources.pozadina;


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
                        p2.ruka[2] = d.getKarte(i);

                        pictureBox7.Image = Properties.Resources.pozadina;


                        i++;
                        label3.Text = (max - i).ToString();

                        p1.ruka[2] = d.getKarte(i);
                        string slika = p1.ruka[2].ToString();
                        pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);

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


            if (p1.punti > 5)
            {

               
                

                OnGameEnded("e");
  
            }
            else if (p2.punti > 2)
            {
                OnGameEnded("c");

            }
            else if (p1.punti == 60 && p2.punti == 60)
            {
                OnGameEnded("t");


            }
            timer1_Tick(null, null);
        


        }
   
        public int Punti(int vr, int vr1)
        {
            int punti = 0;
            if (vr == 9 && vr1 == 9)
            {
                punti += 22;
                return punti;
            }
            if (vr == 8 && vr1 == 8)
            {
                punti += 20;
                return punti;
            }
            if (vr == 7 && vr1 == 7)
            {
                punti += 8;
                return punti;
            }
            if (vr == 6 && vr1 == 6)
            {
                punti += 6;
                return punti;
            }
            if (vr == 5 && vr1 == 5)
            {
                punti += 4;
                return punti;
            }


            if (vr == 9 || vr1 == 9)
            {
                punti += 11;
            }
            if (vr == 8 || vr1 == 8)
            {
                punti += 10;
            }
            if (vr == 7 || vr1 == 7)
            {
                punti += 4;
            }
            if (vr == 6 || vr1 == 6)
            {
                punti += 3;
            }
            if (vr == 5 || vr1 == 5)
            {
                punti += 2;
            }

            return punti;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            p1.turn = false;
            p2.turn = true;
            label4.Visible = true;
            label5.Visible = false;
            card_trown++;

            OnMOuseClicked(pictureBox1.Name.ToString());
            this.potez1 = pictureBox1.Name.ToString();
            string slika = p1.ruka[0].ToString();
            if (i < max)
            {

                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[0];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox1.Tag = null;
                pictureBox1.Image = null;
               
            }
            else if (i == max)
            {

                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[0];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox1.Tag = null;
                pictureBox1.Image = null;



            }

            //kada dode za podilit kartu za igru(zadnju) 
            else
            {
               
                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[0];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox1.Tag = null;
                pictureBox1.Image = null;
            }
            if (card_trown == 2)
            {
                card_trown = 0;
                pokupi();
            }
            if (p1.turn == false)
            {
                panel1.Enabled = false;

            }




        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            p1.turn = false;
            p2.turn = true;
            label4.Visible = true;
            label5.Visible = false;
            this.potez1 = pictureBox2.Name.ToString();

            OnMOuseClicked(pictureBox2.Name.ToString());
            card_trown++;
            string slika = p1.ruka[1].ToString();
            if (i < max)
            {
              
                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[1];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox2.Tag = null;
                pictureBox2.Image = null;
            }
            else if (i == max)
            {

                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[1];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox2.Tag = null;
                pictureBox2.Image = null;
               



            }
            else
            {
              
                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[1];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox2.Tag = null;
                pictureBox2.Image = null;

            }
            if (card_trown == 2)
            {
                card_trown = 0;
                pokupi();
            }
            if (p1.turn == false)
            {
                panel1.Enabled = false;

            }


        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            p1.turn = false;
            p2.turn = true;
            label4.Visible = true;
            label5.Visible = false;
            card_trown++;

            this.potez1 = pictureBox3.Name.ToString();

            OnMOuseClicked(pictureBox3.Name.ToString());

            string slika = p1.ruka[2].ToString();
            if (i < max)
            {
                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[2];
                c1 = (Cards)pictureBox10.Tag;

                pictureBox3.Tag = null;
                pictureBox3.Image = null;
            }
            else if (i == max)
            {
               
                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[2];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox3.Tag = null;
                pictureBox3.Image = null;
            



            }
            else
            {
                pictureBox10.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox10.Tag = (Cards)p1.ruka[2];
                c1 = (Cards)pictureBox10.Tag;
                pictureBox3.Tag = null;
                pictureBox3.Image = null;

            }
            if (card_trown == 2)
            {
                card_trown = 0;
                pokupi();
            }
            if (p1.turn == false)
            {
                panel1.Enabled = false;

            }

        }


        private void pictureBox7_Click(object sender, EventArgs e)
        {

            string slika = p2.ruka[2].ToString();
            this.potez2 = pictureBox7.Name.ToString();
            card_trown++;

            p1.turn = true;
            p2.turn = false;
            label5.Visible = true;
            label4.Visible = false;
            if (i < max)
            {
              

                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[2];
                c2 = (Cards)pictureBox11.Tag;
                pictureBox7.Tag = null;
                pictureBox7.Image = null;

            }
            else if (i == max)
            {
                
                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[2];
                c2 = (Cards)pictureBox11.Tag;
                pictureBox7.Tag = null;
                pictureBox7.Image = null;

            }
            else
            {
                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[2];
                c2 = (Cards)pictureBox11.Tag;
                pictureBox7.Tag = null;
                pictureBox7.Image = null;

            }
            if (card_trown==2)
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

            this.potez2 = pictureBox6.Name.ToString();

            if (i < max)
            {


                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[1];
                c2 = (Cards)pictureBox11.Tag;
                pictureBox6.Tag = null;
                pictureBox6.Image = null;
            }
            else if (i == max)
            {


                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[1];
                c2 = (Cards)pictureBox11.Tag;

                pictureBox6.Tag = null;
                pictureBox6.Image = null;


            }
            else
            {
                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[1];
                c2 = (Cards)pictureBox11.Tag;
                pictureBox6.Tag = null;
                pictureBox6.Image = null;


            }
            if (card_trown == 2)
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

            this.potez2 = pictureBox5.Name.ToString();

            if (i < max)
            {


                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[0];
                c2 = (Cards)pictureBox11.Tag;
                pictureBox5.Tag = null;
                pictureBox5.Image = null;
            }
            else if (i == max)
            {
                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = (Cards)p2.ruka[0];
                c2 = (Cards)pictureBox11.Tag;
                pictureBox5.Tag = null;
                pictureBox5.Image = null;


            }
            else
            {
                pictureBox11.Image = (Image)Properties.Resources.ResourceManager.GetObject(slika);
                pictureBox11.Tag = p2.ruka[0];
                c2 = (Cards)pictureBox11.Tag;

                pictureBox5.Tag = null;
                pictureBox5.Image = null;


            }
            if (card_trown == 2)
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
                timer1.Stop();
                timer_tick = 0;
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnFormExiting();
            this.Dispose();

        }


        private void listView1_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Dispose();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }
    }
}
