using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Argolis.Hydra;
using Argolis.Hydra.Annotations;
using Argolis.Hydra.Core;
using Argolis.Hydra.Discovery.SupportedProperties;
using Argolis.Models;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using Vocab;
using Xunit;

namespace Argolis.Tests
{
    public class IriTemplateFactoryTests
    {
        private readonly IriTemplateFactory factory;
        private readonly IPropertyRangeRetrievalPolicy propertyRangeRetrievalPolicy;
        private string path;

        public IriTemplateFactoryTests()
        {
            this.propertyRangeRetrievalPolicy = A.Fake<IPropertyRangeRetrievalPolicy>();
            var modelTemplateProvider = A.Fake<IModelTemplateProvider>();

            A.CallTo(() => modelTemplateProvider.GetAbsoluteTemplate(A<Type>._)).ReturnsLazily(() => this.path);

            this.factory = new IriTemplateFactory(this.propertyRangeRetrievalPolicy, modelTemplateProvider);
        }

        [Fact]
        public void GivenManyProperties_ShouldCreateIriTemplateMappingForAllOfThem()
        {
            // given
            this.path = "/people";

            // when
            IriTemplate template = this.factory.CreateIriTemplate<ManyProperties, object>();

            // then
            template.Mappings.Should().HaveCount(8);
        }

        [Fact]
        public void GivenPropertyWithoutAttribute_IriTemplateMappingShouldNotBeRequired()
        {
            // given
            this.path = "/people";

            // when
            var template = this.factory.CreateIriTemplate<SingleNonRequiredProperty, object>();

            // then
            var iriTemplateMapping = template.Mappings.Single();
            iriTemplateMapping.Required.Should().BeFalse();
        }

        [Fact]
        public void GivenPropertyWithRequiredAttribute_IriTemplateMappingShouldBeRequired()
        {
            // given
            this.path = "/people";

            // when
            var template = this.factory.CreateIriTemplate<SingleRequiredProperty, object>();

            // then
            var iriTemplateMapping = template.Mappings.Single();
            iriTemplateMapping.Required.Should().BeTrue();
        }

        [Fact]
        public void GivenPropertyWithUnmappedRange_ShouldLeavePropertyNull()
        {
            // given
            this.path = "/people";

            // when
            var template = this.factory.CreateIriTemplate<SingleNonRequiredProperty, object>();

            // then
            var iriTemplateMapping = template.Mappings.Single();
            iriTemplateMapping.Property.Should().BeNull();
        }

        [Fact]
        public void GivenPropertyWithMappedRange_ShouldCreateProperty()
        {
            // given
            this.path = "/people";

            // given
            var prop = typeof(SingleNonRequiredProperty).GetProperty("Prop");
            var propertyRange = (IriRef)Xsd.NCName;
            A.CallTo(() => this.propertyRangeRetrievalPolicy.GetRange(prop, A<IReadOnlyDictionary<Type, Uri>>._))
                .Returns(propertyRange);

            // when
            var template = this.factory.CreateIriTemplate<SingleNonRequiredProperty, object>();

            // then
            var iriTemplateMapping = template.Mappings.Single();
            iriTemplateMapping.Property.Should().NotBeNull();
            iriTemplateMapping.Property.Range.Should().Be(propertyRange);
        }

        [Fact]
        public void GivenPropertyAttribute_ShouldAssignPropertyIdentifier()
        {
            // given
            this.path = "/people";

            // when
            var template = this.factory.CreateIriTemplate<SingleRequiredProperty, object>();

            // then
            var iriTemplateMapping = template.Mappings.Single();
            iriTemplateMapping.Property.Should().NotBeNull();
            iriTemplateMapping.Property.Id.Should().Be(Schema.name);
        }

        [Fact]
        public void GivenNoAttribute_ShouldUsePropertyNameAsVariable()
        {
            // given
            this.path = "/people";

            // when
            var template = this.factory.CreateIriTemplate<SingleRequiredProperty, object>();

            // then
            var iriTemplateMapping = template.Mappings.Single();
            iriTemplateMapping.Variable.Should().Be("Prop");
        }

        [Fact]
        public void GivenVariableAttribute_ShouldUseCustomNameAsVariable()
        {
            // given
            this.path = "/people";

            // when
            var template = this.factory.CreateIriTemplate<PropertyWithCustomVariableName, object>();

            // then
            var iriTemplateMapping = template.Mappings.Single();
            iriTemplateMapping.Variable.Should().Be("t");
        }

        [Fact]
        public void GivenBaseTemplate_WhichDoesNotIncludeTheVariables_CreateOneWithAllVariables()
        {
            // given
            this.path = "/people";
            var expectedTemplate = "/people{?t,Name,Age}";

            // when
            var template = this.factory.CreateIriTemplate<MixedProperties, object>();

            // then
            var actualTemplate = new UriTemplateString.UriTemplateString(template.Template);
            actualTemplate.ToString().Should().Be(expectedTemplate);
        }

        [Fact]
        public void GivenBaseTemplate_WhichIncludeAVariable_DoesNotAppendThatQueryParam()
        {
            // given
            this.path = "/people{/t}";
            var expectedTemplate = "/people{/t}{?Name,Age}";

            // when
            var template = this.factory.CreateIriTemplate<MixedProperties, object>();

            // then
            var actualTemplate = new UriTemplateString.UriTemplateString(template.Template);
            actualTemplate.ToString().Should().Be(expectedTemplate);
        }

#pragma warning disable SA1516 // Elements must be separated by blank line
        private class SingleNonRequiredProperty : ITemplateParameters<object>
        {
            public string Prop { get; set; }
        }

        private class PropertyWithCustomVariableName : ITemplateParameters<object>
        {
            [Variable("t")]
            public string Title { get; set; }
        }

        private class MixedProperties : ITemplateParameters<object>
        {
            [Variable("t")]
            public string Title { get; set; }

            public string Name { get; set; }

            public string Age { get; set; }
        }

        private class SingleRequiredProperty : ITemplateParameters<object>
        {
            [Required]
            [Property(Schema.name)]
            public string Prop { get; set; }
        }

        private class ManyProperties : ITemplateParameters<object>
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
            public string Prop4 { get; set; }
            public string Prop5 { get; set; }
            public string Prop6 { get; set; }
            public string Prop7 { get; set; }
            public string Prop8 { get; set; }
        }
    }
}