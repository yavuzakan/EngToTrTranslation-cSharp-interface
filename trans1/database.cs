using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trans1
{
    class database
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;

        public static void Create_db()
        {
            string path = "deneme.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";

            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE data (id INTEGER, veri TEXT, veri2 TEXT,  tarih TEXT,  cont TEXT UNIQUE ,  PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();



                }

            }

        }
        public static void add(string gelenveri)
        {
            try
            {
                string path = "deneme.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";


                string veri = gelenveri;
                DateTime bugun = DateTime.Now;

                string tarih = DateTime.Now.Date.ToString("dd.MM.yyyy");
                string cont = veri +" "+ tarih;
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);




                //"server=localhost;username=root;password=;database=follow";
                //string sql2 = "CREATE TABLE passwords (id INTEGER, info TEXT , username TEXT, password TEXT,  PRIMARY KEY(id AUTOINCREMENT))";
                cmd.CommandText = "INSERT INTO data(veri,tarih,cont) VALUES(@veri,@tarih,@cont)";

                cmd.Parameters.AddWithValue("@veri", veri);
                cmd.Parameters.AddWithValue("@tarih", tarih);
                cmd.Parameters.AddWithValue("@cont", cont);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {


            }




        }






    }
}
