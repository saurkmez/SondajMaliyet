using SondajMaliyetClass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SondajMaliyetForm.View
{
    public partial class AyarlarFrm : Form
    {
        public AyarlarFrm()
        {
            InitializeComponent();
        }

        private decimal GetRate(string code)
        {
            var result = new List<Currency>();
            try
            {
                string url = string.Empty;
                var date = DateTime.Now;
                if (date.Date == DateTime.Today)
                    url = "http://www.tcmb.gov.tr/kurlar/today.xml";
                else
                    url = string.Format("http://www.tcmb.gov.tr/kurlar/{0}{1}/{2}{1}{0}.xml", date.Year, addZero(date.Month), addZero(date.Day));

                System.Xml.Linq.XDocument document = System.Xml.Linq.XDocument.Load(url);
                Dictionary<string, string> dic = new Dictionary<string, string>();
                result = document.Descendants("Currency")
                .Where(v => v.Element("ForexBuying") != null && v.Element("ForexBuying").Value.Length > 0)
                .Select(v => new Currency
                {
                    Code = v.Attribute("Kod").Value,
                    Rate = decimal.Parse(v.Element("ForexBuying").Value.Replace('.', ','))
                }).ToList();

                return result.FirstOrDefault(s => s.Code == code).Rate;
            }
            catch (Exception ex)
            {
                return 7;
            }
            
        }
        public class Currency
        {
            public string Code { get; set; }
            public decimal Rate { get; set; }
        }

        private string addZero(int p)
        {
            if (p.ToString().Length == 1)
                return "0" + p;
            return p.ToString();
        }

        private void AyarlarFrm_Load(object sender, EventArgs e)
        {
            kur.Text = "USD= "+ GetRate("USD").ToString() + " TL";

            List<MatkapCap> matkaps = new List<MatkapCap>();
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"select * from MatkapCap";
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        matkaps.Add(new MatkapCap() { matkapCapi = reader.GetDouble(1), fiyat = reader.GetDouble(2) });
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }

            txt85inch.Text = matkaps.Where(x => x.matkapCapi == 8.5).FirstOrDefault().fiyat.ToString();
            txt95inch.Text = matkaps.Where(x => x.matkapCapi == 9.5).FirstOrDefault().fiyat.ToString();
            txt105inch.Text = matkaps.Where(x => x.matkapCapi == 10.5).FirstOrDefault().fiyat.ToString();
            txt115inch.Text = matkaps.Where(x => x.matkapCapi == 11.5).FirstOrDefault().fiyat.ToString();
            txt125inch.Text = matkaps.Where(x => x.matkapCapi == 12.5).FirstOrDefault().fiyat.ToString();
            txt135inch.Text = matkaps.Where(x => x.matkapCapi == 13.5).FirstOrDefault().fiyat.ToString();
            txt155inch.Text = matkaps.Where(x => x.matkapCapi == 15.5).FirstOrDefault().fiyat.ToString();
            txt175inch.Text = matkaps.Where(x => x.matkapCapi == 17.5).FirstOrDefault().fiyat.ToString();


            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"select * from IscilikMaliyeti order by kId DESC LIMIT 1";
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.SelectedIndex = reader.GetInt32(1)-1;
                        topMaas.Text = reader.GetDouble(2).ToString();
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }

            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"select * from MazotGideri order by mazotId DESC LIMIT 1";
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        depoLt.Text = reader.GetInt32(1).ToString();
                        litreFiyat.Text = reader.GetDouble(2).ToString();
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }

            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"select * from Nakliye order by nId DESC LIMIT 1";
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        nakliyeGider.Text = reader.GetInt32(1).ToString();
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"insert into IscilikMaliyeti (kisiSayisi, maas) values(@kisiSayisi,@maas)";
                    cmd.Parameters.AddWithValue("@kisiSayisi", comboBox1.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@maas", Convert.ToDouble(topMaas.Text));
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"insert into Nakliye (gunlukGider) values(@gunlukGider)";
                    cmd.Parameters.AddWithValue("@gunlukGider", Convert.ToInt32(nakliyeGider.Text));
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"insert into MazotGideri (tankHacmi, birimFiyat) values(@tankHacmi,@birimFiyat)";
                    cmd.Parameters.AddWithValue("@tankHacmi", Convert.ToInt32(depoLt.Text));
                    cmd.Parameters.AddWithValue("@birimFiyat", Convert.ToDouble(litreFiyat.Text));
                    cmd.ExecuteNonQuery();

                    List<MatkapCap> listMatkapCaps = new List<MatkapCap>()
                    {
                        new MatkapCap(){ matkapCapi=8.5, fiyat=Convert.ToDouble(txt85inch.Text)},
                        new MatkapCap(){ matkapCapi=9.5, fiyat=Convert.ToDouble(txt95inch.Text)},
                        new MatkapCap(){ matkapCapi=10.5, fiyat=Convert.ToDouble(txt105inch.Text)},
                        new MatkapCap(){ matkapCapi=11.5, fiyat=Convert.ToDouble(txt115inch.Text)},
                        new MatkapCap(){ matkapCapi=12.5, fiyat=Convert.ToDouble(txt125inch.Text)},
                        new MatkapCap(){ matkapCapi=13.5, fiyat=Convert.ToDouble(txt135inch.Text)},
                        new MatkapCap(){ matkapCapi=15.5, fiyat=Convert.ToDouble(txt155inch.Text)},
                        new MatkapCap(){ matkapCapi=17.5, fiyat=Convert.ToDouble(txt175inch.Text)}
                    };

                    foreach (var item in listMatkapCaps)
                    {
                        cmd.CommandText = @"UPDATE MatkapCap SET fiyat=@fiyat WHERE matkapCapi=@matkapCapi";
                        cmd.Parameters.AddWithValue("@fiyat", item.fiyat);
                        cmd.Parameters.AddWithValue("@matkapCapi", item.matkapCapi);
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }
        }
    }
}
