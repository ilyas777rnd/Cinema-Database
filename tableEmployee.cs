using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Cinema
{
    public partial class tableEmployee : Form
    {
        public tableEmployee()
        {
            InitializeComponent();
            //cbPosition.Items.Add("Менеджер");
            //cbPosition.Items.Add("Оператор");
            cbField.Items.Add("Имя");
            cbField.Items.Add("Отчество");
            cbField.Items.Add("Фамилия");
            cbField.Items.Add("Телефон");
            cbField.Items.Add("Должность");
            show();
        }

        private void show()
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                dataGridView1.Rows.Clear(); dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 5;
                dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].Width = 30;
                dataGridView1.Columns[1].HeaderText = "Имя"; dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].HeaderText = "Отчество"; dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].HeaderText = "Фамилия"; dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].HeaderText = "Должность"; dataGridView1.Columns[4].Width = 80;
                string query = "Select * from Stuff";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                NpgsqlDataReader reader = cmd.ExecuteReader(); int ind = 0;
                foreach (DbDataRecord dbDataRecord in reader)
                {
                    dataGridView1.RowCount++;
                    dataGridView1[0, ind].Value = dbDataRecord[0].ToString();
                    dataGridView1[1, ind].Value = dbDataRecord[2].ToString();
                    dataGridView1[2, ind].Value = dbDataRecord[4].ToString();
                    dataGridView1[3, ind].Value = dbDataRecord[3].ToString();
                    dataGridView1[4, ind].Value = dbDataRecord[5].ToString();
                    ind++;
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                if (tbPassword.Text.Equals("")||tbSurname.Text.Equals(""))
                {
                    MessageBox.Show("Введите данные!");
                    return;
                }
                string query = " INSERT into Stuff(name,patronimyc,surname,post,password) " +
                    $" VALUES ('{tbName.Text}','{tbMid_Name.Text}','{tbSurname.Text}','{cbPosition.Text}','{Cod.EncodeString(tbPassword.Text)}') ";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.ExecuteNonQuery();
                show(); tbPassword.Text = "";
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                try
                {
                    con.Open(); string id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    string query = " Delete from Stuff WHERE IDstuff=" + id;
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    show();
                } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                try
                {
                    con.Open(); string query1 = "UPDATE Stuff SET ";
                    switch (cbField.Text)
                    {
                        case "Имя":
                            query1 += $" name='{cbNewValue.Text}' ";
                            break;
                        case "Отчество":
                            query1 += $" mid_name='{cbNewValue.Text}' ";
                            break;
                        case "Фамилия":
                            query1 += $" surname='{cbNewValue.Text}' ";
                            break;
                        case "Телефон":
                            query1 += $" phone='{cbNewValue.Text}' ";
                            break;
                        case "Должность":
                            query1 += $" post='{cbNewValue.Text}' ";
                            break;
                    }
                    query1+= " WHERE IDstuff=" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    NpgsqlCommand cmd = new NpgsqlCommand(query1, con);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(query1);
                } catch (Exception ex) { MessageBox.Show(ex.Message); }
                show();
            }
        }

        private void cbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbNewValue.Items.Clear();
            if (cbField.SelectedItem.Equals("Должность"))
            {
                cbNewValue.Items.Add("Менеджер");
                cbNewValue.Items.Add("Оператор");
            }
        }
    }
}
