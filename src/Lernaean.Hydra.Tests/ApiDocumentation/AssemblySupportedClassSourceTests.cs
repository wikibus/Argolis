using FluentAssertions;
using Hydra.DocumentationDiscovery;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class AssemblySupportedClassSourceTests
    {
        private readonly AssemblyDocumentedTypeSelector _source;

        public AssemblySupportedClassSourceTests()
        {
            _source = new AssemblyDocumentedTypeSelector(typeof(Issue).Assembly);
        }

        [Fact]
        public void Should_discover_annotated_supported_classes()
        {
            // when
            var supportedClasses = _source.FindTypes();

            // then
            supportedClasses.Should().NotContain(typeof (UndocumentedClass));
        }
    }
}