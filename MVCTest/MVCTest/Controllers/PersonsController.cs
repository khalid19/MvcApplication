using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTest;
using MVCTest.DAL;
using MVCTest.Models;

namespace MVCTest.Controllers
{
    public class PersonsController : Controller
    {
        private Test3DbContext db = new Test3DbContext();

        // GET: /Persons/
        public ActionResult Index(PersonViewModel model)
        {
            model.Persons = db.People.ToList();
            return View(model);
        }

        // GET: /Persons/Details/5
        public ActionResult Edit(PersonViewModel model)
        {
            Person person = db.People.Find(model.PersonId) ?? new Person();
            model.PersonId = person.PersonId;
            model.FirstName = person.FirstName;
            model.LastName = person.LastName;
            model.Date = person.Date;
            model.Salary = person.Salary;
            model.ImgeURL = person.ImgeURL;

            return View(model);
        }

        public ActionResult Save(PersonViewModel model, HttpPostedFileBase file)
        {
            string imageName = System.IO.Path.GetFileName(file.FileName);

            string physicalPath = Server.MapPath("~/Image/" + imageName);

            file.SaveAs(physicalPath);
            Person person=new Person();

            person.PersonId = model.PersonId;
            person.FirstName = model.FirstName;
            person.LastName = model.LastName;
            person.Date = model.Date;
            person.Salary = model.Salary;
            person.ImgeURL = imageName;
            if (model.PersonId>0)
            {
                db.Entry(person).State = EntityState.Modified;
            }
            else
            {
                db.People.Add(person);
            }
            int saveChanges = db.SaveChanges();
            if (saveChanges>0)
            {
                return RedirectToAction("Index", model);
            }
            return View("Edit",model);
        }

        public ActionResult Delete(PersonViewModel model)
        {
            Person person = db.People.Find(model.PersonId);

            if (model.PersonId>0)
            {
                db.Entry(person).State=EntityState.Deleted;
            }

            int saveChanges = db.SaveChanges();

            if (saveChanges>0)
            {
                return RedirectToAction("Index", model);
            }
            return View("Error");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
