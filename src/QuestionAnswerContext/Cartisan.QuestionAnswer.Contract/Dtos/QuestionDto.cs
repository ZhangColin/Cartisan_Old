namespace Cartisan.QuestionAnswer.Contract.Dtos {
    public class QuestionDto {
        public long Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; } 
        public int VoteCount { get; set; } 
        public int AnswerCount { get; set; } 
        public int ViewCount { get; set; }
        public long Questioner { get; set; }
    }
}