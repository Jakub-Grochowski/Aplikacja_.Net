using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SalahProjekt.Models
{
    public class Druzyna
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa Drużyny")]
        [StringLength(50)]
        public string Nazwa_Druzyny { get; set; }
        //public int MiejsceWTabeli { get; set; }
        [Display(Name = "Mecze u Siebie")]
        public virtual List<Mecz> Lista_Meczy_Gospodarz { get; set; }
        [Display(Name = "Mecze Na Wyjeździe")]
        public virtual List<Mecz> Lista_Meczy_Goscie { get; set; }
        public string Sciezka_Do_Herbu { get; set; }
    }
}
