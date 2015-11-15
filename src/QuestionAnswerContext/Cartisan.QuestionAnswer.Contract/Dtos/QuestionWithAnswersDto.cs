using System.Collections.Generic;

namespace Cartisan.QuestionAnswer.Contract.Dtos {
    public class QuestionWithAnswersDto: QuestionDto {
         public List<AnswerDto> Answers { get; set; } 
    }
}