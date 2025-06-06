using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApp11;

namespace DemoApp
{
    public partial class MainForm : Form
    {
        private string connectionString = @"Data Source=DESKTOP-IT2VBE8;Initial Catalog=demo;Integrated Security=True;TrustServerCertificate=True";
        private int TeacherId;
        public MainForm(string role, int teacherId)
        {
            InitializeComponent();
            cmbDisciplines.SelectedIndexChanged += CmbDisciplines_SelectedIndexChanged;
            dgvStudents.SelectionChanged += DgvStudents_SelectionChanged;
            btnLoadStudents.Click += BtnLoadStudents_Click;
            btnSaveGrade.Click += BtnSaveGrade_Click;
            btnViewGrades.Click += BtnViewGrades_Click;
            TeacherId = teacherId;
            MainForm_Load(this, EventArgs.Empty);
        }

        private void InitStudentView()
        { 
            cmbDisciplines.Visible = false;
            cmbGroups.Visible = false;
            labelDiscipline.Visible = false;
            labelGroup.Visible = false;

            ConfigureStudentDataGridView();

        }

        private void ConfigureStudentDataGridView()
        {
            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AllowUserToOrderColumns = false;
            dgvStudents.AllowUserToResizeRows = false;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.EditMode = DataGridViewEditMode.EditProgrammatically;


            dgvStudents.BackgroundColor = SystemColors.ControlLight;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void InitTeacherView()
        {

            cmbDisciplines.Visible = true;
            cmbGroups.Visible = true;

            cmbDisciplines.DisplayMember = "Name";
            cmbDisciplines.ValueMember = "Id";
        }

        private void BtnViewGrades_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null)
            {
                MessageBox.Show("Сначала выберите студента");
                return;
            }

            /* было: (int)dgvStudents.CurrentRow.Cells["Студент"].Value */
            int sid = Convert.ToInt32(
                         dgvStudents.CurrentRow.Cells["Номер_студента"].Value);

            const string sql = @"
        SELECT d.Название  AS [Дисциплина],
               g.Оценка    AS [Оценка]
        FROM   Оценки g
        JOIN   Дисциплина d ON d.Код_дисциплины = g.Код_дисциплины
        WHERE  g.Номер_студента = @sid
        ORDER  BY d.Название;";

            DataTable dt = ExecToTable(sql, p => p.AddWithValue("@sid", sid));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("У студента нет оценок");
                return;
            }

