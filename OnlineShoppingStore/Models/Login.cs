using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        [DataType("Password")]
        public string Password { get; set; }
    }
}