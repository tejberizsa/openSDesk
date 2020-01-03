using System;

namespace openSDesk.API.Dtos
{
    public class SurveyForDetailedDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int ResponseId { get; set; }
        public string Response { get; set; }
        public bool Refusal { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}