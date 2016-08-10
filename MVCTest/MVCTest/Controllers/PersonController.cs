using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTest.DAL;

namespace MVCTest.Controllers
{
    public class PersonController : Controller
    {
        //
        // GET: /Person/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            if (file != null)
            {
                Test3DbContext db = new Test3DbContext();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Image/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);

                //save new record in database
                Person newRecord = new Person();
                newRecord.FirstName = Request.Form["FirstName"];
                newRecord.LastName = Request.Form["FirstName"];
                newRecord.Date = Convert.ToDateTime(Request.Form["Date"]);
                newRecord.Salary = Convert.ToDouble(Request.Form["Salary"]);
                newRecord.ImgeURL = ImageName;
                db.People.Add(newRecord);
                db.SaveChanges();

            }
            //Display records
            return RedirectToAction("Display");
        }
        public ActionResult Display()
        {
            return View();
        }


	}
}