
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSearch
{
    class DataToExcel
    {
        public static void CreateExcel(string name, List<VideoData> vd)
        {
            //创建工作簿
            HSSFWorkbook wookbook = new HSSFWorkbook();
            //创建工作表
            Sheet sheet = wookbook.CreateSheet(name);
            
            Row titleRow = sheet.CreateRow(0);
            titleRow.CreateCell(0).SetCellValue("标题");
            titleRow.CreateCell(1).SetCellValue("内容");
            titleRow.CreateCell(2).SetCellValue("发文时间");
            titleRow.CreateCell(3).SetCellValue("发帖人");
            titleRow.CreateCell(4).SetCellValue("链接");
            titleRow.CreateCell(5).SetCellValue("来源");



            for (int rowIndex = 0; rowIndex < vd.Count; rowIndex++)
            {
                Row newRow = sheet.CreateRow(rowIndex + 1);
                newRow.CreateCell(0).SetCellValue(vd[rowIndex].Title);
                newRow.CreateCell(1).SetCellValue(vd[rowIndex].Content);
                newRow.CreateCell(2).SetCellValue(vd[rowIndex].Time);
                newRow.CreateCell(3).SetCellValue(vd[rowIndex].Author);
                newRow.CreateCell(4).SetCellValue(vd[rowIndex].Url);
                newRow.CreateCell(5).SetCellValue(vd[rowIndex].Source);
            }

            //使用文件流写入到文件
            using (FileStream file = new FileStream("c:\\test.xls", FileMode.Create))
            {
                //调用创建的工作簿的写入方法
                wookbook.Write(file);
            }
        }
    }
}
