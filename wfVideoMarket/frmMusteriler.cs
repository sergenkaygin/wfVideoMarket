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
    public partial class frmMusteriler : Form
    {
        public frmMusteriler()
        {
            InitializeComponent();
        }

        private void frmMusteriler_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            cMusteri m = new cMusteri();
            m.MusterileriGoster(lvMusteriler);
        }

        private void lvMusteriler_DoubleClick(object sender, EventArgs e)
        {
            txtID.Text = lvMusteriler.SelectedItems[0].SubItems[0].Text;
            txtAdi.Text = lvMusteriler.SelectedItems[0].SubItems[1].Text;
            txtSoyadi.Text = lvMusteriler.SelectedItems[0].SubItems[2].Text;
            txtTelefon.Text = lvMusteriler.SelectedItems[0].SubItems[3].Text;
            txtAdres.Text = lvMusteriler.SelectedItems[0].SubItems[4].Text;
            btnDegistir.Enabled = true;
            btnSil.Enabled = true;
            btnKaydet.Enabled = false;
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            cMusteri m = new cMusteri();
            m.MusteriNo = Convert.ToInt32(txtID.Text);
            m.MusteriAd = txtAdi.Text;
            m.MusteriSoyad = txtSoyadi.Text;
            m.Telefon = txtTelefon.Text;
            m.Adres = txtAdres.Text;
            bool Sonuc = m.MusteriGuncelle(m);
            if (Sonuc)
            {
                MessageBox.Show("Bilgiler güncellendi.");
                m.MusterileriGoster(lvMusteriler);
                Temizle();
            }
            else { MessageBox.Show("Güncelleme yapılamadı!", "DİKKAT!"); }
        }
        private void Temizle()
        {
            txtID.Clear();
            txtAdi.Clear();
            txtSoyadi.Clear();
            txtTelefon.Clear();
            txtAdres.Clear();
            btnDegistir.Enabled = false;
            btnSil.Enabled = false;
            txtAdi.Focus();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek İstiyor musunuz?", "SİLİNSİN Mİ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                cMusteri m = new cMusteri();
                if (m.MusteriSil(Convert.ToInt32(txtID.Text)))
                {
                    MessageBox.Show("Müşteri bilgileri silindi.");
                    m.MusterileriGoster(lvMusteriler);
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
            if (txtAdi.Text.Trim() != "" && txtSoyadi.Text.Trim() != "")
            {
                cMusteri m = new cMusteri();
                if (m.MusteriVarmi(txtAdi.Text, txtSoyadi.Text))
                {
                    MessageBox.Show("Zaten kayıtlı!", "DİKKAT!");
                    txtAdi.Focus();
                }
                else
                {
                    m.MusteriAd = txtAdi.Text;
                    m.MusteriSoyad = txtSoyadi.Text;
                    m.Telefon = txtTelefon.Text;
                    m.Adres = txtAdres.Text;                 
                    if (m.MusteriEkle(m))
                    {
                        MessageBox.Show("Müşteri bilgileri eklendi.");
                        m.MusterileriGoster(lvMusteriler);
                        Temizle();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt işlemi gerçekleşmedi!", "DİKKAT!");
                    }
                }
            }
        }

        private void txtAdaGore_TextChanged(object sender, EventArgs e)
        {
            cMusteri cm = new cMusteri();
            cm.MusterileriGosterByAdaGore(txtAdaGore.Text, lvMusteriler);
        }
    }
}
