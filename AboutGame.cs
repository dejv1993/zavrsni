using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seminar
{
    partial class AboutGame : Form
    {
        public AboutGame()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AboutGame_Load(object sender, EventArgs e)
        {
            textBoxDescription.Text= "Okrenuta karta ispod maca, predstavlja zog (boju), u koji se igra. Taj zog je jači od svih ostalih, a za to dijeljenje predstavlja briškulu (odnosno adut)."+

    "Postoje 4 zoga: kupe(kope), bate(baštone), špade i dinare.Unutar svakog zoga postoje karte od broja 1 - 7 i od 11 - 13.Unutar jednog zoga, najjači je  aš (1), pa trica(3), pa kralj(13), konj(12), fanat(11) i dalje od sedmice(7) prema dvici(2). As i trica se još zovu i karik(korga, karig)."+

    "As se broji kao 11 punata(bodova), trica vrijedi 10 punata, a karte 2 i 4 - 7 zovu se lišine ili škart i ne donose punte(usporedi s izrazom lišo bez punta). Kod ostalih zbrajaju se znamenke pa kralj vrijedi 4 punta, konj 3, a fanat 2 punta.Prema originalnom pravilu, i karta 2 je nosila 10 punat.Ove zadnje 3 karte sa slikama, u igri se zbirno nazivaju – figure ili punti.Ima ukupno 120 punata, tako da partija može završiti i neriješeno, a za pobjedu je potrebno sakupiti 61 punat."+

    "Igrači bacaju po jednu kartu naizmjenično, dok svi ne bace, a zatim najjača strana pokupi bačene karte.Time je završila jedna ruka te partije. Nakon toga svaki igrač, počevši od pobjednika te ruke, peška(uzima) po jednu kartu s maca i započinje nova ruka, i tako dok se ne odigraju sve karte. Izuzetno, u duploj bruškuli, ako se igra u dvoje, svaki igrač baca po dvije karte u svakoj odigranoj ruci, jednu po jednu, a isto tako i peškaje 2 karte s maca, jednu po jednu."+

    "Igru započinje igrač kojemu su prvom podijeljene karte. Bačena karta predstavlja zog u kojemu se igra ta ruka. Igrač s najjačom kartom te ruke, kupi karte, prvi uzima novu kartu s maca i započinje novu ruku. Prva bačena karta može se ubiti";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
