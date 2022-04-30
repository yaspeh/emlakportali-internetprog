using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmlakPortalı.Models;
using EmlakPortalı.ViewModel;

namespace EmlakPortalı.Controllers
{
    public class ServisController : ApiController
    {
        DBEntities2 db = new DBEntities2();
        SonucModel sonuc = new SonucModel();

        #region Kullanıcı
        [HttpGet]
        [Route("api/Kullaniciliste")]
        public List<KullaniciModel> KullaniciListe()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()
            {
                KullaniciId = x.KullaniciId,
                AdSoyad = x.AdSoyad,
                KullaniciAdi = x.KullaniciAdi,
                Sifre = x.Sifre,
                Email = x.Email,
                TelefonNo = x.TelefonNo,
                DogumTarihi = x.DogumTarihi,
                KullaniciYetki = x.KullaniciYetki
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/Kullanicibyid/{KullaniciId}")]
        public KullaniciModel KullaniciUyeById(int KullaniciId)
        {
            KullaniciModel kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).Select(x => new KullaniciModel()
            {
                KullaniciId = x.KullaniciId,
                AdSoyad = x.AdSoyad,
                KullaniciAdi = x.KullaniciAdi,
                Sifre = x.Sifre,
                Email = x.Email,
                TelefonNo = x.TelefonNo,
                DogumTarihi = x.DogumTarihi,
                KullaniciYetki = x.KullaniciYetki
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/Kullaniciekle")]
        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Email == model.Email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Posta Adresi Kayıtlıdır!";
                return sonuc;
            }
            Kullanici yeni = new Kullanici();
            yeni.AdSoyad = model.AdSoyad;
            yeni.KullaniciAdi = model.KullaniciAdi;
            yeni.Sifre = model.Sifre;
            yeni.Email = model.Email;
            yeni.DogumTarihi = model.DogumTarihi;
            yeni.KullaniciYetki = model.KullaniciYetki;
            db.Kullanici.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/Kullaniciduzenle")]
        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == model.KullaniciId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.AdSoyad = model.AdSoyad;
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Sifre = model.Sifre;
            kayit.Email = model.Email;
            kayit.DogumTarihi = model.DogumTarihi;
            kayit.KullaniciYetki = model.KullaniciYetki;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/Kullanicisil/{KullaniciId}")]
        public SonucModel KullaniciSil(int KullaniciId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            if (db.Ilan.Count(s => s.KullaniciId == KullaniciId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ilan Kaydı Olan Kullanıcı Silinemez!";
                return sonuc;
            }
            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Silindi";
            return sonuc;
        }
        #endregion

        #region Favori
        [HttpGet]
        [Route("api/Favoriliste")]
        public List<FavoriModel> FavoriListe()
        {
            List<FavoriModel> liste = db.Favori.Select(x => new FavoriModel()
            {
                FavoriId = x.FavoriId,
                KullaniciId = x.KullaniciId,
                IlanId = x.IlanId,
                Fiyat = x.Ilan.Fiyat
            }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/favoriekle")]
        public SonucModel FavoriEkle(FavoriModel model)
        {
            Favori yeni = new Favori();
            yeni.KullaniciId = model.KullaniciId;
            yeni.IlanId = model.IlanId;

            db.Favori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Favori Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/favoriduzenle")]
        public SonucModel FavoriDuzenle(FavoriModel model)
        {
            Favori kayit = db.Favori.Where(s => s.FavoriId == model.FavoriId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadı!";
                return sonuc;
            }
            kayit.KullaniciId = model.KullaniciId;
            kayit.IlanId = model.IlanId;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Favori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/favorisil/{FavoriId}")]
        public SonucModel FavoriSil(int FavoriId)
        {
            Favori kayit = db.Favori.Where(s => s.FavoriId == FavoriId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Favori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Favori Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/Favoribyid/{KullaniciId}")]
        public List<FavoriModel> favoribykullaniciid(int KullaniciId)
        {
            List<FavoriModel> kayit = db.Favori.Where(s => s.IlanId == KullaniciId).Select(x => new FavoriModel()
            {
                FavoriId = x.FavoriId,
                KullaniciId = x.KullaniciId,
                IlanId = x.IlanId,
                Fiyat = x.Ilan.Fiyat
            }).ToList();
            return kayit;
        }
        #endregion

        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                KategoriAdi = x.KategoriAdi,
                KategoriIlanSay = x.Ilan.Count()
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{KategoriId}")]
        public KategoriModel KategoriById(int KategoriId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.KategoriId == KategoriId).Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                KategoriAdi = x.KategoriAdi,
                KategoriIlanSay = x.Ilan.Count()
            }).FirstOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.KategoriAdi == model.KategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.KategoriAdi = model.KategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == model.KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.KategoriAdi = model.KategoriAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/kategorisil/{KategoriId}")]
        public SonucModel KategoriSil(int KategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Ilan.Count(s => s.KategoriId == KategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Anket Kaydı Olan Kategori Silinemez!";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }
        #endregion

        #region Ilan
        [HttpGet]
        [Route("api/Ilanliste")]
        public List<IlanModel> IlanListe()
        {
            List<IlanModel> liste = db.Ilan.Select(x => new IlanModel()
            {
                IlanId = x.IlanId,
                Baslik = x.Baslik,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                SatilikKiralik = x.SatilikKiralik,
                Adres = x.Adres,
                Fiyat = x.Fiyat,
                KullaniciId = x.KullaniciId,
                AdSoyad = x.Kullanici.AdSoyad,
                Aciklama = x.Aciklama,
                Tarih = x.Tarih
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/Ilanlistebykatid/{katId}")]
        public List<IlanModel> IlanListeByKatId(int katId)
        {
            List<IlanModel> liste = db.Ilan.Where(s => s.KategoriId == katId).Select
           (x => new IlanModel()
           {
               IlanId = x.IlanId,
               Baslik = x.Baslik,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               SatilikKiralik = x.SatilikKiralik,
               Adres = x.Adres,
               Fiyat = x.Fiyat,
               KullaniciId = x.KullaniciId,
               AdSoyad = x.Kullanici.AdSoyad,
               Aciklama = x.Aciklama,
               Tarih = x.Tarih
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/Ilanlistebyuyeid/{KullaniciId}")]
        public List<IlanModel> IlanListeByUyeId(int KullaniciId)
        {
            List<IlanModel> liste = db.Ilan.Where(s => s.IlanId == KullaniciId).Select(x =>
           new IlanModel()
           {
               IlanId = x.IlanId,
               Baslik = x.Baslik,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               SatilikKiralik = x.SatilikKiralik,
               Adres = x.Adres,
               Fiyat = x.Fiyat,
               KullaniciId = x.KullaniciId,
               AdSoyad = x.Kullanici.AdSoyad,
               Aciklama = x.Aciklama,
               Tarih = x.Tarih
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/Ilanbyid/{IlanId}")]
        public IlanModel IlanById(int IlanId)
        {
            IlanModel kayit = db.Ilan.Where(s => s.IlanId == IlanId).Select(x =>
           new IlanModel()
           {
               IlanId = x.IlanId,
               Baslik = x.Baslik,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               SatilikKiralik = x.SatilikKiralik,
               Adres = x.Adres,
               Fiyat = x.Fiyat,
               KullaniciId = x.KullaniciId,
               AdSoyad = x.Kullanici.AdSoyad,
               Aciklama = x.Aciklama,
               Tarih = x.Tarih
           }).SingleOrDefault();
            return kayit;
        }

        #region Detay 
        [HttpGet]
        [Route("api/detayliste")]
        public List<DetayModel> DetayListe()
        {
            List<DetayModel> liste = db.Detay.Select(x => new DetayModel()
            {
                DetayId = x.DetayId,
                OdaSayisi = x.OdaSayisi,
                BinaYasi= x.BinaYasi,
                BinaKati = x.BinaKati,
                KacinciKat = x.KacinciKat,
                Isitma=x.Isitma,
                Esyali = x.Esyali,
                IlanId = x.IlanId
        }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/detayekle")]
        public SonucModel DetayEkle(DetayModel model)
        {
            Detay yeni = new Detay();
            yeni.OdaSayisi = model.OdaSayisi;
            yeni.BinaYasi = model.BinaYasi;
            yeni.BinaKati = model.BinaKati;
            yeni.KacinciKat = model.KacinciKat;
            yeni.Isitma = model.Isitma;
            yeni.Esyali = model.Esyali;
            yeni.IlanId = model.IlanId;


            db.Detay.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ilan Detay Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/detayduzenle")]
        public SonucModel DetayDuzenle(DetayModel model)
        {
            Detay kayit = db.Detay.Where(s => s.DetayId == model.DetayId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadı!";
                return sonuc;
            }
            kayit.OdaSayisi = model.OdaSayisi;
            kayit.BinaYasi = model.BinaYasi;
            kayit.BinaKati = model.BinaKati;
            kayit.KacinciKat = model.KacinciKat;
            kayit.Isitma = model.Isitma;
            kayit.Esyali = model.Esyali;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ilan Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/detaysil/{detayId}")]
        public SonucModel MakaleSil(int DetayId)
        {
            Detay kayit = db.Detay.Where(s => s.DetayId == DetayId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Detay.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Detay Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/detaybyid/{IlanId}")]
        public DetayModel detaybyilanid(int IlanId)
        {
            DetayModel kayit = db.Detay.Where(s => s.IlanId == IlanId).Select(x => new DetayModel()
            {
                DetayId = x.DetayId,
                OdaSayisi = x.OdaSayisi,
                BinaYasi = x.BinaYasi,
                BinaKati = x.BinaKati,
                KacinciKat = x.KacinciKat,
                Isitma = x.Isitma,
                Esyali = x.Esyali,
                IlanId = x.IlanId
            }).FirstOrDefault();
            return kayit;
        }
        #endregion

        #region Resim
        [HttpGet]
        [Route("api/resimliste")]
        public List<ResimModel> resimListe()
        {
            List<ResimModel> liste = db.Resim.Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                IlanId = x.IlanId
            }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/resimekle")]
        public SonucModel ResimEkle(ResimModel model)
        {
            Resim yeni = new Resim();
            yeni.ResimAdi= model.ResimAdi;
            yeni.IlanId = model.IlanId;

            db.Resim.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ilan Detay Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/detayduzenle")]
        public SonucModel ResimDuzenle(ResimModel model)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == model.ResimId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadı!";
                return sonuc;
            }
            kayit.ResimAdi = model.ResimAdi;
            kayit.IlanId = model.IlanId;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Resim Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/resimsil/{ResimId}")]
        public SonucModel ResimSil(int ResimId)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == ResimId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Resim.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Resim Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/resimbyid/{IlanId}")]
        public List<ResimModel> resimbyilanid(int IlanId)
        {
            List<ResimModel> kayit = db.Resim.Where(s => s.IlanId == IlanId).Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                IlanId = x.IlanId
            }).ToList();
            return kayit;
        }
        #endregion


        [HttpPost]
        [Route("api/Ilanekle")]
        public SonucModel IlanEkle(IlanModel model)
        {
            if (db.Ilan.Count(s => s.Baslik == model.Baslik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ilan Başlığı Kayıtlıdır!";
                return sonuc;
            }
            Ilan yeni = new Ilan();
            yeni.Baslik = model.Baslik;
            yeni.KategoriId = model.KategoriId;
            yeni.SatilikKiralik = model.SatilikKiralik;
            yeni.Adres = model.Adres;
            yeni.Fiyat = model.Fiyat;
            yeni.KullaniciId = model.KullaniciId;
            yeni.Aciklama = model.Aciklama;
            yeni.Tarih = model.Tarih;


            db.Ilan.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ilan Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/Ilanduzenle")]
        public SonucModel IlanDuzenle(IlanModel model)
        {
            Ilan kayit = db.Ilan.Where(s => s.IlanId == model.IlanId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.Baslik = model.Baslik;
            kayit.Kategori.KategoriAdi = model.KategoriAdi;
            kayit.SatilikKiralik = model.SatilikKiralik;
            kayit.Adres = model.Adres;
            kayit.Fiyat = model.Fiyat;
            kayit.KullaniciId = model.KullaniciId;
            kayit.Aciklama = model.Aciklama;
            kayit.Tarih = model.Tarih;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ilan Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/Ilansil/{IlanId}")]
        public SonucModel IlanSil(int IlanId)
        {
            Ilan kayit = db.Ilan.Where(s => s.IlanId == IlanId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Detay.Count(s => s.IlanId == IlanId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Detay Kaydı Olan İlan Silinemez!";
                return sonuc;
            }

            db.Ilan.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İlan Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/IlanDetayListe/{IlanId}")]
        public List<IlanModel> IlanDetayListe(int IlanId)
        {
            List<IlanModel> liste = db.Ilan.Where(s => s.IlanId == IlanId).Select(x => new IlanModel()
            {
                IlanId = x.IlanId,
                Baslik = x.Baslik,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                SatilikKiralik = x.SatilikKiralik,
                Adres = x.Adres,
                Fiyat = x.Fiyat,
                KullaniciId = x.KullaniciId,
                AdSoyad = x.Kullanici.AdSoyad,
                Aciklama = x.Aciklama,
                Tarih = x.Tarih

            }).ToList();
            foreach (var Ilan in liste)
            {
                Ilan.detaybilgi = detaybyilanid(Ilan.IlanId);
                Ilan.resimbilgi = resimbyilanid(Ilan.IlanId);
            }
            return liste;
        }
        
        #endregion
    }
}
