using System.Linq;
using System.Threading.Tasks;
using Cartisan.QuestionAnswer.Domain.Models;
using Cartisan.Repository;

namespace Cartisan.QuestionAnswer.Domain.Services {
    public class AcceptService {
        private readonly IRepository<Answer> _answerRepository;
        public AcceptService(IRepository<Answer> answerRepository) {
            _answerRepository = answerRepository;
        }

        public async Task AcceptAnswer(Answer answer) {
            var questionId = answer.QuestionId;
            var previousAcceptedAnswer =
                (await _answerRepository.QueryAsync(a => a.QuestionId == questionId && a.IsAccepted)).FirstOrDefault();

            if(previousAcceptedAnswer != null) {
                previousAcceptedAnswer.IsAccepted = false;

                await _answerRepository.SaveAsync(previousAcceptedAnswer);
            }

            answer.IsAccepted = true;

            await _answerRepository.SaveAsync(answer);
        }
    }
}