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
    public partial class Form11 : Form
    {
        DataSet ds;
        SqlDataAdapter da1,da2,da3;

        private void Form11_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            con = new SqlConnection(ConStr);
            da1 = new SqlDataAdapter("Select * From Dept",con);
            da2 = new SqlDataAdapter("Select * From Emp",con);
            da3 = new SqlDataAdapter("Select * From Salgrade",con);
            
            ds = new DataSet();
            da1.Fill(ds,"Dept");
            da2.Fill(ds,"Emp");
            da3.Fill(ds,"Salgrade");

            ds.Relations.Add("ParentChild", ds.Tables["Dept"].Columns["Deptno"], ds.Tables["Emp"].Columns["Deptno"]);
            dataGrid1.DataSource = ds;
        }

        SqlConnection con;
        public Form11()
        {
            InitializeComponent();
        }
    }
}
