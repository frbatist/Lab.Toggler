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
using System;
using System.Collections.Generic;
using System.Text;
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

            using (new AssertionScope())
            {
                newFeature.Name.Should().Be("isButtonGreen");
                newFeature.IsActive.Should().BeTrue();
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

            using (new AssertionScope())
            {
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
