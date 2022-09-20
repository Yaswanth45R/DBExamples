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
    public partial class Form6 : Form
    {
        DataSet ds;
        SqlDataAdapter da;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            da = new SqlDataAdapter("Select Eno, Ename, Job, Salary From Employee Order By Eno", ConStr);
            ds = new DataSet();
            da.Fill(ds,"Employee");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnSaveDb_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(da);
            da.Update(ds,"Employee");
            MessageBox.Show("Data saved to Database Server.", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
