using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.Backend;

namespace TeamProject.Controllers
{
    public class AdminController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

        // A ViewModel used for the Archived student that contains the list of them
        private ArchiveViewModel ArchiveViewModel = new ArchiveViewModel();

        // The Backend Data source
        private ArchiveBackend ArchiveBackend = ArchiveBackend.Instance;

        // GET: Admin Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // Get: profile
        public ActionResult Profile()
        {
            return View();
        }

        /// <summary>
        /// Students, the page that shows all the Students
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Students
        public ActionResult Students()
        {
            // Load the list of data into the StudentList
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            return View(StudentViewModel);
        }



        /// <summary>
        /// Read information on a single Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Admin/Read/
        public ActionResult Read(string id = null)
        {
            var myDataStudent = StudentBackend.Read(id);
            if (myDataStudent == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            var myData = new StudentDisplayViewModel(myDataStudent);
            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            return View(myData);
        }
        /// <summary>
        /// This opens up the archive a current student screen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Admin/Archive
        public ActionResult Archive(string id = null)
        {
            var targetStudent = StudentBackend.Read(id);
            if (targetStudent == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }
            var data = new ArchiveModel(targetStudent);
            return View(data);
        }

        /// <summary>
        /// Archive the student data and delete the student from current list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Archive([Bind(Include=
                                    "Id,"+
                                    "FName,"+
                                    "LName,"+
                                    "SYear,"+
                                    "Status,"+
                                    "")] ArchiveModel data)
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
            ArchiveBackend.Create(data);
            StudentBackend.Delete(data.Id);
            return RedirectToAction("Students");
        }

        /// <summary>
        /// This opens up the make a new Student screen
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Create
        public ActionResult Create()
        {
            var myData = new StudentModel();
            return View(myData);
        }

        /// <summary>
        /// Make a new Student sent in by the create Admin screen
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "LastName,"+
                                        "Email,"+
                                        "PictureId,"+
                                        "Status,"+
                                        "")] StudentModel data)
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

            if (string.IsNullOrEmpty(data.Id))
            {
                // Return back for Edit
                return View(data);
            }
            StudentBackend.Create(data);
            return RedirectToAction("Students");
        }

        /// <summary>
        /// This will show the details of the Student to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Admin/Update/
        public ActionResult Update(string id = null)
        {
            var myDataStudent = StudentBackend.Read(id);
            if (myDataStudent == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            var myData = new StudentDisplayViewModel(myDataStudent);
            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }
            return View(myData);
        }

        /// <summary>
        /// This updates the Student based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Admin/Update/
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "LastName,"+
                                        "Email,"+
                                        "PictureId,"+
                                        "Status,"+
                                        "")] StudentDisplayViewModel data)
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

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            var myDataStudent = new StudentModel(data);
            StudentBackend.Update(myDataStudent);

            return RedirectToAction("Students");
        }

        /// <summary>
        /// This shows the Student info to be deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Student/Delete/
        public ActionResult Delete(string id = null)
        {
            var myDataStudent = StudentBackend.Read(id);
            if (myDataStudent == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            var myData = new StudentDisplayViewModel(myDataStudent);
            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            return View(myData);
        }

        /// <summary>
        /// This deletes the Student sent up as a post from the Student delete page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "LastName,"+
                                        "Email,"+
                                        "PictureId,"+
                                        "Uri,"+
                                        "")] StudentDisplayViewModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }
            if (data == null)
            {
                // Send to Error page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            StudentBackend.Delete(data.Id);

            return RedirectToAction("Students");
        }

        /// <summary>
        /// Shows the studnet name to remove from archive list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET
        public ActionResult Remove(string id = null)
        {
            var ArchivedStudent = ArchiveBackend.Read(id);
            if (ArchivedStudent == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            var myData = new ArchiveModel(ArchivedStudent);
            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            return View(myData);
        }

        /// <summary>
        /// This remove the Student sent up as a post from the archive remove page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove([Bind(Include=
                                        "Id,"+
                                        "FName,"+
                                        "LName,"+
                                        "SYear,"+
                                        "Status,"+
                                        "")] ArchiveModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }
            if (data == null)
            {
                // Send to Error page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            ArchiveBackend.Delete(data.Id);

            return RedirectToAction("ArchivePage");
        }

        /// <summary>
        /// Shows the studnet name to recover from archive list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET
        public ActionResult Recover(string id = null)
        {
            var ArchivedStudent = ArchiveBackend.Read(id);
            if (ArchivedStudent == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            var myData = new ArchiveModel(ArchivedStudent);
            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            return View(myData);
        }

        /// <summary>
        /// This recover the Student sent up as a post from the archive recover page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Recover ([Bind(Include=
                                        "Id,"+
                                        "FName,"+
                                        "LName,"+
                                        "SYear,"+
                                        "Status,"+
                                        "")] ArchiveModel data)
        {
            var myData = new StudentModel(data);
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }
            if (data == null)
            {
                // Send to Error page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            StudentBackend.Create(myData);
            ArchiveBackend.Delete(data.Id);
            return RedirectToAction("ArchivePage");
        }

        //print admin archive page
        public ActionResult ArchivePage()
        {
            // Load the list of data into the StudentList
            var myDataList = ArchiveBackend.Index();
            var ArchiveViewModel = new ArchiveViewModel(myDataList);
            return View(ArchiveViewModel);
        }

        //print admin report page
        public ActionResult Report()
        {
            return View();
        }

        //print register admin page
        public ActionResult Registeradmin()
        {
            return View();
        }

        //print admin overwritedata page
        public ActionResult Overwritedata()
        {
            return View();
        }

        //print admin overwritedatabyname page
        public ActionResult Overwritedatabyname()
        {
            return View();
        }

        //print admin overwritedatabydate page
        public ActionResult Overwritedatabydate()
        {
            return View();
        }

        //print admin calendar
        public ActionResult Calendar()
        {
            return View();
        }
    }
}