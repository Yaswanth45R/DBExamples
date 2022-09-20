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
    public partial class Form8 : Form
    {
        bool Flag;
        DataSet ds;
        SqlDataAdapter da;
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            ds = new DataSet();
            da = new SqlDataAdapter("Select * From Dept",ConStr);
            da.Fill(ds,"Dept");
            da.SelectCommand.CommandText = "Select * From Emp";
            da.Fill(ds,"Emp");
            dataGridView1.DataSource = ds.Tables["Emp"];
            comboBox1.DataSource = ds.Tables["Dept"];
            comboBox1.DisplayMember = "Dname";
            comboBox1.ValueMember = "Deptno";
            comboBox1.Text = "-Select Department-";
            Flag = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Flag)
            {
                DataView dv = ds.Tables["Emp"].DefaultView;
                dv.RowFilter = "Deptno="+comboBox1.SelectedValue;
                dv.Sort = "Salary,Comm Desc";
            }
        }
    }
}
