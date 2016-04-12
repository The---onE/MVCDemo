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
        #region 首页
        /// <summary>
        /// 首页信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region 注册页面
        /// <summary>
        /// GET注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        } 
        #endregion

        #region 处理注册
        /// <summary>
        /// 处理注册过程，返回注册结果
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回SUCCESS(1)为成功，返回USERNAME_EXIST(-3)为用户已被注册，返回USERNAME_ERROR(-2)为用户名格式不正确，返回PASSWORD_ERROR(-4)为密码格式不正确</returns>
        [HttpPost]
        public ActionResult ProcessRegister(string username, string password)
        {
            int result = UserManager.GetInstance().Register(username, password);

            return Content("" + result);
        } 
        #endregion

        #region 处理登录
        /// <summary>
        /// 处理登录过程，返回登录结果
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回SUCCESS(1)为成功，返回MATCH_ERROR(-1)为密码错误，返回USERNAME_ERROR(-2)为用户不存在</returns>
        [HttpPost]
        public ActionResult ProcessLogin(string username, string password)
        {
            //验证登录结果
            int result = UserManager.GetInstance().Login(username, password);
            if (result == UserManager.SUCCESS)
            {
                //登录成功，将用户信息写入Session
                var user = UserManager.GetInstance().user;
                Session["user"] = user;
            }

            return Content("" + result);
        } 
        #endregion

        #region 修改密码页面
        /// <summary>
        /// GET修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Reset()
        {
            return View();
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 通过旧密码修改为新密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>返回SUCCESS(1)为成功，返回USERNAME_ERROR(-2)为用户不存在，返回MATCH_ERROR(-1)为旧密码错误，返回PASSWORD_ERROR(-4)为密码格式不正确</returns>
        [HttpPost]
        public ActionResult ProcessReset(string username, string oldPassword, string newPassword)
        {
            int result = UserManager.GetInstance().Reset(username, oldPassword, newPassword);

            return Content("" + result);
        }
        #endregion
    }
}
