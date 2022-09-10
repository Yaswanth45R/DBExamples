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
using static Microsoft.VisualBasic.Interaction;

namespace DBExamples
{
    public partial class Form5 : Form
    {
        DataSet ds;
        SqlDataAdapter da;
        int RowIndex = 0;   

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select Eno, Ename, Job, Salary  From Employee Order By Eno", "Data source=DESKTOP-1G6BPHF;Integrated Security=SSPI;Database=CSDB");
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            ds = new DataSet();
            da.Fill(ds,"Employee");
            ShowData();
        }
        private void ShowData()
        {
            if (ds.Tables["Employee"].Rows[RowIndex].RowState != DataRowState.Deleted)
            {
                textBox1.Text = ds.Tables["Employee"].Rows[RowIndex]["Eno"].ToString();
                textBox2.Text = ds.Tables["Employee"].Rows[RowIndex]["Ename"].ToString();
                textBox3.Text = ds.Tables["Employee"].Rows[RowIndex]["Job"].ToString();
                textBox4.Text = ds.Tables["Employee"].Rows[RowIndex]["Salary"].ToString();
            }
            else
            {
                MessageBox.Show("Current row is deleted and can't be accessed anymore.", "Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            RowIndex = 0;
            ShowData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (RowIndex>0)
            {
                RowIndex -= 1;
                ShowData();
            }
            else
            {
                MessageBox.Show("You are at the first record of table.", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (RowIndex < ds.Tables[0].Rows.Count-1)
            {
                RowIndex +=1;
                ShowData();
            }
            else
            {
                MessageBox.Show("You are at the last record of table.", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            RowIndex = ds.Tables[0].Rows.Count-1;
            ShowData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            int LastRowIndex = ds.Tables[0].Rows.Count - 1;
            int MaxEno = Convert.ToInt32(ds.Tables[0].Rows[LastRowIndex]["Eno"]);
            textBox1.Text = (MaxEno+1).ToString();
            btnInsert.Enabled = true;
            textBox2.Focus();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["Eno"] = textBox1.Text;
            dr["Ename"] = textBox2.Text;
            dr["Job"] = textBox3.Text;
            dr["Salary"] = textBox4.Text;
            ds.Tables[0].Rows.Add(dr);  
            RowIndex = ds.Tables[0].Rows.Count-1;
            btnInsert.Enabled = false;
            MessageBox.Show("DataRow added to DataTable of DataSet.", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ds.Tables[0].Rows[RowIndex]["Ename"] = textBox2.Text;
            ds.Tables[0].Rows[RowIndex]["Job"] = textBox3.Text;
            ds.Tables[0].Rows[RowIndex]["Salary"] = textBox4.Text;
            MessageBox.Show("DataRow modified in DataTable of DataSet.", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds.Tables[0].Rows[RowIndex].Delete();
            MessageBox.Show("DataRow deleted from DataTable of DataSet.", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            btnFirst.PerformClick();
        }

        private void btnSaveDb_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(da);
            da.Update(ds, "Employee");
            btnFirst.PerformClick();
            MessageBox.Show("Data saved to Database Server.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Value = InputBox("Enter the Employee No. to Search");
            if (int.TryParse(Value,out int Eno))
            {
                DataRow dr = ds.Tables[0].Rows.Find(Eno);
                if(dr != null)
                {
                    RowIndex = ds.Tables[0].Rows.IndexOf(dr);
                    textBox1.Text = dr["Eno"].ToString();
                    textBox2.Text = dr["Ename"].ToString();
                    textBox3.Text = dr["Job"].ToString();
                    textBox4.Text = dr["Salary"].ToString();
                }
                else
                {
                    MessageBox.Show("There is no Employee existing with given No.", "Warning", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Employee No must be an integer value.", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
