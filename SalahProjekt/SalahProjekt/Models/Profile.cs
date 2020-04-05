using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalahProjekt.Models
{
    public class Profile
    {
        public int ID { get; set; }

        public string Login { get; set; }
        // public string Haslo { get; set; }
        public virtual List<Typ> Typy { get; set; }
        public virtual List<Artykul> Partykuly{get; set; }

        public virtual List<Komentarz> Pkomentarze { get; set; }
        public int SumaPunktow { get; set; } = 0;
        
    }
}