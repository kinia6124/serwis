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
using Serwis.ViewModel;

namespace Serwis.Controllers
{
    public class EmployeeController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: Employee
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if(Request.IsAuthenticated)
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

                var employees = from s in db.Employees
                              select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    employees = employees.Where(s => s.Imię.Contains(searchString)
                                          || s.Nazwisko.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        employees = employees.OrderByDescending(s => s.Imię);
                        break;
                    case "Nazwisko":
                        employees = employees.OrderBy(s => s.Nazwisko);
                        break;
                    case "Nazwisko_desc":
                        employees = employees.OrderByDescending(s => s.Nazwisko);
                        break;
                    default:
                        employees = employees.OrderBy(s => s.Imię);
                        break;
                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);

                return View(employees.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Employee") });
            }
            
        }

        private ActionResult View(Func<ViewResult> view)
        {
            throw new NotImplementedException();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Imię,Nazwisko,Stanowisko,Adres_email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Imię,Nazwisko,Stanowisko,Adres_email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
