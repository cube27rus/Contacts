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
    
    public class ContactsController : Controller
    {
        private ContactContext db = new ContactContext();
        private OrganContext odb = new OrganContext();
        // GET: Contacts
        /*public ActionResult Index()
        {
            var contacts = db.Contact.Include(p => p.Organ);
            
            return View(contacts.ToList());
        }*/
        public ActionResult Org()
        {
            return View(odb.Organ.ToList());

        }


            public ActionResult Index(string searchString,SortState sortOrder = SortState.NameAsc)
        {
            var contacts = db.Contact.Include(p => p.Organ);

            

            ViewBag.NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewBag.CompanySort = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            ViewBag.TelephoneSort = sortOrder == SortState.TelephoneAsc ? SortState.TelephoneDesc : SortState.TelephoneAsc;
            if (!String.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Surname.ToUpper().Contains(searchString.ToUpper()));
            }
                switch (sortOrder)
            {
                case SortState.NameDesc:
                    contacts = contacts.OrderByDescending(s => s.Name);
                    break;
                case SortState.CompanyAsc:
                    contacts = contacts.OrderBy(s => s.Organ.Organisation);
                    break;
                case SortState.CompanyDesc:
                    contacts = contacts.OrderByDescending(s => s.Organ.Organisation);
                    break;
                case SortState.TelephoneAsc:
                    contacts = contacts.OrderBy(s => s.Telephone);
                    break;
                case SortState.TelephoneDesc:
                    contacts = contacts.OrderByDescending(s => s.Telephone);
                    break;
                default:
                    contacts = contacts.OrderBy(s => s.Name);
                    break;
            }



            return View(contacts.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            SelectList organisations = new SelectList(odb.Organ, "Id", "Organisation");
            ViewBag.Organisations = organisations;
            return View();
        }

        // POST: Contacts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contact.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            SelectList organisations = new SelectList(odb.Organ, "Id", "Organisation");
            ViewBag.Organisations = organisations;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Patronymic,Telephone,Adress,Email,Info,Organ_Id")] Contact contact)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contact.Find(id);
            db.Contact.Remove(contact);
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
