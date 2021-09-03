using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Models
{

    public class Record
    {
        [Key]
        public int Id { get; set; }
        public String RecordName { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
