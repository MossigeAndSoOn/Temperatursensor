using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;

namespace MDB_readerClass
{
    public partial class Contacts_list_form : Form
    {
        public Contacts_list_form()
        {
            InitializeComponent();
        }
        public static DataTable Contacts = new DataTable();

        private void button2_Click(object sender, EventArgs e) // add row
        {
        }

        private void button3_Click(object sender, EventArgs e) // remove row
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //// Populate the dataGridView with the database content
            //DataTable contacts = MainForm.Database.readContactsTable();
            //// MainForm.Database.readContactsTable(); return is fucked
            //// delete garbage data:
            //contacts.Columns.RemoveAt(0);
            //contacts.Columns.RemoveAt(1);
            //contacts.Columns.RemoveAt(1);
            //contacts.Columns.RemoveAt(1);
            //contacts.Columns.RemoveAt(1);

            //dataGridView1.DataSource = contacts;
            Contacts = MainForm.Database.readContactsTable();
            dataGridView1.DataSource = Contacts;
            // Set proper column sizes
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[3].Width = 90;
             //Dont allow the user to resize the form (it's perfect as is)
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

             //This goes for maximize button as well
            this.MaximizeBox = false;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //MainForm.Database.writeTableToMDB(Contacts);
        //    //DataRow currentRow = new DataRow();
        //    //currentRow = Contacts.Rows[dataGridView1.CurrentRow.Index];
        //    MainForm.Database.updateContactsTable(Contacts);
        //}

        //private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    MainForm.Database.updateContactsTable(Contacts);
        //}
        //private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        //{
        //    MainForm.Database.updateContactsTable(Contacts);
        //}

        //private void Contacts_list_form_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    // save changes when closing
        //    MainForm.Database.updateContactsTable(Contacts);
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            MainForm.Database.updateContactsTable(Contacts);
        }
    }
}
