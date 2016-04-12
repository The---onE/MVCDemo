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
        #region 线程安全单例模式
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
        #endregion
        private InformationManager()
        {
            context = ContextManager.demoContext;
        }

        #region 添加数据
        /// <summary>
        /// 向数据库中添加数据
        /// </summary>
        /// <param name="userId">创建者ID</param>
        /// <param name="data">数据</param>
        /// <returns>返回SUCCESS(1)为成功</returns>
        public int Add(int userId, string data)
        {
            //向Information数据库添加数据
            DateTime now = DateTime.Now;
            Information info = new Information();
            info.createdUser = userId;
            info.data = data;
            info.createdTime = now;
            info.updatedTime = now;
            context.Information.Add(info);
            context.SaveChanges();

            //向Modify数据库添加创建数据的记录
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
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="infoId">要修改的数据ID</param>
        /// <param name="userId">修改者ID</param>
        /// <param name="data">要修改为的数据</param>
        /// <returns>返回SUCCESS(1)为成功，返回NOT_EXIST(-1)为失败</returns>
        public int Modify(int infoId, int userId, string data)
        {
            DateTime now = DateTime.Now;
            //查询是否存在相应数据
            var info = context.Information.Where(c => c.id == infoId).FirstOrDefault();
            if (info == null)
            {
                return NOT_EXIST;
            }

            //向Modify数据库添加修改记录
            Modify modify = new Modify();
            modify.time = now;
            modify.before = info.data;
            modify.after = data;
            modify.informationId = info.id;
            modify.userId = userId;
            context.Modify.Add(modify);

            //修改Information数据库中数据
            info.data = data;
            info.updatedTime = now;
            context.Entry(info).State = EntityState.Modified;
            context.SaveChanges();

            return SUCCESS;
        } 
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="infoId">要删除数据的ID</param>
        /// <param name="userId">删除者ID</param>
        /// <returns>返回SUCCESS(1)为成功，返回NOT_EXIST(-1)为失败</returns>
        public int Delete(int infoId, int userId)
        {
            DateTime now = DateTime.Now;
            //查询是否存在相应数据
            var info = context.Information.Where(c => c.id == infoId).FirstOrDefault();
            if (info == null)
            {
                return NOT_EXIST;
            }

            //向Modify数据库添加删除记录
            Modify modify = new Modify();
            modify.time = now;
            modify.before = info.data;
            modify.after = null;
            modify.informationId = info.id;
            modify.userId = userId;
            context.Modify.Add(modify);

            //删除Information数据库中数据
            context.Entry(info).State = EntityState.Deleted;
            context.SaveChanges();

            return SUCCESS;
        } 
        #endregion

        #region 数据总数
        /// <summary>
        /// 统计数据总数
        /// </summary>
        /// <returns>返回数据总数</returns>
        public int Count()
        {
            return context.Information.Count();
        } 
        #endregion

        #region 全部数据
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns>包含全部数据的List</returns>
        public List<Information> SelectAll()
        {
            var info = context.Information;

            return info.ToList();
        } 
        #endregion

        #region 分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="size">单页最大条数</param>
        /// <param name="index">所在页数</param>
        /// <returns>包含单页数据的List</returns>
        public List<Information> SelectByPage(int size, int index)
        {
            var info = context.Information.OrderByDescending(c => c.updatedTime).Skip(size * (index - 1)).Take(size);

            return info.ToList();
        } 
        #endregion

        #region 根据创建者获取数据
        /// <summary>
        /// 根据创建者获取数据
        /// </summary>
        /// <param name="username">创建者用户名</param>
        /// <returns>包含该用户创建所有数据的List</returns>
        public List<Information> SelectByCreator(string username)
        {
            var info = context.Information.Where(c => c.User.username.Equals(username));

            return info.ToList();
        } 
        #endregion

        #region 根据数据内容获取数据
        /// <summary>
        /// 根据数据内容获取数据
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <returns>包含该数据内容的数据的List</returns>
        public List<Information> SelectByData(string data)
        {
            var info = context.Information.Where(c => c.data.Equals(data));

            return info.ToList();
        }  
        #endregion

        #region 根据ID获取数据
        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns>该ID对应的数据</returns>
        public Information SelectById(int id)
        {
            var info = context.Information.Find(id);

            return info;
        } 
        #endregion
    }
}