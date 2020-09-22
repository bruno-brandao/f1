using AppF1.Domain;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AppF1.Services
{
    public static class ExcelService
    {
        public static List<Person> ExcelFileToListObject(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //DataSet - Create column names from first row
            DataSet result = excelReader.AsDataSet();

            List<Person> people = new List<Person>();
            DataTable dt = new DataTable();
            dt = result.Tables[0];

            //5. Data Reader methods
            while (excelReader.Read())
            {
                if(excelReader.GetString(0) != "OP_MAT")
                {
                    Person p = new Person
                    {
                        OP_MAT = excelReader.GetString(0),
                        OP_NM = excelReader.GetString(1),
                        OP_CPF = excelReader.GetString(2),
                        DN = excelReader.GetDateTime(3),
                        PAI = excelReader.GetString(4),
                        MAE = excelReader.GetString(5),
                        PROFISSAO = excelReader.GetString(6),
                    };
                    people.Add(p);
                }
            }

            excelReader.Close();
            return people;
        }
    }
}
