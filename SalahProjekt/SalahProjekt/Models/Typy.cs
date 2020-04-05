using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalahProjekt.Models
{
    public class Typ
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }

        public virtual Profile Tprofile { get; set; }
        public int MeczID { get; set; }
        public virtual  Mecz Mecz { get; set; }
       // public int Punkty { get; set; }

        public int Bramki_D1 { get; set; }
        public int Bramki_D2 { get; set; }

    }
}