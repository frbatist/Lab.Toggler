using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Infra.Data.Repository;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Integration
{
    public class ApplicationRepositoryTest : SqliteIntegrationTest
    {
        [Fact]
        public async Task Should_persist_application_data()
        {
            var application = new Application("Sales", "1.1");
            await AddEntity(application);

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new ApplicationRepository(contexto);
                return featureRepository.GetAsync(application.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Name.Should().Be("Sales");
                persistedEntity.Version.Should().Be("1.1");
            }
        }

        [Fact]
        public async Task Should_get_application_data_by_name_and_version()
        {
            var application01 = new Application("Sales", "1.1");
            var application02 = new Application("Sales", "1.2");
            var application03 = new Application("Sales", "1.3");
            var application04 = new Application("stock", "2.3");

            await AddEntity(application01, application02, application03, application04);

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new ApplicationRepository(contexto);
                return featureRepository.GetAsync("Sales", "1.3");
            });

            using (new AssertionScope())
            {
                persistedEntity.Name.Should().Be("Sales");
                persistedEntity.Version.Should().Be("1.3");
            }
        }
    }
}
