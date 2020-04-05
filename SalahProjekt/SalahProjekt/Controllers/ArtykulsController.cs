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
using SalahProjekt.ViewModels;

namespace SalahProjekt.Controllers
{
    public class ArtykulsController : Controller
    {
        private TheRedsContext db = new TheRedsContext();

        // GET: Artykuls
        public ActionResult Index()
        {
            var artykuly = db.Artykuly.Include(a => a.Profil);
            return View(artykuly.ToList());
        }

        // GET: Artykuls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artykul artykul = db.Artykuly.Find(id);
            if (artykul == null)
            {
                return HttpNotFound();
            }
            ArtykulKomentarze ak47 = new ArtykulKomentarze();
            ak47.art = artykul;
            return View(ak47);
        }

        // GET: Artykuls/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login");
            return View();
        }

        // POST: Artykuls/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProfileID,Tresc,Tytul")] Artykul artykul)
        {
            if (ModelState.IsValid)
            {
                db.Artykuly.Add(artykul);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", artykul.ProfileID);
            return View(artykul);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detalis([Bind(Include = "Tresc")] Komentarz komentarz)
        {
            var myUser = db.Profiles.Where(p => p.Login == User.Identity.Name).First();
            var idU = myUser.ID;
            komentarz.ProfileID = idU;
            if (ModelState.IsValid)
            {
                db.Komentarze.Add(komentarz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtykulID = new SelectList(db.Artykuly, "ID", "Tresc", komentarz.ArtykulID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", komentarz.ProfileID);
            return View(komentarz);
        }

        // GET: Artykuls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artykul artykul = db.Artykuly.Find(id);
            if (artykul == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", artykul.ProfileID);
            return View(artykul);
        }

        // POST: Artykuls/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProfileID,Tresc,Tytul")] Artykul artykul)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artykul).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", artykul.ProfileID);
            return View(artykul);
        }

        // GET: Artykuls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artykul artykul = db.Artykuly.Find(id);
            if (artykul == null)
            {
                return HttpNotFound();
            }
            return View(artykul);
        }

        // POST: Artykuls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artykul artykul = db.Artykuly.Find(id);
            db.Artykuly.Remove(artykul);
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
