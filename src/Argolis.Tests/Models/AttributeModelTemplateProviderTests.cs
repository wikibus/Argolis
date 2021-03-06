﻿using Argolis.Hydra.Models;
using Argolis.Hydra.Resources;
using Argolis.Models;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests.Models
{
    public class AttributeModelTemplateProviderTests
    {
        [Fact]
        public void Getting_template_for_model_should_retrieve_it_from_attribute()
        {
            // given
            var type = typeof(ModelWithTemplate);
            var provider = new AttributeModelTemplateProvider();

            // when
            var template = provider.GetTemplate(type);

            // then
            template.Should().Be("model/with/template");
        }

        [Fact]
        public void Getting_template_for_model_should_throw_if_template_isnt_found()
        {
            // given
            var type = typeof(AttributeModelTemplateProviderTests);
            var provider = new AttributeModelTemplateProvider();

            // when
            var ex = Assert.Throws<MissingTemplateException>(() => provider.GetTemplate(type));

            // then
            ex.Message.Should().Be($"Template not found for type {type.FullName}");
        }

        [Fact]
        public void Getting_template_for_model_should_prefix_attribute_value_with_base()
        {
            // given
            var type = typeof(ModelWithTemplate);
            var provider = new AttributeModelTemplateProvider(new FakeBaseUriProvider());

            // when
            var template = provider.GetAbsoluteTemplate(type);

            // then
            template.Should().Be("http://example.com/model/with/template");
        }

        [Fact]
        public void Getting_template_for_Hydra_collection_should_retrieve_it_from_attribute()
        {
            // given
            var type = typeof(Collection<ModelWithTemplate>);
            var provider = new AttributeModelTemplateProvider();

            // when
            var template = provider.GetTemplate(type);

            // then
            template.Should().Be("multiple/models");
        }

        [Fact]
        public void Getting_absoulte_template_should_throw_when_there_is_no_base_URI()
        {
            // given
            var type = typeof(Collection<ModelWithTemplate>);
            var provider = new AttributeModelTemplateProvider();

            // then
            Assert.Throws<MissingTemplateException>(() => provider.GetAbsoluteTemplate(type));
        }

        [Identifier("model/with/template")]
        [CollectionIdentifier("multiple/models")]
        public class ModelWithTemplate
        {
        }

        public class MyClass
        {
        }

        private class FakeBaseUriProvider : IBaseUriProvider
        {
            public string BaseResourceUri => "http://example.com/";
        }
    }
}