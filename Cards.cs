using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Security.Permissions;
using System.IO;
namespace Seminar
{
    [Serializable]
 
    public class Cards 

    {
     
        public Vrsta v;
      
        public Vrijednost vr;
     
        public Cards(){}
       
        public Cards(Vrsta v, Vrijednost vr)
        {

            this.v = v;
            this.vr = vr;
           



        }



      
        public override string ToString()
        {
            return v.ToString()+vr.ToString();
        }
       
    }

}
