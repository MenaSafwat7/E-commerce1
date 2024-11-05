﻿namespace Domain.Entities.OrderEntities
{
    public class ShippingAddress
    {
        public ShippingAddress()
        {
        }
        public ShippingAddress(string firstName, string lastName, string street, string city, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            this.city = city;
            Country = country;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string city { get; set; }
        public string Country { get; set; }
    }
}