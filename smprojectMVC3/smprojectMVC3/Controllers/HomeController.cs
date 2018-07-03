using smprojectMVC3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace smprojectMVC3.Controllers
{
    public class HomeController : Controller
    {
        private SmProjectEntities db = new SmProjectEntities();
        private LibaryandSaveViewModel viewModel = new LibaryandSaveViewModel();

        [HttpPost]
        public ActionResult Index(string newnameInput)
        {
            Session["projectID"] = newnameInput;

            return View(viewModel);
        }

        // GET: Home
        public ActionResult Index()
        {
            var query1 = from o in db.SaveTables
                         select o;
            List<SaveTable> empList1 = query1.ToList();
            var query = from o in db.LegoLibraries
                        select o;
            List<LegoLibrary> empList = query.ToList();
            LibaryandSaveViewModel viewModel = new LibaryandSaveViewModel
            {
                LegoLibrary = empList,
                SaveTables = empList1
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Member(string emailInput, string loginPwd)
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(emailInput))
            {
                errorMessage += "Please keyin your name <br>";
            }
            if (string.IsNullOrEmpty(emailInput))
            {
                errorMessage += "Please keyin password <br>";
            }

            if (errorMessage == "")
            {
                Session["userId"] = emailInput;

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            Session["userid"] = "Guest";

            return RedirectToAction("Index", "Home");
        }
    }
}