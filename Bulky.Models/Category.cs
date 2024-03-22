﻿
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]     
        [MaxLength(30)]
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
