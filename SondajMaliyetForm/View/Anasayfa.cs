using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SondajMaliyetForm.View
{
    public partial class Anasayfa : Form
    {
        splashFrm splash;
        HesaplamaFrm hesaplamaFrm = new HesaplamaFrm();
        AyarlarFrm ayarlarFrm = new AyarlarFrm();
        public Anasayfa(splashFrm _splash)
        {
            splash = _splash;
            InitializeComponent();

            this.Opacity = 0.0;
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            Task.Delay(3000).Wait();
            splash.Close();
            splash.Dispose();
            this.Opacity = 1.0;
            this.IsMdiContainer = true;
            hesaplamaFrm.MdiParent = this;
            hesaplamaFrm.Dock = DockStyle.Fill;
            pnlAna.Controls.Add(hesaplamaFrm);
            hesaplamaFrm.Show();
        }

        private void ayarParametre_Click(object sender, EventArgs e)
        {
            pnlAna.Controls.Clear();
            this.IsMdiContainer = true;
            ayarlarFrm.MdiParent = this;
            ayarlarFrm.Dock = DockStyle.Fill;
            pnlAna.Controls.Add(ayarlarFrm);
            ayarlarFrm.Show();
        }

        private void hesaplar_Click(object sender, EventArgs e)
        {
            pnlAna.Controls.Clear();
            this.IsMdiContainer = true;
            hesaplamaFrm.MdiParent = this;
            hesaplamaFrm.Dock = DockStyle.Fill;
            pnlAna.Controls.Add(hesaplamaFrm);
            hesaplamaFrm.Show();
        }
    }
}
