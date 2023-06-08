using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TelegrammBot.Core.Subscriber
{
    class Subscriber
    {
        public uint Id { get; private set; }
        public uint Code { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string[] Phone { get; private set; }
        public decimal Saldo { get; private set; }
        public decimal Tariff { get; private set; }
        public decimal TariffAmount { get; private set; }
        public bool Active { get; private set; }
        public string Comment { get; private set; }


        public Subscriber(uint id, uint code, string name, string address, string[] phone, decimal saldo, decimal tarrif, decimal tariffAmount, bool active, string comment)
        {
            if (code.ToString().Length != 8)
            {
                throw new ArgumentException("Code must be an 8-digit number.");
            }

            for (int i = 0; i < phone.Length; i++)
            {
                string formattedPhone = $"{phone[i]}".Replace("-", "").Replace(" ", "");
                if (!IsValidPhoneNumber(formattedPhone))
                {
                    throw new ArgumentException("Invalid phone number format.");
                }
            }

            Id = id;
            Code = code;
            Name = name;
            Address = address;
            Phone = phone;
            Saldo = saldo;
            Tariff = tarrif;
            TariffAmount = tariffAmount;
            Active = active;
            Comment = comment;
        }

        public override string ToString()
        {
            string phonesString = string.Join(",", Phone);

            string returnStringOfAllProperties = $"ID: {Id}, Code: {Code}, Name: {Name}, Address: {Address}, Phones: {phonesString}, Saldo: {Saldo}," +
                   $"TariffAmount: {TariffAmount}, Active: {Active}, Comment: {Comment}";

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
                        TariffAmount == subscriber.TariffAmount &&
                        Active == subscriber.Active &&
                        Comment == subscriber.Comment;
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
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + Code.GetHashCode();
                hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
                hash = hash * 23 + (Address != null ? Address.GetHashCode() : 0);
                hash = hash * 23 + (Phone != null ? Phone.GetHashCode() : 0);
                hash = hash * 23 + Saldo.GetHashCode();
                hash = hash * 23 + Tariff.GetHashCode();
                hash = hash * 23 + TariffAmount.GetHashCode();
                hash = hash * 23 + Active.GetHashCode();
                hash = hash * 23 + (Comment != null ? Comment.GetHashCode() : 0);
                return hash;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+38\d{10}$";
            bool IsValidate = Regex.IsMatch(phoneNumber, pattern);
            
            return IsValidate;
        }
    }
}
