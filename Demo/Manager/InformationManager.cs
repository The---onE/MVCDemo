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

        private static InformationManager instance = new InformationManager();
        public static InformationManager GetInstance()
        {
            return instance;
        }
        private InformationManager()
        {
        }

        public int Add(int userId, string data)
        {
            var context = new DemoEntities();
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
            var context = new DemoEntities();
            DateTime now = DateTime.Now;
            var result = context.Information.Where(c => c.id == infoId);
            if (result.Count() == 0)
            {
                return NOT_EXIST;
            }

            foreach (var info in result)
            {
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
            }
            context.SaveChanges();

            return SUCCESS;
        }
    }
}