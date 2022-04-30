using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakPortalı.ViewModel
{
    public class KullaniciModel
    {
        public int KullaniciId { get; set; }
        public string AdSoyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public Nullable<decimal> TelefonNo { get; set; }
        public Nullable<System.DateTime> DogumTarihi { get; set; }
        public Nullable<int> KullaniciYetki { get; set; }
    }
}