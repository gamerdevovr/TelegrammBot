using System.Linq;

namespace TelegrammBot.Core.Users
{
    public class Subscriber
    {
        public int Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public decimal Saldo { get; private set; }
        public string Tariff { get; private set; }
        public decimal TariffMount { get; private set; }
        public bool Active { get; private set; }
        public string Coment { get; private set; }


        public Subscriber(int id, int code, string name, string address, string phone, decimal saldo, string tarrif, decimal tariffMount, bool active, string coment)
        {
            Id = id;
            Code = code;
            Name = name;
            Address = address;
            Phone = phone;
            Saldo = saldo;
            Tariff = tarrif;
            TariffMount = tariffMount;
            Active = active;
            Coment = coment;
        }

        public override string ToString()
        {
            string phonesString = string.Join(",", Phone);

            string returnStringOfAllProperties = $"ID: {Id}, Code: {Code}, Name: {Name}, Address: {Address}, Phones: {phonesString}, Saldo: {Saldo}, " +
                   $"TariffMount: {TariffMount}, Active: {Active}, Comment: {Coment}";

            return returnStringOfAllProperties;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Subscriber subscriber)
            {
                return Id == subscriber.Id &&
                        Code == subscriber.Code &&
                        Name == subscriber.Name &&
                        Address == subscriber.Address &&
                        Phone.SequenceEqual(subscriber.Phone) &&
                        Saldo == subscriber.Saldo &&
                        Tariff == subscriber.Tariff &&
                        TariffMount == subscriber.TariffMount &&
                        Active == subscriber.Active &&
                        Coment == subscriber.Coment;
            }
            else
            {
                return false;
            }
        }
       
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 0;
                hash += Id.GetHashCode();
                hash += Code.GetHashCode();
                hash += (Name != null ? Name.GetHashCode() : 0);
                hash += (Address != null ? Address.GetHashCode() : 0);
                hash += (Phone != null ? Phone.GetHashCode() : 0);
                hash += Saldo.GetHashCode();
                hash += Tariff.GetHashCode();
                hash += TariffMount.GetHashCode();
                hash += Active.GetHashCode();
                hash += (Coment != null ? Coment.GetHashCode() : 0);
                
                return hash;
            }
        }
    }
}
