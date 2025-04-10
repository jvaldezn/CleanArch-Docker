using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Enums;

namespace Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRole Role { get; set; }
    }
}
