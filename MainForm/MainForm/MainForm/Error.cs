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
        //Bitmap eYellow = new Bitmap("exclamation-yellow.png");
        //Bitmap eGreen = new Bitmap("exclamation-green.png");
        //Bitmap eBlue = new Bitmap("exclamation-blue.png");
        //Bitmap eRed = new Bitmap("exclamation-red.png");

        public static Bitmap exclamationGet()
        {
            try
            {
                if (HasError == true) // error
                {
                    return image.ResizeBitmap(new Bitmap("exclamation-yellow.png"), 100, 98);
                }
                else // no error
                {
                    if (MainForm.sensorValue < Properties.Settings.Default.tempMin)
                    {
                        // temp too low
                        return image.ResizeBitmap(new Bitmap("exclamation-blue.png"), 100, 98);
                    }
                    if (MainForm.sensorValue > Properties.Settings.Default.tempMax)
                    {
                        // temp too high
                        return image.ResizeBitmap(new Bitmap("exclamation-red.png"), 100, 98);
                    }
                    else
                    {
                        // temperature just right
                        return image.ResizeBitmap(new Bitmap("exclamation-green.png"), 100, 98);
                    }
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                Error.WriteLog("Error", ex.Message, "error on color excl mark");
                return image.ResizeBitmap(new Bitmap("exclamation-yellow.png"), 100, 98);
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
