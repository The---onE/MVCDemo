using Demo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Demo.Manager
{
    public class InformationManager
    {
        public const int SUCCESS = 1;
        public const int NOT_EXIST = -1;

        DemoEntities context;
        private static InformationManager instance;
        public static InformationManager GetInstance()
        {
            lock ("information")
            {
                if (instance == null)
                {
                    instance = new InformationManager();
                }
            }
            return instance;
        }
        private InformationManager()
        {
            context = ContextManager.demoContext;
        }

        public int Add(int userId, string data)
        {
            DateTime now = DateTime.Now;
            Information info = new Information();
            info.createdUser = userId;
            info.data = data;
            info.createdTime = now;
            info.updatedTime = now;
            context.Information.Add(info);
            context.SaveChanges();

            Modify modify = new Modify();
            modify.time = now;
            modify.before = null;
            modify.after = data;
            modify.informationId = info.id;
            modify.userId = userId;
            context.Modify.Add(modify);
            context.SaveChanges();

            return SUCCESS;
        }

        public int Modify(int infoId, int userId, string data)
        {
            DateTime now = DateTime.Now;
            var info = context.Information.Where(c => c.id == infoId)
                .FirstOrDefault();
            if (info == null)
            {
                return NOT_EXIST;
            }

            Modify modify = new Modify();
            modify.time = now;
            modify.before = info.data;
            modify.after = data;
            modify.informationId = info.id;
            modify.userId = userId;
            context.Modify.Add(modify);

            info.data = data;
            info.updatedTime = now;
            context.Entry(info).State = EntityState.Modified;
            context.SaveChanges();

            return SUCCESS;
        }

        public int Delete(int infoId, int userId)
        {
            DateTime now = DateTime.Now;
            var info = context.Information.Where(c => c.id == infoId)
                .FirstOrDefault();
            if (info == null)
            {
                return NOT_EXIST;
            }

            Modify modify = new Modify();
            modify.time = now;
            modify.before = info.data;
            modify.after = null;
            modify.informationId = info.id;
            modify.userId = userId;
            context.Modify.Add(modify);

            context.Entry(info).State = EntityState.Deleted;
            context.SaveChanges();

            return SUCCESS;
        }

        public int Count()
        {
            return context.Information.Count();
        }

        public List<Information> SelectAll()
        {
            var info = context.Information;

            return info.ToList();
        }

        public List<Information> SelectByPage(int size, int index)
        {
            var info = context.Information.OrderByDescending(c => c.updatedTime).Skip(size * (index - 1)).Take(size);

            return info.ToList();
        }

        public List<Information> SelectByCreator(int userId)
        {
            var info = context.Information.Where(c => c.createdUser == userId);

            return info.ToList();
        }

        public Information SelectById(int id)
        {
            var info = context.Information.Find(id);

            return info;
        }
    }
}