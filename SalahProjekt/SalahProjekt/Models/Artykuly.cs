using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SalahProjekt.Models
{
    public class Artykul
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }

        public virtual Profile Profil { get; set; }

        public string Tresc { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 2)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z ")]
        public string Tytul { get; set; }

        public virtual List<Komentarz> Komentarze { get; set; }
    }
}