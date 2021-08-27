namespace Cinema
{
    partial class Login
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.enter = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            this.passw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btRegistration = new System.Windows.Forms.Button();
            this.btEnterLikeGuest = new System.Windows.Forms.Button();
            this.chbStuff = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // enter
            // 
            this.enter.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.enter.Location = new System.Drawing.Point(129, 173);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(86, 30);
            this.enter.TabIndex = 0;
            this.enter.Text = "Войти";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(40, 35);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(272, 22);
            this.log.TabIndex = 1;
            // 
            // passw
            // 
            this.passw.Location = new System.Drawing.Point(40, 82);
            this.passw.Name = "passw";
            this.passw.Size = new System.Drawing.Size(272, 22);
            this.passw.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(36, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Логин:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(36, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Пароль:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(114, 120);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(143, 21);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Показать пароль";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btRegistration
            // 
            this.btRegistration.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btRegistration.Location = new System.Drawing.Point(40, 218);
            this.btRegistration.Name = "btRegistration";
            this.btRegistration.Size = new System.Drawing.Size(272, 30);
            this.btRegistration.TabIndex = 6;
            this.btRegistration.Text = "Зарегистрироваться";
            this.btRegistration.UseVisualStyleBackColor = true;
            this.btRegistration.Click += new System.EventHandler(this.btRegistration_Click);
            // 
            // btEnterLikeGuest
            // 
            this.btEnterLikeGuest.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btEnterLikeGuest.Location = new System.Drawing.Point(40, 267);
            this.btEnterLikeGuest.Name = "btEnterLikeGuest";
            this.btEnterLikeGuest.Size = new System.Drawing.Size(272, 30);
            this.btEnterLikeGuest.TabIndex = 7;
            this.btEnterLikeGuest.Text = "Войти как гость";
            this.btEnterLikeGuest.UseVisualStyleBackColor = true;
            this.btEnterLikeGuest.Click += new System.EventHandler(this.btEnterLikeGuest_Click);
            // 
            // chbStuff
            // 
            this.chbStuff.AutoSize = true;
            this.chbStuff.Location = new System.Drawing.Point(114, 146);
            this.chbStuff.Name = "chbStuff";
            this.chbStuff.Size = new System.Drawing.Size(163, 21);
            this.chbStuff.TabIndex = 8;
            this.chbStuff.Text = "Войти как персонал";
            this.chbStuff.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(360, 326);
            this.Controls.Add(this.chbStuff);
            this.Controls.Add(this.btEnterLikeGuest);
            this.Controls.Add(this.btRegistration);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passw);
            this.Controls.Add(this.log);
            this.Controls.Add(this.enter);
            this.Name = "Login";
            this.Text = "Авторизация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.TextBox passw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btRegistration;
        private System.Windows.Forms.Button btEnterLikeGuest;
        private System.Windows.Forms.CheckBox chbStuff;
    }
}

