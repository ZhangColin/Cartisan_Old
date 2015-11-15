using System;
using System.Collections.Generic;
using Cartisan.Domain;

namespace Cartisan.QuestionAnswer.Domain.Models {
    public class Question: Entity<long>, IAggregateRoot {
        private Question() {}

        public Question(string title, string content, long questioner) {
            Title = title;
            Content = content;
            Questioner = questioner;

            Created=DateTime.Now;
        }

        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual int VoteCount { get; set; }
        public virtual int AnswerCount { get; set; }
        public virtual int ViewCount { get; set; }

        public virtual long Questioner { get; set; }
        public virtual DateTime Created { get; set; }

        protected override IEnumerable<object> GetIdentityComponents() {
            throw new System.NotImplementedException();
        }
    }
}