using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainForm
{
    class ClientFeedback
    {
        private static bool warningHasBeenSendt = false;

        public static void determineNeedForWarning(decimal sensorValue)
        {
            try
            {
                if (warningHasBeenSendt == false)
                {
                    if (sensorValue > Properties.Settings.Default.tempMax || sensorValue < Properties.Settings.Default.tempMin)
                    {
                        // first time temperature is outside limits. need to send warning
                        sendWarning(sensorValue);
                        warningHasBeenSendt = true;
                    }
                }
                if (warningHasBeenSendt == true)
                {
                    if (sensorValue < Properties.Settings.Default.tempMax && sensorValue > Properties.Settings.Default.tempMin)
                    {
                        // temperature is back within limits. rearm the alarm
                        warningHasBeenSendt = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.WriteLog("ClientFeedback-determine warning", ex.Message, "");
            }
        }

        private static void sendWarning(decimal sensorValue)
        {
            try
            {
                // check if values are outside limits
                if (sensorValue > Properties.Settings.Default.tempMax)
                {
                    // temperature too high
                    if (Properties.Settings.Default.warnMail == true) // send mail
                    {
                        Mail.sendMailToEntireContactsList("Temperature warning", "Temperature has exceeded the limit of: " +
                            Properties.Settings.Default.tempMax + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                    if (Properties.Settings.Default.warnSMS == true) // send SMS
                    {
                        SMS.sendSMSToEntireContactsList("Temperature warning", "Temperature has exceeded the limit of: " +
                            Properties.Settings.Default.tempMax + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                }
                if (sensorValue < Properties.Settings.Default.tempMin)
                {
                    // temperature too low
                    if (Properties.Settings.Default.warnMail == true) // send mail
                    {
                        Mail.sendMailToEntireContactsList("Temperature warning", "Temperature is below the limit of: " +
                            Properties.Settings.Default.tempMin + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                    if (Properties.Settings.Default.warnSMS == true) // send SMS
                    {
                        SMS.sendSMSToEntireContactsList("Temperature warning", "Temperature is below the limit of: " +
                            Properties.Settings.Default.tempMin + " degrees\n" +
                            "Last temperature reading was: " + sensorValue + " degrees\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Error.WriteLog("ClientFeedback-sendMessages", ex.Message, "");
            }
        }
    }
}
