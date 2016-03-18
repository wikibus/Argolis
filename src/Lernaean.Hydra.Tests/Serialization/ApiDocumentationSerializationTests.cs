using System;
using System.Collections.Generic;
using Hydra.Core;
using Xunit;

namespace Lernaean.Hydra.Tests.Serialization
{
    public class ApiDocumentationSerializationTests : SerializationTestsBase
    {
        [Fact]
        public void Should_serialize_property_with_correct_key_name()
        {
            // given
            var doc = new TestApiDocumentation();

            // when
            dynamic jsonld = Serializer.Serialize(doc);

            // then
            Assert.Equal("http://example.api/prop", jsonld.supportedClass.supportedProperty["hydra:property"].range.ToString());
        }

        public class TestApiDocumentation : global::Hydra.Core.ApiDocumentation
        {
            public TestApiDocumentation() : base(new Uri("http://example.test"))
            {
                Id = "http://documentation.uri/";
                SupportedClasses = GetSupportedClasses();
            }

            public IEnumerable<Class> GetSupportedClasses()
            {
                var @class = new Class(new Uri("http://example.test/class"));
                @class.SupportedOperations = new List<Operation>
                {
                    new Operation("POST")
                    {
                        Returns = new Uri("http://example.api/ReturnType")
                    }
                };

                @class.SupportedProperties = new List<SupportedProperty>
                {
                    new SupportedProperty
                    {
                        Property = new Property { Range = new Uri("http://example.api/prop") }
                    }
                };

                yield return @class;
            }
        }
    }
}