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

            List<Subscriber> list = new List<Subscriber>();

            list.Add(subscriber);

            Console.WriteLine(subscriber);
            Console.WriteLine(list);

            Console.ReadKey();
        }
    }
}
