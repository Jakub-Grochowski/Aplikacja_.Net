using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalahProjekt.DAL;
using SalahProjekt.Models;

namespace SalahProjekt.Controllers
{
    public class DruzynasController : Controller
    {
        private TheRedsContext db = new TheRedsContext();

        // GET: Druzynas
        public ActionResult Index()
        {
            return View(db.Druzyny.ToList());
        }

        // GET: Druzynas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Druzyna druzyna = db.Druzyny.Find(id);
            if (druzyna == null)
            {
                return HttpNotFound();
            }
            return View(druzyna);
        }

        // GET: Druzynas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Druzynas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nazwa_Druzyny")] Druzyna druzyna)
        {
            Druzyna team = druzyna;
            UpdateModel(team);

            HttpPostedFileBase file = Request.Files["plikZHerbem"];
            if(file!=null && file.ContentLength > 0)
            {
                //team.Sciezka_Do_Herbu = System.Guid.NewGuid().ToString();
                team.Sciezka_Do_Herbu = file.FileName;
                file.SaveAs(HttpContext.Server.MapPath("~/foto/") + team.Sciezka_Do_Herbu);
            }
            if (ModelState.IsValid)
            {
                db.Druzyny.Add(druzyna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(druzyna);
        }

        // GET: Druzynas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Druzyna druzyna = db.Druzyny.Find(id);
            if (druzyna == null)
            {
                return HttpNotFound();
            }
            return View(druzyna);
        }

        // POST: Druzynas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nazwa_Druzyny")] Druzyna druzyna)
        {
            Druzyna team = druzyna;
            UpdateModel(team);
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["plikZHerbem"];
                if (file != null && file.ContentLength > 0)
                {
                    //team.Sciezka_Do_Herbu = System.Guid.NewGuid().ToString();
                    team.Sciezka_Do_Herbu = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/foto/") + team.Sciezka_Do_Herbu);
                }
                db.Entry(druzyna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(druzyna);
        }

        // GET: Druzynas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
                Druzyna druzyna = db.Druzyny.Find(id);
            //File f = new File(HttpContext.Server.MapPath("~/foto/") + druzyna.Sciezka_Do_Herbu);
            // f.delete();
            if (druzyna == null)
            {
                return HttpNotFound();
            }
            return View(druzyna);
        }

        // POST: Druzynas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Druzyna druzyna = db.Druzyny.Find(id);

            db.Druzyny.Remove(druzyna);
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
