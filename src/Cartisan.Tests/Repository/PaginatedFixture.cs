using System;
using System.Linq;
using Cartisan.Repository;
using Xunit;

namespace Cartisan.Tests.Repository {
    public class PaginatedFixture {
        [Fact]
        public void Throws_ArgumentNullException_If_source_Param_Is_Null() {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new Paginated<object>(null, 1, 1, 1));
        }

        [Fact]
        public void Sets_The_Property_Corretly() {
            // Arrange
            var source = Enumerable.Empty<object>();
            var pageIndex = 2;
            var pageSize = 20;
            var total = 50;

            // Act
            var paginated = new Paginated<object>(source, pageIndex, pageSize, total);

            // Assert
            Assert.Equal(2, paginated.PageIndex);
            Assert.Equal(20, paginated.PageSize);
            Assert.Equal(50, paginated.Total);
            Assert.Equal(3, paginated.PageTotal);

            Assert.True(paginated.HasPreviousPage);
            Assert.True(paginated.HasNextPage);

        }
    }
}