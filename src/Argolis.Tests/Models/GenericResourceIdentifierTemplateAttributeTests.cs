using System;
using System.Collections.Generic;
using Argolis.Models;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests.Models
{
    public class GenericResourceIdentifierTemplateAttributeTests
    {
        [Fact]
        public void Can_not_be_constructed_with_non_generic_type()
        {
            var ex = Assert.Throws<ArgumentException>(() => new GenericResourceIdentifierTemplateAttributeTestable(string.Empty, typeof(object)));

            ex.ParamName.Should().Be("containerType");
        }

        [Fact]
        public void Can_not_be_constructed_with_generic_type_which_has_two_type_params()
        {
            var ex = Assert.Throws<ArgumentException>(() => new GenericResourceIdentifierTemplateAttributeTestable(string.Empty, typeof(KeyValuePair<,>)));

            ex.ParamName.Should().Be("containerType");
        }

        private class GenericResourceIdentifierTemplateAttributeTestable : GenericResourceIdentifierTemplateAttribute
        {
            public GenericResourceIdentifierTemplateAttributeTestable(string template, Type containerType)
                : base(template, containerType)
            {
            }
        }
    }
}