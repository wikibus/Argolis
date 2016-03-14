using System;
using System.Collections.Generic;
using Hydra.Core;
using Newtonsoft.Json.Linq;
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
            Assert.Equal("http://example.api/prop", jsonld.supportedClass[0].supportedProperty[0].property);
        }

        public class TestApiDocumentation : ApiDocumentation
        {
            public TestApiDocumentation() : base(new Uri("http://example.test"))
            {
                Id = "http://documentation.uri/";
            }

            protected override JToken GetLocalContext()
            {
                return new JObject();
            }

            public override IEnumerable<Class> SupportedClasses
            {
                get
                {
                    var @class = new Class("http://example.test/class");
                    @class.SupportedOperations = new List<Operation>
                    {
                        new Operation("POST")
                        {
                            Returns = "http://example.api/ReturnType"
                        }
                    };

                    @class.SupportedProperties = new List<Property>
                    {
                        new Property
                        {
                            Predicate = "http://example.api/prop"
                        }
                    };

                    yield return @class;
                }
            }
        }
    }
}