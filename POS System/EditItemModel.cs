﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace POS_System
{
    public partial class EditItemModel : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public EditItemModel(string code)
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            c = code;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text==string.Empty || comboBox10.Text == string.Empty || textBox9.Text == string.Empty)
            {
                if (textBox9.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox9, "warning");
                }

                if (comboBox10.Text == string.Empty)
                {
                    errorProvider1.SetError(comboBox10, "warning");
                }

                if (comboBox1.Text == string.Empty)
                {
                    errorProvider1.SetError(comboBox1, "warning");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("update ItemModel set ItemModelName=@n,IsActive=@a where ItemModelCode=@c", con);
                    cmd2.Parameters.AddWithValue("@c", textBox4.Text);
                    cmd2.Parameters.AddWithValue("@n", textBox9.Text);
                    cmd2.Parameters.AddWithValue("@a", checkBox3.CheckState);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item Model updated Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void EditItemModel_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmnd = new SqlCommand("Select * from ItemModelGrid where ItemModelID=@BC", con);
                cmnd.Parameters.AddWithValue("@BC", c);
                SqlDataReader rd1 = cmnd.ExecuteReader();
                if (rd1.Read())
                {
                    comboBox1.Text = rd1[5].ToString();
                    comboBox10.Text = rd1[4].ToString();
                    textBox4.Text = rd1[1].ToString();
                    textBox9.Text = rd1[2].ToString();
                    string state = rd1[3].ToString();
                    if (state == "True")
                    {
                        checkBox3.Checked = true;
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
    }
}
