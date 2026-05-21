using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerdeCim.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string? userName { get; set; }
        public string? userPass { get; set; }
        public string? userRole { get; set; }
    }
}