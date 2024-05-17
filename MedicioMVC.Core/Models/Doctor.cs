using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicioMVC.Core.Models
{
    public class Doctor : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [StringLength(50)]
        public string Surname { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Position { get; set; } = null!;
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
