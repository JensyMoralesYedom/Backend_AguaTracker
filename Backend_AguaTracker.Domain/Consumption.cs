using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend_AguaTracker.Domain
{
    public class Consumption
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required] 
        public double Amount { get; set; }
        
        // Navigation property
        public User User { get; set; }
    }
}
