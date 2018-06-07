using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.Backend;

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        // A ViewModel used for the Message that contains the Message list
        private MessageViewModel MessageViewModel = new MessageViewModel();

        // The Backend Data source
        private QABackend QABackend = QABackend.Instance;

        // Get: Home page
        public ActionResult Index()
        {
            return View();
        }

        // Get: About
        public ActionResult About()
        {
            return View();
        }

        // Get: Privacy
        public ActionResult Privacy()
        {
            return View();
        }

        // Get: Contact
        public ActionResult Contact()
        {
            var newMessage = new MessageModel();
            return View(newMessage);
        }

        [HttpPost]
        public ActionResult Contact([Bind(Include =
                                    "Id,"+
                                    "Message,"+
                                    "")] MessageModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }
            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }
            QABackend.Create(data);
            return RedirectToAction("Contact");
        }

        // Get: ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // Get: UpdatePassword
        public ActionResult UpdatePassword()
        {
            return View();
        }

        // Get: AdminLogin
        public ActionResult AdminLogin()
        {
            return View();
        }

        // Get: StudentLogin
        public ActionResult StudentLogin()
        {
            return View();
        }

        // Get: KioskLogin
        public ActionResult KioskLogin()
        {
            return View();
        }

        // GET: Q&A List
        public ActionResult QA()
        {
            var myDataList = QABackend.Index();
            var MessageViewModel = new MessageViewModel(myDataList);
            return View(MessageViewModel);
        }
    }
}