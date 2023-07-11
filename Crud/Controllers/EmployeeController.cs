using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Crud.Controllers
{
    public class EmployeeController : Controller
    {
        Databasecontext db = new Databasecontext();
        // GET: Employee
        public ActionResult Index()
        {
            var data = db.Emps.ToList();
            return View(data);
        }
        
        public ActionResult Create(int id = 0)
        {
            if (id ==0)
            {
                ViewBag.title = "Create";
                ViewBag.btn = "create";

                return View();
            }
            else
            {
                Emp emp = new Emp();
                 emp = db.Emps.Where(x=>x.E_Id==id).FirstOrDefault();
                ViewBag.btn = "update";
                ViewBag.title = "Edit";

                return View(emp);
            }
            
        }

        [HttpPost]
        public ActionResult Create(Emp emp)
        {
            if (emp.E_Id > 0) //for update
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else //for create
            {
                db.Emps.Add(emp);
                db.SaveChanges(); 
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            var data = db.Emps.Find(id);
            db.Emps.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}