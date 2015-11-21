using System;
using System.Linq;
using System.Threading.Tasks;
using Cartisan.AutoMapper;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Cartisan.QuestionAnswer.Domain.Models;
using Cartisan.QuestionAnswer.Domain.Services;
using Cartisan.Repository;

namespace Cartisan.QuestionAnswer.Service {
    public class QuestionAnswerApplicationService: IQuestionAnswerApplicationService {
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly AcceptService _acceptService;

        public QuestionAnswerApplicationService(IRepository<Question> questionRepository, IRepository<Answer> answerRepository, AcceptService acceptService) {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _acceptService = acceptService;
        }

        public Task<Paginated<QuestionDto>> GetQuestions(int pageIndex, int pageSize, string sorting) {
            var questions = _questionRepository.Paginate(pageIndex, pageSize, q=>q.Id);
            return Task.FromResult(new Paginated<QuestionDto>(questions.Datas.Select(data => data.MapTo<QuestionDto>()),
                questions.PageIndex, questions.PageSize, questions.Total));
        }

        public Task<QuestionWithAnswersDto> GetQuestion(long questionId, bool incrementViewCount) {
            var question = _questionRepository.Get(questionId);
            if(incrementViewCount) {
                question.ViewCount++;
                _questionRepository.Save(question);
            }

            var questionWithAnswersDto = question.MapTo<QuestionWithAnswersDto>();
            questionWithAnswersDto.Answers =
                _answerRepository.Query(a => a.QuestionId == questionId).ToList().Select(a => a.MapTo<AnswerDto>()).ToList();
            return Task.FromResult(questionWithAnswersDto);
        }

        public async Task CreateQuestion(string title, string content, long questioner) {
            await _questionRepository.AddAsync(new Question(title, content, questioner));
        }

        public async Task<int> VoteUp(long questionId) {
            var question = _questionRepository.Get(questionId);
            question.VoteCount++;

            await _questionRepository.SaveAsync(question);

            return question.VoteCount;
        }

        public async Task<int> VoteDown(long questionId) {
            var question = _questionRepository.Get(questionId);
            question.VoteCount--;

            await _questionRepository.SaveAsync(question);

            return question.VoteCount;
        }

        public async Task<AnswerDto> SubmitAnswer(long questionId, string content, long answerer) {
            var question = _questionRepository.Get(questionId);
            question.AnswerCount++;

            await _questionRepository.SaveAsync(question);

            var answer = new Answer(questionId, content, answerer);
            await _answerRepository.AddAsync(answer);

            return answer.MapTo<AnswerDto>();
        }

        public async Task AcceptAnswer(long answerId) {
            var answer = _answerRepository.Get(answerId);
            await _acceptService.AcceptAnswer(answer);

        }
    }
}