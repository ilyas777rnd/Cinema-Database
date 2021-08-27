using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using System.Data.Common;

namespace Cinema
{
    public partial class ActorsAndGenre : Form
    {
        public ActorsAndGenre()
        {
            InitializeComponent();
            dataGridView1.CellClick += film_info;
            cbTable.Items.AddRange(new string[] { "Актеры", "Жанры" });
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); int fields = 0;
                string query1 = "Select * from Movies";
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
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
            }
            updateTables();
        }

        private void updateTables()
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); int fields = 0;
                string query2 = "Select * from Actors ";
                string query3 = "Select * from Genre";
                dataGridView2.Rows.Clear(); dataGridView3.Rows.Clear();
                dataGridView2.Columns.Clear(); dataGridView3.Columns.Clear();
                dataGridView2.ColumnCount = fields = 2;
                dataGridView2.Columns[0].HeaderText = "ID Актера"; dataGridView2.Columns[0].Width = 50;
                dataGridView2.Columns[1].HeaderText = "Имя Актера"; dataGridView2.Columns[1].Width = 140;
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con);
                int ind = 0;
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
                dataGridView3.ColumnCount = fields = 2;
                dataGridView3.Columns[0].HeaderText = "ID Жанра"; dataGridView3.Columns[0].Width = 40;
                dataGridView3.Columns[1].HeaderText = "Навзание Жанра"; dataGridView3.Columns[1].Width = 90;
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
            }
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
                        case "Актеры":
                            query = $"INSERT into Actors (name_actor) VALUES ('{tbName.Text}')";
                            break;
                        case "Жанры":
                            query = $"INSERT into Genre (name_genre) VALUES ('{tbName.Text}')";
                            break;
                    }
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    updateTables();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btAssignActor_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string query = "";
                try
                {
                    if (dataGridView1.CurrentCell.Value == null || dataGridView3.CurrentCell.Value == null)
                    throw new Exception("Выберите фильм и актера!");
                    string film_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    string actor_id = dataGridView2[0, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                    query = $"INSERT into Actors_list (actor,movie) VALUES ({actor_id},{film_id})";
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    film_info(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btAssignGengre_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string query = "";
                try
                {
                    if (dataGridView1.CurrentCell.Value == null || dataGridView3.CurrentCell.Value == null)
                        throw new Exception("Выберите фильм и жанр!");
                    string film_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    string genre_id = dataGridView3[0, dataGridView3.CurrentCell.RowIndex].Value.ToString();
                    query = $"INSERT into Genre_list (genre,movie) VALUES ({genre_id},{film_id})";
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    film_info(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DismissGenre_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string query = "";
                try
                {
                    if (dataGridView1.CurrentCell.Value == null)
                        throw new Exception("Выберите фильм и жанр!");
                    string film_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    string genre_id = lbGenre.SelectedItem.ToString();
                    query = $"Delete from Genre_list WHERE genre=genre_id('{genre_id}') AND movie={film_id}";
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    film_info(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DismissActor_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string query = "";
                try
                {
                    if (dataGridView1.CurrentCell.Value == null)
                        throw new Exception("Выберите фильм и актера!");
                    string film_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    string actor_id = lbActors.SelectedItem.ToString();
                    query = $"Delete from Actors_list WHERE actor=actor_id('{actor_id}') AND movie={film_id}";
                    //MessageBox.Show(query);
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    film_info(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string query = "";
                try
                {
                    switch (cbTable.Text)
                    {
                        case "Актеры":
                            string actor_id = dataGridView2[0, dataGridView2.CurrentCell.RowIndex].Value.ToString();
                            query = $"Delete from Actors WHERE IDactor={actor_id}";
                            break;
                        case "Жанры":
                            string genre_id = dataGridView3[0, dataGridView3.CurrentCell.RowIndex].Value.ToString();
                            query = $"Delete from Genre WHERE IDgenre={genre_id}";
                            break;
                    }
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                    updateTables();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
