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
using System.Data.Common;

namespace DBExamples
{
    public partial class Form10 : Form
    {
        DataSet ds;
        SqlDataAdapter da1,da2;
        SqlConnection con;
        DataRelation dr;

        private void Form10_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            ds = new DataSet();
            con = new SqlConnection(ConStr);
            da1 = new SqlDataAdapter("Select * From Dept",con);
            da1.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da2 = new SqlDataAdapter("Select * From Emp",con);
            da2.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            da1.Fill(ds,"Dept");
            da2.Fill(ds,"Emp");

            dr = new DataRelation("ParentChild", ds.Tables["Dept"].Columns["Deptno"], ds.Tables["Emp"].Columns["Deptno"]);
            ds.Relations.Add(dr);

            dr.ChildKeyConstraint.DeleteRule = Rule.None;
            dr.ChildKeyConstraint.UpdateRule = Rule.None;

            dataGridView1.DataSource = ds.Tables["Dept"];
            dataGridView2.DataSource = ds.Tables["Emp"];
        }

        public Form10()
        {
            InitializeComponent();
        }
    }
}
