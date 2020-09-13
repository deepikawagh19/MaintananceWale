using School_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Application.Controllers
{
    public class AdminController : Controller
    {
        SchoolEntities dalObj = new SchoolEntities();

       
        public ActionResult Index(string Sorting_Order, string Search_Data)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
            ViewBag.SortingDate = Sorting_Order == "Date_Enroll" ? "Date_Description" : "Date";
           

            var students = from stu in dalObj.StudentDetails select stu;
            {
                students = students.Where(stu => stu.StudentName.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Name_Description":
                    students = students.OrderByDescending(stu => stu.StudentName);
                    break;
                case "Date_Enroll":
                    students = students.OrderBy(stu => stu.BirthDate);
                    break;
                case "Date_Description":
                    students = students.OrderByDescending(stu => stu.BirthDate);
                    break;
                default:
                    students = students.OrderBy(stu => stu.StudentName);
                    break;
            }
            return View(students.ToList());
        }
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(StudentDetail s)
        {
           
            using (SchoolEntities dalObj = new SchoolEntities())
            {
                dalObj.StudentDetails.Add(s);
                dalObj.SaveChanges();
            }
            ModelState.Clear();
            //return View();
            //  dalObj.Services.Add(service);
            //  dalObj.SaveChanges();

            return Redirect("ShowStudent");
        }

        public ActionResult ShowStudent()
        {
            List<StudentDetail> s = dalObj.StudentDetails.ToList();


            return View("ShowStudent", s);
        }
        public ActionResult Edit(int id)
        {
            StudentDetail s= dalObj.StudentDetails.Find(id);
            return View("Edit", s);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentDetail s)
        {
          StudentDetail student= dalObj.StudentDetails.Find(s.StudentId);

           student.StudentName = s.StudentName;
            student.Address = s.Address;
            student.BirthDate = s.BirthDate;
            student.ContactNo = s.ContactNo;
            student.EmailId = s.EmailId;
            dalObj.SaveChanges();
            return Redirect("/Admin/ShowStudent");
        }


    }
}
