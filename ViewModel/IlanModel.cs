using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakPortalı.ViewModel
{
    public class IlanModel
    {
        public int IlanId { get; set; }
        public string Baslik { get; set; }
        public Nullable<int> KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public string SatilikKiralik { get; set; }
        public string Adres { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public Nullable<int> KullaniciId { get; set; }
        public string AdSoyad { get; set; }
        public string Aciklama { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public DetayModel detaybilgi { get; set; }
        public List<ResimModel> resimbilgi { get; set; }
    }
}