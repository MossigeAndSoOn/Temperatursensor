using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

namespace MainForm
{
    class Database
    {
        const string dataMDB = "Data.mdb";
        static OleDbConnection lConn;
//-------------------CONTACTS--------------------------------------------------
        public static DataTable readContactsTable()
        {
            try
            {
                string sql = "Select ID, Name, Email, PhoneNumber from Contacts"; //Where 'Contacts' is the name of the table in the MDB file 
                DataTable contacts = new DataTable();
                contacts = passSQLstringToMDB(sql);

                return contacts;
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "readContactsTable");
                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("Error!", typeof(string));
                return errorTable;
            }
        }
        public static DataTable addRowToMDB(string Name, string Email, string PhoneNumber)
        {
            try
            {
                DataTable MyDataTable = new DataTable();
                // connect to database
                lConn = CreateLocalConnection(dataMDB);
                lConn.Open();

                // getting the table, so i can count the rows later on
                string lSQL = "Select ID, Name, Email, PhoneNumber from Contacts";

                OleDbDataAdapter dadapt = new OleDbDataAdapter(lSQL, lConn); //This assigns the Select statement and connection of the data adapter 
                //OleDbCommandBuilder cb = new OleDbCommandBuilder(dadapt); //This builds the update and Delete queries for the table in the above SQL. this only works if the select is a single table. 

                dadapt.Fill(MyDataTable); //this populates the data table in your application 
                dadapt.UpdateCommand = new OleDbCommandBuilder(dadapt).GetUpdateCommand();
                // Add a new row to the dataTable
                MyDataTable.Rows.Add(MyDataTable.Rows.Count, Name, Email, PhoneNumber);

                // Save to the MDB file
                dadapt.Update(MyDataTable);
                lConn.Close();

                return MyDataTable;
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "");
                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("Error!", typeof(string));
                return errorTable;
            }
        }
        public static DataTable updateContactsTable(DataTable contacts) // update entire table
        {
            try
            {
                DataTable MyDataTable = new DataTable();
                lConn = CreateLocalConnection(dataMDB);
                lConn.Open();

                // getting the table, so i can count the rows later on
                string lSQL = "Select ID, Name, Email, PhoneNumber from Contacts";

                OleDbDataAdapter dadapt = new OleDbDataAdapter(lSQL, lConn); //This assigns the Select statement and connection of the data adapter 
                //OleDbCommandBuilder cb = new OleDbCommandBuilder(dadapt); //This builds the update and Delete queries for the table in the above SQL. this only works if the select is a single table. 

                dadapt.Fill(MyDataTable); //this populates the data table in your application 
                dadapt.UpdateCommand = new OleDbCommandBuilder(dadapt).GetUpdateCommand();
                // Add a new row to the dataTable

                // Save to the MDB file
                dadapt.Update(contacts);

                // close the connection
                lConn.Close();

                return MyDataTable;
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "passStringToMDB");
                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("Error!", typeof(string));
                return errorTable;
            }
        }
        public static DataTable addTableToMDB(DataTable contacts) // update entire table
        {
            try
            {
                DataTable MyDataTable = new DataTable();
                lConn = CreateLocalConnection(dataMDB);
                lConn.Open();

                // getting the table, so i can count the rows later on
                string lSQL = "Select ID, Name, Email, PhoneNumber from Contacts";

                OleDbDataAdapter dadapt = new OleDbDataAdapter(lSQL, lConn); //This assigns the Select statement and connection of the data adapter 
                //OleDbCommandBuilder cb = new OleDbCommandBuilder(dadapt); //This builds the update and Delete queries for the table in the above SQL. this only works if the select is a single table. 

                dadapt.Fill(MyDataTable); //this populates the data table in your application 
                dadapt.UpdateCommand = new OleDbCommandBuilder(dadapt).GetUpdateCommand();
                // Add a new row to the dataTable

                // Save to the MDB file
                dadapt.Update(contacts);

                // close the connection
                lConn.Close();

                return MyDataTable;
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "addTableToMDB");
                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("Error!", typeof(string));
                return errorTable;
            }
        }

        public static void removeRowFromMDB(int rowID)
        {
            try
            {
                string sql = "DELETE FROM Contacts WHERE ID =" + rowID;
                passSQLstringToMDB(sql);
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "removeRowFromMDB");
            }
        }
// ------------------- GENERAL---------------------------------------
        public static DataTable passSQLstringToMDB(string sql)
        {
            try
            {
                lConn = CreateLocalConnection(dataMDB);
                lConn.Open();

                // passing inncomming SQL string
                OleDbDataAdapter dadapt = new OleDbDataAdapter(sql, lConn); //This assigns the Select statement and connection of the data adapter 

                // cant use one static datatable for all 
                // the datatable retains column headers
                DataTable MyDataTable = new DataTable();
                // populate datatable
                dadapt.Fill(MyDataTable);

                //Then save to the MDB file 
                dadapt.Update(MyDataTable);
                dadapt.Dispose();
                lConn.Close();

                return MyDataTable;
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "passStringToMDB");
                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("Error!", typeof(string));
                return errorTable;
            }
        }



