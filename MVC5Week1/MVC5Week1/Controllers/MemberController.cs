using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Week1.Models;
using System.Web.Security;

namespace MVC5Week1.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberViewModels login)
        {
            if(CheckLogin(login.帳號,login.密碼))
            {
                FormsAuthentication.RedirectFromLoginPage(login.帳號, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "帳號或密碼輸入錯誤");
            return View();
        }

        private bool CheckLogin(string account, string pwd)
        {
            var db = new 客戶資料Entities();
            string hashpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
            var data = db.客戶資料.FirstOrDefault(p => p.帳號 == account && p.密碼 == hashpwd  && !p.是否已刪除);
            if(data != null)
            {
                return true;
            }
            return false;

        }
        
    }
}