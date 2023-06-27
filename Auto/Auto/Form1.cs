using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Auto
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Dalov\Desktop\Auto.accdb");
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void LOAD_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand comand = con.CreateCommand();
            comand.CommandType = CommandType.Text;
            comand.CommandText = "select * from Auto";
            comand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(comand);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand comand = con.CreateCommand();
            comand.CommandType = CommandType.Text;
            comand.CommandText = "insert into AUTO(Name,Family,Adress,Phone,CarModel,DateSet,DateGet) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "')";
            comand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record inserded");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand comand = con.CreateCommand();
            comand.CommandType = CommandType.Text;
            comand.CommandText = "delete From AUTO where Name= ('" + textBox1.Text + "')";
            comand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Deleted");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

       /* private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand comand = con.CreateCommand();
            comand.CommandType = CommandType.Text;
            comand.CommandText = "Update AUTO set (Name,Family,Adress,Phone,CarModel,DateSet,DateGet) = ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "')";
            comand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record inserded");
        }*/
    }
    
}