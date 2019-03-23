using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyFinance.Util {
    //DAL = camada de acesso a dados
    public class DAL {
        private static string server = "localhost";
        private static string database = "financeiro";
        private static string user = "root";
        private static string password = "";
        private static string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};convert zero datetime=True";
        private MySqlConnection connection;

        public DAL() {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        // executa select
        public DataTable RetDataTable(string sql) {
            DataTable dataTable = new DataTable();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dataTable);

            return dataTable;
        }

        // executa INSERT, UPDATE E DELETE
        public void ExecutarComandoSQL(string sql) {
            MySqlCommand command = new MySqlCommand (sql,connection);
            command.ExecuteNonQuery();
        }

    }
}
