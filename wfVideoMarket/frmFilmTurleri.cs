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
    public partial class frmFilmTurleri : Form
    {
        public frmFilmTurleri()
        {
            InitializeComponent();
        }

        private void frmFilmTurleri_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            cFilmTuru ft = new cFilmTuru();
            ft.FilmTurleriGoster(lvFilmTurleri);
        }

        private void lvFilmTurleri_DoubleClick(object sender, EventArgs e)
        {
            txtID.Text = lvFilmTurleri.SelectedItems[0].SubItems[0].Text;
            txtFilmTuru.Text = lvFilmTurleri.SelectedItems[0].SubItems[1].Text;
            txtAciklama.Text = lvFilmTurleri.SelectedItems[0].SubItems[2].Text;
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
            btnKaydet.Enabled = false;
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = true;
            btnDegistir.Enabled = false;
            btnSil.Enabled = false;
            Temizle();
        }
        private void Temizle()
        {
            txtID.Clear();
            txtFilmTuru.Clear();
            txtAciklama.Clear();
            btnDegistir.Enabled = false;
            btnSil.Enabled = false;
            txtFilmTuru.Focus();
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            cFilmTuru ft = new cFilmTuru();
            bool Sonuc = ft.FilmTuruGuncelle(Convert.ToInt32(txtID.Text), txtFilmTuru.Text, txtAciklama.Text);
            if (Sonuc)
            {
                MessageBox.Show("Bilgiler güncellendi.");
                ft.FilmTurleriGoster(lvFilmTurleri);
                Temizle();
            }
            else { MessageBox.Show("Güncelleme yapılamadı!", "DİKKAT!"); }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "SİLİNSİN Mİ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                cFilmTuru ft = new cFilmTuru();
                if(ft.FilmTuruSil(Convert.ToInt32(txtID.Text)))
                {
                    MessageBox.Show("Film Türü silindi.");
                    ft.FilmTurleriGoster(lvFilmTurleri);
                    Temizle();
                }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtFilmTuru.Text.Trim() != "")
            {
                cFilmTuru ft = new cFilmTuru();
                if (ft.FilmTuruVarmi(txtFilmTuru.Text))
                {
                    MessageBox.Show("Zaten kayıtlı!", "DİKKAT!");
                    txtFilmTuru.Focus();
                }
                else
                {
                    ft.TurAd = txtFilmTuru.Text;
                    ft.Aciklama = txtAciklama.Text;
                    if (ft.FilmTuruEkle(ft))
                    {
                        MessageBox.Show("Film Türü eklendi.");
                        ft.FilmTurleriGoster(lvFilmTurleri);
                        Temizle();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt işlemi gerçekleşmedi!", "DİKKAT!");
                    }
                }



            }
        }
    }
}
