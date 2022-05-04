using System.ComponentModel.DataAnnotations;

namespace MVCProject2.Data.Models
{
    public class RoleUser
    {
        
        public int roleID { get; set; }
        [Key]
        public int userID { get; set; }
    }
}
