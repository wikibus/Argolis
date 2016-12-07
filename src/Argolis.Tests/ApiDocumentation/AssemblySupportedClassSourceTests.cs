using System.Collections.Generic;
using System.Reflection;
using Argolis.Hydra.Discovery.SupportedClasses;
using FluentAssertions;
using TestHydraApi;
using Xunit;

namespace Argolis.Tests.ApiDocumentation
{
    public class AssemblySupportedClassSourceTests
    {
        private readonly AssemblyAnnotatedTypeSelector source;

        public AssemblySupportedClassSourceTests()
        {
            this.source = new TestAssemblyAnnotatedTypeSelector();
        }

        [Fact]
        public void Should_discover_annotated_supported_classes()
        {
            // when
            var supportedClasses = this.source.FindTypes();

            // then
            supportedClasses.Should().NotContain(typeof(UndocumentedClass));
        }

        private class TestAssemblyAnnotatedTypeSelector : AssemblyAnnotatedTypeSelector
        {
            protected override IEnumerable<Assembly> Assemblies
            {
                get { yield return typeof(Issue).Assembly; }
            }
        }
    }
}