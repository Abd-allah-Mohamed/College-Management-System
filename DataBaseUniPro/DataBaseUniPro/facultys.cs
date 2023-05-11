using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseUniPro
{
    public partial class facultys : Form
    {
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PM5TM9J;Database=uni;Trusted_Connection=True");
        public facultys()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cmd = "insert into facultys (Name,phoneNumber,adminId) values('" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.SelectedValue + "')";
            SqlCommand comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();

        }

        private void facultys_Load(object sender, EventArgs e)
        {
            string Cmd = "select adminId, firstName +' '+middleName +' '+lastName as Name from adminstretors";
            SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "adminId";
             Cmd = "select facultyId, Name from facultys";
             da = new SqlDataAdapter(Cmd, Con);
             dt = new DataTable();
            da.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "facultyId";
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "facultyId";
            comboBox4.DataSource = dt;
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "facultyId";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string sql = "select * from  students where facultyNo = @Id ";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@Id", comboBox2.SelectedValue);
            SqlDataReader r;
            Con.Open();
            r = com.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    listBox1.Items.Add(r["firstName"].ToString() + " " + r["middleName"].ToString() + " " + r["lastName"].ToString());
                }
            }
            else

                MessageBox.Show("invalid");
            Con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string sql = "select * from  profesors where facultyNo = @Id";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@Id",comboBox2.SelectedValue );
            SqlDataReader r;
            Con.Open();
            r = com.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    listBox2.Items.Add(r["firstName"].ToString() + " " + r["middleName"].ToString() + " " + r["lastName"].ToString());
                }
            }
            else

                MessageBox.Show("invalid");
            Con.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string sql = "select * from  departments where facultyNo = @Id";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@Id", comboBox2.SelectedValue);
            SqlDataReader r;
            Con.Open();
            r = com.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    listBox3.Items.Add(r["departmentName"].ToString() );
                }
            }
            else

                MessageBox.Show("invalid");
            Con.Close();

        }
    }
}
