using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakPortalı.ViewModel
{
    public class ResimModel
    {
        public int ResimId { get; set; }
        public string ResimAdi { get; set; }
        public Nullable<int> IlanId { get; set; }
    }
}