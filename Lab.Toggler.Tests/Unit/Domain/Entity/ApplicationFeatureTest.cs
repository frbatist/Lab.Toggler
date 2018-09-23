using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Lab.Toggler.Tests.Unit.Domain.Entity
{
    public class ApplicationFeatureTest
    {
        [Fact]
        public void Should_construct_a_valid_application_feature()
        {
            var name = "isButtonBlue";
            var feature = new Feature(name, false);
            var applicationName = "sales";
            var applicationVersion = "1.1";
            var application = new Application(applicationName, applicationVersion);
            var applicationFeature = new ApplicationFeature(feature, application, true);

            using (new AssertionScope())
            {
                applicationFeature.IsActive.Should().BeTrue();
                applicationFeature.Feature.Name.Should().Be(name);
                applicationFeature.Feature.IsActive.Should().BeFalse();
                applicationFeature.Application.Name.Should().Be(applicationName);
                applicationFeature.Application.Version.Should().Be(applicationVersion);
            }
        }

        [Fact]
        public void Should_enable_an_application_feature()
        {
            var name = "isButtonBlue";
            var feature = new Feature(name, false);
            var applicationName = "sales";
            var applicationVersion = "1.1";
            var application = new Application(applicationName, applicationVersion);
            var applicationFeature = new ApplicationFeature(feature, application, false);

            applicationFeature.Enable();

            applicationFeature.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Should_disable_an_application_feature()
        {
            var name = "isButtonBlue";
            var feature = new Feature(name, false);
            var applicationName = "sales";
            var applicationVersion = "1.1";
            var application = new Application(applicationName, applicationVersion);
            var applicationFeature = new ApplicationFeature(feature, application, false);

            applicationFeature.Enable();

            applicationFeature.IsActive.Should().BeTrue();
        }
    }
}
