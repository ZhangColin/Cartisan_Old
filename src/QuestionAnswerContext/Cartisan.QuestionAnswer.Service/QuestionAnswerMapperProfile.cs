using AutoMapper;
using Cartisan.QuestionAnswer.Contract.Dtos;
using Cartisan.QuestionAnswer.Domain.Models;

namespace Cartisan.QuestionAnswer.Service {
    public class QuestionAnswerMapperProfile: Profile {
        protected override void Configure() {
            Mapper.CreateMap<Question, QuestionDto>();
            Mapper.CreateMap<Answer, AnswerDto>();
        }
    }
}