//------------------SENSOR DATA----------------------------
        public static void addSensorDataToMDB(int temperature)
        {
            try
            {
                // add sensordata and timestamp 
                string lSQL = "INSERT INTO SensorData (Temperature, DateString, TimeString) VALUES (" +
                    temperature.ToString() + ",'" +
                    DateTime.Now.ToShortDateString().ToString() + "','" +
                    DateTime.Now.ToShortTimeString().ToString() + "')";
                passSQLstringToMDB(lSQL);
            }
            catch (Exception ex)
            {
                Error.WriteLog("Database", ex.Message, "addSensorDataToMDB");
            }
        }
        public static void addSensorDataToMDB(int temperature, string comment)
        {
            try
            {
                // add sensordata and timestamp with comment
                string lSQL = "INSERT INTO SensorData (Temperature, DateString, TimeString, Comment) VALUES (" +
                    temperature.ToString() + ",'" +
                    DateTime.Now.ToShortDateString().ToString() + "','" +
                    DateTime.Now.ToShortTimeString().ToString() + "','" +
                    comment + "')";
                passSQLstringToMDB(lSQL);
            }
            catch (Exception ex)
            {
                Error.WriteLog("Database", ex.Message, "addSensorDataToMDB");
            }
        }
        public static void addSensorDataToMDB(decimal temperature, string comment)
        {
            try
            {
                // add sensordata and timestamp with comment
                string lSQL = "INSERT INTO SensorData (Temperature, DateString, TimeString, Comment) VALUES ('" +
                    temperature.ToString() + "','" +
                    DateTime.Now.ToShortDateString().ToString() + "','" +
                    DateTime.Now.ToShortTimeString().ToString() + "','" +
                    comment + "')";
                passSQLstringToMDB(lSQL);
            }
            catch (Exception ex)
            {
                Error.WriteLog("Database", ex.Message, "addSensorDataToMDB");
            }
        }
        public static void addSensorDataToMDB(decimal temperature)
        {
            try
            {
                // add sensordata and timestamp with comment
                string lSQL = "INSERT INTO SensorData (Temperature, DateString, TimeString) VALUES ('" +
                    temperature.ToString() + "','" +
                    DateTime.Now.ToShortDateString().ToString() + "','" +
                    DateTime.Now.ToShortTimeString().ToString() + "')";
                passSQLstringToMDB(lSQL);
            }
            catch (Exception ex)
            {
                Error.WriteLog("Database", ex.Message, "addSensorDataToMDB");
            }
        }

        public static DataTable readSensorDataFromMDB()
        {
            string lSQL = "Select ID, Temperature, DateString, TimeString, Comment from SensorData";
            try
            {
                DataTable MyDataTable = new DataTable();
                lConn = CreateLocalConnection(dataMDB);
                lConn.Open(); // connect to database
                //This assigns the Select statement and connection of the data adapter 
                OleDbDataAdapter dadapt = new OleDbDataAdapter(lSQL, lConn);
                dadapt.Fill(MyDataTable); //this populates the data table in your application 
                lConn.Close();// close the connection

                return MyDataTable;
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "readSensorDataFromMDB");
                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("Error!", typeof(string));
                return errorTable;
            }
        }
        public static DataTable generatePlotPoints(int numberOfPoints)
        {
            string lSQL = "SELECT TOP " + numberOfPoints + " * " +
                "FROM SensorData " +
                "Order By ID DESC";
            try
            {
                DataTable plotpoints = passSQLstringToMDB(lSQL);
                return plotpoints;
            }
            catch (Exception ex)
            {
                // if an exception is thrown, a empty table is returned, with one column named "Error"
                Error.WriteLog("Database", ex.Message, "generate plot points");
                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("Error!", typeof(string));
                return errorTable;
            }
        }
// -----------------------CONNECTION STRING-------------------
        public static OleDbConnection CreateLocalConnection(string mdb)
        {
            // using an ridiculously long connection string, only reluctant to change it since it works
            string provider = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;";
            string opt1 = "Mode=Share Deny None;Extended Properties='';Jet OLEDB:System database='';Jet OLEDB:Registry Path='';";
            string opt2 = "Jet OLEDB:Engine Type=4;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Global Partial Bulk Ops=2;";
            string opt3 = "Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;";
            string opt4 = "Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False";
            OleDbConnection lConn = new OleDbConnection(provider + "Data Source=" + mdb + ";" + opt1 + opt2 + opt3 + opt4);
            return lConn;
        }
    }
}
