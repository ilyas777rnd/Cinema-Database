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
using System.Diagnostics;
using Npgsql;

namespace Cinema
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            cbTable.Items.AddRange(new string[] { "Сотрудники", "Актеры/Жарны",
            "Фильмы/Залы/Сеансы"});

        }

        private void btTable_Click(object sender, EventArgs e)
        {
            switch (cbTable.Text) 
            {
                case "Сотрудники":
                    tableEmployee employee = new tableEmployee();
                    employee.Show();
                    break;

                case "Актеры/Жарны":
                    ActorsAndGenre actorsAndGenre = new ActorsAndGenre();
                    actorsAndGenre.Show();
                    break;

                case "Фильмы/Залы/Сеансы":
                    FilmsHollsSessions filmsHollsSessions = new FilmsHollsSessions();
                    filmsHollsSessions.Show();
                    break;

                default:
                    break;
            }
        }

        private void createCopy_Click(object sender, EventArgs e)
        {
            //дата для имени файла
            DateTime date = new DateTime();
            string d = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString()
                + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            //cmd с указанием пути до pg_dump с параметром
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"C:\Program Files\PostgreSQL\12\bin\";
            //C:\Program Files\PostgreSQL\12\bin\pg_dump.exe --file "C:\\Users\\sulin\\Desktop\\1.sql" --host "localhost" --port "7777" --username "postgres" --no-password --verbose --format=c --blobs --encoding "UTF8" "Autodiller 1.0"
            p.StartInfo.Arguments = "/k pg_dump.exe --file \"C:\\Users\\sulin\\Desktop\\" + d + ".sql\" --host \"localhost\" --port \"7777\" --username \"postgres\" --verbose --format=c --blobs \"ulmart\"";
            p.Start();
            MessageBox.Show("Успешно!");
            p.Close();
        }

        private void loadCopy_Click(object sender, EventArgs e)
        {
            //return;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "SQL file (*.sql)|*.sql|All files(*.*)|*.*";
            string pathdb = "";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            pathdb = openFileDialog1.FileName;

            //cmd с указанием пути до pg_dump с параметром
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"C:\Program Files\PostgreSQL\12\bin\";
            //C:\Program Files\PostgreSQL\12\bin\pg_dump.exe --file "C:\\Users\\sulin\\DOCUME~1\\aaa" --host "localhost" --port "7777" --username "postgres" --no-password --verbose --format=c --blobs "ulmart"
            p.StartInfo.Arguments = "/k pg_restore.exe --host \"localhost\" --port \"7777\" --username \"postgres\" --role \"postgres\" --dbname \"ulmart1\" --verbose \"" + pathdb + "\"";
            p.Start();
            MessageBox.Show("Успешно!");
            p.Close();
        }

        private void Backup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
        }
    }
}
