using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Serwis.DAL;
using Serwis.Models;

namespace Serwis.Controllers
{
    public class Repair_statusController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: Repair_status
       
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;

                var repair_statuses = db.Repair_statuses.Include(s => s.Client);
                
                
                switch (sortOrder)
                {
                    case "name_desc":
                        repair_statuses = repair_statuses.OrderByDescending(s => s.ID);
                        break;
                  
                    default:
                        repair_statuses = repair_statuses.OrderBy(s => s.ID);
                        break;
                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);
            
                return View(repair_statuses.ToPagedList(pageNumber, pageSize));

                
               
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Repair_status") });
            }

        }

        // GET: Repair_status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair_status repair_status = db.Repair_statuses.Find(id);
            if (repair_status == null)
            {
                return HttpNotFound();
            }
            return View(repair_status);
        }

        // GET: Repair_status/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "ImięNazwisko");
            return View();
        }

        // POST: Repair_status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequestID,ClientID,Data_rozpoczęcia,Data_zakończenia,Status,Opis_naprawy")] Repair_status repair_status)
        {
            if (ModelState.IsValid)
            {
                db.Repair_statuses.Add(repair_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ID", "ImięNazwisko", repair_status.ClientID);
            return View(repair_status);
        }

        // GET: Repair_status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair_status repair_status = db.Repair_statuses.Find(id);
            if (repair_status == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "ImięNazwisko", repair_status.ClientID);
            return View(repair_status);
        }

        // POST: Repair_status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RequestID,ClientID,Data_rozpoczęcia,Data_zakończenia,Status,Opis_naprawy")] Repair_status repair_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repair_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "ImięNazwisko", repair_status.ClientID);
            return View(repair_status);
        }

        // GET: Repair_status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair_status repair_status = db.Repair_statuses.Find(id);
            if (repair_status == null)
            {
                return HttpNotFound();
            }
            return View(repair_status);
        }

        // POST: Repair_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repair_status repair_status = db.Repair_statuses.Find(id);
            db.Repair_statuses.Remove(repair_status);
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
