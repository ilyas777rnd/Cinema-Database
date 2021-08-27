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
    public partial class ClientForm : Form
    {

        private void output(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                if (dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value == null || dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString().Equals(""))
                {
                    MessageBox.Show("Ошибка, нажмите на непустую ячейку");
                    return;
                }
                con.Open();
                string ord_id = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                //orderDetail(ord_id);
                string query = "Select row_of_place,num_of_place,time,name_hall" +
                " from(( Tickets LEFT JOIN Places ON place = IDplace) " +
                " LEFT JOIN Sessions ON session = IDsession) " +
                " LEFT JOIN Holls ON holl_of_session = IDholl " +
                $" WHERE orderr = {ord_id}";
                dgvDetail.Rows.Clear(); dgvDetail.Columns.Clear();
                dgvDetail.ColumnCount = 5;
                dgvDetail.Columns[0].HeaderText = "Ряд"; dgvDetail.Columns[0].Width = 60;
                dgvDetail.Columns[1].HeaderText = "Место"; dgvDetail.Columns[1].Width = 60;
                dgvDetail.Columns[2].HeaderText = "Дата сеанса"; dgvDetail.Columns[2].Width = 90;
                dgvDetail.Columns[3].HeaderText = "Время сеанса"; dgvDetail.Columns[3].Width = 60;
                dgvDetail.Columns[4].HeaderText = "Зал"; dgvDetail.Columns[4].Width = 60;
                NpgsqlCommand find = new NpgsqlCommand(query, con);
                NpgsqlDataReader npgSqlDataReader = find.ExecuteReader();
                int ind = 0;
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    dgvDetail.RowCount++;
                    dgvDetail[0, ind].Value = dbDataRecord[0].ToString();
                    dgvDetail[1, ind].Value = dbDataRecord[1].ToString();
                    dgvDetail[2, ind].Value = dbDataRecord[2].ToString().Split(' ')[0];
                    dgvDetail[3, ind].Value = dbDataRecord[2].ToString().Split(' ')[1];
                    dgvDetail[4, ind].Value = dbDataRecord[3].ToString();
                    ind++;
                }
            }
        }

        private void show_my_orders()
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                string query = $"Select IDorder,total,date from Orderr WHERE user_ord={Login.purch_id} AND total>0";
                dataGridView1.Rows.Clear(); dataGridView1.Columns.Clear(); int fields = 6;
                dataGridView1.ColumnCount = fields;
                dataGridView1.Columns[0].HeaderText = "ID заказа"; dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].HeaderText = "Cумма заказа"; dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].HeaderText = "Дата заказа"; dataGridView1.Columns[2].Width = 70;
                NpgsqlCommand find = new NpgsqlCommand(query, con); int ind = 0;
                NpgsqlDataReader npgSqlDataReader = find.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    dataGridView1.RowCount++;
                    dataGridView1[0, ind].Value = dbDataRecord[0].ToString();
                    dataGridView1[1, ind].Value = dbDataRecord[1].ToString();
                    dataGridView1[2, ind].Value = dbDataRecord[2].ToString().Split(' ')[0];
                    ind++;
                }
            }
        }
        public ClientForm()
        {
            InitializeComponent();
            dataGridView1.CellClick += output;
        }

        private void MakeOrder_Click(object sender, EventArgs e)
        {
            chooseForm chooseForm = new chooseForm();
            chooseForm.Show();
        }

        private void myOrders_Click(object sender, EventArgs e)
        {
            show_my_orders();
        }
    }
}
