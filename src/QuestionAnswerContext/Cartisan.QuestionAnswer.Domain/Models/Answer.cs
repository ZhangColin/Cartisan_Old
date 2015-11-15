using System;
using System.Collections.Generic;
using Cartisan.Domain;

namespace Cartisan.QuestionAnswer.Domain.Models {
    public class Answer: Entity<long>, IAggregateRoot {
        private Answer() {}

        public Answer(long questionId, string content, long answerer) {
            Content = content;
            Answerer = answerer;
            QuestionId = questionId;

            Created=DateTime.Now;
        }

        public virtual string Content { get; set; }
        public virtual long QuestionId { get; set; }
        public virtual bool IsAccepted { get; set; }
        
        public virtual long Answerer { get; set; }
        public virtual DateTime Created { get; set; }

        protected override IEnumerable<object> GetIdentityComponents() {
            throw new System.NotImplementedException();
        }
    }
}