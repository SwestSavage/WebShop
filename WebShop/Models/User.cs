﻿namespace WebShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
    }
}
