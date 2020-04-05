using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SalahProjekt.Models
{
    public class Komentarz
    {
        public int ID { get; set; }
        public int ArtykulID { get; set; }

        public virtual Artykul Artykul { get; set; }
        [StringLength(500)]
        public string Tresc { get; set; }
        public int ProfileID { get; set; }

        public virtual Profile Profil { get; set; }

    }
}