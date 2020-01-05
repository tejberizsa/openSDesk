using System;

namespace openSDesk.API.Dtos
{
    public class SurveyForAddDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int ResponseId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}