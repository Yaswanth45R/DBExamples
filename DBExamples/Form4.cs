using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace DBExamples
{
    public partial class Form4 : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=D:\\MVC7\\School.xls;Extended Properties=Excel 8.0");
            cmd = new OleDbCommand();
            cmd.Connection = con;
            con.Open();
            LoadData();
        }
        private void LoadData()
        {
            cmd.CommandText = "Select * From[Student$]";
            dr = cmd.ExecuteReader();
            ShowData();
        }
        private void ShowData()
        {
            if (dr.Read())
            {
                textBox1.Text = dr["Sno"].ToString();
                textBox2.Text = dr["Sname"].ToString();
                textBox3.Text = dr["Class"].ToString();
                textBox4.Text = dr["Fees"].ToString();
            }
            else
            {
                MessageBox.Show("You are at the last record of table.", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = ctrl as TextBox;
                    tb.Clear();
                }
            }
            dr.Close();
            textBox1.Focus();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            dr.Close();
            cmd.CommandText = $"Insert Into [Student$] (Sno, Sname, Class, Fees) Values ({textBox1.Text}, '{textBox2.Text}', {textBox3.Text}, {textBox4.Text})";
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Insert operations is successful.", "Success", MessageBoxButtons.OK,
              MessageBoxIcon.Information);
                LoadData();
                btnInsert.Enabled = false;
            }
            else
            {
                MessageBox.Show("Failed inserting record into the table.", "Failure", MessageBoxButtons.OK,
              MessageBoxIcon.Error);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dr.Close();
            cmd.CommandText = $"Update [Student$] Set Sname='{textBox2.Text}', Class={textBox3.Text}, Fees ={textBox4.Text} Where Sno = {textBox1.Text}";
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Update operations is successful.", "Success", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Failed updating record in the table.", "Failure", MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (con.State!=ConnectionState.Closed)
            {
                con.Close();
            }
            this.Close();
        }
    }
}
