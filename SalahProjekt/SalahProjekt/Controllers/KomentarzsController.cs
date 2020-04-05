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
    public class KomentarzsController : Controller
    {
        private TheRedsContext db = new TheRedsContext();

        // GET: Komentarzs
        public ActionResult Index()
        {
            var komentarze = db.Komentarze.Include(k => k.Artykul).Include(k => k.Profil);
            return View(komentarze.ToList());
        }

        // GET: Komentarzs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentarz komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            return View(komentarz);
        }

        // GET: Komentarzs/Create
        public ActionResult Create()
        {
            ViewBag.ArtykulID = new SelectList(db.Artykuly, "ID", "Tresc");
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login");
            return View();
        }

        // POST: Komentarzs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ArtykulID,Tresc,ProfileID")] Komentarz komentarz)
        {
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

        // GET: Komentarzs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentarz komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtykulID = new SelectList(db.Artykuly, "ID", "Tresc", komentarz.ArtykulID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", komentarz.ProfileID);
            return View(komentarz);
        }

        // POST: Komentarzs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ArtykulID,Tresc,ProfileID")] Komentarz komentarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komentarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtykulID = new SelectList(db.Artykuly, "ID", "Tresc", komentarz.ArtykulID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Login", komentarz.ProfileID);
            return View(komentarz);
        }

        // GET: Komentarzs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentarz komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            return View(komentarz);
        }

        // POST: Komentarzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Komentarz komentarz = db.Komentarze.Find(id);
            db.Komentarze.Remove(komentarz);
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
