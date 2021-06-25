using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace NorthwindTradersDatabase
{
    public partial class frmSQLTester : Form
    {
        public frmSQLTester()
        {
            InitializeComponent();
        }

        SqlConnection NwindConnection;

        private void frmSQLTester_Load(object sender, EventArgs e)
        {
            //Connect to NorthWind Database
            string path = Path.GetFullPath("SQLNwindDB.mdf");
            NwindConnection = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=30;User Instance=True");
            
            //Checks to see if the File Path is correct and going straight to the mdf file
            //MessageBox.Show(path);

            //Open NorthWind Database
            NwindConnection.Open();
        }

        private void frmSQLTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close and Dispose of the connection
            NwindConnection.Close();
            NwindConnection.Dispose();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            SqlCommand resultsCommand = null;
            SqlDataAdapter resultsAdapter = new SqlDataAdapter();
            DataTable resultsTable = new DataTable();

            try
            {
                //Establish command object and data adapter
                resultsCommand = new SqlCommand(txtSQLTester.Text, NwindConnection);
                resultsAdapter.SelectCommand = resultsCommand;
                resultsAdapter.Fill(resultsTable);

                //Bind grid view to data table
                grdSQLTester.DataSource = resultsTable;
                lblRecords.Text = resultsTable.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in Processing SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            resultsCommand.Dispose();
            resultsAdapter.Dispose();
            resultsTable.Dispose();
        }
    }
}
