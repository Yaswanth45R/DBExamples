﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DBExamples
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            label1.Text = ConfigurationManager.AppSettings.Get("CompanyName");
            label2.Text = ConfigurationManager.AppSettings.Get("Address");
            label3.Text = $"Phone:{ConfigurationManager.AppSettings.Get("Phone")}; WhatsApp:{ConfigurationManager.AppSettings.Get("WhatsApp")}";
            label4.Text = $"Email:{ConfigurationManager.AppSettings.Get("Email")}; Website:{ConfigurationManager.AppSettings.Get("Website")}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = ConfigurationManager.ConnectionStrings["ExcelConStr"].ConnectionString;
        }
    }
}
