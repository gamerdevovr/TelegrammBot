using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;
using TelegrammBot.Core.Users;

namespace TelegrammBot.Core.DataProviders.MySQL
{
    public class ConnectMySQL
    {
        private readonly string _server;
        private readonly string _username;
        private readonly string _password;
        private readonly string _database;

        public ConnectMySQL()
        {
            string configFilePath = "configMySQLConnection.txt";
            string[] configLines = File.ReadAllLines(configFilePath);

            _server = GetValueFromLine(configLines[0]);
            _username = GetValueFromLine(configLines[1]);
            _password = GetValueFromLine(configLines[2]);
            _database = GetValueFromLine(configLines[3]);
        }

        public List<Subscriber> GetTableSubscriber()
        {
            ConnectMySQL connector = new ConnectMySQL();
            using (MySqlConnection connection = connector.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM users";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                List<Subscriber> subscribers = new List<Subscriber>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    int code = (int)reader["code"];
                    string name = reader["name"].ToString();
                    string address = reader["address"].ToString();
                    // TODO: Зробити розбивку телефона з рядка на массив рядків
                    //string phone = reader["phone"].ToString();
                    string[] phone = new string[] { "+380671111111", "+380671111111" };
                    decimal saldo = Convert.ToDecimal((float)reader["saldo"]);
                    string tariff = reader["tariff"].ToString();
                    decimal tariffAmount = Convert.ToDecimal((float)reader["tariffmount"]);
                    bool active = (reader["active"].ToString() == "ВКЛ") ? true : false;
                    string comment = reader["coment"].ToString();

                    Subscriber subscriber = new Subscriber(id, code, name, address, phone, saldo, tariff, tariffAmount, active, comment);
                    subscribers.Add(subscriber);
                }

                reader.Close();

                return subscribers;
            }
        }

        private string GetValueFromLine(string line)
        {
            int separatorIndex = line.IndexOf(':');
            return line.Substring(separatorIndex + 1).Trim();
        }

        private MySqlConnection GetConnection()
        {
            string connectionString = $"Server={_server};Database={_database};Uid={_username};Pwd={_password};";
            return new MySqlConnection(connectionString);
        }
    }
}