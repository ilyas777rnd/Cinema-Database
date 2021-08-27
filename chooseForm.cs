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
    public partial class chooseForm : Form
    {
        public chooseForm()
        {
            InitializeComponent();
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); int fields = 0;
                //string query1 = "Select * from Movies";
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                show_sessions(get_base_query() + " ORDER BY time DESC ");
                Login.connectionReset(con);
                string query = "Select name_actor from (Actors_list JOIN Actors ON actor=IDactor)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader)
                {
                    lbActorsSearch.Items.Add(dbDataRecord[0].ToString());
                }
                Login.connectionReset(con);
                string query2 = "Select DISTINCT  name_genre from (Genre_list JOIN Genre ON genre=IDgenre) ";
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader2)
                {
                    lbGenreSearch.Items.Add(dbDataRecord[0].ToString());
                }
            }
            if (Login.purch_id == null && Login.stuff_id == null)
            {
                btBuy.Visible = false;
            }
            if (Login.stuff_id == null)
            {
                btOrdList.Visible = false;
            }
            dataGridView1.CellClick += film_info;
        }

        private string get_base_query() => "Select IDMovie,IDsession,name_movie,time,price,name_hall,descriptioin,holl_of_session,price " +
                " from(Sessions LEFT JOIN Movies ON film = IDmovie) " +
                $" LEFT JOIN Holls ON holl_of_session = IDholl WHERE (name_movie LIKE '%{tbName.Text}%') AND time<now() ";

        private void show_sessions(string query)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); int fields = 0;
                //string query1 = "Select * from Movies";
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = fields = 9;
                //ID фильма
                dataGridView1.Columns[0].HeaderText = ""; dataGridView1.Columns[0].Width = 1;
                //ID сеанса
                dataGridView1.Columns[1].HeaderText = ""; dataGridView1.Columns[1].Width = 1;
                dataGridView1.Columns[2].HeaderText = "Название фильма"; dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[3].HeaderText = "Время сеанса"; dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[4].HeaderText = "Цена за 1 билет (руб)"; dataGridView1.Columns[4].Width = 80;
                dataGridView1.Columns[5].HeaderText = "Название зала"; dataGridView1.Columns[5].Width = 100;
                //Описание
                dataGridView1.Columns[6].HeaderText = ""; dataGridView1.Columns[6].Width = 1;
                //ID зала
                dataGridView1.Columns[7].HeaderText = ""; dataGridView1.Columns[7].Width = 1;
                //Цена
                dataGridView1.Columns[8].HeaderText = ""; dataGridView1.Columns[8].Width = 1;
                NpgsqlCommand cmd1 = new NpgsqlCommand(query, con);
                int ind = 0;
                NpgsqlDataReader reader1 = cmd1.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader1)
                {
                    dataGridView1.RowCount++;
                    for (int i = 0; i < fields; i++)
                        dataGridView1[i, ind].Value = dbDataRecord[i].ToString();
                    ind++;
                }
            }
        }

        private void film_info(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                lbActors.Items.Clear(); lbGenre.Items.Clear();
                con.Open();
                string film_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string query1 = "Select name_actor from (Actors_list JOIN Actors ON actor=IDactor) " +
                $" JOIN Movies ON movie = IDmovie WHERE IDmovie = {film_id}";
                string query2 = "Select name_genre from (Genre_list JOIN Genre ON genre=IDgenre) " +
                $" JOIN Movies ON movie = IDmovie WHERE IDmovie = {film_id}";
                NpgsqlCommand cmd1 = new NpgsqlCommand(query1, con);
                NpgsqlDataReader reader1 = cmd1.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader1)
                {
                    lbActors.Items.Add(dbDataRecord[0].ToString());
                }
                Login.connectionReset(con);
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader2)
                {
                    lbGenre.Items.Add(dbDataRecord[0].ToString());
                }
                tbDescription.Text = dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                //tbDescription.Text += dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            string query = get_base_query();
            //Добавили всех выбранных актеров
            foreach (var actor in lbActorsSearch.CheckedItems)
            {
                query += $" AND has_actor(actor_id('{actor}'),IDMovie)>0 ";
            }
            //Добавили все выбранные жанры
            foreach (var genre in lbGenreSearch.CheckedItems)
            {
                query += $" AND has_genre(genre_id('{genre}'), IDmovie)>0 ";
            }

            query += " ORDER BY time DESC ";
            show_sessions(query);
        }

        private void btBuy_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                string id_holl = dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string id_session = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string price = dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string query = $"Select rows_qty,places_qty from Holls WHERE IDholl={id_holl}";
                int rows_qty = 0, places_qty = 0;
                NpgsqlCommand cmd1 = new NpgsqlCommand(query, con);
                NpgsqlDataReader reader1 = cmd1.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader1)
                {
                    rows_qty = int.Parse(dbDataRecord[0].ToString());
                    places_qty = int.Parse(dbDataRecord[1].ToString());
                }
                
                BuyForm buyForm = new BuyForm(int.Parse(id_session), int.Parse(id_holl), int.Parse(price), rows_qty, places_qty);
                buyForm.Show();
            }
        }

        private void btOrdList_Click(object sender, EventArgs e)
        {
            string id_session = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
            OrdersForm ordersForm = new OrdersForm(id_session);
            ordersForm.Show();
        }
    }
}
