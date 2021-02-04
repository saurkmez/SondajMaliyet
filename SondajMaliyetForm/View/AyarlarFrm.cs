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
            string url = string.Empty;
            var date = DateTime.Now;
            if (date.Date == DateTime.Today)
                url = "http://www.tcmb.gov.tr/kurlar/today.xml";
            else
                url = string.Format("http://www.tcmb.gov.tr/kurlar/{0}{1}/{2}{1}{0}.xml", date.Year, addZero(date.Month), addZero(date.Day));

            System.Xml.Linq.XDocument document = System.Xml.Linq.XDocument.Load(url);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var result = document.Descendants("Currency")
            .Where(v => v.Element("ForexBuying") != null && v.Element("ForexBuying").Value.Length > 0)
            .Select(v => new Currency
            {
                Code = v.Attribute("Kod").Value,
                Rate = decimal.Parse(v.Element("ForexBuying").Value.Replace('.', ','))
            }).ToList();
            return result.FirstOrDefault(s => s.Code == code).Rate;
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
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }
        }

        private void nakliyeGider_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
