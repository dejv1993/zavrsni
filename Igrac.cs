using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Security.Permissions;
namespace Seminar
{
    [Serializable]

    public class Igrac
    {

        public String name;

        public int punti;
        public  int pobjede = 0;
        public bool turn;
        public Cards[] ruka;


        public Igrac(String name)
        {
            this.name = name;
            this.punti = 0;
            this.turn = false;
            this.ruka = new Cards[4];



        }

        public Cards getRuka(int i)
        {
            return this.ruka[i];
        }



    }



}
