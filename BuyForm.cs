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
using System.IO;

namespace Cinema
{
    public partial class BuyForm : Form
    {
        private int session_id;
        private int holl_id;
        private int price;

        private int rows_qty;
        private int places_in_row_qty;

        private bool is_done;
        public BuyForm(int session_id, int holl_id, int price, int rows_qty, int places_in_row_qty)
        {
            InitializeComponent();
            this.session_id = session_id;
            this.holl_id = holl_id;
            this.price = price;

            this.rows_qty = rows_qty;
            this.places_in_row_qty = places_in_row_qty;
            this.is_done = false;

            bool[,] places = new bool[rows_qty, places_in_row_qty];
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open(); string order_query = "";
                if (Login.purch_id == null)
                {
                    order_query = $" INSERT into Orderr (total, date) VALUES (0, now()) ";
                }
                else
                {
                    order_query = $" INSERT into Orderr (total, date, user_ord) VALUES (0, now(), {Login.purch_id}) ";
                }
                Console.WriteLine(order_query);
                new NpgsqlCommand(order_query, con).ExecuteNonQuery();

                string query = "Select row_of_place,num_of_place from (Tickets LEFT JOIN Places ON place=IDplace)" +
                $"WHERE session = {session_id}";
                NpgsqlCommand cmd1 = new NpgsqlCommand(query, con);
                NpgsqlDataReader reader1 = cmd1.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader1)
                {
                    int row = int.Parse(dbDataRecord[0].ToString());
                    int place = int.Parse(dbDataRecord[1].ToString());
                    places[row - 1, place - 1] = true;
                }
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].HeaderText = "Ряд"; dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].HeaderText = "Место"; dataGridView1.Columns[1].Width = 50;
                dataGridView1.Columns[2].HeaderText = "Цена"; dataGridView1.Columns[2].Width = 70;
                dataGridView1.Columns[3].HeaderText = "Статус"; dataGridView1.Columns[3].Width = 100;
                int dgv_row_ind = 0;
                int rows = places.GetUpperBound(0) + 1;
                int columns = places.GetUpperBound(1) + 1;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        dataGridView1.RowCount++;
                        dataGridView1[0, dgv_row_ind].Value = i + 1;
                        dataGridView1[1, dgv_row_ind].Value = j + 1;
                        dataGridView1[2, dgv_row_ind].Value = this.price;
                        dataGridView1[3, dgv_row_ind].Value = places[i, j] ? "Занято" : "Свободно";
                        dgv_row_ind++;
                    }
                }
            }
            this.FormClosed += completeOrder;
        }

        private void completeOrder(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                if (!is_done)
                {
                    string query = "Delete from Orderr WHERE IDorder = last_ord()";
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                }
            }
        }

        private void btAddPlace_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();

                string status = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                if (status.Equals("Занято"))
                {
                    MessageBox.Show("Данное место уже занято.");
                    return;
                }
                
                string row = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string place = dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                
                string query_place = $"INSERT into Places (row_of_place,num_of_place,holl) VALUES ({row},{place},{holl_id})" +
                "ON CONFLICT(row_of_place, num_of_place, holl) DO NOTHING";
                new NpgsqlCommand(query_place, con).ExecuteNonQuery();
                Login.connectionReset(con);
                //Добавили место, если его не было, теперь добавим билет
                string ticket_query = $"INSERT into Tickets (orderr, place, session) " +
                    $" VALUES (last_ord(),find_place_id({row},{place},{holl_id}),{session_id}) ";
                new NpgsqlCommand(ticket_query, con).ExecuteNonQuery();
                using (StreamWriter writer = new StreamWriter("C:\\Users\\sulin\\Desktop\\OJ.txt")) 
                {
                    writer.Write(ticket_query);
                }
                dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value = "Занято";
                MessageBox.Show($"Ряд {row}, место {place} успешно забронировано!");
                lbTickets.Items.Add($"Ряд {row} , место {place}");
            }
        }

        private void btDeletePlace_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                if (lbTickets.SelectedItem == null)
                {
                    MessageBox.Show("Выберите место!");
                    return;
                }
                con.Open();
                string[] info = lbTickets.SelectedItem.ToString().Split(' ');
                //string status = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string row = info[1];
                string place = info[4];
                //if (status.Equals("Свободно"))
                //{
                //    MessageBox.Show("Данное место свободно, его нельзя удалить из заказа!");
                //    return;
                //}

                string del_query = "Delete from Tickets WHERE " +
                    " orderr = last_ord() AND " +
                    $" place = find_place_id({row},{place},{holl_id}) AND " +
                    $" session = {session_id} ";
                new NpgsqlCommand(del_query, con).ExecuteNonQuery();
                
                //MessageBox.Show(del_query);
                for(int i=0;i<dataGridView1.RowCount;i++)
                {
                    if (dataGridView1[0,i].Value.ToString().Equals(row) && dataGridView1[1, i].Value.ToString().Equals(place))
                    {
                        dataGridView1[3, i].Value = "Свободно";
                        break;
                    }
                }
                lbTickets.Items.RemoveAt(lbTickets.SelectedIndex);
            }
        }

        private void btDone_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                is_done = true; string ord_number = "0";
                string query_number = "Select last_ord()";
                NpgsqlCommand cmd1 = new NpgsqlCommand(query_number, con);
                NpgsqlDataReader reader1 = cmd1.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader1)
                {
                    ord_number = dbDataRecord[0].ToString();
                }
                Login.connectionReset(con);
                int price = int.Parse(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString()) * lbTickets.Items.Count;
                string price_query = $"UPDATE Orderr SET total={price} WHERE IDorder={ord_number}";
                new NpgsqlCommand(price_query, con).ExecuteNonQuery();
                MessageBox.Show($"Номер вашего заказа - {ord_number}, цена - {price} руб");
                this.Close();
            }
        }
    }
}
