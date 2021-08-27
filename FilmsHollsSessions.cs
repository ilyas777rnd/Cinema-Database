using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Data.Common;

namespace Cinema
{
    public partial class FilmsHollsSessions : Form
    {
        private Control[] film_elems;
        private Control[] session_elems;
        private Control[] holl_elems;
        public FilmsHollsSessions()
        {
            InitializeComponent();
            cbTable.Items.Add("Фильмы");
            cbTable.Items.Add("Сеансы");
            cbTable.Items.Add("Залы");
            film_elems = new Control[] { label2, label3, tbFilmName, tbDescription };

            session_elems = new Control[] { groupBoxSession, label7, label8, label9,
            tbHours, tbMinutes, tbPrice, Calendar};

            holl_elems = new Control[] { label10, label11, label12,
            tbHallName, tbRows, tbSeats};

            hide();
            show();
        }

        private void hide()
        {
            foreach (var item in film_elems.Union(session_elems).Union(holl_elems))
            {
                item.Visible = false;
            }
        }

        private string getDate()
        {
            string[] date = Calendar.SelectionStart.ToString().Split(' ')[0].Split('.');
            return $"{date[0]}-{date[1]}-{date[2]}";
        }

        private void show()
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); int fields = 0;
                string query1 = "Select * from Movies";
                string query2 = "Select IDsession,name_movie,time,price,name_hall "
                + " from (Sessions LEFT JOIN Movies ON film = IDmovie) " +
                " LEFT JOIN Holls ON holl_of_session=IDholl ORDER BY time DESC";
                string query3 = "Select * from Holls";
                dataGridView1.Rows.Clear(); dataGridView2.Rows.Clear(); dataGridView3.Rows.Clear();
                dataGridView1.Columns.Clear(); dataGridView2.Columns.Clear(); dataGridView3.Columns.Clear();
                dataGridView1.ColumnCount = fields = 3;
                dataGridView1.Columns[0].HeaderText = "ID"; dataGridView1.Columns[0].Width = 30;
                dataGridView1.Columns[1].HeaderText = "Название"; dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Описание"; dataGridView1.Columns[2].Width = 120;
                NpgsqlCommand cmd1 = new NpgsqlCommand(query1, con);
                int ind = 0;
                NpgsqlDataReader reader1 = cmd1.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader1)
                {
                    dataGridView1.RowCount++;
                    for (int i = 0; i < fields; i++)
                        dataGridView1[i, ind].Value = dbDataRecord[i].ToString();
                    ind++;
                }
                Login.connectionReset(con);
                //2-я таблица Сеансы
                dataGridView2.ColumnCount = fields = 5;
                dataGridView2.Columns[0].HeaderText = "ID"; dataGridView2.Columns[0].Width = 30;
                dataGridView2.Columns[1].HeaderText = "Название"; dataGridView2.Columns[1].Width = 140;
                dataGridView2.Columns[2].HeaderText = "Время"; dataGridView2.Columns[2].Width = 120;
                dataGridView2.Columns[3].HeaderText = "Цена"; dataGridView2.Columns[3].Width = 40;
                dataGridView2.Columns[4].HeaderText = "Зал"; dataGridView2.Columns[4].Width = 70;
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                ind = 0;
                NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader2)
                {
                    dataGridView2.RowCount++;
                    for (int i = 0; i < fields; i++)
                    {
                        dataGridView2[i, ind].Value = dbDataRecord[i].ToString();
                    }
                    ind++;
                }
                Login.connectionReset(con);
                dataGridView3.ColumnCount = fields = 4;
                dataGridView3.Columns[0].HeaderText = "ID"; dataGridView3.Columns[0].Width = 40;
                dataGridView3.Columns[1].HeaderText = "Навзание"; dataGridView3.Columns[1].Width = 90;
                dataGridView3.Columns[2].HeaderText = "Кол-во рядов"; dataGridView3.Columns[2].Width = 50;
                dataGridView3.Columns[3].HeaderText = "Кол-во кресел"; dataGridView3.Columns[3].Width = 50;
                NpgsqlCommand cmd3 = new NpgsqlCommand(query3, con);
                ind = 0;
                NpgsqlDataReader reader3 = cmd3.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader3)
                {
                    dataGridView3.RowCount++;
                    for (int i = 0; i < fields; i++)
                    {
                        dataGridView3[i, ind].Value = dbDataRecord[i].ToString();
                    }
                    ind++;
                }
            }
        }

        private void groupBoxSession_Enter(object sender, EventArgs e)
        {
           //Не трогать
        }

        private void Add_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string query = "";
                try
                {
                    switch (cbTable.Text) 
                    {
                        case "Фильмы":
                            query = $"INSERT into Movies (name_movie, descriptioin) VALUES ('{tbFilmName.Text}','{tbDescription.Text}');";
                            break;
                        case "Сеансы":
                            if (dataGridView1.CurrentCell == null || dataGridView3.CurrentCell == null)
                                throw new Exception("Выберите фильм и выберите зал, чтобы добавить сеанс!");
                            string film_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                            string holl_id = dataGridView3[0, dataGridView3.CurrentCell.RowIndex].Value.ToString();
                            query = $"INSERT into Sessions(film, time, price, holl_of_session) " +
                                $" VALUES ({film_id},'{getDate()} {tbHours.Text}:{tbMinutes.Text}:00',{tbPrice.Text},{holl_id})";
                            //MessageBox.Show(query);
                            break;
                        case "Залы":
                            query = $"INSERT into Holls (name_hall, rows_qty, places_qty) VALUES ('{tbHallName.Text}', {tbRows.Text},{tbSeats.Text})";
                            break;
                    }
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            hide();
            switch (cbTable.Text) {
                case "Фильмы":
                    foreach (var item in film_elems) item.Visible = true;
                    break;
                case "Сеансы":
                    foreach (var item in session_elems) item.Visible = true;
                    break;
                case "Залы":
                    foreach (var item in holl_elems) item.Visible = true;
                    break;
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string query = ""; string err = $"Выберите строку в таблице {cbTable.Text}";
                try
                {
                    switch (cbTable.Text)
                    {
                        case "Фильмы":
                            if (dataGridView1.CurrentCell.Value == null)
                                throw new Exception(err);
                            string mov_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                            query = $"Delete from Movies WHERE IDmovie={mov_id}";
                            break;
                        case "Сеансы":
                            if (dataGridView2.CurrentCell.Value == null)
                                throw new Exception(err);              
                            string session_id = dataGridView2[0, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                            query = $"Delete from Sessions WHERE IDsession={session_id}";
                            //MessageBox.Show(query);
                            break;
                        case "Залы":
                            if (dataGridView3.CurrentCell.Value == null)
                                throw new Exception(err);
                            string holl_id = dataGridView3[0, dataGridView3.CurrentCell.RowIndex].Value.ToString();
                            query = $"Delete from Holls WHERE IDholl={holl_id}";
                            break;
                    }
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
