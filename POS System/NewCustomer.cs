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
using System.Globalization;

namespace POS_System
{
    public partial class NewCustomer : Form
    {
        string code;
        SqlConnection con = new SqlConnection();
        public NewCustomer(string c)
        {
            InitializeComponent();
            code = c;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        public void Load_Data()
        {
            if (code != "1")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select max(CustomerCode) from Customers", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        int num = Convert.ToInt32(rd[0].ToString());
                        num++;
                        txtcode.Text = num.ToString();
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                txtcode.Text = "1001";
            }
        }
        private void Load_Area()
        {
            try
            {
                cboarea.Items.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select AreaCode,AreaName from Areas", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cboarea.Items.Add(rd[0].ToString() + "-" + rd[1].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void NewCustomer_Load(object sender, EventArgs e)
        {
            Load_Data();
            Load_Area();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtename.Text==string.Empty || txtaddress.Text==string.Empty || cboarea.Text==string.Empty || txtcl.Text==string.Empty || txtdis.Text==string.Empty)
            {
                if (txtename.Text == string.Empty)
                {
                    errorProvider1.SetError(txtename, "warning");
                }
                if (txtaddress.Text == string.Empty)
                {
                    errorProvider1.SetError(txtaddress, "warning");
                }
                if (cboarea.Text == string.Empty)
                {
                    errorProvider1.SetError(cboarea, "warning");
                }
                if (txtcl.Text == string.Empty)
                {
                    errorProvider1.SetError(txtcl, "warning");
                }
                if (txtdis.Text == string.Empty)
                {
                    errorProvider1.SetError(txtdis, "warning");
                }

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Customers(CustomerCode,CustomerEnglishName,CustomerUrduName,CustomerAddress,CustomerPhone1,CustomerPhone2,CustomerArea,CreditLimit,Discount,AccountOpeningDate,CustomerBalance) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@k,@l)", con);
                    cmd.Parameters.AddWithValue("@a", txtcode.Text);
                    cmd.Parameters.AddWithValue("@b", txtename.Text);
                    cmd.Parameters.AddWithValue("@c", txtuname.Text);
                    cmd.Parameters.AddWithValue("@d", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@e", txtp1.Text);
                    cmd.Parameters.AddWithValue("@f", txtp2.Text);
                    float Area_ = float.Parse(cboarea.Text.Substring(0, cboarea.Text.IndexOf("-")).ToString());
                    cmd.Parameters.AddWithValue("@g", Area_);
                    cmd.Parameters.AddWithValue("@h", txtcl.Text);
                    cmd.Parameters.AddWithValue("@i", txtdis.Text);
                    cmd.Parameters.AddWithValue("@k", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@l", "0");
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New Customer added Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtuname_Enter(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("ur-PK");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }

        private void txtuname_Leave(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("en-GB");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }
    }
}
