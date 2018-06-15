using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModel
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string RoleName { get; set; }
        public string fullname { get; set; }
        public string birthdate { get; set; }
        public string gender { get; set; }
        public string[] mealarr { get; set; }
        public string mealpreference { get; set; }

    }
}