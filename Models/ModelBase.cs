using System;
using System.ComponentModel.DataAnnotations;

namespace CMP332.Models
{
    public class ModelBase 
    {
        [Key]
        public int Id { get; set; }
        
    }
}