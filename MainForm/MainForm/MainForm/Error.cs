using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace MainForm
{
    class Error
    {
        public static bool HasError = false;
        private static Bitmap eYellow = image.ResizeBitmap(new Bitmap("exclamation-yellow.png"), 100, 98);
        private static Bitmap eGreen = image.ResizeBitmap(new Bitmap("exclamation-green.png"), 100, 98);
        private static Bitmap eBlue = image.ResizeBitmap(new Bitmap("exclamation-blue.png"), 100, 98);
        private static Bitmap eRed = image.ResizeBitmap(new Bitmap("exclamation-red.png"), 100, 98);

        public static Bitmap exclamationGet()
        {
            try
            {
                if (HasError == true) // error
                {
                    return eYellow;
                }
                else // no error
                {
                    if (MainForm.sensorValue < Properties.Settings.Default.tempMin)
                    {
                        // temp too low
                        return eBlue;
                    }
                    if (MainForm.sensorValue > Properties.Settings.Default.tempMax)
                    {
                        // temp too high
                        return eRed;
                    }
                    else
                    {
                        // temperature just right
                        return eGreen;
                    }
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                Error.WriteLog("Error", ex.Message, "error on color excl mark");
                return eYellow;
            }
        }
        public static void WriteLog(string origin, string errorMsg, string comment)
        {
            try
            {
                HasError = true;
                string timestring = DateTime.Now.ToShortDateString() + " " +
                    DateTime.Now.ToShortTimeString();
                string sql = "INSERT INTO ErrorLog (DateTimeString, ErrorMsg, Comment, Origin) VALUES ('" +
                    timestring + "','" + errorMsg + "','" + comment + "','" + origin + "')";
                Database.passSQLstringToMDB(sql);

                using (FileStream fs = new FileStream("errorLog.txt", FileMode.CreateNew))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.Write("{0} {1} {2} {4}", timestring, errorMsg, comment, origin);
                    }
                }
            }
            catch (Exception)
            {
                HasError = true;
            }
        }
    }
}
