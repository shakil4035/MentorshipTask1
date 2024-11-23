using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipTask1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }

}
