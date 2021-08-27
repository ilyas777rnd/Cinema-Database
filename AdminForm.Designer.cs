namespace Cinema
{
    partial class AdminForm
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
            this.btEmployee = new System.Windows.Forms.Button();
            this.createCopy = new System.Windows.Forms.Button();
            this.loadCopy = new System.Windows.Forms.Button();
            this.Backup = new System.Windows.Forms.Button();
            this.cbTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btEmployee
            // 
            this.btEmployee.Location = new System.Drawing.Point(24, 32);
            this.btEmployee.Name = "btEmployee";
            this.btEmployee.Size = new System.Drawing.Size(295, 56);
            this.btEmployee.TabIndex = 0;
            this.btEmployee.Text = "Открыть таблицу для редактирования";
            this.btEmployee.UseVisualStyleBackColor = true;
            this.btEmployee.Click += new System.EventHandler(this.btTable_Click);
            // 
            // createCopy
            // 
            this.createCopy.BackColor = System.Drawing.Color.PaleGreen;
            this.createCopy.Location = new System.Drawing.Point(24, 218);
            this.createCopy.Name = "createCopy";
            this.createCopy.Size = new System.Drawing.Size(240, 69);
            this.createCopy.TabIndex = 3;
            this.createCopy.Text = "Создать резервную копию БД";
            this.createCopy.UseVisualStyleBackColor = false;
            this.createCopy.Click += new System.EventHandler(this.createCopy_Click);
            // 
            // loadCopy
            // 
            this.loadCopy.BackColor = System.Drawing.Color.PaleGreen;
            this.loadCopy.Location = new System.Drawing.Point(24, 293);
            this.loadCopy.Name = "loadCopy";
            this.loadCopy.Size = new System.Drawing.Size(240, 69);
            this.loadCopy.TabIndex = 4;
            this.loadCopy.Text = "Загрузить резервную копию БД";
            this.loadCopy.UseVisualStyleBackColor = false;
            this.loadCopy.Click += new System.EventHandler(this.loadCopy_Click);
            // 
            // Backup
            // 
            this.Backup.BackColor = System.Drawing.Color.LightCoral;
            this.Backup.Location = new System.Drawing.Point(523, 293);
            this.Backup.Name = "Backup";
            this.Backup.Size = new System.Drawing.Size(220, 69);
            this.Backup.TabIndex = 5;
            this.Backup.Text = "Система отката и темпоральные таблицы";
            this.Backup.UseVisualStyleBackColor = false;
            this.Backup.Click += new System.EventHandler(this.Backup_Click);
            // 
            // cbTable
            // 
            this.cbTable.FormattingEnabled = true;
            this.cbTable.Location = new System.Drawing.Point(24, 123);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(295, 24);
            this.cbTable.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Таблица:";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTable);
            this.Controls.Add(this.Backup);
            this.Controls.Add(this.loadCopy);
            this.Controls.Add(this.createCopy);
            this.Controls.Add(this.btEmployee);
            this.Name = "AdminForm";
            this.Text = "Админ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btEmployee;
        private System.Windows.Forms.Button createCopy;
        private System.Windows.Forms.Button loadCopy;
        private System.Windows.Forms.Button Backup;
        private System.Windows.Forms.ComboBox cbTable;
        private System.Windows.Forms.Label label1;
    }
}