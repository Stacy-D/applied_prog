using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rep_Unit2.Models
{
    public class UniversityWorker
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public IEnumerable<Course> getAllCourses() {
            return this.unitOfWork.CourseRepository.Get(includeProperties: "Department");
        }

        public Course getCourseById(int id) {
        return unitOfWork.CourseRepository.GetByID(id);
        }

        public void createCourse(Course course) {
            unitOfWork.CourseRepository.Insert(course);
            unitOfWork.Save();
        }
        public void updateCourse(Course course) {
            unitOfWork.CourseRepository.Update(course);
            unitOfWork.Save();
        }

        public IEnumerable<Department> getAllDepartments() {
           return unitOfWork.DepartmentRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name));
        }
        public void deleteCourse(int id) {
            unitOfWork.CourseRepository.Delete(id);
            unitOfWork.Save();
        }
        public IEnumerable<Student> getAllStudents(string searchString, string sortOrder) {
            var students = unitOfWork.StudentRepository.Get();
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            return students;
        }
        public Student getStudentById(int id)
        {
            return unitOfWork.StudentRepository.GetByID(id);
        }
        public void createStudent(Student stud)
        {
            unitOfWork.StudentRepository.Insert(stud);
            unitOfWork.Save();
        }
        public void updateStudent(Student stud)
        {
            unitOfWork.StudentRepository.Update(stud);
            unitOfWork.Save();
        }
        public void deleteStudent(int id)
        {
            unitOfWork.StudentRepository.Delete(id);
            unitOfWork.Save();
        }
        public IEnumerable<Enrollment> getStudentEnrollments(int id) {
            return getStudentById(id).Enrollments;
        }
        public Enrollment getStudentEnrollment(int stud, int id)
        {
            return getStudentById(stud).Enrollments.Where(e => e.EnrollmentID.Equals(id)).First();
        }

        public void enrollStudent(Student stud, Course course) {
        }
        public void editEnrollment(Student stud, int courseId, Grade grade) {
        }
        public void disenrollStudent(Student stud, int course) {
            int enroll = this.unitOfWork.EnrollmentRepository.Get().Where(e => e.StudentID.Equals(stud.StudentID) && e.CourseID.Equals(course)).First().EnrollmentID;
            unitOfWork.EnrollmentRepository.Delete(enroll);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}