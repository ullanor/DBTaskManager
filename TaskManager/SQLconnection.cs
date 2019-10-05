using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager
{
    public class SQLconnection
    {
        public System.Data.SqlClient.SqlConnection connectionStream;
        public ObservableCollection<string> DataBaseSays { get; private set; } = new ObservableCollection<string>();
        public bool IsConnectionOpen { get; set; }

        public void CloseSQLConnection()
        {
            connectionStream.Close();
            IsConnectionOpen = false;
        }

        public void AddTaskToDataBase(string taskPriority)
        {
            SqlCommand komendaSQL = connectionStream.CreateCommand();
            komendaSQL.CommandText = "insert into osoba(imie_osoba,nazwisko_osoba) values ('test','"+taskPriority+"')";
            SqlDataReader thisReader = komendaSQL.ExecuteReader();

            thisReader.Close();
        }

        public void DeleteTaskFromDataBase()
        {
            // 1. declare command object with parameter
            SqlCommand cmd = new SqlCommand(
                "select * from osoba where imie_osoba = @Imie", connectionStream);
            // 2. define parameters used in command object
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Imie";
            param.Value = "8";
            // 3. add new parameter to command object
            cmd.Parameters.Add(param);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DataBaseSays.Add(reader["ID_osoba"] + " " + reader["imie_osoba"].ToString() + reader["nazwisko_osoba"]);
            }
        }

        public void ImportTasksFromDataBase()
        {
            SqlCommand komendaSQL = connectionStream.CreateCommand();
            komendaSQL.CommandText = "SELECT * FROM osoba"; //"SELECT * FROM osoba WHERE imie_osoba='pablo'"
            SqlDataReader thisReader = komendaSQL.ExecuteReader();

            while (thisReader.Read())
            {
                DataBaseSays.Add(thisReader["ID_osoba"].ToString() + " " + thisReader["imie_osoba"].ToString() + thisReader["nazwisko_osoba"].ToString());
            }

            thisReader.Close();
        }


        //setting connection
        public SQLconnection()
        {
            try
            {
                //SqlConnection polaczenie = new SqlConnection(@"Data source=P-KOMPUTER;Initial Catalog=testowaBazaPN;User ID=sa;Password=gothic2001;Integrated Security=false");
                SqlConnection polaczenie = new SqlConnection("Server=(local);DataBase=testowaBazaPN;Integrated Security=SSPI");
                polaczenie.Open();

                connectionStream = polaczenie;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (connectionStream.State == System.Data.ConnectionState.Open)
                    IsConnectionOpen = true;
            }
        }
    }
}
