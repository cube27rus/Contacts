using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Contacts.Models;

namespace Contacts.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrgansController : Controller
    {
        private OrganContext db = new OrganContext();

        // GET: Organs
        public ActionResult Index()
        {
            return View(db.Organ.ToList());
        }

        // GET: Organs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organ organ = db.Organ.Find(id);
            if (organ == null)
            {
                return HttpNotFound();
            }
            return View(organ);
        }

        // GET: Organs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Info,Organisation")] Organ organ)
        {
            if (ModelState.IsValid)
            {
                db.Organ.Add(organ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organ);
        }

        // GET: Organs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organ organ = db.Organ.Find(id);
            if (organ == null)
            {
                return HttpNotFound();
            }
            return View(organ);
        }

        // POST: Organs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Info,Organisation")] Organ organ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organ);
        }

        // GET: Organs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organ organ = db.Organ.Find(id);
            if (organ == null)
            {
                return HttpNotFound();
            }
            return View(organ);
        }

        // POST: Organs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organ organ = db.Organ.Find(id);
            db.Organ.Remove(organ);
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