            using (var f = new Form())
            {
                f.Text = "Все оценки студента";
                f.StartPosition = FormStartPosition.CenterParent;
                f.ClientSize = new System.Drawing.Size(450, 300);

                var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = dt,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };
                f.Controls.Add(grid);
                f.ShowDialog(this);
            }
        }

        private DataTable ExecToTable(string sql, Action<SqlParameterCollection> addParams)
        {
            var table = new DataTable();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (addParams != null)
                        addParams(cmd.Parameters);

                    using (var ad = new SqlDataAdapter(cmd))
                        ad.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return table;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            const string sql = @"
              SELECT DISTINCT d.Код_дисциплины, d.Название
              FROM [Нагрузка преподавателя] tw
              JOIN Дисциплина d ON d.Код_дисциплины = tw.Код_дисциплины
              WHERE tw.Номер_преподавателя = @tid
              ORDER BY d.Название";

            var dt = ExecToTable(sql, p => p.AddWithValue("@tid", TeacherId));

            cmbDisciplines.DisplayMember = "Название";
            cmbDisciplines.ValueMember = "Код_дисциплины";
            cmbDisciplines.DataSource = dt;
            cmbDisciplines.SelectedIndex = -1;
        }


        private void CmbDisciplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbGroups.DataSource = null;

            if (cmbDisciplines.SelectedValue == null)
                return;

            int did = (int)cmbDisciplines.SelectedValue;

            const string sql = @"
              SELECT DISTINCT g.Номер_группы, g.Код_специальности
              FROM [Нагрузка преподавателя] tw
              JOIN [Группа] g ON g.Номер_группы = tw.Номер_группы
              WHERE tw.Номер_преподавателя = @tid AND tw.Код_дисциплины = @did
              ORDER BY g.Код_специальности";

            var dt = ExecToTable(sql, p =>
            {
                p.AddWithValue("@tid", TeacherId);
                p.AddWithValue("@did", did);
            });

            cmbGroups.DisplayMember = "Код_специальности";
            cmbGroups.ValueMember = "Номер_группы";
            cmbGroups.DataSource = dt;
            cmbGroups.SelectedIndex = -1;
        }


        private void BtnLoadStudents_Click(object sender, EventArgs e)
        {
            if (cmbDisciplines.SelectedValue == null)
            {
                MessageBox.Show("Выберите дисциплину"); return;
            }
            if (cmbGroups.SelectedValue == null)
            {
                MessageBox.Show("Выберите группу"); return;
            }

            int did = (int)cmbDisciplines.SelectedValue;
            int gid = (int)cmbGroups.SelectedValue;

            const string sql = @"
              SELECT s.Номер_студента,
                     s.Фамилия + ' ' + s.Имя + ' ' + s.Отчество AS Студент,
                     g.Оценка AS Оценка
              FROM Студент  s
              LEFT JOIN Оценки g
                     ON g.Номер_студента = s.Номер_студента
                    AND g.Код_дисциплины = @did
              WHERE s.Номер_группы = @gid
              ORDER BY s.Фамилия, s.Имя";

            dgvStudents.DataSource = ExecToTable(sql, p =>
            {
                p.AddWithValue("@did", did);
                p.AddWithValue("@gid", gid);
            });

            dgvStudents.Columns["Номер_студента"].Visible = false;
            dgvStudents.Columns["Студент"].Width = 200;
            dgvStudents.Columns["Оценка"].Width = 60;
            dgvStudents.ClearSelection();
            numGrade.Value = 2;
        }

        private void DgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null)
                return;

            object val = dgvStudents.CurrentRow.Cells["Оценка"].Value;
            numGrade.Value = (val == null || val == DBNull.Value) ? 2 : Convert.ToDecimal(val);
        }

        private void BtnSaveGrade_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null)
            {
                MessageBox.Show("Сначала выберите студента"); return;
            }
            if (cmbDisciplines.SelectedValue == null || cmbGroups.SelectedValue == null)
            {
                MessageBox.Show("Сначала выберите дисциплину и группу"); return;
            }

            int sid = (int)dgvStudents.CurrentRow.Cells["Номер_студента"].Value;
            int did = (int)cmbDisciplines.SelectedValue;
            byte? gradeValue = numGrade.Value > 0 ? (byte?)numGrade.Value : null;

            const string sqlExists = "SELECT 1 FROM Оценки WHERE Номер_студента=@sid AND Код_дисциплины=@did";
            const string sqlInsert = "INSERT INTO Оценки(Код_дисциплины,Номер_студента,Оценка) VALUES(@did,@sid,@gv)";
            const string sqlUpdate = "UPDATE Оценки SET Оценка=@gv WHERE Номер_студента=@sid AND Код_дисциплины=@did";
            const string sqlDelete = "DELETE FROM Оценки WHERE Номер_студента=@sid AND Код_дисциплины=@did";

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    bool exists;
                    using (var cmd = new SqlCommand(sqlExists, conn))
                    {
                        cmd.Parameters.AddWithValue("@sid", sid);
                        cmd.Parameters.AddWithValue("@did", did);
                        exists = cmd.ExecuteScalar() != null;
                    }

                    if (gradeValue.HasValue)
                    {
                        var cmdText = exists ? sqlUpdate : sqlInsert;
                        using (var cmd = new SqlCommand(cmdText, conn))
                        {
                            cmd.Parameters.AddWithValue("@sid", sid);
                            cmd.Parameters.AddWithValue("@did", did);
                            cmd.Parameters.AddWithValue("@gv", gradeValue.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (exists)
                    {
                        using (var cmd = new SqlCommand(sqlDelete, conn))
                        {
                            cmd.Parameters.AddWithValue("@sid", sid);
                            cmd.Parameters.AddWithValue("@did", did);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BtnLoadStudents_Click(null, null);

            foreach (DataGridViewRow row in dgvStudents.Rows)
            {
                if ((int)row.Cells["Номер_студента"].Value == sid)
                {
                    row.Selected = true;
                    dgvStudents.CurrentCell = row.Cells["Студент"];
                    break;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
            this.Close();
        }
    }
}