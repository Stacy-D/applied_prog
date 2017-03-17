using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep_Unit2.Models;
using Rep_Unit2.DAL;
using PagedList;

namespace Rep_Unit2.Controllers
{
    public class StudentController : Controller
    {
        private UniversityWorker uniWorker = new UniversityWorker();


        //
        // GET: /Student/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(uniWorker.getAllStudents(searchString, sortOrder).ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            return View(uniWorker.getStudentById(id));
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Enrollments(int id) {
            return View(uniWorker.getStudentEnrollments(id));
        }
        public ActionResult Enroll(int id) {
            return View();
        } 

        //
        // POST: /Student/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
           [Bind(Include = "LastName, FirstMidName, EnrollmentDate")]
           Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    uniWorker.createStudent(student);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(student);
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id)
        {
            Student student = uniWorker.getStudentById(id);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disenroll([Bind(Include = "LastName, FirstMidName, EnrollmentDate, StudentID")]
           Student student, int courseId) {
            uniWorker.disenrollStudent(student, courseId);
            return View(student);
        }
        //
        // POST: /Student/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
           [Bind(Include = "StudentID, LastName, FirstMidName, EnrollmentDate")]
         Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    uniWorker.updateStudent(student);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(student);
        }
        //
        // GET: /Student/Dissenroll

        public ActionResult Disenroll(bool? saveChangesError = false, int stud=0, int enroll=0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Enrollment enrollment = uniWorker.getStudentEnrollment(stud, enroll);
            return View(enrollment);
        }
        //
        // GET: /Student/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Student student = uniWorker.getStudentById(id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                uniWorker.deleteStudent(id);
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            uniWorker.Dispose();
            base.Dispose(disposing);
        }
    }
}