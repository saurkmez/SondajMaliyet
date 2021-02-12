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
    public partial class HesaplamaFrm : Form
    {
        List<Label> maliyetList = new List<Label>();
        List<ComboBox> zemincombo = new List<ComboBox>();
        List<ComboBox> matkapcombo = new List<ComboBox>();
        List<TextBox> derinlikListText = new List<TextBox>();
        List<ComboBox> delgiListText = new List<ComboBox>();
        double[] yipranmaList = new double[6];
        double[] mazotlist = new double[6];
        double[] nakliyelist = new double[6];
        double topmal = 0;
        double topbirimmal = 0;
        int topderin = 0;
        int topDelgiDerinlik = 0;
        int y = 65;
        int panelSayisi = 0;
        public HesaplamaFrm()
        {
            InitializeComponent();
        }


        public Panel GetPanel(int yAxis)
        {
            panelSayisi++;
            Panel panel = new Panel()
            {
                Name = panelSayisi.ToString(),
                Size = new Size(1000, 50),
                Location = new Point(50, yAxis),
                BackColor = Color.FromArgb(176, 229, 247),
                Tag = panelSayisi
            };

            FlowLayoutPanel flp = new FlowLayoutPanel()
            {
                Size = new Size(1000, 49),
                Dock= DockStyle.Left,
                BackColor = Color.FromArgb(176, 229, 247),
                FlowDirection = FlowDirection.LeftToRight
            };


            //////////////////////////////////////////////////////

            Label delmeTur = new Label()
            {
                Text = "Delme Çeşidi",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Tag = panelSayisi,
                Dock=DockStyle.Top

            };

            ComboBox delmeCesit = new ComboBox()
            {
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Size = new Size(60, 21),
                Dock=DockStyle.Bottom,
            };

            delmeCesit.Items.Add("Delgi");
            delmeCesit.Items.Add("Tarama");

            Panel pnlDelme = new Panel()
            {
                Margin = new Padding(10,0,0,0),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };
            pnlDelme.Controls.Add(delmeTur);
            pnlDelme.Controls.Add(delmeCesit);
            delmeCesit.Tag = delmeCesit.SelectedIndex;
            delgiListText.Add(delmeCesit);


            ////////////////////////////////////////////



            List<ZeminTipi> listzemin = new List<ZeminTipi>();
            listzemin.Add(new ZeminTipi() { zId = 0, tipAdi = "Tip Seçiniz" });
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"select * from ZeminTipi";
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listzemin.Add(new ZeminTipi()
                        {
                            zId = reader.GetInt32(0),
                            tipAdi = reader.GetString(1)
                        });
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }


            
            List<MatkapCap> listmatkap = new List<MatkapCap>();
            listmatkap.Add(new MatkapCap() { mId = 0 });
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
                        listmatkap.Add(new MatkapCap()
                        {
                            mId = reader.GetInt32(0),
                            matkapCapi = reader.GetDouble(1)
                        });
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    throw;
                }
            }

            //////////////////////////////////////////////////
            Panel pnlDerinlik = new Panel()
            {
                Margin = new Padding(10, 0, 0, 0),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };

            TextBox derinliktext = new TextBox()
            {
                Text = "1",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Size = new Size(148, 20),
                Dock= DockStyle.Bottom
            };
            derinlikListText.Add(derinliktext);
            Label derinlik = new Label()
            {
                Text = "DERİNLİK:",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
            };

            pnlDerinlik.Controls.Add(derinlik);
            pnlDerinlik.Controls.Add(derinliktext);

            ////////////////////////////////////////////////////
            
            Panel pnlMaliyet = new Panel()
            {
                Margin = new Padding(10, 0, 0, 0),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };

            Label maliyet = new Label()
            {
                Text = "MALİYET:",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
            };

            Label maliyetlbl = new Label()
            {
                Text = "",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = false,
                Anchor = AnchorStyles.None,
                BackColor = Color.White,
                Size = new Size(148, 20),
                Tag=panelSayisi,
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleLeft
            };
            maliyetList.Add(maliyetlbl);

            pnlMaliyet.Controls.Add(maliyet);
            pnlMaliyet.Controls.Add(maliyetlbl);


            ///////////////////////////////////////////
            Panel pnlinch = new Panel()
            {
                Size = new Size(35, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };
            Label inch = new Label()
            {
                Text = "inch",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Dock= DockStyle.Bottom,
            };
            pnlinch.Controls.Add(inch);
            //////////////////////////////////////////////////
            Panel pnltl = new Panel()
            {
                Size = new Size(30, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };

            Label tl = new Label()
            {
                Text = "TL",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.Bottom,
            };
            pnltl.Controls.Add(tl);

            //////////////////////////////////////////
            Panel pnlm = new Panel()
            {
                Size = new Size(20, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };
            Label metre = new Label()
            {
                Text = "m",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.Bottom
            };

            pnlm.Controls.Add(metre);
            ///////////////////////////////////////////////////
            Panel pnlMatkap = new Panel()
            {
                Margin = new Padding(10, 0, 0, 0),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };

            Label matkap = new Label()
            {
                Text = "MATKAP ÇAPI:",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
            };
            ComboBox matkapucu = new ComboBox()
            {
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Dock = DockStyle.Bottom,
                Size = new Size(142, 21)
            };
            matkapcombo.Add(matkapucu);

            pnlMatkap.Controls.Add(matkap);
            pnlMatkap.Controls.Add(matkapucu);

            ////////////////////////////////////////////////////////
            

            derinliktext.Tag = new maliyetHesapParameter()
            {
                panelsayisi = panelSayisi
            };
            matkapucu.DataSource = listmatkap;
            matkapucu.DisplayMember = "matkapCapi";
            matkapucu.ValueMember = "mId";


            /////////////////////////////////////////

            Label zemin = new Label()
            {
                Text = "ZEMİN TİPİ:",
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Margin = new Padding(3, 3, 3, 3),
                AutoSize = true,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.Top,
            };

            ComboBox zemintipi = new ComboBox()
            {
                Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold),
                Location = new Point(80, 18),
                Dock=DockStyle.Bottom
            };
            zemincombo.Add(zemintipi);
            zemintipi.Tag = new maliyetHesapParameter()
            {
                derinlik = Convert.ToInt32(derinliktext.Text),
                matkapId = matkapucu.SelectedIndex,
                panelsayisi = panelSayisi
            };
            zemintipi.DataSource = listzemin;
            zemintipi.DisplayMember = "tipAdi";
            zemintipi.ValueMember = "zId";

            Panel pnlZemin = new Panel()
            {
                Margin = new Padding(10, 0, 0, 0),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };
            pnlZemin.Controls.Add(zemin);
            pnlZemin.Controls.Add(zemintipi);

            //////////////////////////////////////////////////

            matkapucu.Tag = new maliyetHesapParameter()
            {
                derinlik = Convert.ToInt32(derinliktext.Text),
                zeminId = zemintipi.SelectedIndex,
                panelsayisi = panelSayisi
            };

            /////////////////////////////////////////////


            Panel pnlSil = new Panel()
            {
                Margin = new Padding(10, 0, 0, 0),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(176, 229, 247)
            };

            Button btnSil = new Button()
            {
                Text = "Sil",
                Dock = DockStyle.Bottom,
                BackColor = Color.Red,
                Tag = panel
            };
            btnSil.Click += BtnSil_Click;

            pnlSil.Controls.Add(btnSil);

            flp.Controls.Add(pnlDelme);
            flp.Controls.Add(pnlZemin);
            flp.Controls.Add(pnlMatkap);
            flp.Controls.Add(pnlinch);
            flp.Controls.Add(pnlDerinlik);
            flp.Controls.Add(pnlm);
            flp.Controls.Add(pnlMaliyet);
            flp.Controls.Add(pnltl);
            flp.Controls.Add(pnlSil);

            panel.Controls.Add(flp);
            return panel;

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if(panelSayisi == 1)
            {
                MessageBox.Show("Minimum Adete ulaşıldı!!");
                return;
            }
            Button btn = (Button)sender;
            Panel pnl = (Panel)btn.Tag;
            int panelsira = (int)pnl.Tag;
            panelsira--;

            maliyetList.RemoveAt(panelsira);
            zemincombo.RemoveAt(panelsira);
            matkapcombo.RemoveAt(panelsira);
            derinlikListText.RemoveAt(panelsira);
            yipranmaList = yipranmaList.Where(x => x != panelsira).ToArray();
            mazotlist = yipranmaList.Where(x => x != panelsira).ToArray();
            nakliyelist = yipranmaList.Where(x => x != panelsira).ToArray();

            int veri = 0;
            int silinecek = 0;
            int i = 0;
            panelsira++;
            foreach (Control item in panel2.Controls)
            {
                int.TryParse(item.Name, out veri);

                if (item.Name == panelsira.ToString())
                {
                    silinecek = i;   
                }
                else if (veri > panelsira)
                {
                    item.Name = (veri - 1).ToString();
                    item.Tag = (veri - 1);
                    item.Location = new Point(50, item.Location.Y - 50);
                }
                i++;
            }
            panel2.Controls.RemoveAt(silinecek);

            panelSayisi--;
            y = y - 50;

        }

        public void Hesapla()
        {
            topmal = 0;
            topbirimmal = 0;
            topderin = 0;
            for (int i = 0; i < panelSayisi; i++)
            {
                Label maliyet = maliyetList[i];
                if (derinlikListText[i].Text == "")
                {
                    derinlikListText[i].Text = "0";
                }
                maliyet.Text = maliyetHesapla(zemincombo[i].SelectedIndex, matkapcombo[i].SelectedIndex,
                    Convert.ToInt32(derinlikListText[i].Text), i).ToString();
                topmal += Convert.ToDouble(maliyet.Text);
                if(delgiListText[i].SelectedIndex == 0)
                {
                    topderin += Convert.ToInt32(derinlikListText[i].Text);
                }
            }
            lblTopMaliyet.Text = topmal.ToString("0.00") + " TL";
            topbirimmal = (topmal / topderin);
            lblMetreMaliyet.Text = topbirimmal.ToString("0.00") + " TL";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (y < 316)
            {
                panel2.Controls.Add(GetPanel(y));
                y += 50;
            }
            else
            {
                MessageBox.Show("Daha fazla ekleme yapılamaz!!");
            }
        }

        private void HesaplamaFrm_Load(object sender, EventArgs e)
        {
            panel2.Controls.Add(GetPanel(y));
            y += 50;
        }

        public double maliyetHesapla(int zId, int mId, int derinlik,int index)
        {
            double toplamMaliyetHesabı = 0;
            try
            {
                int maxDerinlik = 0;
                using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
                {
                    try
                    {
                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand(con);
                        cmd.CommandText = @"select maxDerinlik from Yipranma where zeminId = @zeminId and matkapId=@matkapId";
                        cmd.Parameters.AddWithValue("@zeminId", zId);
                        cmd.Parameters.AddWithValue("@matkapId", mId);
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            maxDerinlik = reader.GetInt32(0);
                        }
                        con.Close();
                    }
                    catch (Exception)
                    {
                        con.Close();
                        throw;
                    }
                }
                double fiyat = 0;
                int gunlukIs = 0;
                using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
                {
                    try
                    {
                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand(con);
                        cmd.CommandText = @"select fiyat from MatkapCap where mId=@matkapId";
                        cmd.Parameters.AddWithValue("@matkapId", mId);
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fiyat = reader.GetDouble(0);
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
                        cmd.CommandText = @"select gunlukIs from ZeminTipi where zId=@zId";
                        cmd.Parameters.AddWithValue("@zId", zId);
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            gunlukIs = reader.GetInt32(0);
                        }
                        con.Close();
                    }
                    catch (Exception)
                    {
                        con.Close();
                        throw;
                    }
                }


                double yipranmaUcreti = (derinlik * fiyat / maxDerinlik) * Convert.ToDouble(GetRate("USD"));

                yipranmaList[index] = yipranmaUcreti;

                double calisilacakGunSayisi = Convert.ToDouble(derinlik) / Convert.ToDouble(gunlukIs);


                /////////////////////////////////////////////////////////////////////////////////////////


                IscilikMaliyeti iscilikMaliyeti = new IscilikMaliyeti();
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
                            iscilikMaliyeti.kId = reader.GetInt32(0);
                            iscilikMaliyeti.kisiSayisi = reader.GetInt32(1);
                            iscilikMaliyeti.maas = reader.GetDouble(2);
                        }
                        con.Close();
                    }
                    catch (Exception)
                    {
                        con.Close();
                        throw;
                    }
                }

                double hesapiscilikMaliyeti = (iscilikMaliyeti.maas / 25)* calisilacakGunSayisi;

                /////////////////////////////////////////////////////////////////////////////////////////

                MazotGiderleri mazotGiderleri = new MazotGiderleri();
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
                            mazotGiderleri.mazotId = reader.GetInt32(0);
                            mazotGiderleri.tankHacmi = reader.GetInt32(1);
                            mazotGiderleri.birimFiyat = reader.GetDouble(2);
                        }
                        con.Close();
                    }
                    catch (Exception)
                    {
                        con.Close();
                        throw;
                    }
                }

                double mazotHesabı = mazotGiderleri.tankHacmi * mazotGiderleri.birimFiyat* calisilacakGunSayisi;
                mazotlist[index] = mazotHesabı;

                /////////////////////////////////////////////////////////////////////////////////////////

                Nakliye nakliyeGider = new Nakliye();
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
                            nakliyeGider.nId = reader.GetInt32(0);
                            nakliyeGider.gunlukGider = reader.GetInt32(1);
                        }
                        con.Close();
                    }
                    catch (Exception)
                    {
                        con.Close();
                        throw;
                    }
                }

                double nakliyeHesabı = nakliyeGider.gunlukGider * calisilacakGunSayisi;
                nakliyelist[index] = nakliyeHesabı;

                toplamMaliyetHesabı = nakliyeHesabı + mazotHesabı + hesapiscilikMaliyeti + yipranmaUcreti;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            return toplamMaliyetHesabı;
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

        private void button2_Click(object sender, EventArgs e)
        {
            double yipranma = 0;
            double mazot = 0;
            double nakliye = 0;
            foreach (var item in yipranmaList)
            {
                yipranma += item;
            }
            foreach (var item in mazotlist)
            {
                mazot += item;
            }
            foreach (var item in nakliyelist)
            {
                nakliye += item;
            }
            string promptValue = Prompt.ShowDialog("FİRMA İSMİ", "KAYIT OLUŞTURMA");

            double iscilikgider = 0;
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd1 = new SQLiteCommand(con);
                    cmd1.CommandText = @"select * from IscilikMaliyeti order by kId DESC LIMIT 1";
                    SQLiteDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        iscilikgider = reader.GetDouble(2) / 20;
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
                    cmd.CommandText = @"insert into TumGiderler(giderAdi,topMaliyet,metreMaliyet, sondajDerinligi, dovizKuru, tarih, matkapGider,iscilikGider,mazotGider,nakliyeGider) 
                                        values(@giderAdi,@topMaliyet,@metreMaliyet,@sondajDerinligi,@dovizKuru,@tarih,@matkapGider,@iscilikGider,@mazotGider,@nakliyeGider)";
                    cmd.Parameters.AddWithValue("@giderAdi",promptValue);
                    cmd.Parameters.AddWithValue("@topMaliyet",topmal);
                    cmd.Parameters.AddWithValue("@metreMaliyet",topbirimmal);
                    cmd.Parameters.AddWithValue("@sondajDerinligi",topderin);
                    cmd.Parameters.AddWithValue("@dovizKuru",GetRate("USD"));
                    cmd.Parameters.AddWithValue("@tarih",DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@matkapGider", yipranma);
                    cmd.Parameters.AddWithValue("@iscilikGider", iscilikgider);
                    cmd.Parameters.AddWithValue("@mazotGider",mazot);
                    cmd.Parameters.AddWithValue("@nakliyeGider",nakliye);
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

        private void button3_Click(object sender, EventArgs e)
        {
            yipranmaList = new double[6];
            mazotlist = new double[6];
            nakliyelist = new double[6];
            Hesapla();
        }
    }
    public class maliyetHesapParameter{
        public int zeminId { get; set; }
        public int matkapId { get; set; }
        public int derinlik { get; set; }
        public int panelsayisi { get; set; }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "TAMAM", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
