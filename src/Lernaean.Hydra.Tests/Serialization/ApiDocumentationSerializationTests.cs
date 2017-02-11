using System;
using System.Collections.Generic;
using Hydra.Core;
using Hydra.Discovery.SupportedOperations;
using JsonLD.Entities;
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
            dynamic jsonld = this.Serializer.Serialize(doc);

            // then
            Assert.Equal("http://example.api/prop", jsonld.supportedClass.supportedProperty["hydra:property"].range.ToString());
        }

        public class TestApiDocumentation : global::Hydra.Core.ApiDocumentation
        {
            public TestApiDocumentation()
                : base((IriRef)new Uri("http://example.test"))
            {
                this.Id = "http://documentation.uri/";
                this.SupportedClasses = this.GetSupportedClasses();
            }

            public IEnumerable<Class> GetSupportedClasses()
            {
                var @class = new Class(new Uri("http://example.test/class"));
                @class.SupportedOperations = new List<Operation>
                {
                    new Operation(HttpMethod.Post)
                    {
                        Returns = (IriRef)new Uri("http://example.api/ReturnType")
                    }
                };

                @class.SupportedProperties = new List<SupportedProperty>
                {
                    new SupportedProperty
                    {
                        Property = new Property { Range = (IriRef)new Uri("http://example.api/prop") }
                    }
                };

                yield return @class;
            }
        }
    }
}