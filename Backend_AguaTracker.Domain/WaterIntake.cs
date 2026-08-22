using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend_AguaTracker.Domain
{
    public class WaterIntake
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DailyResumenId { get; set; } = 0;

        [Required] 
        public double Amount { get; set; }

        // navigation property to the DailyResumen entity
        public DailyResume DailyResumen { get; set; }
    }
}
