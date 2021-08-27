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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            for (int i = 1; i <= 12; i++)
            {
                cbMonth.Items.Add(i);
            }
            for(int y = 21; y <= 35; y++)
            {
                cbYear.Items.Add(y);
            }
        }

        //Проверка номера карты и CVV кода
        private bool check(string s, int len)
        {
            if (s.Length != len) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if (!Char.IsDigit(s, i))
                {
                    return false;
                }
            }
            return true;
        }

        private string getCard() => Num1.Text + Num2.Text + Num3.Text + Num4.Text;

        private void btDone_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = Login.getConnection())
            {
                con.Open();
                try
                {
                    if (tbPassword.Text.Equals("") || !tbEmail.Text.Contains("@"))
                        throw new Exception("Введите Email и пароль!)");
                    string user_query = $"INSERT into Userr (login, password, email) " +
                    $"VALUES ('{tbLogin.Text}','{Cod.EncodeString(tbPassword.Text)}','{tbEmail.Text}')";


                    string cardNumber = getCard();
                    if (!check(getCard(), 16)) throw new Exception("Введите номер карты");
                    if (!check(CVV.Text, 3)) throw new Exception("Введите CVV!");
                    string card_query = $"INSERT into Card (Number,date,CVV) " +
                    $"VALUES ('{Cod.EncodeString(cardNumber)}','{cbMonth.Text}/{cbYear.Text}','{Cod.EncodeString(CVV.Text)}')";
                    //Console.WriteLine(Cod.DecodeString(Cod.EncodeString(cardNumber)));

                    new NpgsqlCommand(user_query, con).ExecuteNonQuery();
                    Login.connectionReset(con);
                    //Сначала добавляем юзера, потом карту
                    new NpgsqlCommand(card_query, con).ExecuteNonQuery();
                    Login.connectionReset(con);

                    string add_card = $"UPDATE Userr SET user_card=last_card() WHERE login='{tbLogin.Text}'";
                    new NpgsqlCommand(add_card, con).ExecuteNonQuery();
                    MessageBox.Show($"Регистрация прошла успешно,{tbLogin.Text}!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
