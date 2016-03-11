using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Week1.Models;
using System.Data.Entity;

namespace MVC5Week1.Controllers
{
    public class CustomerController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: Customer
        public ActionResult Index(string QueryName)
        {
            var data = db.客戶資料.Where(p => p.是否已刪除 == false).AsQueryable();
            if(!string.IsNullOrEmpty(QueryName))
            {
                ViewBag.QueryName = QueryName;
                data = data.Where(p => p.客戶名稱.Contains(QueryName));
            }
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 CustomerData)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(CustomerData);
                db.SaveChanges();
            }
            return RedirectToAction("客戶資料");
        }

        public ActionResult Details(int Id)
        {
            var data = db.客戶資料.FirstOrDefault(p => p.Id == Id);
            return View(data);
        }

        public ActionResult Edit(int Id)
        {
            var data = db.客戶資料.Find(Id);
            if(data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(客戶資料 CustomerData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(CustomerData).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var data = db.客戶資料.Find(id);
            data.是否已刪除 = true;
            //db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Full(string QueryName)
        {

            var data = db.vw客戶完整資料.AsQueryable();
            if(!string.IsNullOrEmpty(QueryName))
            {
                ViewBag.QueryName = QueryName;
                data = data.Where(p => p.客戶名稱.Contains(QueryName));
            }
            return View(data.ToList());
        }
    }
}