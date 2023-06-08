using System;
using System.Collections.Generic;
using TelegrammBot.Core.Subscriber;

namespace TelegrammBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Subscriber subscriber = new Subscriber(0, 11111111, "ПІБ", "Адреса", new string[1] { "+380671111111" }, 100.00M, 100.00M, 100.00M, true, "Коментарій");
            Subscriber subscriber1 = new Subscriber(1, 11111111, "ПІБ", "Адреса", new string[1] { "+380671111111" }, 100.00M, 100.00M, 100.00M, true, "Коментарій");
            Subscriber subscriber2 = new Subscriber(2, 11111111, "ПІБ", "Адреса", new string[1] { "+380671111111" }, 100.00M, 100.00M, 100.00M, true, "Коментарій");
            Subscriber subscriber3 = new Subscriber(3, 11111111, "ПІБ", "Адреса", new string[1] { "+380671111111" }, 100.00M, 100.00M, 100.00M, true, "Коментарій");

            List<Subscriber> list = new List<Subscriber>();

            list.Add(subscriber);
            list.Add(subscriber1);
            list.Add(subscriber2);
            list.Add(subscriber3);

            foreach (Subscriber sub in list)
            {
                Console.WriteLine(sub);
            }

            Console.ReadKey();
        }
    }
}
