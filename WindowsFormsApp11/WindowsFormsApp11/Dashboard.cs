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
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp11
{
    public partial class Dashboard : Form
    {
        private string connectionString = @"Data Source=DESKTOP-IT2VBE8;Initial Catalog=demo;Integrated Security=True;TrustServerCertificate=True";
        public Dashboard()
        {
            InitializeComponent();
            LoadGroups();
        }

        private DataTable Query(string sql, Action<SqlParameterCollection> add = null)
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                add?.Invoke(cmd.Parameters);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }


        private void LoadGroups()
        {
            cbGroup.DataSource = Query(
                "SELECT Номер_группы, Специальность FROM [Группа] ORDER BY Специальность");
            cbGroup.ValueMember = "Номер_группы";
            cbGroup.DisplayMember = "Специальность";
            cbGroup.SelectedIndexChanged += (_, __) => LoadStudents();
            LoadStudents();
        }

        private void LoadStudents()
        {
            if (cbGroup.SelectedValue == null) return;
            int gid = (int)cbGroup.SelectedValue;

            cbStudent.DataSource = Query(
                @"SELECT Номер_группы,
                     Фамилия+' '+LEFT(Имя,1)+'.'+LEFT(Отчество,1)+'.' AS fio
              FROM Студент WHERE Номер_группы=@g ORDER BY Фамилия",
                p => p.AddWithValue("@g", gid));

            cbStudent.ValueMember = "Номер_группы";
            cbStudent.DisplayMember = "fio";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cbStudent.SelectedValue == null) return;
            int sid = (int)cbStudent.SelectedValue;

            var dt = Query(
                @"SELECT d.Название  AS Дисциплина,
                     g.Оценка AS Оценки
              FROM Оценки g
              JOIN Дисциплина d ON d.Код_дисциплины = g.Код_дисциплины
              WHERE g.Номер_студента=@s
              ORDER BY d.Название",
                p => p.AddWithValue("@s", sid));

            gridMarks.DataSource = dt;

            chartMarks.Series.Clear();
            var ser = chartMarks.Series.Add("Оценки");
            ser.ChartType = SeriesChartType.Column;
            ser.XValueMember = "Дисциплина";
            ser.YValueMembers = "Оценка";
            chartMarks.ChartAreas[0].AxisY.Minimum = 0;
            chartMarks.ChartAreas[0].AxisY.Maximum = 5;
            chartMarks.DataSource = dt;
            chartMarks.DataBind();
        }
    }
}
