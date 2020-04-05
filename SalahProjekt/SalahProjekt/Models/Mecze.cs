using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalahProjekt.Models
{
    public class Mecz
    {
        public int ID { get; set; }
        public int Druzyna1ID{ get; set; }
        public virtual Druzyna druzyna1 { get; set; }

        public int Druzyna2ID { get; set; }
        public virtual Druzyna druzyna2 { get; set; }
        [DisplayName("Bramki Gospodarzy")]
        public int Bramki_D1 { get; set; }
        [DisplayName("Bramki Gosci")]
        public int Bramki_D2 { get; set; }
        [DisplayName("data meczu")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd:hh-mm}", ApplyFormatInEditMode = true)]
        public DateTime dataMeczu { get; set; }
        //public int MyProperty { get; set; }
        public bool czyTypowac { get; set; }

        public virtual List<Typ> Typy { get; set; }

       
    }
}