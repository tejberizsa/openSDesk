using System;

namespace openSDesk.API.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int ResponseId { get; set; }
        public SurveyResponse Response { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? NotificationSentAt { get; set; }
    }
}