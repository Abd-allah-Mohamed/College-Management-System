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
    public partial class profesors : Form
    {
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PM5TM9J;Database=uni;Trusted_Connection=True");

        public profesors()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cmd = "insert into profesors (firstName,middleName,lastName,profesorSalary,facultyNo) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + double.Parse(textBox5.Text) + "','" + comboBox1.SelectedValue+ "')";
            SqlCommand comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();
            Con.Close();
            string sql = "SELECT TOP 1 * FROM profesors ORDER BY profesorId DESC";
            SqlCommand com = new SqlCommand(sql, Con);
            Con.Open();
            SqlDataReader r;
            r = com.ExecuteReader();
            while (r.Read())
            {
                MessageBox.Show("Welcome " + r["firstName"] + " " + r["middleName"] + " " + r["LastName"] + "\nYour Id Is :" + r["profesorId"].ToString());
            }
            Con.Close();


        }

        private void students_Load(object sender, EventArgs e)
        {
            string Cmd = "select facultyId, Name from facultys";
            SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "facultyId";
            Cmd = "select profesorId, firstName +' '+middleName +' '+lastName as Name from profesors";
            da = new SqlDataAdapter(Cmd, Con);
            dt = new DataTable();
            da.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "profesorId";
            Cmd = "select departmentId , departmentName from departments";
            da = new SqlDataAdapter(Cmd, Con);
            dt = new DataTable();
            da.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "departmentName";
            comboBox3.ValueMember = "departmentId";
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "select * from  profesors ,facultys where profesorId = @profesorId and facultyNo =facultyId ";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@profesorId", textBox1.Text);
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
                    textBox6.Text = r["profesorSalary"].ToString();
                }
            }
            else

                MessageBox.Show("invalid");
            Con.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Cmd = "insert into profesorsDepartments (facultyId,departmentNo) values('" + comboBox1.SelectedValue + "','" + comboBox3.SelectedValue + "')";
            SqlCommand comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string sql = "select subjectName from subjects where profesorNo = @profesorId";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@profesorId", textBox11.Text);
            SqlDataReader r;
            Con.Open();
            r = com.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    listBox1.Items.Add(r["subjectName"].ToString());
                }
            }
            else
                MessageBox.Show("invalid");
            Con.Close();
        }
    }
    }

