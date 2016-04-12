using Demo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Demo.Manager
{
    public class UserManager
    {
        public const int SUCCESS = 1; //成功
        public const int MATCH_ERROR = -1; //用户名密码不匹配
        public const int USERNAME_ERROR = -2; //用户名格式不正确
        public const int USERNAME_EXIST = -3; //用户名已被注册
        public const int PASSWORD_ERROR = -4; //密码格式不正确

        DemoEntities context;
        #region 单例模式
        private static UserManager instance = new UserManager();
        public static UserManager GetInstance()
        {
            return instance;
        } 
        #endregion
        private UserManager()
        {
            loginFlag = false;
            context = ContextManager.demoContext;
        }

        public User user { get; private set; } //用户信息
        public bool loginFlag { get; private set; } //是否已登录

        #region 注册
        /// <summary>
        /// 处理注册，注册成功后写入数据库
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回SUCCESS(1)为成功，返回USERNAME_EXIST(-3)为用户已被注册，返回USERNAME_ERROR(-2)为用户名格式不正确，返回PASSWORD_ERROR(-4)为密码格式不正确</returns>
        public int Register(string username, string password)
        {
            if (username.Equals(""))
            {
                return USERNAME_ERROR; //用户名格式不正确
            }

            //检查用户名是否已被注册
            var data = context.User.Where(c => c.username.Equals(username)).FirstOrDefault();
            if (data != null)
            {
                return USERNAME_EXIST; 
            }

            if (password.Equals(""))
            {
                return PASSWORD_ERROR; //密码格式不正确
            }

            //将新用户信息添加到数据库
            User user = new User();
            user.username = username;
            user.password = password;
            context.User.Add(user);
            context.SaveChanges();

            return SUCCESS;
        } 
        #endregion

        #region 登录
        /// <summary>
        /// 处理登录，登录成功后可以获取用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回SUCCESS(1)为成功，返回MATCH_ERROR(-1)为密码错误，返回USERNAME_ERROR(-2)为用户不存在</returns>
        public int Login(string username, string password)
        {
            //查找用户
            var u = context.User.Where(c => c.username.Equals(username)).FirstOrDefault();
            if (u == null)
            {
                return USERNAME_ERROR; //用户不存在
            }

            string psw = u.password;
            //登录成功，保存登录的用户
            if (psw.Equals(password))
            {
                user = u;
                loginFlag = true;
                return SUCCESS; 
            }
            else
            {
                return MATCH_ERROR; //用户名密码不匹配
            }
        } 
        #endregion

        #region 修改密码
        /// <summary>
        /// 通过旧密码修改为新密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>返回SUCCESS(1)为成功，返回USERNAME_ERROR(-2)为用户不存在，返回MATCH_ERROR(-1)为旧密码错误，返回PASSWORD_ERROR(-4)为新密码格式不正确</returns>
        public int Reset(string username, string oldPassword, string newPassword)
        {
            //查找用户
            var u = context.User.Where(c => c.username.Equals(username)).FirstOrDefault();
            if (u == null)
            {
                return USERNAME_ERROR; //用户不存在
            }

            string psw = u.password;
            if (!psw.Equals(oldPassword))
            {
                return MATCH_ERROR; //旧密码错误
            }

            if (newPassword.Equals(""))
            {
                return PASSWORD_ERROR; //新密码格式不正确
            }

            //修改用户为新密码
            u.password = newPassword;
            context.Entry(u).State = EntityState.Modified;
            context.SaveChanges();
            return SUCCESS;
        }
        #endregion
    }
}