using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MVCProject2.Data.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }

        [Display(Name = "Enter your name")]
        public string Name { get; set; }

        [Display(Name = "Enter your surname")]
        public string Surname { get; set; }

        [Display(Name = "Address")]

        public string Address { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)] 
        public DateTime OrderTime { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
