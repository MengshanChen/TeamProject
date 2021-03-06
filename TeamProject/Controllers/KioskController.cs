﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.Backend;

namespace TeamProject.Controllers
{
    public class KioskController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

        // GET: Kiosk
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Attendance, the page that shows all the students
        /// </summary>
        /// <returns></returns>
        // Get: Attendance
        public ActionResult Attendance()
        {
            // Load the list of data into the StudentList
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }

        // GET: Kiosk/SetLogout/
        public ActionResult SetLogin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Data");
            }

            StudentBackend.ToggleStatusById(id);
            return RedirectToAction("ConfirmLogin", "Kiosk", new { id });
        }

        // GET: Kiosk/SetLogout/
        public ActionResult SetLogout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Data");
            }

            StudentBackend.ToggleStatusById(id);
            return RedirectToAction("ConfirmLogout", "Kiosk", new { id });
        }

        /// <summary>
        /// Shows the login confirmation screen
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult ConfirmLogin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var myDataList = StudentBackend.Read(id);
            var StudentViewModel = new StudentDisplayViewModel(myDataList);

            //Todo, replace with actual transition time
            StudentViewModel.LastDateTime = DateTime.Now;

            return View(StudentViewModel);
        }

        /// <summary>
        /// Shows the login confirmation screen
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns></returns>
        public ActionResult ConfirmLogout(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var myDataList = StudentBackend.Read(id);
            var StudentViewModel = new StudentDisplayViewModel(myDataList);

            //Todo, replace with actual transition time
            StudentViewModel.LastDateTime = DateTime.Now;

            return View(StudentViewModel);
        }
    }
}