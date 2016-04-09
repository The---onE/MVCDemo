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
            int result = UserManager.GetInstance().Login("xmx", "12345678");
            if (result == UserManager.SUCCESS)
            {
                if (UserManager.GetInstance().loginFlag)
                {
                    User user = UserManager.GetInstance().user;
                    int id = user.id;

                    var list = InformationManager.GetInstance().SelectAll();

                    foreach (var info in list)
                    {
                        string s = info.data;
                    }

                    list = InformationManager.GetInstance().SelectByCreator(id);
                    foreach (var info in list)
                    {
                        string s = info.data;
                    }
                }
            }

            return View();
        }
    }
}
