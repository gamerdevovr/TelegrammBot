using System;
using System.Collections.Generic;
using System.Text;
using TelegrammBot.Core.DataProviders.MySQL;
using TelegrammBot.Core.Users;


namespace TelegrammBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8; //украънська мова для консолі

            ConnectMySQL connectMySQL = new ConnectMySQL();
            List<Subscriber> myList = connectMySQL.GetTableSubscriber();

            Console.WriteLine(connectMySQL.TimeConnect);
            Console.WriteLine(connectMySQL.TimeGetBase);

            foreach (Subscriber subscriber in myList)
            {
                Console.WriteLine(subscriber.ToString());
            }

            Console.ReadKey();

            
        }
    }
}
