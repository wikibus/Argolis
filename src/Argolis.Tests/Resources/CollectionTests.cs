using System;
using Argolis.Hydra.Resources;
using FluentAssertions;
using JsonLD.Entities;
using Vocab;
using Xunit;

namespace Argolis.Tests.Resources
{
    public class CollectionTests
    {
        [Fact]
        public void Manages_ShouldBeInitializedWithGenericArgumentsTypes()
        {
            // given
            var collection = new Collection<HasType>();

            // then
            collection.Manages.Should().ContainSingle(mb =>
                mb.Property == (IriRef)Rdf.type && mb.Object == (IriRef)HasType.TheType);
        }

        [Fact]
        public void Manages_WhenClassInheritsCollection_ShouldBeInitializedWithGenericArgumentsTypes()
        {
            // given
            var collection = new DerivedCollection();

            // then
            collection.Manages.Should().ContainSingle(mb =>
                mb.Property == (IriRef)Rdf.type && mb.Object == (IriRef)HasType.TheType);
        }

        [Fact]
        public void Manages_ShouldBeInitializedEmptyWhenTypeHasNoRdfType()
        {
            // given
            var collection = new Collection<object>();

            // then
            collection.Manages.Should().BeEmpty();
        }

        private class DerivedCollection : Collection<HasType>
        {
        }

        private class HasType
        {
            public const string TheType = "http://foo.bar/";

            private static Uri Type
            {
                get
                {
                    return new Uri(TheType);
                }
            }
        }
    }
}