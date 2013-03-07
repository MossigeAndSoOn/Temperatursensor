using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NationalInstruments;
using NationalInstruments.DAQmx;

namespace MainForm
{
    class Sensor
    {
        public static decimal GetTemp()
        {
            try
            {
                Task temperatureTask = new Task();
                AIChannel myAIChannel;
                myAIChannel = temperatureTask.AIChannels.CreateThermocoupleChannel(
                "Dev1/ai0",
                "Temperature",
                0,
                100,
                AIThermocoupleType.J,
                AITemperatureUnits.DegreesC,
                25
                );
                AnalogSingleChannelReader reader = new
                AnalogSingleChannelReader(temperatureTask.Stream);
                double analogDataIn = reader.ReadSingleSample();
                return Convert.ToDecimal(analogDataIn.ToString("0.0"));
            }
            catch (Exception ex)
            {
                Error.WriteLog("Sensor", ex.Message, "GetTemp failed");
            }
        }
    }
}
