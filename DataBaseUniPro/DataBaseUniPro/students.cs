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
    public partial class students : Form
    {
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-PM5TM9J;Database=uni;Trusted_Connection=True");

        public SqlCommand SqlCommand { get; private set; }

        public students()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cmd = "insert into students (firstName,middleName,lastName,address,birthDate,facultyNo)" +
         " values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox1.Text + "','" + dateTimePicker1.Value + "','" + comboBox1.SelectedValue + "')";
            SqlCommand comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();
            Con.Close();
            //select
            string sql = "SELECT TOP 1 * FROM students ORDER BY studentId DESC";
            SqlCommand com = new SqlCommand(sql, Con);
            Con.Open();
            SqlDataReader r;
            r = com.ExecuteReader();
                while (r.Read())
                {
                   MessageBox.Show ("Welcome "+r["firstName"] + " " + r["middleName"]+ " " +r["LastName"] +"\nYour Id Is :" +r["studentId"].ToString());
                }
            Con.Close();


        }

            private void groupBox1_Enter(object sender, EventArgs e)
            {

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

             Cmd = "select studentId , firstName +' '+middleName +' '+lastName as Name from students";
             da = new SqlDataAdapter(Cmd, Con);
             dt = new DataTable();
            da.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "studentId";
            Cmd = "select subjectId ,subjectName from subjects";
            da = new SqlDataAdapter(Cmd, Con);
            dt = new DataTable();
            da.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "subjectName";
            comboBox3.ValueMember = "subjectId";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Cmd = "insert into lerning (studentId,subjectId)" +
        " values('" + comboBox2.SelectedValue + "','" + comboBox3.SelectedValue + "')";
            SqlCommand comand = new SqlCommand(Cmd, Con);
            Con.Open();
            comand.ExecuteNonQuery();
            Con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "select * from  students ,facultys where studentId = @studentId and facultyNo =facultyId ";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@studentId", textBox5.Text);
            SqlDataReader r;
            Con.Open();
            r = com.ExecuteReader();
            if(r.HasRows)
            {
             while(r.Read())
                {
                    textBox9.Text = r["firstName"].ToString();
                    textBox8.Text = r["middleName"].ToString();
                    textBox7.Text = r["lastName"].ToString();
                    textBox10.Text = r["Name"].ToString();
                    textBox6.Text = r["address"].ToString();
                    dateTimePicker2.Value = (DateTime)r["birthDate"];
                }
            }
            else
          
                MessageBox.Show("invalid");
            Con.Close();
            }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string sql = "select * from  students, subjects where students.studentId = @studentId and subjects.subjectId in (select subjectId from lerning where studentId=@studentId) ";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@studentId", textBox11.Text);
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

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE lerning SET studentGrade = @G WHERE studentId =@sID and subjectId = @subID";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@G", textBox12.Text);
            com.Parameters.AddWithValue("@sID", comboBox2.SelectedValue);
            com.Parameters.AddWithValue("@subID", comboBox3.SelectedValue);
            Con.Open();
            com.ExecuteNonQuery();
            Con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT studentGrade FROM lerning where studentId = @sID and subjectId = @subID; ";
            SqlCommand com = new SqlCommand(sql, Con);
            com.Parameters.AddWithValue("@sID", comboBox2.SelectedValue);
            com.Parameters.AddWithValue("@subID", comboBox3.SelectedValue);
            SqlDataReader r;
            Con.Open();
            r = com.ExecuteReader();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    textBox13.Text=r["studentGrade"].ToString();
                }
            }
            else
                MessageBox.Show("invalid");
            Con.Close();
        }
    }
}
    

