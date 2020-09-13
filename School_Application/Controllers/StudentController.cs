using School_Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Application.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
       SchoolEntities dalObj = new SchoolEntities();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
      return View();
        }
        [HttpPost]
        public ActionResult Add(StudentDetail s)
        {
            string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
            string extension = Path.GetExtension(s.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            s.DocumentImage = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            s.ImageFile.SaveAs(fileName);
            using (SchoolEntities dalObj = new SchoolEntities())
            {
                dalObj.StudentDetails.Add(s);
                dalObj.SaveChanges();
            }
            ModelState.Clear();
            //return View();
            //  dalObj.Services.Add(service);
            //  dalObj.SaveChanges();

            return Redirect("Show");
        }
     
        public ActionResult Show()
        {
            List<StudentDetail> s = dalObj.StudentDetails.ToList();


            return View("Show", s);
        }
        

    }
}
