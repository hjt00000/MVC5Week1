using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Week1.Models;
using PagedList;
using PagedList.Mvc;

namespace MVC5Week1.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        //private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶聯絡人
        public ActionResult Index(string QueryName,int? page)
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料).Where(p => p.是否已刪除 == false);
            var 客戶聯絡人 = repo.All(QueryName);
            var pageNumber = page ?? 1;
            ViewBag.QueryName = QueryName;
            return View(客戶聯絡人.ToPagedList(pageNumber,2));
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                //db.客戶聯絡人.Add(客戶聯絡人);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            //ViewBag.客戶Id = new SelectList(db.客戶資料.Where(p => p.是否已刪除 == false), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db = (客戶資料Entities)repo.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.是否已刪除 = true;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult List(int CustomerId)
        {
            var data = repo.All().Where(p => p.客戶Id == CustomerId);
            ViewData["CustomerId"] = CustomerId;
            return View(data.ToList());
        }

        [HttpPost]
        public ActionResult List(IList<BatchContact> Customers,int CustomerId)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in Customers)
                {
                    var tmp = repo.Find(item.Id);
                    tmp.手機 = item.手機;
                    tmp.職稱 = item.職稱;
                    tmp.電話 = item.電話;
                }
                repo.UnitOfWork.Commit();
                
            }
            return RedirectToAction("Details","Customer",new {id = CustomerId });
        }
    }
}
