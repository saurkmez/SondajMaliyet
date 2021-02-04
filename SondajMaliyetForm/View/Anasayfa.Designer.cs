
namespace SondajMaliyetForm.View
{
    partial class Anasayfa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlAna = new System.Windows.Forms.Panel();
            this.ayarParametre = new System.Windows.Forms.Button();
            this.hesaplar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(229)))), ((int)(((byte)(247)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.hesaplar);
            this.panel1.Controls.Add(this.ayarParametre);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1119, 72);
            this.panel1.TabIndex = 0;
            // 
            // pnlAna
            // 
            this.pnlAna.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(158)))), ((int)(((byte)(188)))));
            this.pnlAna.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAna.Location = new System.Drawing.Point(0, 72);
            this.pnlAna.Name = "pnlAna";
            this.pnlAna.Size = new System.Drawing.Size(1119, 581);
            this.pnlAna.TabIndex = 1;
            // 
            // ayarParametre
            // 
            this.ayarParametre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.ayarParametre.FlatAppearance.BorderSize = 0;
            this.ayarParametre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ayarParametre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ayarParametre.Location = new System.Drawing.Point(31, 18);
            this.ayarParametre.Name = "ayarParametre";
            this.ayarParametre.Size = new System.Drawing.Size(139, 38);
            this.ayarParametre.TabIndex = 0;
            this.ayarParametre.Text = "Ayarlar";
            this.ayarParametre.UseVisualStyleBackColor = false;
            this.ayarParametre.Click += new System.EventHandler(this.ayarParametre_Click);
            // 
            // hesaplar
            // 
            this.hesaplar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.hesaplar.FlatAppearance.BorderSize = 0;
            this.hesaplar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hesaplar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.hesaplar.Location = new System.Drawing.Point(176, 18);
            this.hesaplar.Name = "hesaplar";
            this.hesaplar.Size = new System.Drawing.Size(132, 38);
            this.hesaplar.TabIndex = 1;
            this.hesaplar.Text = "Hesaplama";
            this.hesaplar.UseVisualStyleBackColor = false;
            this.hesaplar.Click += new System.EventHandler(this.hesaplar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SondajMaliyetForm.Properties.Resources.unnamed;
            this.pictureBox1.Location = new System.Drawing.Point(881, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 50);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Anasayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 653);
            this.Controls.Add(this.pnlAna);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(1135, 692);
            this.MinimumSize = new System.Drawing.Size(1135, 692);
            this.Name = "Anasayfa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SONDAJ MALİYET HESAPLAMA v0.1";
            this.Load += new System.EventHandler(this.Anasayfa_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button hesaplar;
        private System.Windows.Forms.Button ayarParametre;
        private System.Windows.Forms.Panel pnlAna;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}