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

namespace fest
{
    public partial class avtorizaciya : Form
    {
        db DB = new db();
        public avtorizaciya()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void registr_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB.OpenConnection();
            var loginUser = textBox1.Text;
            var passwordUser = textBox2.Text;
            var firstname = textBox3.Text;
            //создание и выполнение запроса на проверку логина и пароля
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            string select = $"select idРегистрация, Логин, Пароль, Имя from Регистрация where Логин='{loginUser}' and Пароль='{passwordUser}'";
            SqlCommand command = new SqlCommand(select, DB.getConnection);

            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count == 1)
            {


                MessageBox.Show("Вы успешно вошли", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                main main = new main();
                main.name = firstname;
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ошибка авторизации! Неверный логин или пароль", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DB.CloseConnection();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registr reg = new registr();
            reg.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
