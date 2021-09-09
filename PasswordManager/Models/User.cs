using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
