using System.Collections.Generic;
using Argolis.Hydra.Nancy;
using FluentAssertions;
using Nancy;
using Xunit;

namespace Argolis.Tests.ApiDocumentation
{
    public class RequestResponseExtensionsTests
    {
        [Theory]
        [InlineData("https")]
        [InlineData("HTTPS")]
        public void GetApiDocumentationUri_should_honor_X_Forwarded_Proto(string proto)
        {
            // given
            var headers = new Dictionary<string, IEnumerable<string>>
            {
                { "X-Forwarded-Proto", new[] { proto } }
            };
            var request = new Request(
                "GET",
                new Url("http://hydra.app/some/resource"),
                headers: headers);

            // when
            var apiDocUri = request.GetApiDocumentationUri("/api/doc");

            // then
            apiDocUri.Should().Be("https://hydra.app/api/doc");
        }
    }
}