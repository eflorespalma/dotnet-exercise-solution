using System;
using Exercise.Domain.Core;

namespace Exercise.Domain
{
    public class Product : Entity
    {
        public Product() { }

        public Product(string Name, string Description, decimal Price, int Quantity, string RegisterUserName)
        {
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Quantity = Quantity;
            this.RegistrationUser = RegisterUserName;
            this.RegistrationDate = DateTime.Now;
            this.Active = true;
        }

        public Product(int Id, string Name, string Description, decimal Price, int Quantity, string ModificationUser, bool Active)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Quantity = Quantity;
            this.ModificationUser = ModificationUser;
            this.ModificationDate = DateTime.Now;
            this.Active = Active;
        }

        public Product(int Id, string ModificationUser)
        {
            this.Id = Id;
            this.ModificationUser = ModificationUser;
            this.ModificationDate = DateTime.Now;
            this.Active = false;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
