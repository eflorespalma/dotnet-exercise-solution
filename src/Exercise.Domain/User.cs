using Exercise.Domain.Core;
using System;

namespace Exercise.Domain
{
    public class User : Entity
    {
        public User() { }

        public User(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
            this.RegistrationDate = DateTime.Now;
            this.Active = true;
        }
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
