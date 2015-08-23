//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Cartisan.EntityFramework.Tests.Infrastructure;
//using Moq;
//using Xunit;
//
//namespace Cartisan.EntityFramework.Tests {
//    public class EntityFrameworkRepositoryFixture {
//        [Fact]
//        public void GetAll_Should_Call_Set_Once_And_Get_Expected_Amount_Of_Entities() {
//            // Arrange
//            var people = GetDummyPeople(2);
//            var personDbSet = GetPersonDbSet(people);
//            var peopleContextMock = new Mock<PeopleContext>();
//            peopleContextMock.Setup(pc => pc.Set<Person>()).Returns(personDbSet);
//            var entityRepository =new EntityFrameworkRepository<Person>(peopleContextMock.Object);
//
//            // Act
//            IEnumerable<Person> peopleResult = entityRepository.All().ToList();
//
//            // Assert
//            Assert.Equal(personDbSet.Count(), peopleResult.Count());
//        }
//
//        private FakeDbSet<Person> GetPersonDbSet(IEnumerable<Person> people) {
//            var personDbSet = new FakeDbSet<Person>();
//            foreach(var person in people) {
//                personDbSet.Add(person);
//            }
//
//            return personDbSet;
//        }
//
//        private IEnumerable<Person> GetDummyPeople(int count) {
//            for(int i = 0; i < count; i++) {
//                yield return new Person() {
//                    Id = i,
//                    Name = string.Concat("Foo", i),
//                    Surname = string.Concat("Bar", i),
//                    Age = 5 * i,
//                    CreatedOn = DateTime.Parse("2012-10-23 18:48")
//                };
//            }
//        }  
//    }
//}