namespace DataAccessLayer.Model
{
    public class MonthlyCount
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
        public int TotalUnits { get; set; }
    }
    public class DailyCount
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }       
        public decimal Total { get; set; }    
        public int TotalUnits { get; set; }   
    }

    public class TimeRange
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public static TimeRange Last7Days() => new TimeRange
        {
            From = DateTime.UtcNow.Date.AddDays(-7),
            To = DateTime.UtcNow.Date
        };

        public static TimeRange Last30Days() => new TimeRange
        {
            From = DateTime.UtcNow.Date.AddDays(-30),
            To = DateTime.UtcNow.Date
        };

        public static TimeRange LastYear() => new TimeRange
        {
            From = DateTime.UtcNow.Date.AddYears(-1),
            To = DateTime.UtcNow.Date
        };
    }

}