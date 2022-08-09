using mvccrud.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace mvccrud.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        entityEntities3 dbobj = new entityEntities3();
        
        public ActionResult Student(tbl_Student stobj)
        {
                return View(stobj);
        }

        [HttpPost]
        public ActionResult AddStudent(tbl_Student model)
        {
            tbl_Student stobj = new tbl_Student();
            if (ModelState.IsValid)
            {

                stobj.id = model.id;
                stobj.name = model.name;
                stobj.fname = model.fname;
                stobj.email = model.email;
                stobj.mobile = model.mobile;
                stobj.description = model.description;
                if (model.id == 0)
                {
                    dbobj.tbl_Student.Add(stobj);
                    dbobj.SaveChanges();
                }
                else
                {
                    dbobj.Entry(stobj).State = EntityState.Modified;
                    dbobj.SaveChanges();
                }

            }
            ModelState.Clear();
            return RedirectToAction("StudentList");
        }

        public ActionResult EditStudent(tbl_Student stobj)
        {
            //    dbobj.Entry(stobj).State = EntityState.Modified;
            // dbobj.SaveChanges();

            return View(stobj);

        }

        public ActionResult StudentList()
        {
            var res = dbobj.tbl_Student.ToList();
            return View(res);
        }

        public ActionResult Delete(int Id)
        {
            var res = dbobj.tbl_Student.Where(x => x.id == Id).First();
            dbobj.tbl_Student.Remove(res);
            dbobj.SaveChanges();
            var list = dbobj.tbl_Student.ToList();
            return View("StudentList",list);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_Student stobj)
        {

           var user = dbobj.tbl_Student.Where(x => x.name == stobj.name && x.email == stobj.email).Count();
            if (user > 0)
            {
                ModelState.Clear();
                return RedirectToAction("Dashboard");
            }
            else
            {
                Response.Write("Somthing Wrong!!");
                ModelState.Clear();
                return View();
            }
           
        }

        public ActionResult Dashboard()
        {
            ViewBag.key = "successfully Login";
            return View();
        }
       
      

       


    }

}