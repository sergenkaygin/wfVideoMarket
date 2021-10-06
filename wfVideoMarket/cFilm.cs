using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfVideoMarket
{
    class cFilm
    {
        private int _filmNo;
        private string _filmAd;
        private int _filmTurNo;
        private string _yonetmen;
        private string _oyuncular;
        private string _ozet;
        private int _miktar;

        #region Properties
        public int FilmNo
        {
            get { return _filmNo; }
            set { _filmNo = value; }
        }

        public string FilmAd
        {
            get { return _filmAd; }
            set { _filmAd = value; }
        }

        public int FilmTurNo
        {
            get { return _filmTurNo; }
            set { _filmTurNo = value; }
        }

        public string Yonetmen
        {
            get { return _yonetmen; }
            set { _yonetmen = value; }
        }

        public string Oyuncular
        {
            get { return _oyuncular; }
            set { _oyuncular = value; }
        }

        public string Ozet
        {
            get { return _ozet; }
            set { _ozet = value; }
        }

        public int Miktar
        {
            get { return _miktar; }
            set { _miktar = value; }
        } 
        #endregion

        public void FilmleriGoster(ListView liste)
        {
            liste.Items.Clear();
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Select FilmNo, FilmAd, TurAd, Yonetmen, Oyuncular, Ozet, Miktar, Filmler.FilmTurNo from Filmler inner join FilmTurleri on Filmler.FilmTurNo = FilmTurleri.FilmTurNo where Varmi=1", conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                liste.Items.Add(dr[0].ToString());
                liste.Items[i].SubItems.Add(dr[1].ToString());
                liste.Items[i].SubItems.Add(dr[2].ToString());
                liste.Items[i].SubItems.Add(dr[3].ToString());
                liste.Items[i].SubItems.Add(dr[4].ToString());
                liste.Items[i].SubItems.Add(dr[5].ToString());
                liste.Items[i].SubItems.Add(dr[6].ToString());
                liste.Items[i].SubItems.Add(dr[7].ToString());
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public void FilmleriGosterByAdaGore(string AdaGore, ListView liste)
        {
            liste.Items.Clear();
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Select FilmNo, FilmAd, TurAd, Yonetmen, Oyuncular, Ozet, Miktar, Filmler.FilmTurNo from Filmler inner join FilmTurleri on Filmler.FilmTurNo = FilmTurleri.FilmTurNo where FilmAd like @FilmAd + '%' and Varmi=1", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = AdaGore;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                liste.Items.Add(dr[0].ToString());
                liste.Items[i].SubItems.Add(dr[1].ToString());
                liste.Items[i].SubItems.Add(dr[2].ToString());
                liste.Items[i].SubItems.Add(dr[3].ToString());
                liste.Items[i].SubItems.Add(dr[4].ToString());
                liste.Items[i].SubItems.Add(dr[5].ToString());
                liste.Items[i].SubItems.Add(dr[6].ToString());
                liste.Items[i].SubItems.Add(dr[7].ToString());
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public bool FilmGuncelle(cFilm f)
        {
            bool Sonuc = false;
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Update Filmler Set FilmAd=@FilmAd, FilmTurNo=@FilmTurNo, Yonetmen=@Yonetmen,Oyuncular=@Oyuncular,Ozet=@Ozet,Miktar=@Miktar where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = f._filmAd;
            comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = f._filmTurNo;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = f._yonetmen;
            comm.Parameters.Add("@Oyuncular", SqlDbType.VarChar).Value = f._oyuncular;
            comm.Parameters.Add("@Ozet", SqlDbType.VarChar).Value = f._ozet;
            comm.Parameters.Add("@Miktar", SqlDbType.Int).Value = f._miktar;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = f._filmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try  //hata oluşabilecek satır try içine yazılır.
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex) //hata oluşursa catch bloğuna düşer.
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }  //hata olsun olmasın programın çalışması finally bloğundan devam eder.
            return Sonuc;
        }
        public bool FilmSil(int FilmNo)
        {
            bool Sonuc = false;
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Update Filmler Set Varmi=0 where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try  
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex) 
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }  
            return Sonuc;
        }
        public bool FilmEkle(cFilm f)
        {
            bool Sonuc = false;
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Insert into Filmler (FilmAd, FilmTurNo, Yonetmen,Oyuncular,Ozet,Miktar) values (@FilmAd, @FilmTurNo, @Yonetmen, @Oyuncular, @Ozet, @Miktar)", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = f._filmAd;
            comm.Parameters.Add("@FilmTurNo", SqlDbType.Int).Value = f._filmTurNo;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = f._yonetmen;
            comm.Parameters.Add("@Oyuncular", SqlDbType.VarChar).Value = f._oyuncular;
            comm.Parameters.Add("@Ozet", SqlDbType.VarChar).Value = f._ozet;
            comm.Parameters.Add("@Miktar", SqlDbType.Int).Value = f._miktar;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try  //hata oluşabilecek satır try içine yazılır.
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex) //hata oluşursa catch bloğuna düşer.
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }  //hata olsun olmasın programın çalışması finally bloğundan devam eder.
            return Sonuc;
        }
        public bool FilmVarmi(string FilmAdi, string Yonetmen)
        {
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Select FilmAd from Filmler where FilmAd=@FilmAd and Yonetmen=@Yonetmen and Varmi=1", conn);
            comm.Parameters.Add("@FilmAd", SqlDbType.VarChar).Value = FilmAdi;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = Yonetmen;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            if(dr.HasRows)
                return true;
            else
                return false;
        }
        public void FilmleriGosterByDetayliSorgulama(string Adi, string FilmTuru, string Yonetmen, string Oyuncular, ListView liste)
        {
            liste.Items.Clear();
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Select FilmNo, FilmAd, TurAd, Yonetmen, Oyuncular, Ozet, Miktar, Filmler.FilmTurNo from Filmler inner join FilmTurleri on Filmler.FilmTurNo = FilmTurleri.FilmTurNo where Varmi=1 and FilmAd like @filmAdi +'%' and TurAd like @turAdi +'%' and Yonetmen like @Yonetmen +'%' and Oyuncular like '%'+ @oyuncu +'%'", conn);
            comm.Parameters.Add("@filmAdi", SqlDbType.VarChar).Value=Adi;
            comm.Parameters.Add("@turAdi", SqlDbType.VarChar).Value = FilmTuru;
            comm.Parameters.Add("@Yonetmen", SqlDbType.VarChar).Value = Yonetmen;
            comm.Parameters.Add("@oyuncu", SqlDbType.VarChar).Value = Oyuncular;
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                liste.Items.Add(dr[0].ToString());
                liste.Items[i].SubItems.Add(dr[1].ToString());
                liste.Items[i].SubItems.Add(dr[2].ToString());
                liste.Items[i].SubItems.Add(dr[3].ToString());
                liste.Items[i].SubItems.Add(dr[4].ToString());
                liste.Items[i].SubItems.Add(dr[5].ToString());
                liste.Items[i].SubItems.Add(dr[6].ToString());
                liste.Items[i].SubItems.Add(dr[7].ToString());
                i++;
            }
            dr.Close();
            conn.Close();
        }
        public bool StokGuncelleFromSatisEkle(int FilmNo, int Adet)
        {
            bool Sonuc = false;
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Update Filmler Set  Miktar=Miktar - @Adet where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = Adet;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex) 
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); } 
            return Sonuc;
        }
        public bool StokGuncelleFromSatisSil(int FilmNo, int Adet)
        {
            bool Sonuc = false;
            SqlConnection conn = new SqlConnection(cGenel.connStr);
            SqlCommand comm = new SqlCommand("Update Filmler Set  Miktar=Miktar + @Adet where FilmNo=@FilmNo", conn);
            comm.Parameters.Add("@Adet", SqlDbType.Int).Value = Adet;
            comm.Parameters.Add("@FilmNo", SqlDbType.Int).Value = FilmNo;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
            return Sonuc;
        }
    }
}
