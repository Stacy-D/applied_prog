using ContosoUniversity.DAL;
using Rep_Unit2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rep_Unit2.Models
{
    public class UnitOfWork : IDisposable
    {
        private SchoolContext context = new SchoolContext();
        private GenericRepository<Department> departmentRepository;
        private GenericRepository<Course> courseRepository;
        private GenericRepository<Student> studentRepository;
        private GenericRepository<Enrollment> enrollmentRepository;

        public GenericRepository<Department> DepartmentRepository
        {
            get
            {

                if (this.departmentRepository == null)
                {
                    this.departmentRepository = new GenericRepository<Department>(context);
                }
                return departmentRepository;
            }
        }
        public GenericRepository<Enrollment> EnrollmentRepository
        {
            get
            {

                if (this.enrollmentRepository == null)
                {
                    this.enrollmentRepository = new GenericRepository<Enrollment>(context);
                }
                return enrollmentRepository;
            }
        }
        public GenericRepository<Student> StudentRepository
        {
            get
            {

                if (this.studentRepository == null)
                {
                    this.studentRepository = new GenericRepository<Student>(context);
                }
                return studentRepository;
            }
        }

        public GenericRepository<Course> CourseRepository
        {
            get
            {

                if (this.courseRepository == null)
                {
                    this.courseRepository = new GenericRepository<Course>(context);
                }
                return courseRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}