using System.Threading.Tasks;
using System.Web.Mvc;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Cartisan.Repository;
using Flurl;
using Flurl.Http;
using ControllerBase = Cartisan.Web.Mvc.Controllers.ControllerBase;

namespace Cartisan.Website.Controllers {
    public class QuestionController: ControllerBase {
        public async Task<JsonResult> GetQuestions() {
            Paginated<QuestionDto> questionDtos = await "http://localhost:50217/api/questions"
                .GetJsonAsync<Paginated<QuestionDto>>();

            return Json(questionDtos, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetQuestion(long questionId, bool incrementViewCount) {
            QuestionWithAnswersDto question = await $"http://localhost:50217/api/questions/{questionId}"
                .SetQueryParams(new { incrementViewCount })
                .GetJsonAsync<QuestionWithAnswersDto>();
            
            return Json(question, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> CreateQuestion(string title, string content) {
            await "http://localhost:50217/api/questions".PostJsonAsync(new {
                title, content, questioner=1
            });
            return Json(new {
                success = true
            });
        }

        [HttpPost]
        public async Task<JsonResult> VoteUp(long questionId) {
            var voteCount = await $"http://localhost:50217/api/questions/{questionId}/voteup".PutAsync().ReceiveJson<int>();
            return Json(new { voteCount });
        }

        [HttpPost]
        public async Task<JsonResult> VoteDown(long questionId) {
            var voteCount = await $"http://localhost:50217/api/questions/{questionId}/votedown".PutAsync().ReceiveJson<int>();
            return Json(new { voteCount });
        }

        [HttpPost]
        public async Task<JsonResult> SubmitAnswer(long questionId, string content) {
            var answerDto = await $"http://localhost:50217/api/questions/{questionId}/answers".PostJsonAsync(new {
                content,
                answerer = 1
            }).ReceiveJson<AnswerDto>();

            
            return Json(answerDto);
        }

        [HttpPost]
        public async Task<JsonResult> AcceptAnswer(long answerId) {
            await $"http://localhost:50217/api/answers/{answerId}/acceptanswer".PutAsync();
            return Json(new { });
        }
    }
}