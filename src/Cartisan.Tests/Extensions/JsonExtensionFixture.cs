using System;
using Cartisan.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Cartisan.Tests.Extensions {
    public class JsonExtensionFixture {
        private readonly ITestOutputHelper _output;

        public JsonExtensionFixture(ITestOutputHelper output) {
            this._output = output;
        }

        public class Parent {
            private string PrivateField = "This is private field.";
            public string PublicField = "This is public field.";

            private string PrivateProperty { get { return "This is private Property."; } }
            public string PublicProperty { get { return "This is private Property."; } }

            public Sub PublicSub { get { return new Sub(); } }
            private Sub PrivateSub { get { return new Sub(); } }
        }

        public class Sub {
            public string PublicName { get { return "Public Name"; } }
            private string PrivateName { get { return "Private Name"; } }
        }

        [Fact]
        public void NonPublicSerialize() {
            Parent p = new Parent();
            _output.WriteLine(p.ToJson(true, false, true));
        }

        [Fact]
        public void NonPublicCamelCasePropertyNameSerialize() {
            Parent p = new Parent();
            _output.WriteLine(p.ToJson(true, true, true));
        }

        [Fact]
        public void PublicSerialize() {
            Parent p = new Parent();
            _output.WriteLine(p.ToJson(false, false, true));
        }

        [Fact]
        public void PublicCamelCasePropertyNameSerialize() {
            Parent p = new Parent();
            _output.WriteLine(p.ToJson(false, true, true));
        }

        [Fact]
        public void NormalUsing() {
            Parent p = new Parent();
            _output.WriteLine(p.ToJson());
        }
    }
}