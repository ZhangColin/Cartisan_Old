using System.Threading.Tasks;
using System.Web.Http;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Cartisan.QuestionAnswer.Service;
using Cartisan.Repository;

namespace Cartisan.QuestionAnswer.Api.Controllers {
    public class QuestionsController: ApiController {
        private readonly IQuestionAnswerApplicationService _questionAnswerService;

        public QuestionsController(IQuestionAnswerApplicationService questionAnswerService) {
            this._questionAnswerService = questionAnswerService;
        }

//        public async Task<Paginated<QuestionDto>> GetQuestions(int pageIndex, int pageSize, string sorting) {
//            return await _questionAnswerService.GetQuestions(pageIndex, pageSize, sorting);
//        }

        public async Task<Paginated<QuestionDto>> GetQuestions() {
            return await _questionAnswerService.GetQuestions(1, 10, "");
        }

        public async Task<QuestionWithAnswersDto> GetQuestion(long questionId, bool incrementViewCount) {
            return await _questionAnswerService.GetQuestion(questionId, incrementViewCount);
        }

        public async Task CreateQuestion(string title, string content, long questioner) {
            await _questionAnswerService.CreateQuestion(title, content, questioner);
        }

        public async Task<int> VoteUp(long questionId) {
            return await _questionAnswerService.VoteUp(questionId);
        }

        public async Task<int> VoteDown(long questionId) {
            return await VoteDown(questionId);
        }

        public async Task<AnswerDto> SubmitAnswer(long questionId, string content, long answerer) {
            return await SubmitAnswer(questionId, content, answerer);
        }

        public async Task AcceptAnswer(long answerId) {
            await _questionAnswerService.AcceptAnswer(answerId);
        }
    }
}