using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Flurl.Http;

namespace Cartisan.Website.Controllers {
    public class QuestionController: Controller {
        public async Task<JsonResult> GetQuestions() {
            //            return await _questionAnswerService.GetQuestions(1, 10, "");
            //            List<QuestionDto> questionDtos = await "http://localhost:50217/api/question".GetAsync().ReceiveJson<List<QuestionDto>>();
            List<QuestionDto> questionDtos = new List<QuestionDto>() {
                new QuestionDto() {
                    Id = 1,
                    Title = "处子问",
                    Content = "第一次提问，是否需要严肃点？",
                    AnswerCount = 10,
                    ViewCount = 23,
                    VoteCount = 5,
                    Questioner = 1
                },
                new QuestionDto() {
                    Id = 2,
                    Title = "天国在哪？",
                    Content = "神既道，道法自然，如来。",
                    AnswerCount = 8,
                    ViewCount = 5480,
                    VoteCount = 2315,
                    Questioner = 2
                }
            };
            return Json(new {items = questionDtos , totalCount=2} , JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetQuestion(long questionId, bool incrementViewCount) {
            //            return await _questionAnswerService.GetQuestion(questionId, incrementViewCount);
            return Json(new {});
        }

        public async Task<JsonResult> CreateQuestion(string title, string content, long questioner) {
            //            await _questionAnswerService.CreateQuestion(title, content, questioner);
            return Json(new { });
        }

        public async Task<JsonResult> VoteUp(long questionId) {
            //            return await _questionAnswerService.VoteUp(questionId);
            return Json(new { });
        }

        public async Task<JsonResult> VoteDown(long questionId) {
            //            return await VoteDown(questionId);
            return Json(new { });
        }

        public async Task<JsonResult> SubmitAnswer(long questionId, string content, long answerer) {
            //            return await SubmitAnswer(questionId, content, answerer);
            return Json(new { });
        }

        public async Task<JsonResult> AcceptAnswer(long answerId) {
            //            await _questionAnswerService.AcceptAnswer(answerId);
            return Json(new { });
        }
    }
}