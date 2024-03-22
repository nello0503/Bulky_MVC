using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        [Required]
       
        public string Description { get; set; }
        [Required]
        [DisplayName("Durata")]
        public int Hour { get; set; }
        [Required] 
        public string Name { get;}
        [Required]       
        public string Price { get; set; }  
        [Required]     
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        
    } 
}
