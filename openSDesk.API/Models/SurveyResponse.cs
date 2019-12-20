namespace openSDesk.API.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public bool Refusal { get; set; }
    }
}