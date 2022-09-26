using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using static Microsoft.VisualBasic.Interaction;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace DBExamples
{
    public partial class Form14 : Form
    {
        string imgPath = "";
        byte[] imgData = null;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
            con = new SqlConnection(ConStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Value = InputBox("Enter Employee No. to Search");
            if (int.TryParse(Value, out int Eno))
            {
                try
                {
                    cmd.CommandText = "Employee_Select";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Eno", Eno);
                    cmd.Parameters.AddWithValue("@Status", true);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox1.Text = dr["Eno"].ToString();
                        textBox2.Text = dr["Ename"].ToString();
                        textBox3.Text = dr["Job"].ToString();
                        textBox4.Text = dr["Salary"].ToString();
                        if (dr["Photo"] != DBNull.Value)
                        {
                            imgData = (byte[])dr["Photo"];
                            MemoryStream ms = new MemoryStream(imgData);
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            imgData = null;
                            imgPath = "";
                            pictureBox1.Image = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No employee exist's with the given number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Employee No. should be integer value.", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imgPath = "";
            imgData = null;
            pictureBox1.Image = null;
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox2.Focus();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "Employee_Update";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Eno", textBox1.Text);
                cmd.Parameters.AddWithValue("@Ename", textBox2.Text);
                cmd.Parameters.AddWithValue("@Job", textBox3.Text);
                cmd.Parameters.AddWithValue("@Salary", textBox4.Text);

                if (imgData == null && imgPath.Trim().Length == 0)
                {
                    cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                    cmd.Parameters["@Photo"].SqlDbType = SqlDbType.VarBinary;
                }
                else if (imgPath.Trim().Length > 0)
                {
                    imgData = File.ReadAllBytes(imgPath);
                    cmd.Parameters.AddWithValue("@Photo", imgData);
                }
                else if (imgPath.Trim().Length == 0 && imgData != null)
                {
                    cmd.Parameters.AddWithValue("@Photo", imgData);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record updated in Database-Table.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandText = "Employee_Delete";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Eno", textBox1.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                button2.PerformClick();
                MessageBox.Show("Record deleted from Database-Table.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Jpeg Images|*.jpg|Bitmap Images|*.bmp|All Files|*.*";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                imgPath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = imgPath;
            }

        }
    }
}

