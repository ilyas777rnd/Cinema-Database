namespace Cinema
{
    partial class BuyForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTickets = new System.Windows.Forms.ListBox();
            this.btAddPlace = new System.Windows.Forms.Button();
            this.btDeletePlace = new System.Windows.Forms.Button();
            this.btDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(865, 344);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Места:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(880, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Содержимое заказа:";
            // 
            // lbTickets
            // 
            this.lbTickets.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbTickets.FormattingEnabled = true;
            this.lbTickets.ItemHeight = 23;
            this.lbTickets.Location = new System.Drawing.Point(884, 52);
            this.lbTickets.Name = "lbTickets";
            this.lbTickets.Size = new System.Drawing.Size(557, 349);
            this.lbTickets.TabIndex = 3;
            // 
            // btAddPlace
            // 
            this.btAddPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btAddPlace.Location = new System.Drawing.Point(16, 418);
            this.btAddPlace.Name = "btAddPlace";
            this.btAddPlace.Size = new System.Drawing.Size(317, 50);
            this.btAddPlace.TabIndex = 4;
            this.btAddPlace.Text = "Добавить место в заказ";
            this.btAddPlace.UseVisualStyleBackColor = true;
            this.btAddPlace.Click += new System.EventHandler(this.btAddPlace_Click);
            // 
            // btDeletePlace
            // 
            this.btDeletePlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btDeletePlace.Location = new System.Drawing.Point(884, 418);
            this.btDeletePlace.Name = "btDeletePlace";
            this.btDeletePlace.Size = new System.Drawing.Size(231, 50);
            this.btDeletePlace.TabIndex = 5;
            this.btDeletePlace.Text = "Убрать место из заказа";
            this.btDeletePlace.UseVisualStyleBackColor = true;
            this.btDeletePlace.Click += new System.EventHandler(this.btDeletePlace_Click);
            // 
            // btDone
            // 
            this.btDone.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btDone.Location = new System.Drawing.Point(1176, 573);
            this.btDone.Name = "btDone";
            this.btDone.Size = new System.Drawing.Size(265, 84);
            this.btDone.TabIndex = 6;
            this.btDone.Text = "Оформить заказ";
            this.btDone.UseVisualStyleBackColor = true;
            this.btDone.Click += new System.EventHandler(this.btDone_Click);
            // 
            // BuyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(1453, 669);
            this.Controls.Add(this.btDone);
            this.Controls.Add(this.btDeletePlace);
            this.Controls.Add(this.btAddPlace);
            this.Controls.Add(this.lbTickets);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "BuyForm";
            this.Text = "BuyForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbTickets;
        private System.Windows.Forms.Button btAddPlace;
        private System.Windows.Forms.Button btDeletePlace;
        private System.Windows.Forms.Button btDone;
    }
}