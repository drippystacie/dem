namespace DemoApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.cmbDisciplines = new System.Windows.Forms.ComboBox();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            this.labelDiscipline = new System.Windows.Forms.Label();
            this.labelGroup = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLoadStudents = new System.Windows.Forms.Button();
            this.btnViewGrades = new System.Windows.Forms.Button();
            this.numGrade = new System.Windows.Forms.NumericUpDown();
            this.btnSaveGrade = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrade)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStudents
            // 
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(12, 71);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.Size = new System.Drawing.Size(345, 243);
            this.dgvStudents.TabIndex = 1;
            // 
            // cmbDisciplines
            // 
            this.cmbDisciplines.FormattingEnabled = true;
            this.cmbDisciplines.Location = new System.Drawing.Point(28, 25);
            this.cmbDisciplines.Name = "cmbDisciplines";
            this.cmbDisciplines.Size = new System.Drawing.Size(121, 21);
            this.cmbDisciplines.TabIndex = 2;
            // 
            // cmbGroups
            // 
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Location = new System.Drawing.Point(183, 25);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(121, 21);
            this.cmbGroups.TabIndex = 3;
            // 
            // labelDiscipline
            // 
            this.labelDiscipline.AutoSize = true;
            this.labelDiscipline.Location = new System.Drawing.Point(49, 9);
            this.labelDiscipline.Name = "labelDiscipline";
            this.labelDiscipline.Size = new System.Drawing.Size(70, 13);
            this.labelDiscipline.TabIndex = 6;
            this.labelDiscipline.Text = "Дисциплина";
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(216, 9);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(42, 13);
            this.labelGroup.TabIndex = 7;
            this.labelGroup.Text = "Группа";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(52, 329);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Закрыть";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLoadStudents
            // 
            this.btnLoadStudents.Location = new System.Drawing.Point(376, 154);
            this.btnLoadStudents.Name = "btnLoadStudents";
            this.btnLoadStudents.Size = new System.Drawing.Size(139, 23);
            this.btnLoadStudents.TabIndex = 15;
            this.btnLoadStudents.Text = "Загрузить студентов";
            this.btnLoadStudents.UseVisualStyleBackColor = true;
            // 
            // btnViewGrades
            // 
            this.btnViewGrades.Location = new System.Drawing.Point(172, 329);
            this.btnViewGrades.Name = "btnViewGrades";
            this.btnViewGrades.Size = new System.Drawing.Size(131, 23);
            this.btnViewGrades.TabIndex = 16;
            this.btnViewGrades.Text = "Показать все оценки";
            this.btnViewGrades.UseVisualStyleBackColor = true;
            // 
            // numGrade
            // 
            this.numGrade.Location = new System.Drawing.Point(322, 25);
            this.numGrade.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numGrade.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numGrade.Name = "numGrade";
            this.numGrade.Size = new System.Drawing.Size(120, 20);
            this.numGrade.TabIndex = 17;
            this.numGrade.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnSaveGrade
            // 
            this.btnSaveGrade.Location = new System.Drawing.Point(376, 200);
            this.btnSaveGrade.Name = "btnSaveGrade";
            this.btnSaveGrade.Size = new System.Drawing.Size(134, 23);
            this.btnSaveGrade.TabIndex = 18;
            this.btnSaveGrade.Text = "Сохранить оценку";
            this.btnSaveGrade.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Дашборд";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 365);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSaveGrade);
            this.Controls.Add(this.numGrade);
            this.Controls.Add(this.btnViewGrades);
            this.Controls.Add(this.btnLoadStudents);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.labelDiscipline);
            this.Controls.Add(this.cmbGroups);
            this.Controls.Add(this.cmbDisciplines);
            this.Controls.Add(this.dgvStudents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Оценки";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.ComboBox cmbDisciplines;
        private System.Windows.Forms.ComboBox cmbGroups;
        private System.Windows.Forms.Label labelDiscipline;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLoadStudents;
        private System.Windows.Forms.Button btnViewGrades;
        private System.Windows.Forms.NumericUpDown numGrade;
        private System.Windows.Forms.Button btnSaveGrade;
        private System.Windows.Forms.Button button1;
    }
}