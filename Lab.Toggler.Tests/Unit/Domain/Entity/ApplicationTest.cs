using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.Entities;
using Xunit;

namespace Lab.Toggler.Tests.Unit.Domain.Entity
{
    public class ApplicationTest
    {
        [Fact]
        public void Should_construct_a_valid_application()
        {
            var name = "sales";
            var version = "1.1";
            var application = new Application(name, version);


            using (new AssertionScope())
            {
                application.Name.Should().Be(name);
                application.Version.Should().Be(version);
            }
        }
    }
}
