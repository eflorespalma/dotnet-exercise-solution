using System;

namespace Exercise.Domain.Core
{
    public abstract class Entity
    {
        public string RegistrationUser { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ModificationUser { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool Active { get; set; }
    }
}
