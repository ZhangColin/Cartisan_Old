using System.Threading.Tasks;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Cartisan.Repository;

namespace Cartisan.QuestionAnswer.Service {
    public interface IQuestionAnswerApplicationService {
        Paginated<QuestionDto> GetQuestions(int pageIndex, int pageSize, string sorting);
        QuestionWithAnswersDto GetQuestion(long questionId, bool incrementViewCount);

        Task CreateQuestion(string title, string content, long questioner);
        Task<int> VoteUp(long questionId);
        Task<int> VoteDown(long questionId);

        Task<AnswerDto> SubmitAnswer(long questionId, string content, long answerer);

        Task AcceptAnswer(long answerId);
    }
}