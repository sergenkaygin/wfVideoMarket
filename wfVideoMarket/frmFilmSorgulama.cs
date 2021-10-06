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
    public partial class frmFilmSorgulama : Form
    {
        public frmFilmSorgulama()
        {
            InitializeComponent();
        }

        private void frmFilmSorgulama_Load(object sender, EventArgs e)
        {
            cFilm f = new cFilm();
            f.FilmleriGoster(lvFilmler);

            cFilmTuru ft = new cFilmTuru();
            ft.FilmTurleriGoster(cbFilmTurleri);
            cbFilmTurleri.Items.Insert(0, "Tüm Türler");
            cbFilmTurleri.SelectedIndex = 0;
        }

        private void txtAdaGore_TextChanged(object sender, EventArgs e)
        {
            FilmleriGetir();
        }

        private void cbFilmTurleri_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilmleriGetir();
            
        }

        private void FilmleriGetir()
        {
            string tur="";
            cFilm f = new cFilm();
            if (cbFilmTurleri.SelectedItem.ToString()=="Tüm Türler")
            {
                tur = "";
            }
            else
            {
                tur = cbFilmTurleri.SelectedItem.ToString();
            }
            f.FilmleriGosterByDetayliSorgulama(txtAdaGore.Text, tur, txtYonetmeneGore.Text, txtOyuncularaGore.Text, lvFilmler);
        }

        private void txtYonetmeneGore_TextChanged(object sender, EventArgs e)
        {
            FilmleriGetir();
        }

        private void txtOyuncularaGore_TextChanged(object sender, EventArgs e)
        {
            FilmleriGetir();
        }

        private void lvFilmler_DoubleClick(object sender, EventArgs e)
        {
            cGenel.filmNo = Convert.ToInt32(lvFilmler.SelectedItems[0].SubItems[0].Text);
            cGenel.filmAdi = lvFilmler.SelectedItems[0].SubItems[1].Text;
            this.Close();
        }
    }
}
