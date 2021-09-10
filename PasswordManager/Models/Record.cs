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
        [Required]
        public String RecordName { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        public virtual User User { get; set; }
    }

    public class RecordForm
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String RecordName { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }

}
