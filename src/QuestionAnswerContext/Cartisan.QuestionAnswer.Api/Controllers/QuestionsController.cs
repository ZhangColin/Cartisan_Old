using System.Threading.Tasks;
using System.Web.Http;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Cartisan.QuestionAnswer.Service;

namespace Cartisan.QuestionAnswer.Api.Controllers {


    public class QuestionsController : ApiController {
        private readonly IQuestionAnswerApplicationService _questionAnswerService;

        public QuestionsController(IQuestionAnswerApplicationService questionAnswerService) {
            this._questionAnswerService = questionAnswerService;
        }

        public async Task<IHttpActionResult> GetQuestions(int pageIndex=1, int pageSize=10, string sorting="desc") {
            var paginated = await _questionAnswerService.GetQuestions(pageIndex, pageSize, sorting);
            return Ok(paginated);
        }

        public async Task<IHttpActionResult> GetQuestion(long questionId, bool incrementViewCount) {
            var questionWithAnswersDto = await _questionAnswerService.GetQuestion(questionId, incrementViewCount);
            return Ok(questionWithAnswersDto);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostQuestion(QuestionDto question) {
            await _questionAnswerService.CreateQuestion(question.Title, question.Content, question.Questioner);

            return Ok();
        }

        [HttpPut]
        [Route("Api/Questions/{questionId}/VoteUp")]
        public async Task<IHttpActionResult> VoteUp(long questionId) {
            var count = await _questionAnswerService.VoteUp(questionId);
            return Ok(count);
        }

        [HttpPut]
        [Route("Api/Questions/{questionId}/VoteDown")]
        public async Task<IHttpActionResult> VoteDown(long questionId) {
            var count = await _questionAnswerService.VoteDown(questionId);
            return Ok(count);
        }

        [HttpPost]
        [Route("Api/Questions/{questionId}/Answers")]
        public async Task<IHttpActionResult> SubmitAnswer(AnswerDto answer) {
            var submitAnswer = await _questionAnswerService.SubmitAnswer(answer.QuestionId, answer.Content, answer.Answerer);
            return Ok(submitAnswer);
        }

//        [HttpGet]
//        [Route("Api/Questions/{questionId}/Answers")]
//        public async Task<IHttpActionResult> Answers(long questionId) {
//            var count = await _questionAnswerService.VoteUp(questionId);
//            return Ok(count);
//        }

        [HttpPut]
        [Route("Api/Answers/{answerId}/AcceptAnswer")]
        public async Task<IHttpActionResult> AcceptAnswer(long answerId) {
            await _questionAnswerService.AcceptAnswer(answerId);
            return Ok();
        }
    }
}