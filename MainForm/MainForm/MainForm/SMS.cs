using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

namespace MainForm
{
    class SMS
    {
        public static void sendSMS(string Number, string Message)
        {
            // the android-sdk-tools requires that the android phone has apropriate device drivers installed
            // for HTC phones: HTC Sync must have been installed as the HTC drivers are bundeled with the 
            // HTC-Sync app.
            // The adb shell needs to be started once before this code will run smoothly.
            // if adb shell is not started pre-run, it will be started when the first sms is to be sendt, but this will
            // interfere with the sms sending, and render the first sms not sendt (and prevent sending for the next ~10 seconds)
            // 
            // find the path were our android-sdk tools are
            string adb_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\android-sdk-tools";

            // making our process ready to run adb.exe with arguments
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            // adding startup info to hide the cmd window
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = adb_path;
            // then we build our command string
            // need to add a /c to the start of the command string, otherwise cmd will ignore it
            startInfo.Arguments = "/c adb shell am start -a android.intent.action.SENDTO -d \"sms:" + Number +
                "\" --es \"sms_body\" \"" + Message + "\" --ez exit_on_sent true";
            process.StartInfo = startInfo;
            // and finally we can execute it
            // this first bit will start the SMS app on the phone, and add the message
            // it also specifies that the app should close after the message is sendt
            process.Start();

            // but we need to command the phone once more, to simulate pushing the 'send' button
            startInfo.Arguments = "/c adb shell input keyevent 22";
            process.StartInfo = startInfo;
            // wait a few ms since my phone is slight crap
            System.Threading.Thread.Sleep(500);
            process.Start();

            // and even once more to simulate letting go of the 'send' button
            startInfo.Arguments = "/c adb shell input keyevent 66";
            process.StartInfo = startInfo;
            // wait a few more ms 
            System.Threading.Thread.Sleep(500);
            process.Start();
            // SMS is now sendt, and the app closed, making it ready for sending another SMS 
        }
        public static void sendSMSToEntireContactsList(string subject, string message)
        {
            // get contacts list from database
            DataTable contactsListDataTable = Database.readContactsTable();

            // sort thru the numbers
            string recieverNumber = "";
            foreach (DataRow row in contactsListDataTable.Rows)
            {
                recieverNumber = row["PhoneNumber"].ToString();

                // send a mail to each entry in the contacts list
                sendSMS(recieverNumber, message);
            }
        }
    }
}
