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
using DemoApp;

namespace WindowsFormsApp11
{
    public partial class TeacherForm : Form
    {
        private string connectionString = @"Data Source=DESKTOP-IT2VBE8;Initial Catalog=demo;Integrated Security=True;TrustServerCertificate=True";
        public TeacherForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username != username.ToLower() || password != password.ToLower())
            {
                MessageBox.Show("Логин и пароль должны быть в нижнем регистре",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Преподаватель WHERE username = @username AND password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int teacherId = Convert.ToInt32(reader["Номер_преподавателя"]);
                        string lastName = reader["Фамилия"].ToString();
                        string firstName = reader["Имя"].ToString();

                        // Показываем капчу
                        using (Captcha captchaForm = new Captcha())
                        {
                            captchaForm.Role = "teacher";
                            captchaForm.UserId = teacherId;

                            if (captchaForm.ShowDialog() == DialogResult.OK)
                            {
                                MessageBox.Show($"Добро пожаловать, {lastName} {firstName}!",
                                    "Успешный вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MainForm mainForm = new MainForm("teacher", teacherId);
                                this.Hide();
                                mainForm.Show();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения: " + ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }
    }
}
