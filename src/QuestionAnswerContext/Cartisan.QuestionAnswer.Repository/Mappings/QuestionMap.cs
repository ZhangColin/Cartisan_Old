using System.Data.Entity.ModelConfiguration;
using Cartisan.QuestionAnswer.Domain.Models;

namespace Cartisan.QuestionAnswer.Repository.Mappings {
    public class QuestionMap: EntityTypeConfiguration<Question> {
        public QuestionMap() {
            this.ToTable("QA_Questions").HasKey(q => q.Id);

            Property(q => q.Id);
            Property(q => q.Title).IsRequired();
            Property(q => q.Content).IsRequired();
            Property(q => q.VoteCount).IsRequired();
            Property(q => q.ViewCount).IsRequired();
            Property(q => q.AnswerCount).IsRequired();
            Property(q => q.Questioner).IsRequired();
            Property(q => q.Created);
        }
    }
}