using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Serwis.DAL;
using Serwis.ViewModel;

namespace Serwis.Controllers
{
    public class HomeController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        public ActionResult Index()
        {
            return View();
        }

       

        public async Task<ActionResult> About()
        {
            if (Request.IsAuthenticated)
            {
                IQueryable<RequestStatistic> data =
                from request in db.Requests
                group request by request.ClientID into rCount
                select new RequestStatistic()
                {
                    ClientID = rCount.Key,
                    RequestCount = rCount.Count()
                };

                return View(data.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("About", "Home") });
            }

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt do Administratora serwisu";

            return View();
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}