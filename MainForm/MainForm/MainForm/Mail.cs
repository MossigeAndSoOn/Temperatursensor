using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

namespace MainForm
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Reciever { get; set; }
        public string Sender { get; set; }
        public string Date { get; set; }
    }

    class Mail
    {
        public static string sendMail(string recieverMailAddress, string recieverName, string messageSubject, string messageBody)
        { // method for sending mail
            try
            {
                // using a GMail account
                var fromAddress = new MailAddress("ia2013testkontoforprosjekt@gmail.com", "IA-Prosjekt-2013");
                var toAddress = new MailAddress(recieverMailAddress, recieverName);
                const string fromPassword = "passord123456789";

                // create smtp client object containing all needed info
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                // create mail and send it
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = messageSubject,
                    Body = messageBody
                })
                {
                    smtp.Send(message);
                }
                // mail sendt successfully
                return "Mail sendt";
            }
            catch (Exception ex)
            {
                Error.WriteLog("sendMail", ex.Message, "");
                // something failed, return error message
                return "Error: " + ex.Message;
            }
        }
        public static void sendMailToEntireContactsList(string subject, string message)
        {
            try
            {
                // get contacts list from database
                DataTable contactsListDataTable = Database.readContactsTable();

                // sort thru the names
                string recieverMailAddress = "";
                string recieverName = "";
                foreach (DataRow row in contactsListDataTable.Rows)
                {
                    recieverName = row["Name"].ToString();
                    recieverMailAddress = row["Email"].ToString();

                    // send a mail to each entry in the contacts list
                    Mail.sendMail(recieverMailAddress, recieverName, subject, message);
                }
            }
            catch (Exception ex)
            {
                Error.WriteLog("email entire contact list", ex.Message, "");
            }
        }

        public static List<Email> readEmail() 
        {
            var unreadMail = new List<Email>();
            try
            {
                ImapX.ImapClient client = new ImapX.ImapClient("imap.gmail.com", 993, true);
                bool result = false;

                result = client.Connection();
                if (result)
                {
                    // MessageBox.Show("Connection Established");
                    result = client.LogIn("ia2013testkontoforprosjekt@gmail.com", "passord123456789");
                    if (result)
                    {
                        // MessageBox.Show("Logged in");
                        ImapX.FolderCollection folders = client.Folders;
                        ImapX.MessageCollection messages = client.Folders["INBOX"].Search("UNSEEN", true);
                        // ,true - means all message parts will be received from server

                        int unread = messages.Count;

                        for (int i = 0; i <= unread; i++)
                        {
                            // read mails to list
                            unreadMail.Add(new Email
                            {
                                Subject = messages[i].Subject,
                                Body = messages[i].TextBody.TextData,
                                Sender = messages[i].From[0].ToString(),
                                Date = messages[i].Date.ToShortDateString()
                            });
                            // still dont know how to mark messages as read
                            //client.Folders["INBOX"].Messages[i].SetFlag("SEEN");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.WriteLog("read Mail", ex.Message, "Read mail failed");
            }
            // return the array of unread mails
            return unreadMail;
        }
    }
}
