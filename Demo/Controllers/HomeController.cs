using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Demo.Models;
using Demo.Manager;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessRegister(string username, string password)
        {
            //string username = Request["username"];
            //string password = Request["password"];
            int result = UserManager.GetInstance().Register(username, password);
            switch (result)
            {
                case UserManager.SUCCESS:
                    ViewData["result"] = "注册成功";
                    break;

                case UserManager.USERNAME_EXIST:
                    ViewData["result"] = "用户名已存在";
                    break;

                case UserManager.USERNAME_ERROR:
                    ViewData["result"] = "用户名格式不正确";
                    break;

                case UserManager.PASSWORD_ERROR:
                    ViewData["result"] = "密码格式不正确";
                    break;
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessLogin(string username, string password)
        {
            int result = UserManager.GetInstance().Login(username, password);
            switch (result)
            {
                case UserManager.SUCCESS:
                    ViewData["result"] = "登录成功";
                    var user = UserManager.GetInstance().user;
                    break;

                case UserManager.USERNAME_ERROR:
                    ViewData["result"] = "用户名不存在";
                    break;

                case UserManager.MATCH_ERROR:
                    ViewData["result"] = "用户名密码不匹配";
                    break;
            }

            return View();
        }
    }
}
