using Demo.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Demo.Manager
{
    public class DataManager
    {
        #region 线程安全单例模式
        private static DataManager instance;
        public static DataManager GetInstance()
        {
            lock ("information")
            {
                if (instance == null)
                {
                    instance = new DataManager();
                }
            }
            return instance;
        } 
        #endregion
        private DataManager()
        {
        }

        #region 导出数据到Excel文件
        public void ExportExcel(List<Information> informations, string pathFileName)
        {
            //创建存放Excel的文件夹
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