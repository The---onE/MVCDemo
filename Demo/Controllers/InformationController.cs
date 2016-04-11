using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Manager;

namespace Demo.Controllers
{
    public class InformationController : Controller
    {
        private DemoEntities db = new DemoEntities();

        //
        // GET: /Information/

        public ActionResult Index()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                var information = InformationManager.GetInstance().SelectAll();
                return View(information.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Information/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                Information information = InformationManager.GetInstance().SelectById(id);
                if (information == null)
                {
                    return HttpNotFound();
                }
                return View(information);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Information/Create

        public ActionResult Create()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Information/Create

        [HttpPost]
        public ActionResult Create(string data)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    InformationManager.GetInstance().Add(user.id, data);
                    return RedirectToAction("Index");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Information/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                Information information = InformationManager.GetInstance().SelectById(id);
                if (information == null)
                {
                    return HttpNotFound();
                }
                return View(information);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Information/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, string data)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    InformationManager.GetInstance().Modify(id, user.id, data);
                    return RedirectToAction("Index");
                }
                return View(data);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Information/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                Information information = InformationManager.GetInstance().SelectById(id);
                if (information == null)
                {
                    return HttpNotFound();
                }
                return View(information);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Information/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                Information information = InformationManager.GetInstance().SelectById(id);
                InformationManager.GetInstance().Delete(id, user.id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}