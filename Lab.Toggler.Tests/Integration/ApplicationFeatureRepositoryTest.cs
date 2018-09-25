using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Integration
{
    public class ApplicationFeatureRepositoryTest : SqliteIntegrationTest
    {
        [Fact]
        public async Task Should_persist_application_feature_data()
        {
            var feature = new Feature("Feature01", true);
            var application = new Application("Sales", "1.1");
            var applicationFeature = new ApplicationFeature(feature, application, false);
            await AddEntity(feature, application, applicationFeature);

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new ApplicationFeatureRepository(contexto);
                return featureRepository.GetApplicationFeatureAsync(applicationFeature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Should().NotBeNull();
                persistedEntity.Application.Name.Should().Be("Sales");
                persistedEntity.Application.Version.Should().Be("1.1");
                persistedEntity.Feature.Name.Should().Be("Feature01");
                persistedEntity.Feature.IsActive.Should().Be(true);
                persistedEntity.IsActive.Should().Be(false);
            }
        }

        [Fact]
        public async Task Should_update_application_feature_data()
        {
            var feature = new Feature("Feature01", true);
            var application = new Application("Sales", "1.1");
            var applicationFeature = new ApplicationFeature(feature, application, false);
            await AddEntity(feature, application, applicationFeature);


            await ExecuteCommand(async (contexto) =>
            {
                var applicationFeatureRepository = new ApplicationFeatureRepository(contexto);
                var featureToUpdate = await applicationFeatureRepository.GetAsync(feature.Id);
                featureToUpdate.Enable();
                await contexto.SaveChangesAsync();
            });

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new ApplicationFeatureRepository(contexto);
                return featureRepository.GetApplicationFeatureAsync(applicationFeature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Should().NotBeNull();
                persistedEntity.Application.Name.Should().Be("Sales");
                persistedEntity.Application.Version.Should().Be("1.1");
                persistedEntity.Feature.Name.Should().Be("Feature01");
                persistedEntity.Feature.IsActive.Should().Be(true);
                persistedEntity.IsActive.Should().Be(true);
            }
        }

        [Fact]
        public async Task Should_get_application_feature_by_application_and_feature()
        {
            var feature = new Feature("Feature01", false);
            var application1 = new Application("Sales", "1.1");
            var applicationFeature1 = new ApplicationFeature(feature, application1, false);
            var application2 = new Application("Sales", "1.2");
            var applicationFeature2 = new ApplicationFeature(feature, application2, true);
            var application3 = new Application("Sales", "1.3");
            var applicationFeature3 = new ApplicationFeature(feature, application3, false); 

            await AddEntity(feature, application1, application2, application3, applicationFeature1, applicationFeature2, applicationFeature3);

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new ApplicationFeatureRepository(contexto);
                return featureRepository.GetAsync(application2.Id, feature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Should().NotBeNull();
                persistedEntity.ApplicationId.Should().Be(application2.Id);
                persistedEntity.FeatureId.Should().Be(feature.Id);
                persistedEntity.IsActive.Should().Be(true);
            }
        }
    }
}
