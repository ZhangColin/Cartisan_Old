using Cartisan.Extensions;
using Xunit;

namespace Cartisan.Tests.Extensions {
    public class StringExtensionFixture {
        [Fact]
        public void RepeatTest() {
            Assert.Equal("strstrstr", "str".Repeat(3));
            Assert.Equal("str,str,str", "str".Repeat(",", 3));
        }

        [Fact]
        public void ToCamelCaseTest() {
            Assert.Equal("urlValue", "URLValue".ToCamelCase());
            Assert.Equal("url", "URL".ToCamelCase());
            Assert.Equal("id", "ID".ToCamelCase());
            Assert.Equal("i", "I".ToCamelCase());
            Assert.Equal("", "".ToCamelCase());
            string nullStr = null;
            Assert.Equal(null, nullStr.ToCamelCase());
            Assert.Equal("iPhone", "iPhone".ToCamelCase());
            Assert.Equal("person", "Person".ToCamelCase());
            Assert.Equal("iPhone", "IPhone".ToCamelCase());
            Assert.Equal("i Phone", "I Phone".ToCamelCase());
            Assert.Equal(" IPhone", " IPhone".ToCamelCase());
        }
    }
}