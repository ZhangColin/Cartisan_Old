namespace Cartisan.QuestionAnswer.Contract.Dtos {
    public class AnswerDto {
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public string Content { get; set; }
        public bool IsAccepted { get; set; }
        public long Answerer { get; set; }
    }
}