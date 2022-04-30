using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakPortalı.ViewModel
{
    public class FavoriModel
    {
        public int FavoriId { get; set; }
        public Nullable<int> KullaniciId { get; set; }
        public Nullable<int> IlanId { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
    }
}