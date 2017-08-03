using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVC_LAB01.Models.Utility
{
    public static class ExcelUtil
    {
        public static DataTable ConvertObjectsToDataTable(IEnumerable<object> objects)
        {
            DataTable dt = null;

            if (objects != null && objects.Count() > 0)
            {
                Type type = objects.First().GetType();
                dt = new DataTable(type.Name);

                foreach (PropertyInfo property in type.GetProperties())
                {
                    if (property.PropertyType == typeof(string) || property.PropertyType == typeof(int))
                    {
                        if(!property.Name.Contains("Id"))
                            dt.Columns.Add(new DataColumn(property.Name));
                    }
                }

      

                foreach (object obj in objects)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        PropertyInfo propertyInfo = type.GetProperty(column.ColumnName);
                        if (propertyInfo != null)
                        {
                            dr[column.ColumnName] = propertyInfo.GetValue(obj, null);
                        }

               
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        public static void ExportExcelFromDataTable(DataTable dt,MemoryStream ms)
        {
            XLWorkbook workbook = new XLWorkbook();
            workbook.AddWorksheet(dt, "Sheet1");
            workbook.SaveAs(ms);
            workbook.Dispose();

        }

    }
}