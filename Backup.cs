using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using Npgsql;

namespace Cinema
{
    public partial class Backup : Form
    {
        public Backup()
        {
            InitializeComponent();
            rbOrder.Checked = false;
            rbTickets.Checked = false;
            show();
        }

        void update_cost(int ord_id, int session_id, string direction) //Обновляет стоимость заказа при изменении корзины
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                try
                {
                    string query = "UPDATE Orderr SET " +
                    $" total = total {direction} find_price({session_id}) " +
                    $" WHERE IDorder = {ord_id}";
                    new NpgsqlCommand(query, con).ExecuteNonQuery();
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        void show()
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                string query1 = "Select * from OrderH ORDER BY Time_edit DESC";
                string query2 = "Select * from TicketsH ORDER BY Time_edit DESC";
                int fields;
                //1-я таблица - Заказ
                dataGridView1.Rows.Clear(); dataGridView2.Rows.Clear();
                dataGridView1.Columns.Clear(); dataGridView2.Columns.Clear();
                dataGridView1.ColumnCount = fields = 6; int ind = 0;
                dataGridView1.Columns[0].HeaderText = "ID заказа"; dataGridView1.Columns[0].Width = 90;
                dataGridView1.Columns[1].HeaderText = "Стоимость заказа"; dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].HeaderText = "Дата заказа"; dataGridView1.Columns[2].Width = 60;
                dataGridView1.Columns[3].HeaderText = "ID пользователя"; dataGridView1.Columns[3].Width = 60;
                dataGridView1.Columns[4].HeaderText = "Действие"; dataGridView1.Columns[4].Width = 90;
                dataGridView1.Columns[5].HeaderText = "Время"; dataGridView1.Columns[5].Width = 90;
                NpgsqlCommand cmd1 = new NpgsqlCommand(query1, con);
                NpgsqlDataReader reader1 = cmd1.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader1)
                {
                    dataGridView1.RowCount++;
                    for (int col = 0; col < fields; col++)
                    {
                        dataGridView1[col, ind].Value = dbDataRecord[col].ToString();
                    }
                    ind++;
                }
                con.Close(); con.Open();
                //2-я таблица - Состав заказа
                dataGridView2.ColumnCount = fields = 5;
                dataGridView2.Columns[0].HeaderText = "ID заказа"; dataGridView2.Columns[0].Width = 60;
                dataGridView2.Columns[1].HeaderText = "ID места"; dataGridView2.Columns[1].Width = 60;
                dataGridView2.Columns[2].HeaderText = "ID сеанса"; dataGridView2.Columns[2].Width = 60;
                dataGridView2.Columns[3].HeaderText = "Действие"; dataGridView2.Columns[3].Width = 90;
                dataGridView2.Columns[4].HeaderText = "Время"; dataGridView2.Columns[4].Width = 90;
                NpgsqlCommand cmd2 = new NpgsqlCommand(query2, con); ind = 0;
                NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in reader2)
                {
                    dataGridView2.RowCount++;
                    for (int col = 0; col < fields; col++)
                    {
                        dataGridView2[col, ind].Value = dbDataRecord[col].ToString();
                    }
                    ind++;
                }
            }
        }

        void backup(int row_ind)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                string queryH = "", main_query = "";
                try
                {
                    if (rbOrder.Checked)
                    {
                        //queryH = $"Delete from OrderH WHERE idorder={dataGridView1[0, row_ind].Value.ToString()}";
                        if (dataGridView1[4, row_ind].Value.ToString().Equals("INSERT"))
                        {
                            main_query = $"Delete from Orderr WHERE IDorder={dataGridView1[0, row_ind].Value.ToString()}";
                            //new NpgsqlCommand(queryH, con).ExecuteNonQuery();
                            Console.WriteLine(main_query);
                            new NpgsqlCommand(main_query, con).ExecuteNonQuery();
                        }
                        else if (dataGridView1[4, row_ind].Value.ToString().Equals("UPDATE"))
                        {
                            string upd_query = "UPDATE Orderr SET " +
                            $" total={dataGridView1[1, row_ind].Value.ToString()} " +
                            $", date='{dataGridView1[2, row_ind].Value.ToString()}' " +
                            $", user_ord=" + (dataGridView1[3, row_ind].Value.ToString().Equals("") ? "NULL" : dataGridView1[3, row_ind].Value.ToString()) + " " +
                            $" WHERE IDorder={dataGridView1[0, row_ind].Value.ToString()}";
                            // new NpgsqlCommand(queryH, con).ExecuteNonQuery();
                            Console.WriteLine(upd_query);
                            new NpgsqlCommand(upd_query, con).ExecuteNonQuery();
                        }
                        else if (dataGridView1[4, row_ind].Value.ToString().Equals("DELETE"))
                        {
                            string query_add = "INSERT INTO Orderr (IDorder, total, date, user_ord) " +
                    $"VALUES({dataGridView1[0, row_ind].Value.ToString()}, { dataGridView1[1, row_ind].Value.ToString()} " +
                            $", '{dataGridView1[2, row_ind].Value.ToString()}' " +
                            $", " + (dataGridView1[3, row_ind].Value.ToString().Equals("") ? "NULL" : dataGridView1[3, row_ind].Value.ToString()) + " )";
                            //new NpgsqlCommand(queryH, con).ExecuteNonQuery();
                            Console.WriteLine(query_add);
                            new NpgsqlCommand(query_add, con).ExecuteNonQuery();
                        }
                    }
                    else if (rbTickets.Checked)
                    {
                        queryH = $"Delete from TicketsH WHERE orderr={dataGridView2[0, row_ind].Value.ToString()} AND place={dataGridView2[1, row_ind].Value.ToString()} AND session={dataGridView2[2, row_ind].Value.ToString()}";
                        if (dataGridView2[3, row_ind].Value.ToString().Equals("INSERT"))
                        {
                            main_query = $"Delete from Tickets WHERE orderr={dataGridView2[0, row_ind].Value.ToString()} AND place={dataGridView2[1, row_ind].Value.ToString()} AND session={dataGridView2[2, row_ind].Value.ToString()}";
                            //new NpgsqlCommand(queryH, con).ExecuteNonQuery();
                            new NpgsqlCommand(main_query, con).ExecuteNonQuery();
                            update_cost(int.Parse(dataGridView2[0, row_ind].Value.ToString()), int.Parse(dataGridView2[2, row_ind].Value.ToString()), "-");
                        }
                        else if (dataGridView2[3, row_ind].Value.ToString().Equals("DELETE"))
                        {
                            string ins_query = "INSERT INTO Tickets (orderr, place, session) VALUES (" +
                                $" {dataGridView2[0, row_ind].Value.ToString()} " +
                                $" ,{dataGridView2[1, row_ind].Value.ToString()} " +
                                $" ,{dataGridView2[2, row_ind].Value.ToString()} )";
                            //new NpgsqlCommand(queryH, con).ExecuteNonQuery();
                            new NpgsqlCommand(ins_query, con).ExecuteNonQuery();
                            update_cost(int.Parse(dataGridView2[0, row_ind].Value.ToString()), int.Parse(dataGridView2[2, row_ind].Value.ToString()), "+");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Таблица не выбрана");
                        return;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void rbOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTickets.Checked == true)
                rbTickets.Checked = false;   
        }

        private void rbOrdList_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOrder.Checked == true)
                rbOrder.Checked = false;
        }

        private void btBackup_Click(object sender, EventArgs e)
        {
            try
            {
                int row_index = 0;
                if (rbOrder.Checked == true) row_index = dataGridView1.CurrentCell.RowIndex;
                else if (rbTickets.Checked == true) row_index = dataGridView2.CurrentCell.RowIndex;
                else throw new Exception("Выберите таблицу!");
                backup(row_index);
                show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
