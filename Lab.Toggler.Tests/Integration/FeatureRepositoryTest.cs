using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Infra.Data.Repository;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Integration
{
    public class FeatureRepositoryTest : SqliteIntegrationTest
    {
        [Fact]
        public async Task Should_persist_feature_data()
        {
            var feature = new Feature("Feature01", true);
            await AddEntity(feature);

            var persistedEntity = await ExecuteCommand<Feature>((contexto) =>
            {
                var featureRepository = new FeatureRepository(contexto);
                return featureRepository.GetAsync(feature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Name.Should().Be("Feature01");
                persistedEntity.IsActive.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Should_update_feature_data()
        {
            var feature = new Feature("Feature01", true);
            await AddEntity(feature);

            await ExecuteCommand(async (contexto) =>
            {
                var featureRepository = new FeatureRepository(contexto);
                var featureToUpdata = await featureRepository.GetAsync(feature.Id);
                featureToUpdata.Disable();
                await contexto.SaveChangesAsync();
            });

            var persistedEntity = await ExecuteCommand<Feature>((contexto) =>
            {
                var featureRepository = new FeatureRepository(contexto);
                return featureRepository.GetAsync(feature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Name.Should().Be("Feature01");
                persistedEntity.IsActive.Should().BeFalse();
            }
        }

        [Fact]
        public async Task Should_get_feature_by_name()
        {
            var feature01 = new Feature("Feature01", true);
            var feature02 = new Feature("Feature02", false);
            var feature03 = new Feature("Feature03", true);
            await AddEntity(feature01, feature02, feature03);

            var persistedEntity = await ExecuteCommand<Feature>((contexto) =>
            {
                var featureRepository = new FeatureRepository(contexto);
                return featureRepository.GetByName("Feature02");
            });

            using (new AssertionScope())
            {
                persistedEntity.Name.Should().Be("Feature02");
                persistedEntity.IsActive.Should().BeFalse();
            }
        }
    }
}
