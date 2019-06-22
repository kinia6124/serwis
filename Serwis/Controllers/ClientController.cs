using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Serwis.DAL;
using Serwis.Models;
using PagedList;
using Serwis.ViewModel;

using System.Threading.Tasks;

namespace Serwis.Controllers
{
    public class ClientController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: Clients
              
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.LastNameSortParm = sortOrder == "Nazwisko" ? "Nazwisko_desc" : "Nazwisko";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;

                var clients = from s in db.Clients
                              select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    clients = clients.Where(s => s.Imię.Contains(searchString)
                                          || s.Nazwisko.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        clients = clients.OrderByDescending(s => s.Imię);
                        break;
                    case "Nazwisko":
                        clients = clients.OrderBy(s => s.Nazwisko);
                        break;
                    case "Nazwisko_desc":
                        clients = clients.OrderByDescending(s => s.Nazwisko);
                        break;
                    default:
                        clients = clients.OrderBy(s => s.Imię);
                        break;
                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);

                return View(clients.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Client") });
            }

        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Imię,Nazwisko,Adres,Miasto,Nr_telefonu,Adres_email")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                            {
                                db.Clients.Add(client);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
            }
            catch(DataException /* dex */ )
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, ans if the " +
                    "problem persists see your system administrator.");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Imię,Nazwisko,Adres,Miasto,Nr_telefonu,Adres_email")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                      db.Entry(client).State = EntityState.Modified;
                      db.SaveChanges();
                      return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                    "problem persists see your system administrator.");
            }
            
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        { 
            try
            {
                Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            }
            catch (DataException /*dex*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
