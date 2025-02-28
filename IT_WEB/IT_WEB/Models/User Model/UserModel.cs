using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_WEB.Models.User_Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Office { get; set; }
    }
}