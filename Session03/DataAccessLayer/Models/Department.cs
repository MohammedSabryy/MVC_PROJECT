using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session03.DataAccessLayer.Models
{
    public class Department
    {
        [Range(0, 500)]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]
        public string Name { get; set; }
        [Display(Name = "Created At")]
        public DateTime Date { get; set; }
    }
}
