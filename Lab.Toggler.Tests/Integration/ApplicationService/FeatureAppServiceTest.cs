using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.ApplicationService;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Resources;
using Lab.Toggler.Domain.Service;
using Lab.Toggler.Infra.Data;
using Lab.Toggler.Infra.Data.Repository;
using Lab.Toggler.Model;
using MediatR;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Integration.ApplicationService
{
    public class FeatureAppServiceTest : SqliteIntegrationTest
    {
        private readonly IMediator _mediator;

        public FeatureAppServiceTest()
        {
            _mediator = Substitute.For<IMediator>();
        }

        [Fact]
        public async Task Should_add_new_feature()
        {

            var newFeature = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.Add(new FeatureModel { Name = "isButtonGreen", IsActive = true });
            });

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new FeatureRepository(contexto);
                return featureRepository.GetAsync(newFeature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Should().NotBeNull();
                newFeature.Name.Should().Be("isButtonGreen");
                newFeature.IsActive.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Should_toggle_feature()
        {
            var feature = new Feature("isButtonGreen", false);
            await AddEntity(feature);

            await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.ToggleFeature(new FeatureModel { Name = "isButtonGreen", IsActive = true });
            });

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new FeatureRepository(contexto);
                return featureRepository.GetAsync(feature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Name.Should().Be("isButtonGreen");
                persistedEntity.IsActive.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Should_toggle_application_feature()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isButtonGreen", false);

            await AddEntity(application, feature);

            var newFeature = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.AddApplicationFeature(new ApplicationFeatureModel("App01", "0.1", "isButtonGreen", true));
            });

            await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.ToggleApplicationFeature(new ApplicationFeatureModel("App01", "0.1", "isButtonGreen", false));
            });

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new ApplicationFeatureRepository(contexto);
                return featureRepository.GetAsync(newFeature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.ApplicationId.Should().Be(application.Id);
                persistedEntity.FeatureId.Should().Be(feature.Id);
                persistedEntity.IsActive.Should().BeFalse();
            }
        }

        [Fact]
        public async Task Should_check_application_feature()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isButtonGreen", false);

            await AddEntity(application, feature);

            var newFeature = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.AddApplicationFeature(new ApplicationFeatureModel("App01", "0.1", "isButtonGreen", true));
            });

            var featureCheck = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.Check("App01", "0.1", "isButtonGreen");
            });

            using (new AssertionScope())
            {
                featureCheck.Enabled.Should().BeTrue();
                featureCheck.Mesage.Should().Be(DomainMessage.FeatureEnabled);
            }
        }

        [Fact]
        public async Task Should_check_feature()
        {
            var feature = new Feature("isButtonGreen", true);
            await AddEntity(feature);

            var featureCheck = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.Check("App01", "0.1", "isButtonGreen");
            });

            using (new AssertionScope())
            {
                featureCheck.Enabled.Should().BeTrue();
                featureCheck.Mesage.Should().Be(DomainMessage.FeatureEnabled);
            }
        }

        [Fact]
        public async Task Should_add_new_application_feature()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isButtonGreen", false);

            await AddEntity(application, feature);

            var newFeature = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.AddApplicationFeature(new ApplicationFeatureModel("App01", "0.1", "isButtonGreen", true));
            });

            var persistedEntity = await ExecuteCommand((contexto) =>
            {
                var featureRepository = new ApplicationFeatureRepository(contexto);
                return featureRepository.GetAsync(newFeature.Id);
            });

            using (new AssertionScope())
            {
                persistedEntity.Should().NotBeNull();
                newFeature.ApplicationName.Should().Be("App01");
                newFeature.ApplicationVersion.Should().Be("0.1");
                newFeature.FeatureName.Should().Be("isButtonGreen");
                newFeature.IsActive.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Should_return_null_and_raise_error_when_adding_new_application_feature_with_invalid_data()
        {
            var feature = new Feature("isButtonGreen", false);

            await AddEntity(feature);

            var newFeature = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.AddApplicationFeature(new ApplicationFeatureModel("App01", "0.1", "isButtonGreen", true));
            });

            using (new AssertionScope())
            {
                newFeature.Should().BeNull();
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationCannotBeNull)));
            }
        }

        [Fact]
        public async Task Should_return_null_and_raise_error_when_invalid_data_is_provided()
        {
            var newFeature = await ExecuteCommand((context) =>
            {
                FeatureAppService featureAppService = GetFeatureAppService(context);
                return featureAppService.Add(new FeatureModel { Name = "", IsActive = true });
            });

            using (new AssertionScope())
            {
                newFeature.Should().BeNull();
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.FeatureNameCannotBeNulllOrEmpty)));
            }
        }

        private FeatureAppService GetFeatureAppService(TogglerContext context)
        {
            var featureRepository = new FeatureRepository(context);
            var applicationRepository = new ApplicationRepository(context);
            var applicationFeatureRepository = new ApplicationFeatureRepository(context);
            var uow = new UnitOfWork(context);
            var featureDomainService = new FeatureDomainService(_mediator, featureRepository);
            var applicationFeatureDomainService = new ApplicationFeatureDomainService(_mediator, applicationFeatureRepository, featureRepository);

            var featureAppService = new FeatureAppService(uow, applicationRepository, featureRepository, featureDomainService, applicationFeatureDomainService);
            return featureAppService;
        }
    }
}
