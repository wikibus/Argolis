using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Argolis.Models;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests.Models
{
    public class UriTemplateMatchesTests
    {
        [Fact]
        public void Failed_match_should_not_throw_on_dictionary_members()
        {
            // given
            var failure = UriTemplateMatches.Failure();

            // then
            failure.Values.Should().BeEmpty();
            failure.Keys.Should().BeEmpty();
            failure.ContainsKey("whatever").Should().BeFalse();
            object val;
            failure.TryGetValue("something", out val).Should().BeFalse();
            failure.Count.Should().Be(0);
            failure.GetEnumerator().MoveNext().Should().BeFalse();
        }

        [Fact]
        public void Successful_match_should_get_converted_values_for_existing_key()
        {
            // given
            var matches = new Dictionary<string, object>
            {
                { "key", "5" }
            };
            var templateMatches = new UriTemplateMatches(matches);

            // when
            var actualValue = templateMatches.Get<int>("key");

            // then
            actualValue.Should().Be(5);
        }

        [Fact]
        public void Successful_match_should_get_values_as_is_when_no_conversions_required()
        {
            // given
            var list = new List<int>();
            var matches = new Dictionary<string, object>
            {
                { "key", list }
            };
            var templateMatches = new UriTemplateMatches(matches);

            // when
            var actualValue = templateMatches.Get<List<int>>("key");

            // then
            actualValue.Should().BeSameAs(list);
        }
    }
}
