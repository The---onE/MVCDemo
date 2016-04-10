using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Manager
{
    public class UserManager
    {
        public const int SUCCESS = 1;
        public const int MATCH_ERROR = -1;
        public const int USERNAME_ERROR = -2;
        public const int USERNAME_EXIST = -3;
        public const int PASSWORD_ERROR = -4;

        DemoEntities context;
        private static UserManager instance = new UserManager();
        public static UserManager GetInstance()
        {
            return instance;
        }
        private UserManager()
        {
            loginFlag = false;
            context = ContextManager.demoContext;
        }

        public User user { get; private set; }
        public bool loginFlag { get; private set; }


        public int Register(string username, string password)
        {
            string format = @"^[a-zA-Z0-9_]{3,16}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(username, format))
            {
                return USERNAME_ERROR;
            }

            var data = context.User.Where(c => c.username.Equals(username));
            if (data.Count() > 0)
            {
                return USERNAME_EXIST;
            }

            format = @"^[0-9a-zA-Z!@#$%^&*()_\-+|{}?]{6,16}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, format))
            {
                return PASSWORD_ERROR;
            }

            User user = new User();
            user.username = username;
            user.password = password;
            context.User.Add(user);
            context.SaveChanges();

            return SUCCESS;
        }

        public int Login(string username, string password)
        {
            var u = context.User.Where(c => c.username.Equals(username)).FirstOrDefault();
            if (u == null)
            {
                return USERNAME_ERROR;
            }

            string psw = u.password;
            if (psw.Equals(password))
            {
                user = u;
                loginFlag = true;
                return SUCCESS;
            }
            else
            {
                return MATCH_ERROR;
            }
        }
    }
}