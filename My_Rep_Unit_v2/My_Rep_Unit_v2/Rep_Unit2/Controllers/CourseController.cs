using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rep_Unit2.Models;
using Rep_Unit2.DAL;

namespace Rep_Unit2.Controllers
{
    public class CourseController : Controller
    {
        private UniversityWorker uniWorker = new UniversityWorker();

        //
        // GET: /Course/

        public ViewResult Index()
        {
            return View(this.uniWorker.getAllCourses().ToList());
        }

        //
        // GET: /Course/Details/5

        public ViewResult Details(int id)
        {
            return View(this.uniWorker.getCourseById(id));
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "CourseID,Title,Credits,DepartmentID")]
         Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.uniWorker.createCourse(course);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        public ActionResult Edit(int id)
        {
            Course course = uniWorker.getCourseById(id);
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
             [Bind(Include = "CourseID,Title,Credits,DepartmentID")]
         Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    uniWorker.updateCourse(course);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = uniWorker.getAllDepartments();
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id)
        {
            Course course = uniWorker.getCourseById(id);
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uniWorker.deleteCourse(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            uniWorker.Dispose();
            base.Dispose(disposing);
        }
    }
}