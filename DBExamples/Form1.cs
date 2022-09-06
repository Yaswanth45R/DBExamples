using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace DBExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OdbcConnection con = new OdbcConnection("DSN=SqlDSN");
            con.Open();
            MessageBox.Show("Connection State :" + con.State);
            con.Close();
            MessageBox.Show("Connection State :" + con.State);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = "Provider=SqlOledb;Data Source=DESKTOP-1G6BPHF;Integrated Security=SSPI;Database=Master";
            con.Open();
            MessageBox.Show("Connection State :" + con.State);
            con.Close();
            MessageBox.Show("Connection State :" + con.State);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-1G6BPHF;Integrated Security=SSPI;Database=Master");
            con.Open();
            MessageBox.Show("Connection State :" + con.State);
            con.Close();
            MessageBox.Show("Connection State :" + con.State);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OdbcConnection con = new OdbcConnection("DSN=ExcelDSN");
            con.Open();
            MessageBox.Show("Connection State :" + con.State);
            con.Close();
            MessageBox.Show("Connection State :" + con.State);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=D:\\MVC7\\DBExcel.xls;Extended Properties=Excel 8.0");
            con.Open();
            MessageBox.Show("Connection State :" + con.State);
            con.Close();
            MessageBox.Show("Connection State :" + con.State);
        }
    }
}
