using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Week1.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;

namespace MVC5Week1.Controllers
{
    public class CustomerController : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        //private 客戶資料Entities db = new 客戶資料Entities();
        // GET: Customer
        public ActionResult Index(string QueryName,string type,int? page)
        {
            //var data = db.客戶資料.Where(p => p.是否已刪除 == false).AsQueryable();
            var data = repo.All(QueryName, type);
            ViewBag.type = new SelectList(repo.Get客戶分類());
            ViewBag.QueryName = QueryName;
            var pagenumber = page ?? 1;
            return View(data.ToPagedList(pagenumber,2));
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
                
                CustomerData.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(CustomerData.密碼, "SHA1");
                repo.Add(CustomerData);
                repo.UnitOfWork.Commit();
                //db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            //var data = db.客戶資料.FirstOrDefault(p => p.Id == Id);
            var data = repo.Find(Id);
            return View(data);
        }

        public ActionResult Edit(int Id)
        {
            var data = repo.Find(Id);
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
                var db = (客戶資料Entities)repo.UnitOfWork.Context;
                db.Entry(CustomerData).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            客戶資料 data = repo.Find(id);
            data.是否已刪除 = true;
            //db.Entry(data).State = EntityState.Modified;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }


        public ActionResult Full(string QueryName)
        {
            vw客戶完整資料Repository repovw = RepositoryHelper.Getvw客戶完整資料Repository();
            var data = repovw.All().AsQueryable();
            if(!string.IsNullOrEmpty(QueryName))
            {
                ViewBag.QueryName = QueryName;
                data = data.Where(p => p.客戶名稱.Contains(QueryName));
            }
            return View(data.ToList());
        }
    }
}