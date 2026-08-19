using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Domain
{
    public class DailyResume
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int TotalIntake { get; set; } = 0;

        public ActivityLevelEnum ActivityLevel { get; set; }

        public int IntakeGoal { get; set; } = 0;
    }
}
