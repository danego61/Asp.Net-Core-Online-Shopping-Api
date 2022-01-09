using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineShopping.WebApi.Models
{
    public class UserModel
    {

        public int UserID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsPasswordChange { get; set; }

        public string NameSurname { get => $"{Name} {Surname}"; }

    }
}
