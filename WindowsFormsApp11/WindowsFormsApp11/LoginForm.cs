using DemoApp;
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
using static System.Collections.Specialized.BitVector32;

namespace WindowsFormsApp11
{
    public partial class LoginForm : Form
    {
        private string connectionString = @"Data Source=DESKTOP-IT2VBE8;Initial Catalog=demo;Integrated Security=True;TrustServerCertificate=True";
        public LoginForm()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (login != login.ToLower() || password != password.ToLower())
            {
                MessageBox.Show("Логин и пароль должны быть в нижнем регистре",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Студент WHERE username = @login AND password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int studentId = Convert.ToInt32(reader["Номер_студента"]);
                        string name = reader["Имя"].ToString();

                        // Показываем капчу
                        using (Captcha captchaForm = new Captcha())
                        {
                            captchaForm.Role = "student";
                            captchaForm.UserId = studentId;

                            if (captchaForm.ShowDialog() == DialogResult.OK)
                            {
                                MessageBox.Show($"Добро пожаловать, {name}!", "Успешный вход",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MainForm mainForm = new MainForm("student", studentId);
                                this.Hide();
                                mainForm.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtLogin.Clear();
            txtPassword.Clear();
            txtLogin.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TeacherForm teacherForm = new TeacherForm();
            this.Hide();
            teacherForm.Show();
        }
    }
}
