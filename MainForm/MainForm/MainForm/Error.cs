using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainForm
{
    class Error
    {
        public static void WriteLog(string origin, string errorMsg, string comment)
        {
            string timestring = DateTime.Now.ToShortDateString() + " " +
                DateTime.Now.ToShortTimeString();
            string sql = "INSERT INTO ErrorLog (DateTimeString, ErrorMsg, Comment, Origin) VALUES ('" +
                timestring + "','" + errorMsg + "','" + comment + "','" + origin + "')";
            Database.passSQLstringToMDB(sql);
        }
    }
}
