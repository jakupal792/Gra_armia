using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        List<Pojazd> Lista_pojazdow;
        public Form1()
        {
            InitializeComponent();
            Lista_pojazdow = new List<Pojazd>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public abstract class Pojazd
        {
            protected static int Ilosc = 0;

            protected int _id;
            public int Id { get => _id; set => _id = value; }

            protected string _nazwa;
            public string Nazwa { get => _nazwa; set => _nazwa = value; }

            protected double _moc;
            public double Moc { get => _moc; set => _moc = value; }

            public Pojazd(string nazwa, double moc)
            {
                Ilosc++;
                Id = Ilosc;
                Nazwa = nazwa;
                Moc = moc;
            }

            public virtual string Wypisz()
            {
                return " - " + this.Nazwa + "; moc: " + this.Moc;
            }
        }

        public class Czolg : Pojazd
        {
            protected double _kaliber;
            public double Kaliber { get=>_kaliber; set=>_kaliber=value; }

            public Czolg(string nazwa, double moc, double kaliber) : base(nazwa, moc)
            {
                Kaliber = kaliber;
            }

            public override string Wypisz()
            {
                return this.Id + ". Czolg " + base.Wypisz() + "; kaliber: " + this.Kaliber;
            }
        }

        public class Samolot : Pojazd
        {
            protected double _bomby;
            public double Bomby { get=>_bomby; set=>_bomby=value; }

            protected double _pulap;
            public double Pulap { get=>_pulap; set=>_pulap=value; }

            public Samolot(string nazwa, double moc, double bomby, double pulap) : base(nazwa, moc)
            {
                Bomby = bomby;
                Pulap = pulap;
            }

            public override string Wypisz()
            {
                return this.Id + ". Samolot " + base.Wypisz() + "; ładunek bomb: " + this.Bomby + "; pulap: " + this.Pulap;
            }
        }

        private void btnCreateTank_Click(object sender, EventArgs e)
        {
            try
            {
                string nazwa = txtNazwa.Text;
                if (String.IsNullOrWhiteSpace(nazwa)) throw new Exception();
                double moc = double.Parse(txtMoc.Text);
                double kaliber = double.Parse(txtKaliber.Text);
                if (moc <= 0 || kaliber <= 0) throw new Exception();
                Czolg c = new Czolg(nazwa, moc, kaliber);
                Lista_pojazdow.Add(c);
                Wyswietl_pojazdy();

            }
            catch (Exception h)
            {
                MessageBox.Show("Wprowadź poprawne dane!");
            }
       
        }

        private void btnCreatePlane_Click(object sender, EventArgs e)
        {
            try 
            {
                string nazwa = txtSNazwa.Text;
                if (String.IsNullOrWhiteSpace(nazwa)) throw new Exception();
                double moc = double.Parse(txtSMoc.Text);
                double bomby = double.Parse(txtSBomby.Text);
                double pulap = double.Parse(txtPulap.Text);
                if (moc <= 0 || bomby <= 0 || pulap <=0) throw new Exception();
                Samolot c = new Samolot(nazwa, moc, bomby, pulap);
                Lista_pojazdow.Add(c);
                Wyswietl_pojazdy();
            }
            catch(Exception h)
            {
                MessageBox.Show("Wprowadź poprawne dane!");
            }
        }

        private void Wyswietl_pojazdy()
        {
            lstPojazdy.Items.Clear();
            foreach (Pojazd p in Lista_pojazdow)
            {
                lstPojazdy.Items.Add(p.Wypisz());
            }
        }

        private void btnFigth_Click(object sender, EventArgs e)
        {
            try
            {
                int id1 = int.Parse(txtID1.Text);
                int id2 = int.Parse(txtID2.Text);
                Fight(id1, id2);
            }
            catch (Exception h)
            {
                MessageBox.Show("Wprowadź id istniejących pojazdów!");
            }
        }

        private void btnRandomFigth_Click(object sender, EventArgs e)
        {
            try
            {
                Random r = new Random();
                int max = Lista_pojazdow.Count + 1;
                Fight(r.Next(1, max), r.Next(1, max));
            }
            catch (Exception h)
            {
                MessageBox.Show("Wprowadź id istniejących pojazdów!");
            }
        }

        private void Fight(int id1, int id2)
        {
            var p1 = Lista_pojazdow.Find(p => p.Id == id1);
            var p2 = Lista_pojazdow.Find(p => p.Id == id2);

            if (p1 is Czolg && p2 is Czolg)
            {
                Czolg c1 = p1 as Czolg;
                Czolg c2 = p2 as Czolg;

                if (c1.Kaliber > c2.Kaliber) txtResult.Text = opis_walki(id1, id2, id1);
                else if (c1.Kaliber < c2.Kaliber) txtResult.Text = opis_walki(id1, id2, id2);
                else if (c1.Kaliber == c2.Kaliber)
                {
                    if (c1.Moc > c2.Moc) txtResult.Text = opis_walki(id1, id2, id1);
                    else if (c1.Moc < c2.Moc) txtResult.Text = opis_walki(id1, id2, id2);
                    else if (c1.Moc == c2.Moc) txtResult.Text = opis_walki(id1, id2);
                }
            }
            else if (p1 is Samolot && p2 is Samolot)
            {
                Samolot s1 = p1 as Samolot;
                Samolot s2 = p2 as Samolot;

                if (s1.Pulap > s2.Pulap) txtResult.Text = opis_walki(id1, id2, id1);
                else if (s1.Pulap < s2.Pulap) txtResult.Text = opis_walki(id1, id2, id2);
                else if (s1.Pulap == s2.Pulap)
                {
                    if (s1.Moc > s2.Moc) txtResult.Text = opis_walki(id1, id2, id1);
                    else if (s1.Moc < s2.Moc) txtResult.Text = opis_walki(id1, id2, id2);
                    else if (s1.Moc == s2.Moc) txtResult.Text = opis_walki(id1, id2);
                }
            }
            else
            {
                Samolot s;
                Czolg c;
                if (p1 is Samolot)
                {
                    s = p1 as Samolot;
                    c = p2 as Czolg;
                }
                else
                {
                    c = p1 as Czolg;
                    s = p2 as Samolot;
                }

                if (s.Bomby > c.Kaliber) txtResult.Text = opis_walki(s.Id, c.Id, s.Id);
                else if (s.Bomby < c.Kaliber) txtResult.Text = opis_walki(s.Id, c.Id, c.Id);
                else if (s.Bomby == c.Kaliber)
                {
                    if (s.Moc > c.Moc) txtResult.Text = opis_walki(s.Id, c.Id, s.Id);
                    else if (s.Moc < c.Moc) txtResult.Text = opis_walki(s.Id, c.Id, c.Id);
                    else if (s.Moc == c.Moc) txtResult.Text = opis_walki(s.Id, c.Id);
                }
            }
        }

        private string opis_walki(int id1, int id2, int idZw)
        {
            var p1 = Lista_pojazdow.Find(p => p.Id == id1);
            var p2 = Lista_pojazdow.Find(p => p.Id == id2);
            var zw = Lista_pojazdow.Find(p => p.Id == idZw);
            return (p1.GetType().Name).ToString() + " " + p1.Nazwa + " vs " + (p2.GetType().Name).ToString() + " " + p2.Nazwa + ": Wygrał " + (zw.GetType().Name).ToString() + " " + zw.Nazwa;
        }

        private string opis_walki(int id1, int id2)
        {
            var p1 = Lista_pojazdow.Find(p => p.Id == id1);
            var p2 = Lista_pojazdow.Find(p => p.Id == id2);
            return (p1.GetType().Name).ToString() + " " + p1.Nazwa + " vs " + (p2.GetType().Name).ToString() + " " + p2.Nazwa + ": REMIS";
        }
    }
}
