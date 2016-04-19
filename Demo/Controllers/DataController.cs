using Demo.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using System.IO;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace Demo.Controllers
{
    public class DataController : Controller
    {
        const int DefaultPageSize = 10; //默认单页显示数据条数
        const int DefaultPageIndex = 1; //默认所在页

        #region 数据列表首页
        /// <summary>
        /// 数据列表首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获取分页信息
            int pageSize = Request["pageSize"] == null ? DefaultPageSize : int.Parse(Request["pageSize"]);
            int pageIndex = Request["pageIndex"] == null ? DefaultPageIndex : int.Parse(Request["pageIndex"]);
            int total = InformationManager.GetInstance().Count();
            var information = InformationManager.GetInstance().SelectByPage(pageSize, pageIndex);

            ViewBag.pageSize = pageSize;
            ViewBag.pageIndex = pageIndex;
            ViewBag.Total = total;

            ViewData["data"] = information;

            return View();
        }
        #endregion

        #region 添加数据页面
        /// <summary>
        /// 添加数据页面
        /// </summary>
        /// <returns>若未登录返回登录页面，否则显示添加数据页面</returns>
        public ActionResult Create()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home"); //若未登录跳转到首页
            }
        }
        #endregion

        #region 添加数据请求处理
        /// <summary>
        /// 处理添加请求
        /// </summary>
        /// <param name="data">要添加的数据</param>
        /// <returns>若未登录返回登录界面，返回1为添加成功，返回-1为添加失败</returns>
        [HttpPost]
        public ActionResult Create(string data)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    int result = InformationManager.GetInstance().Add(user.id, data);
                    return Content("" + result);
                }

                return Content("" + -1);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        } 
        #endregion

        #region 编辑数据页面
        /// <summary>
        /// 编辑数据页面
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns>若未登录返回登录页面，否则显示编辑数据页面</returns>
        public ActionResult Edit(int id)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                Information information = InformationManager.GetInstance().SelectById(id);
                ViewData["data"] = information;
                ViewBag.id = id;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home"); //若未登录跳转到首页
            }
        } 
        #endregion

        #region 编辑数据请求处理
        /// <summary>
        /// 处理编辑请求
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="data">要修改为的数据</param>
        /// <returns>若未登录返回登录界面，返回1为修改成功，返回-1为修改失败</returns>
        [HttpPost]
        public ActionResult Edit(int id, string data)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    int result = InformationManager.GetInstance().Modify(id, user.id, data);
                    return Content("" + result);
                }

                return Content("" + -1);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }  
        #endregion

        #region 数据详情页面
        /// <summary>
        /// 数据详情页面
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns>若未登录返回登录页面，否则显示数据详情页面</returns>
        public ActionResult Details(int id)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                Information information = InformationManager.GetInstance().SelectById(id);
                ViewData["data"] = information;
                ViewBag.id = id;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home"); //若未登录跳转到首页
            }
        }
        #endregion

        #region 删除数据请求处理
        /// <summary>
        /// 处理删除请求
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns>若未登录返回登录界面，返回1为删除成功，返回-1为删除失败</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    int result = InformationManager.GetInstance().Delete(id, user.id);
                    return Content("" + result);
                }

                return Content("" + -1);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }   
        #endregion

        #region 条件删除页面
        /// <summary>
        /// 条件删除页面
        /// </summary>
        /// <returns>若未登录返回登录页面，否则显示条件删除页面</returns>
        public ActionResult DeleteByCondition()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                //获取分页信息
                int pageSize = Request["pageSize"] == null ? DefaultPageSize : int.Parse(Request["pageSize"]);
                int pageIndex = Request["pageIndex"] == null ? DefaultPageIndex : int.Parse(Request["pageIndex"]);
                int total = InformationManager.GetInstance().Count();
                var information = InformationManager.GetInstance().SelectByPage(pageSize, pageIndex);

                ViewBag.pageSize = pageSize;
                ViewBag.pageIndex = pageIndex;
                ViewBag.Total = total;

                ViewData["data"] = information;

                ViewData["type"] = new List<SelectListItem>()
                {
                    new SelectListItem() {Selected = false, Text = "创建者", Value = "1"},
                    new SelectListItem() {Selected = false, Text = "数据", Value = "2"},
                };

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home"); //若未登录跳转到首页
            }
        } 
        #endregion

        #region 条件删除请求处理
        /// <summary>
        /// 条件删除请求处理
        /// </summary>
        /// <param name="type">条件类型</param>
        /// <param name="data">条件内容</param>
        /// <returns>若未登录返回登录界面，返回-1为删除失败，否则返回删除数量</returns>
        [HttpPost]
        public ActionResult DeleteByCondition(int type, string data)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    switch (type)
                    {
                        case 1: //通过创建者删除
                            return Content("" + InformationManager.GetInstance().DeleteByCondition(c => c.User.username.Equals(data), user.id));

                        case 2: //通过数据删除
                            return Content("" + InformationManager.GetInstance().DeleteByCondition(c => c.data.Equals(data), user.id));

                    }
                }

                return Content("" + -1);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        } 
        #endregion 

        #region 导出数据到Excel表格
        public ActionResult Export()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                string pathFileName = string.Empty;
                List<Information> list = InformationManager.GetInstance().SelectAll();
                ExportUserExcel(list, out pathFileName);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                return File(pathFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public void ExportUserExcel(List<Information> informations, out string pathFileName)
        {
            //创建存放Excel的文件夹
            string path = Server.MapPath("~/Excel/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            pathFileName = path + fileName;
            FileInfo newFile = new FileInfo(pathFileName);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(pathFileName);
            }
            //创建工作簿和工作表
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Information");
                /*添加表头*/
                workSheet.InsertRow(1, 1);
                using (var range = workSheet.Cells[1, 1, 1, 4])
                {
                    range.Merge = true;
                    range.Style.Font.SetFromFont(new Font("Britannic Bold", 18, FontStyle.Italic));
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    range.Style.Font.Color.SetColor(Color.Black);
                    range.Value = "数据";
                }
                //设置列宽
                workSheet.Column(1).Width = 20;
                workSheet.Column(2).Width = 20;
                workSheet.Column(3).Width = 20;
                workSheet.Column(4).Width = 20;

                /*设置标题*/
                workSheet.Cells[2, 1].Value = "创建者";
                workSheet.Cells[2, 2].Value = "数据";
                workSheet.Cells[2, 3].Value = "创建时间";
                workSheet.Cells[2, 4].Value = "更新时间";
                using (var range = workSheet.Cells[2, 1, 2, 4])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.AutoFilter = true;
                }
                /*设置单元格内容*/
                int row = 3;
                foreach (Information info in informations)
                {
                    workSheet.Cells[row, 1].Value = info.User.username;
                    workSheet.Cells[row, 2].Value = info.data;
                    workSheet.Cells[row, 3].Value = info.createdTime;
                    workSheet.Cells[row, 4].Value = info.updatedTime;
                    workSheet.Cells[row, 3].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                    workSheet.Cells[row, 4].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                    row++;
                }
                /*添加表尾*/
                //using (var range = workSheet.Cells[pocoUsers.Count + 3, 1, pocoUsers.Count + 3, 7])
                //{
                //    range.Merge = true;
                //    range.Style.Font.SetFromFont(new Font("Britannic Bold", 18, FontStyle.Italic));
                //    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                //    range.Style.Font.Color.SetColor(Color.Black);
                //    range.Value = "总人数:" + pocoUsers.Count + "人";
                //}

                /*设置整个Excel样式*/

                //workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                //workSheet.Cells[workSheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //workSheet.Cells[workSheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                //workSheet.Cells[workSheet.Dimension.Address].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;

                workSheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                package.Save();
            }
        } 
        #endregion
    }
}
