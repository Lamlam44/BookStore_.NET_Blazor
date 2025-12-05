namespace StoreManagement.Client.Models
{
    public class ShiftHistoryEntry
    {
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
