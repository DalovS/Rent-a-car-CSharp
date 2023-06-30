using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Auto.Resources;

namespace Auto
{
   
   /* */

    public partial class Form1 :Form
    {
        OleDbConnection con = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();
            string databaseName = "CarsReservation.accdb";
            AccessDatabaseCreator.CreateDatabase(databaseName);

            Console.WriteLine("Database and table created successfully.");
            
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string databasePath = Path.Combine(baseDirectory, "Resources", databaseName);
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath};Jet OLEDB:Engine Type=5";
            con.ConnectionString = connectionString;
        }
        private void GetData() {
            DataTable dt = new DataTable();
            using (OleDbCommand command = new OleDbCommand("SELECT * FROM Auto", con))
            {
                con.Open();
                using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                {
                    da.Fill(dt);
                }
                con.Close();
            }
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void LOAD_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
         
            OleDbCommand comand = con.CreateCommand();
            comand.CommandType = CommandType.Text;
            comand.CommandText = "insert into Auto(Name,Family,Adress,Phone,CarModel,DateSet,DateGet) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "')";
            comand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record inserded");


        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Auto WHERE Name = @name";
            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record deleted");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Name"].Value.ToString();
                textBox2.Text = row.Cells["Family"].Value.ToString();
                textBox3.Text = row.Cells["Adress"].Value.ToString();
                textBox4.Text = row.Cells["Phone"].Value.ToString();
                textBox5.Text = row.Cells["CarModel"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateSet"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["DateGet"].Value);
            }
        }

       
        private void button2_Click_1(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE AUTO SET Name = @name, Family = @family, Adress = @address, Phone = @phone, CarModel = @carModel, DateSet = @dateSet, DateGet = @dateGet Where Name=@name";
            
            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.Parameters.AddWithValue("@family", textBox2.Text);
            command.Parameters.AddWithValue("@address", textBox3.Text);
            command.Parameters.AddWithValue("@phone", textBox4.Text);
            command.Parameters.AddWithValue("@carModel", textBox5.Text);
            command.Parameters.AddWithValue("@dateSet", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@dateGet", dateTimePicker2.Value);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record updated");
        }
    }
}
