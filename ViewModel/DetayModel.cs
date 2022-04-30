using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakPortalı.ViewModel
{
    public class DetayModel
    {
        public int DetayId { get; set; }
        public string OdaSayisi { get; set; }
        public string BinaYasi { get; set; }
        public string BinaKati { get; set; }
        public string KacinciKat { get; set; }
        public string Isitma { get; set; }
        public string Esyali { get; set; }
        public Nullable<int> IlanId { get; set; }
    }
}