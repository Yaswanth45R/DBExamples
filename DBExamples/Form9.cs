using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace DBExamples
{
    public partial class Form9 : Form
    {
        DataSet ds;
        SqlDataAdapter sda1;
        SqlDataAdapter sda2;
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            sda1 = new SqlDataAdapter("Select Eno, Ename, Job, Salary, Status From Employee", ConStr);
            sda2 = new SqlDataAdapter("Select Grade, LoSal, HiSal From Salgrade", ConStr);
            ds = new DataSet();
            sda1.Fill(ds,"Employee");
            sda2.Fill(ds,"Salgrade");
            dataGridView1.DataSource = ds.Tables["Salgrade"];
            dataGridView2.DataSource = ds.Tables["Employee"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder cd = new SqlCommandBuilder(sda2);
            sda2.Update(ds,"Salgrade");
            MessageBox.Show("Data saved to SQL Server Database Salgrade Table.", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder cd = new SqlCommandBuilder(sda1);
            sda1.Update(ds,"Employee");
            MessageBox.Show("Data saved to SQL Server Database Employee Table.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
