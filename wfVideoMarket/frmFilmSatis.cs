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
    public partial class frmFilmSatis : Form
    {
        public frmFilmSatis()
        {
            InitializeComponent();
        }

        private void dtpTarih_ValueChanged(object sender, EventArgs e)
        {
            txtTarih.Text = dtpTarih.Value.ToShortDateString();
        }

        private void frmFilmSatis_Load(object sender, EventArgs e)
        {
            cFilmSatis fs = new cFilmSatis();
            fs.SatislariGetir(lvSatislar, txtToplamAdet, txtToplamTutar);
            txtTarih.Text = DateTime.Now.ToShortDateString();
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
            txtFiyat.Text = "0";
            txtAdet.Text = "1";
            txtTutar.Text = "0";
            txtFiyat.Focus();
        }

        private void txtFiyat_TextChanged(object sender, EventArgs e)
        {
            if (txtAdet.Text.Trim() != "" && txtFiyat.Text.Trim() != "")
            {
                try
                {
                    txtTutar.Text = (Convert.ToDecimal(txtFiyat.Text) * Convert.ToInt32(txtAdet.Text)).ToString();
                }
                catch (Exception ex)
                {
                    txtTutar.Text = "0";
                }
            }
        }

        private void txtAdet_TextChanged(object sender, EventArgs e)
        {
            if (txtAdet.Text.Trim() != "" && txtFiyat.Text.Trim() != "")
            {
                try
                {
                    txtTutar.Text = (Convert.ToDecimal(txtFiyat.Text) * Convert.ToInt32(txtAdet.Text)).ToString();
                }
                catch (Exception ex)
                {
                    txtTutar.Text = "0";
                }
            }
        }

        private void btnMusteriBul_Click(object sender, EventArgs e)
        {
            frmMusteriSorgulama frm = new frmMusteriSorgulama();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            txtMusteriNo.Text = cGenel.musteriNo.ToString();
            txtMusteri.Text = cGenel.musteri;
        }

        private void btnFilmBul_Click(object sender, EventArgs e)
        {
            frmFilmSorgulama frm = new frmFilmSorgulama();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            txtFilmNo.Text = cGenel.filmNo.ToString();
            txtFilm.Text = cGenel.filmAdi;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtMusteri.Text.Trim() != "" && txtFilm.Text.Trim() != "")
            {
                if (txtTutar.Text == "0")
                    MessageBox.Show("İşlem Tutarı 0 (Sıfır) olamaz!");
                else
                {
                    cFilmSatis fs = new cFilmSatis();
                    fs.Tarih = Convert.ToDateTime(txtTarih.Text);
                    fs.FilmNo = Convert.ToInt32(txtFilmNo.Text);
                    fs.MusteriNo = Convert.ToInt32(txtMusteriNo.Text);
                    fs.BirimFiyat = Convert.ToDecimal(txtFiyat.Text);
                    fs.Adet = Convert.ToInt32(txtAdet.Text);
                    if (fs.SatisEkle(fs))
                    {
                        MessageBox.Show("Film Satış Bilgisi eklendi");
                        cFilm f = new cFilm();
                        if (f.StokGuncelleFromSatisEkle(fs.FilmNo, fs.Adet))
                        {
                            MessageBox.Show("Film Stok Miktarı güncellendi.");
                            btnKaydet.Enabled = false;
                            fs.SatislariGetir(lvSatislar, txtToplamAdet, txtToplamTutar);
                            Temizle();
                        }
                    }
                    else { MessageBox.Show("Satış Ekleme gerçekleşmedi"); }
                }
            }
            else { MessageBox.Show("Müşteri ve film boş bırakılamaz.", "DİKKAT! Eksik Bilgi!"); }
        }

        private void lvSatislar_DoubleClick(object sender, EventArgs e)
        {
            txtSatisNo.Text = lvSatislar.SelectedItems[0].SubItems[0].Text;
            txtMusteri.Text = lvSatislar.SelectedItems[0].SubItems[3].Text;
            txtFilm.Text = lvSatislar.SelectedItems[0].SubItems[2].Text;
            txtTarih.Text = lvSatislar.SelectedItems[0].SubItems[1].Text;
            txtFiyat.Text = lvSatislar.SelectedItems[0].SubItems[4].Text;
            txtAdet.Text = lvSatislar.SelectedItems[0].SubItems[5].Text;
            txtMusteriNo.Text = lvSatislar.SelectedItems[0].SubItems[8].Text;
            txtFilmNo.Text = lvSatislar.SelectedItems[0].SubItems[7].Text;
            btnSil.Enabled = true;
            btnDegistir.Enabled = true;
            btnKaydet.Enabled = false;

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "SİLİNSİN Mİ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                cFilmSatis fs = new cFilmSatis();
                if (fs.SatisSil(Convert.ToInt32(txtSatisNo.Text)))
                {
                    MessageBox.Show("Film Satış bilgileri silindi.");
                        cFilm f = new cFilm();
                        if (f.StokGuncelleFromSatisSil(Convert.ToInt32(txtFilmNo.Text), Convert.ToInt32(txtAdet.Text)))
                        {
                            MessageBox.Show("Film Stok Miktarı güncellendi.");
                            fs.SatislariGetir(lvSatislar, txtToplamAdet, txtToplamTutar);
                            Temizle();
                            btnDegistir.Enabled = false;
                            btnSil.Enabled = false;
                        }
                }
            }
        }
    }
}
