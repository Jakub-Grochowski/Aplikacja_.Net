using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalahProjekt.DAL;
using SalahProjekt.Models;
using SalahProjekt  .ViewModels;

namespace SalahProjekt.Controllers
{
    public class HomeController : Controller
    {
        private TheRedsContext db = new TheRedsContext();
        private int maxP = 4;
        private int points = 2;
        private int minP = 1;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<TeamGamesCount> data = db.Druzyny.
                Select(d => new TeamGamesCount { teamName=d.Nazwa_Druzyny, gamesCount=d.Lista_Meczy_Goscie.Count + d.Lista_Meczy_Gospodarz.Count });

            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Wyniki()
        {
            podumujPunkty();
            var prof = db.Profiles.OrderBy(p=>p.SumaPunktow);
            return View(prof.ToList());
        }
        public void podumujPunkty()
        {
            var readyM = db.Mecze.Where(m => !m.czyTypowac).ToList();
            foreach (var m in readyM)
            {
                var mt = m.Typy;
               foreach(var t in mt)
                {
                    Profile prof = db.Profiles.Where(p=>p.ID==t.ProfileID).First();
                    if(t.Bramki_D1==m.Bramki_D1 && t.Bramki_D2 == m.Bramki_D2)
                    {
                        prof.SumaPunktow = prof.SumaPunktow + maxP;
                    }else if (t.Bramki_D1 - t.Bramki_D2 == m.Bramki_D1 - m.Bramki_D2)
                    {
                        prof.SumaPunktow = prof.SumaPunktow + points;
                    }else if((t.Bramki_D1>t.Bramki_D2 && m.Bramki_D1>m.Bramki_D2 )|| t.Bramki_D1 < t.Bramki_D2 && m.Bramki_D1 < m.Bramki_D2)
                    {
                        prof.SumaPunktow = prof.SumaPunktow + minP;
                    }
                    else
                    {
                        prof.SumaPunktow = prof.SumaPunktow + 0;
                    }
                }
            }
            db.SaveChanges();
        }
    }
}