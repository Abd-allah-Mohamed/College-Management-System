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

namespace DataBaseUniPro
{
    public partial class Admin : Form
    {
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PM5TM9J;Database=uni;Trusted_Connection=True");

        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          string Cmd = "insert into adminstretors(firstName,middleName,lastName,adminSalary,facultyNo) " +
          "values('" + textBox2.Text+ "','" + textBox3.Text + "','" + textBox4.Text + "','" + double.Parse(textBox5.Text) + "','" + comboBox1.SelectedValue + "')";
          SqlCommand  comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            string Cmd = "select facultyId, Name from facultys";
            SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember= "facultyId";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "select * from  adminstretors ,facultys where adminstretors.adminId = @adminId and  facultyId = facultyNo";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@adminId", textBox1.Text);
            SqlDataReader r;
            Con.Open();
            r = com.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    textBox9.Text = r["firstName"].ToString();
                    textBox8.Text = r["middleName"].ToString();
                    textBox7.Text = r["lastName"].ToString();
                    textBox10.Text = r["Name"].ToString();
                    textBox6.Text = r["adminSalary"].ToString();
                }
            }
            else

                MessageBox.Show("invalid");
            Con.Close();
        }
    }
}
