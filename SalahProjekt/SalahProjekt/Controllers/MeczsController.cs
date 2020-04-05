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
using PagedList;


namespace SalahProjekt.Controllers
{
    public class MeczsController : Controller
    {
        private TheRedsContext db = new TheRedsContext();

        // GET: Meczs
        //public ActionResult Index()
        //{
        //    var mecze = db.Mecze.Include(m => m.druzyna1).Include(m => m.druzyna2);
        //    return View(mecze.ToList());
        //}

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Bramki_D1" ? "Bramki_D1_desc" : "Bramki_D1";
            ViewBag.BramkiSortParm = sortOrder == "Bramki_D2" ? "Bramki_D2_desc" : "Bramki_D2";
            var mecze = db.Mecze.Include(m => m.druzyna1).Include(m => m.druzyna2);


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                mecze = db.Mecze.Include(m => m.druzyna1).Include(m => m.druzyna2).Where(m=>m.druzyna1.Nazwa_Druzyny.Contains(searchString) || m.druzyna2.Nazwa_Druzyny.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    mecze = mecze.OrderBy(s => s.dataMeczu);
                    break;
                case "Bramki_D1":
                    mecze = mecze.OrderBy(s => s.Bramki_D1);
                    break;
                case "Bramki_D1_desc":
                    mecze = mecze.OrderByDescending(s => s.Bramki_D1);
                    break;
                case "Bramki_D2":
                    mecze = mecze.OrderByDescending(s => s.Bramki_D1);
                    break;
                case "Bramki_D2_desc":
                    mecze = mecze.OrderByDescending(s => s.Bramki_D1);
                    break;
                default:
                    mecze = mecze.OrderByDescending(s => s.dataMeczu);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(mecze.ToPagedList(pageNumber, pageSize));
            //return View(mecze.ToList());
        }

        // GET: Meczs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecz mecz = db.Mecze.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }
            return View(mecz);
        }

        // GET: Meczs/Create
        public ActionResult Create()
        {
            ViewBag.Druzyna1ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny");
            ViewBag.Druzyna2ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny");
            return View();
        }

        // POST: Meczs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Druzyna1ID,Druzyna2ID,Bramki_D1,Bramki_D2,dataMeczu,czyTypowac")] Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                db.Mecze.Add(mecz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Druzyna1ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny", mecz.Druzyna1ID);
            ViewBag.Druzyna2ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny", mecz.Druzyna2ID);
            return View(mecz);
        }

        // GET: Meczs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecz mecz = db.Mecze.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }
            ViewBag.Druzyna1ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny", mecz.Druzyna1ID);
            ViewBag.Druzyna2ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny", mecz.Druzyna2ID);
            return View(mecz);
        }

        // POST: Meczs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Druzyna1ID,Druzyna2ID,Bramki_D1,Bramki_D2,dataMeczu,czyTypowac")] Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mecz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Druzyna1ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny", mecz.Druzyna1ID);
            ViewBag.Druzyna2ID = new SelectList(db.Druzyny, "ID", "Nazwa_Druzyny", mecz.Druzyna2ID);
            return View(mecz);
        }

        // GET: Meczs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecz mecz = db.Mecze.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }
            return View(mecz);
        }

        // POST: Meczs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mecz mecz = db.Mecze.Find(id);
            db.Mecze.Remove(mecz);
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
