using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Cartisan.Repository;
using Flurl.Http;
using Flurl.Util;
using ControllerBase = Cartisan.Web.Mvc.Controllers.ControllerBase;

namespace Cartisan.Website.Controllers {
    public class QuestionController: ControllerBase {
        public async Task<JsonResult> GetQuestions() {
            Paginated<QuestionDto> questionDtos = await "http://localhost:50217/api/questions".GetAsync().ReceiveJson<Paginated<QuestionDto>>();
//            IList<QuestionDto> questionDtos = await "http://localhost:50217/api/questions".GetAsync().ReceiveJson<IList<QuestionDto>>();

            return Json(questionDtos, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetQuestion(long questionId, bool incrementViewCount) {
            //            return await _questionAnswerService.GetQuestion(questionId, incrementViewCount);
            return Json(new {});
        }


        [HttpPost]
        public async Task<JsonResult> CreateQuestion(string title, string content, long questioner) {
            //            await _questionAnswerService.CreateQuestion(title, content, questioner);
            return Json(new {
            });
        }

        [HttpPost]
        public async Task<JsonResult> VoteUp(long questionId) {
            //            return await _questionAnswerService.VoteUp(questionId);
            return Json(new { });
        }

        [HttpPost]
        public async Task<JsonResult> VoteDown(long questionId) {
            //            return await VoteDown(questionId);
            return Json(new { });
        }

        [HttpPost]
        public async Task<JsonResult> SubmitAnswer(long questionId, string content, long answerer) {
            //            return await SubmitAnswer(questionId, content, answerer);
            return Json(new { });
        }

        [HttpPost]
        public async Task<JsonResult> AcceptAnswer(long answerId) {
            //            await _questionAnswerService.AcceptAnswer(answerId);
            return Json(new { });
        }
    }
}