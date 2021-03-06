﻿using System;
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
    public class 客戶銀行資訊Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();

        // GET: 客戶銀行資訊
        public ActionResult Index(string QueryName,int? page)
        {
            var 客戶銀行資訊 = repo.All(QueryName);
            var pagenumber = page ?? 1;
            ViewBag.QueryName = QueryName;
            return View(客戶銀行資訊.ToPagedList(pagenumber,2));
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱");
            //ViewBag.客戶Id = new SelectList(db.客戶資料.Where(p => p.是否已刪除 == false), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            //ViewBag.客戶Id = new SelectList(db.客戶資料.Where(p => p.是否已刪除 == false), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                var db = (客戶資料Entities)repo.UnitOfWork.Context;
                db.Entry(客戶銀行資訊).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var repoC = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
            ViewBag.客戶Id = new SelectList(repoC.All().AsEnumerable<客戶資料>(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            //ViewBag.客戶Id = new SelectList(db.客戶資料.Where(p => p.是否已刪除 == false), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 =repo.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 =repo.Find(id);
            客戶銀行資訊.是否已刪除 = true;
            //db.客戶銀行資訊.Remove(客戶銀行資訊);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}
