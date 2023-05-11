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
    public partial class Departments : Form
    {
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PM5TM9J;Database=uni;Trusted_Connection=True");
        public Departments()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cmd = "insert into departments (Name,phoneNumber,facultyNo) values('" + textBox2.Text + "','" + comboBox1.SelectedValue + "')";
            SqlCommand comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();
            Con.Close();
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            string Cmd = "select facultyId, Name from facultys";
            SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
            DataTable  dt = new DataTable();
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
            comboBox2.ValueMember = "profesorId"; Cmd = "select departmentId , departmentName from departments";
            da = new SqlDataAdapter(Cmd, Con);
            dt = new DataTable();
            da.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "departmentName";
            comboBox3.ValueMember = "departmentId";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Cmd = "insert into profesorsDepartments (facultyId,departmentNo) values('" + comboBox2.SelectedValue + "','" + comboBox3.SelectedValue + "')";
            SqlCommand comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();
            Con.Close();
        }
    }
}
