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
namespace DBExamples
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-1G6BPHF;Database=CSDB;Integrated Security=SSPI");
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            LoadData();
        }
        private void LoadData()
        {
            cmd.CommandText = "Select Eno, Ename, Job, Salary, Status From Employee Where Status=1 Order By Eno";
            dr = cmd.ExecuteReader();
            ShowData();
        }
        private void ShowData()
        {
            if (dr.Read())
            {
                textBox1.Text = dr["Eno"].ToString();
                textBox2.Text = dr["Ename"].ToString();
                textBox3.Text = dr["Job"].ToString();
                textBox4.Text = dr["Salary"].ToString();
                checkBox1.Checked = (bool)dr["Status"];
            }
            else
            {
                MessageBox.Show("You are at the Last Record of the table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            foreach(Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = ctrl as TextBox;
                    tb.Clear();
                }
            }
            checkBox1.Checked = false;
            dr.Close();
            cmd.CommandText = "select IsNull(Max(Eno),1000)+1 From Employee";
            textBox1.Text = cmd.ExecuteScalar().ToString();
            checkBox1.Enabled = btnInsert.Enabled = true;
            textBox2.Focus();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }
    }
}
