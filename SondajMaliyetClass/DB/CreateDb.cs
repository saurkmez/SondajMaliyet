using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SondajMaliyetClass.Models;

namespace SondajMaliyetClass.DB
{
    public class CreateDb
    {
        public void CreateDB()
        {
            if (!File.Exists("sondajMaliyet.db"))
            {
                SQLiteConnection.CreateFile("sondajMaliyet.db");
            }
            CreateTables();
            CreateModel();
        }
        public void CreateTables()
        {
            #region --> işçilik maliyeti
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"CREATE TABLE if not exists 'IscilikMaliyeti' 
                                        ('kId'   INTEGER NOT NULL,'kisiSayisi' INTEGER NOT NULL,'maas'  REAL NOT NULL,
                                            PRIMARY KEY('kId' AUTOINCREMENT))";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch(Exception ex)
                {

                }
            }
            #endregion

            #region --> Matkap Çap
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"CREATE TABLE 'MatkapCap' (
	'mId'	INTEGER NOT NULL,
	'matkapCapi'	REAL NOT NULL,
	'fiyat'	REAL NOT NULL,
	'gunlukIs'	INTEGER NOT NULL,
	PRIMARY KEY('mId' AUTOINCREMENT)
)";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
            #endregion

            #region --> Mazot Gideri
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"CREATE TABLE if not exists 'MazotGideri' ('mazotId' INTEGER NOT NULL,'tankHacmi'	INTEGER NOT NULL, 'birimFiyat' REAL NOT NULL,
                                        PRIMARY KEY('mazotId' AUTOINCREMENT))";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
            #endregion

            #region --> Nakliye
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"CREATE TABLE if not exists 'Nakliye' ('nId' INTEGER NOT NULL,'gunlukGider' INTEGER NOT NULL,
                        PRIMARY KEY('nId' AUTOINCREMENT))";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
            #endregion

            #region --> Tum Giderler
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"CREATE TABLE 'TumGiderler' (
	'giderId'	INTEGER NOT NULL,
	'giderAdi'	TEXT NOT NULL,
	'topMaliyet'	REAL NOT NULL,
	'metreMaliyet'	REAL NOT NULL,
	'sondajDerinligi'	REAL NOT NULL,
	'dovizKuru'	NUMERIC NOT NULL,
	'tarih'	TEXT NOT NULL,
	'matkapGider'	REAL NOT NULL,
	'iscilikGider'	REAL NOT NULL,
	'mazotGider'	REAL NOT NULL,
	'nakliyeGider'	REAL NOT NULL,
	PRIMARY KEY('giderId' AUTOINCREMENT)
)";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
            #endregion

            #region --> Zemin Tipi
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"CREATE TABLE if not exists 'ZeminTipi' (
	'zId'	INTEGER NOT NULL,
	'tipAdi'	TEXT NOT NULL,
	PRIMARY KEY('zId')
)";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
            #endregion

            #region --> Yipranma
            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"CREATE TABLE 'Yipranma' (
	'yId'	INTEGER NOT NULL,
	'matkapId'	INTEGER NOT NULL,
	'zeminId'	INTEGER NOT NULL,
	'maxDerinlik'	INTEGER NOT NULL,
	FOREIGN KEY('matkapId') REFERENCES 'MatkapCap'('mId'),
	FOREIGN KEY('zeminId') REFERENCES 'ZeminTipi'('zId'),
	PRIMARY KEY('yId' AUTOINCREMENT)
)";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
            #endregion

        }

        public void CreateModel()
        {
            List<MatkapCap> listMatkap = new List<MatkapCap>()
            {
                new MatkapCap(){mId=1, matkapCapi=8.5 , fiyat=1000, gunlukIs=80},
                new MatkapCap(){mId=2, matkapCapi=9.5 , fiyat=1200, gunlukIs=70},
                new MatkapCap(){mId=3, matkapCapi=10.5 , fiyat=1700, gunlukIs=60},
                new MatkapCap(){mId=4, matkapCapi=11.5 , fiyat=1900, gunlukIs=50},
                new MatkapCap(){mId=5, matkapCapi=12.5 , fiyat=2000, gunlukIs=40},
                new MatkapCap(){mId=6, matkapCapi=13.5 , fiyat=2500, gunlukIs=30},
                new MatkapCap(){mId=7, matkapCapi=15.5 , fiyat=3000, gunlukIs=20},
                new MatkapCap(){mId=8, matkapCapi=17.5 , fiyat=3200, gunlukIs=20},
                new MatkapCap(){mId=9, matkapCapi=26.5 , fiyat=3500, gunlukIs=20}
            };
            List<ZeminTipi> listZemin = new List<ZeminTipi>()
            {
                new ZeminTipi(){zId=1, tipAdi="Çok Yumuşak"},
                new ZeminTipi(){zId=2, tipAdi="Yumuşak"},
                new ZeminTipi(){zId=3, tipAdi="Orta Sert"},
                new ZeminTipi(){zId=4, tipAdi="Sert"},
                new ZeminTipi(){zId=5, tipAdi="Çok Sert"}
            };

            List<Yipranma> listYipranma = new List<Yipranma>()
            {
                new Yipranma(){matkapId=1,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=2,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=3,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=4,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=5,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=6,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=7,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=8,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=9,zeminId=1,maxDerinlik=2000},
                new Yipranma(){matkapId=1,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=2,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=3,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=4,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=5,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=6,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=7,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=8,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=9,zeminId=2,maxDerinlik=1700},
                new Yipranma(){matkapId=1,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=2,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=3,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=4,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=5,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=6,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=7,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=8,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=9,zeminId=3,maxDerinlik=1300},
                new Yipranma(){matkapId=1,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=2,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=3,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=4,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=5,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=6,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=7,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=8,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=9,zeminId=4,maxDerinlik=1000},
                new Yipranma(){matkapId=1,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=2,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=3,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=4,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=5,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=6,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=7,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=8,zeminId=5,maxDerinlik=500},
                new Yipranma(){matkapId=9,zeminId=5,maxDerinlik=500}

            };


            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    foreach (var item in listMatkap)
                    {
                        cmd.CommandText = @"replace into MatkapCap (mId, matkapCapi,fiyat,gunlukIs) 
                                            VALUES (@mId,@matkapCapi,@fiyat,@gunlukIs)";
                        cmd.Parameters.AddWithValue("@mId", item.mId);
                        cmd.Parameters.AddWithValue("@matkapCapi", item.matkapCapi);
                        cmd.Parameters.AddWithValue("@fiyat", item.fiyat);
                        cmd.Parameters.AddWithValue("@gunlukIs", item.gunlukIs);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }

            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    foreach (var item in listZemin)
                    {
                        cmd.CommandText = @"replace into ZeminTipi (zId, tipAdi) 
                                            VALUES (@zId,@tipAdi)";
                        cmd.Parameters.AddWithValue("@zId", item.zId);
                        cmd.Parameters.AddWithValue("@tipAdi", item.tipAdi);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }

            using (SQLiteConnection con = new SQLiteConnection("Data Source=sondajMaliyet.db;Version=3;"))
            {
                try
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(con);
                    foreach (var item in listYipranma)
                    {
                        cmd.CommandText = @"replace into Yipranma (matkapId,zeminId,maxDerinlik) 
                                            VALUES (@matkapId,@zeminId,@maxDerinlik)";
                        cmd.Parameters.AddWithValue("@matkapId", item.matkapId);
                        cmd.Parameters.AddWithValue("@zeminId", item.zeminId);
                        cmd.Parameters.AddWithValue("@maxDerinlik", item.maxDerinlik);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
        }
    }
}
