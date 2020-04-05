using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalahProjekt.DAL;
using SalahProjekt.Models;

namespace SalahProjekt.Controllers
{
    public class TypsController : Controller
    {
        private TheRedsContext db = new TheRedsContext();

        // GET: Typs
        public ActionResult Index()
        {
            //if (Context.User.IsInRole("Admin"))
            //{
            //    return MyTyps(int ? id); 
            //}
            var typy = db.Typy.Include(t => t.Mecz).Include(t => t.Tprofile);
            return View(typy.ToList());
        }
        public ActionResult MyTyps(int? id)
        {
            //ViewBag.MeczID = new SelectList(db.Mecze, "ID", "ID");
            //ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login");
            var typy = db.Typy.Include(t => t.Mecz).Include(t => t.Tprofile).Where(t=>t.Tprofile.Login==User.Identity.Name);
            return View(typy.ToList());
        }
        public ActionResult Wyniki()
        {
            var thinker = db.Profiles;
            thinker.OrderBy(p => p.SumaPunktow);
            LinkedList<PomClass> pom = new LinkedList<PomClass>();
            PomClass temp;
            foreach (var profile in thinker)
            {
                temp = new PomClass(profile.Login, profile.ID);
                pom.AddLast(temp);
            }
            
            return View(pom);
        }
 
        public class PomClass
        {
            public string login { get; set; }
            public int suma { get; set; }
            public PomClass(string l,int s)
            {
                login = l;
                suma = s;
            }
        }

        // GET: Typs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Typ typ = db.Typy.Find(id);
            if (typ == null)
            {
                return HttpNotFound();
            }
            return View(typ);
        }

        // GET: Typs/Create
        public ActionResult Create()
        {
            

            var meczeToType = db.Mecze.Where(m => m.czyTypowac).ToList();
            var userTyps = db.Typy.Where(t => t.Tprofile.Login == User.Identity.Name);
            Mecz mecz=null;

            foreach(var m in meczeToType)
            {
                var temp = userTyps.Where(t => t.MeczID == m.ID);

                if (temp!=null && temp.ToList().Count>0)
                {
                    mecz = m;
                    break;
                }
          
            }
            if(mecz == null)
            {
                return View();
            }
            ViewBag.DruzynaGospodarzy = mecz.druzyna1.Nazwa_Druzyny;
            ViewBag.DruzynaGosci = mecz.druzyna2.Nazwa_Druzyny;
            ViewBag.MeczID = mecz.ID;
            return View();
        }

        // POST: Typs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MeczID,Bramki_D1,Bramki_D2")] Typ typ)
        {
            typ.ProfileID = db.Profiles.First(p => p.Login == User.Identity.Name).ID;
            if (ModelState.IsValid)
            {
                db.Typy.Add(typ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MeczID = new SelectList(db.Mecze, "ID", "ID", typ.MeczID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", typ.ProfileID);
            return View(typ);
        }

        // GET: Typs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Typ typ = db.Typy.Find(id);
            if (typ == null)
            {
                return HttpNotFound();
            }
            ViewBag.MeczID = new SelectList(db.Mecze, "ID", "ID", typ.MeczID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", typ.ProfileID);
            return View(typ);
        }

        // POST: Typs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProfileID,MeczID,Bramki_D1,Bramki_D2,MyProperty")] Typ typ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MeczID = new SelectList(db.Mecze, "ID", "ID", typ.MeczID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", typ.ProfileID);
            return View(typ);
        }

        // GET: Typs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Typ typ = db.Typy.Find(id);
            if (typ == null)
            {
                return HttpNotFound();
            }
            return View(typ);
        }

        // POST: Typs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Typ typ = db.Typy.Find(id);
            db.Typy.Remove(typ);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
