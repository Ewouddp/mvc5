using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LoginCount { get; set; }
        public string RoleName { get; set; }
    }
}