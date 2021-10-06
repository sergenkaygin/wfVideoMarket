using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfVideoMarket
{
    public partial class frmFilmler : Form
    {
        public frmFilmler()
        {
            InitializeComponent();
        }

        private void frmFilmler_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            cFilm f = new cFilm();
            f.FilmleriGoster(lvFilmler);

            cFilmTuru ft = new cFilmTuru();
            ft.FilmTurleriGoster(cbFilmTurleri);
        }

        private void cbFilmTurleri_SelectedIndexChanged(object sender, EventArgs e)
        {
            cFilmTuru ft = (cFilmTuru)cbFilmTurleri.SelectedItem;            
            txtFilmTuru.Text = ft.TurAd;
            txtTurNo.Text = Convert.ToString(ft.FilmTurNo);       
           
        }

        private void lvFilmler_DoubleClick(object sender, EventArgs e)
        {
            txtID.Text = lvFilmler.SelectedItems[0].SubItems[0].Text;
            txtFilmAdi.Text = lvFilmler.SelectedItems[0].SubItems[1].Text;
            txtFilmTuru.Text = lvFilmler.SelectedItems[0].SubItems[2].Text;
            txtYonetmen.Text = lvFilmler.SelectedItems[0].SubItems[3].Text;
            txtOyuncular.Text = lvFilmler.SelectedItems[0].SubItems[4].Text;
            txtOzet.Text = lvFilmler.SelectedItems[0].SubItems[5].Text;
            txtMiktar.Text = lvFilmler.SelectedItems[0].SubItems[6].Text;
            txtTurNo.Text = lvFilmler.SelectedItems[0].SubItems[7].Text;
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
            btnKaydet.Enabled = false;
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            cFilm f = new cFilm();
            f.FilmNo = Convert.ToInt32(txtID.Text);
            f.FilmAd = txtFilmAdi.Text;
            f.FilmTurNo = Convert.ToInt32(txtTurNo.Text);
            f.Yonetmen = txtYonetmen.Text;
            f.Oyuncular = txtOyuncular.Text;
            f.Ozet = txtOzet.Text;
            f.Miktar = Convert.ToInt32(txtMiktar.Text);
            bool Sonuc = f.FilmGuncelle(f);
            if (Sonuc)
            {
                MessageBox.Show("Bilgiler güncellendi.");
                f.FilmleriGoster(lvFilmler);
                Temizle();
            }
            else { MessageBox.Show("Güncelleme yapılamadı!", "DİKKAT!"); }
        }
        private void Temizle()
        {
            txtID.Clear();
            txtFilmAdi.Clear();
            txtYonetmen.Clear();
            txtOyuncular.Clear();
            txtOzet.Clear();
            txtMiktar.Clear();
            btnDegistir.Enabled = false;
            btnSil.Enabled = false;
            txtFilmAdi.Focus();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "SİLİNSİN Mİ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                cFilm f = new cFilm();
                if (f.FilmSil(Convert.ToInt32(txtID.Text)))
                {
                    MessageBox.Show("Film bilgileri silindi.");
                    f.FilmleriGoster(lvFilmler);
                    Temizle();
                }
            }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = true;
            btnDegistir.Enabled = false;
            btnSil.Enabled = false;
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtFilmAdi.Text.Trim() != "" && txtYonetmen.Text.Trim() != "")
            {
                cFilm f = new cFilm();
                if (f.FilmVarmi(txtFilmAdi.Text, txtYonetmen.Text))
                {
                    MessageBox.Show("Zaten kayıtlı!", "DİKKAT!");
                    txtFilmAdi.Focus();
                }
                else
                {
                    f.FilmAd = txtFilmAdi.Text;
                    f.FilmTurNo = Convert.ToInt32(txtTurNo.Text);
                    f.Yonetmen = txtYonetmen.Text;
                    f.Oyuncular = txtOyuncular.Text;
                    f.Ozet = txtOzet.Text;
                    f.Miktar = Convert.ToInt32(txtMiktar.Text);
                    if (f.FilmEkle(f))
                    {
                        MessageBox.Show("Film bilgileri eklendi.");
                        f.FilmleriGoster(lvFilmler);
                        Temizle();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt işlemi gerçekleşmedi!", "DİKKAT!");
                    }
                }
            }
            else { MessageBox.Show("Film Adı ve Yönetmeni boş geçilemez!", "DİKKAT!"); }
        }

        private void txtAdaGore_TextChanged(object sender, EventArgs e)
        {
            cFilm f = new cFilm();
            f.FilmleriGosterByAdaGore(txtAdaGore.Text, lvFilmler);
        }
    }
}
