using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc4Crud.Entities
{
    public class EmployeeMaster
    {
        [Key]
        public string ID { get; set; }
        [Required(ErrorMessage = "Please Enter Employee Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Salary")]
        public decimal Salary { get; set; }  
    }
}