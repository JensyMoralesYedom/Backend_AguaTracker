namespace Backend_AguaTracker.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public decimal Weight { get; set; }

        public ActivityLevelEnum DefaultActivityLevel { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        // Navigation property
        public ICollection<DailyResume> DailyResumens { get; set; }


    }
}
