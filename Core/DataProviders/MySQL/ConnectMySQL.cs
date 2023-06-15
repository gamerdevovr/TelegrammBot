using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;
using TelegrammBot.Core.Users;
using static System.Net.Mime.MediaTypeNames;

namespace TelegrammBot.Core.DataProviders.MySQL
{
    public class ConnectMySQL
    {
        public TimeSpan TimeConnect { get; private set; }
        public TimeSpan TimeGetBase { get; private set; }

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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //ConnectMySQL connector = new ConnectMySQL();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();

                stopwatch.Stop();
                TimeConnect = stopwatch.Elapsed;
                stopwatch.Start();
                
                string query = "SELECT * FROM users";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                stopwatch.Stop();
                TimeGetBase = stopwatch.Elapsed;

                List<Subscriber> subscribers = new List<Subscriber>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    int code = (int)reader["code"];
                    string name = reader["name"].ToString();
                    string address = reader["address"].ToString();
                    // TODO: Зробити розбивку телефона з рядка на массив рядків
                    string phoneFromBd = reader["phone"].ToString();
                    string phone = SelectPhones(phoneFromBd);
                    decimal saldo = Convert.ToDecimal((float)reader["saldo"]);
                    string tariff = reader["tariff"].ToString();
                    decimal tariffAmount = Convert.ToDecimal((float)reader["tariffmount"]);
                    bool active = (reader["active"].ToString() == "ВКЛ") ? true : false;
                    string comment = reader["coment"].ToString();

                    Subscriber subscriber = new Subscriber(id, code, name, address, phone, saldo, tariff, tariffAmount, active, comment);
                    subscribers.Add(subscriber);
                }

                reader.Close();
                connection.Close();

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

        private string SelectPhones(string phoneFromBd)
        {
            if (phoneFromBd != "")
            {
               
                if (int.TryParse(Convert.ToString(phoneFromBd[0]), out int i)) // перевіряє чи перший символ рядка є числом
                {
                    return ToClearNumber(phoneFromBd);
                }
                else if (phoneFromBd[0] == '+')
                {
                    return ToClearNumber(phoneFromBd);
                }
                else
                {
                    return "Відсутній";
                }

            }
            else {return "Відсутній"; }
            
        }

        private string ToClearNumber(string phone)
        {
            for (int i = 0; i < phone.Length; i++) // цикл видаляє всі не числа з рядка окрім ' ', '.', ';', ','
            {
                if (int.TryParse(Convert.ToString(phone[i]), out int x) != true)
                {
                    if (phone[i] != ' ' && phone[i] != ',' && phone[i] != ';' && phone[i] != '.')
                    {
                        phone = phone.Remove(i, 1);
                        i--;
                    }
                }

            }
            phone = phone.Replace(" ", "s"); //заміна симовлів що залишилися на 'S', а потім на усі 'S' на '; '
            phone = phone.Replace(",", "s");
            phone = phone.Replace(";", "s");
            phone = phone.Replace(".", "s");

            phone = phone.Replace("ssss", "s");
            phone = phone.Replace("sss", "s");
            phone = phone.Replace("ss", "s");
            phone = phone.Replace("s", "; ");


            return phone; 
        }
    }
}