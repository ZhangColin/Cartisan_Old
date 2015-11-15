using System.Data.Entity.ModelConfiguration;
using Cartisan.QuestionAnswer.Domain.Models;

namespace Cartisan.QuestionAnswer.Repository.Mappings {
    public class AnswerMap: EntityTypeConfiguration<Answer> {
        public AnswerMap() {
            this.ToTable("QA_Answers").HasKey(a => a.Id);

            Property(a => a.Id);
            Property(a => a.Content).IsRequired();
            Property(a => a.IsAccepted).IsRequired();
            Property(a => a.Answerer).IsRequired();
            Property(a => a.Created);
        }
    }
}