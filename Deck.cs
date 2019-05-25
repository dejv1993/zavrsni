using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Resources;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Security.Permissions;
namespace Seminar
{
    [Serializable]
    public enum Vrsta { Kupe, Spade, Dinari, Bastoni };
    public enum Vrijednost { dva, cetri, pet, sest, sedam, fanat, konj, kralj,tri,As };
  
    
    public   class Deck 
    {
        
        public Cards[] karte;

        public Deck()
        {
            karte = new Cards[40];
         

            setCards();
        }
      
     
        public Cards getKarte(int i)
        {
            return this.karte[i];
        }
  
        public string getCard(int i)
        {
            return this.karte[i].v + this.karte[i].vr.ToString();
        }
        // radi spil karata 
        public void setCards()
        {
            int i = 0;

            foreach (Vrsta v in Enum.GetValues(typeof(Vrsta)))
            {
                foreach (Vrijednost vr in Enum.GetValues(typeof(Vrijednost)))
                {
                    string s = v.ToString() + vr.ToString();

                  
                    this.karte[i] = new Cards(v, vr);
                    
                  
                    i++;
                    
                }
            }
          
        }


        public void shuffle()
        {

            Random r = new Random();
            Cards t=new Cards();
            for (int sh = 0; sh < 1000; sh++)
            {
                for (int i = 0; i < 40; i++)
                {
                    int noviInd = r.Next(10);
                    t = karte[i];
                    karte[i] = karte[noviInd];
                    karte[noviInd] = t;

                }
            }

        }


    }
}
