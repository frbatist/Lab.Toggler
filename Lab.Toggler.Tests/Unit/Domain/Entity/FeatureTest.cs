using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.Entities;
using Xunit;

namespace Lab.Toggler.Tests.Unit.Domain.Entity
{
    public class FeatureTest
    {
        [Fact]
        public void Should_construct_a_valid_feature()
        {
            var name = "isButtonBlue";
            var isActive = true;

            var feature = new Feature(name, isActive);
            using (new AssertionScope())
            {
                feature.IsActive.Should().BeTrue();
                feature.Name.Should().Be("isButtonBlue");
            }
        }

        [Fact]
        public void Should_enable_feature()
        {
            var name = "isButtonBlue";
            var isActive = false;

            var feature = new Feature(name, isActive);

            feature.Enable();

            feature.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Should_disable_feature()
        {
            var name = "isButtonBlue";
            var isActive = false;

            var feature = new Feature(name, isActive);

            feature.Disable();

            feature.IsActive.Should().BeFalse();
        }
    }
}
