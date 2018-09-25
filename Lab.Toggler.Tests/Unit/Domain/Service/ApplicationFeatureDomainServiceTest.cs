using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Resources;
using Lab.Toggler.Domain.Service;
using MediatR;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Unit.Domain.Service
{
    public class ApplicationFeatureDomainServiceTest
    {
        private readonly IMediator _mediator;
        private readonly IApplicationFeatureRepository _applicationFeatureRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IApplicationFeatureDomainService _applicationFeatureDomainService;        

        public ApplicationFeatureDomainServiceTest()
        {
            _mediator = Substitute.For<IMediator>();
            _applicationFeatureRepository = Substitute.For<IApplicationFeatureRepository>();
            _featureRepository = Substitute.For<IFeatureRepository>();
            _applicationFeatureDomainService = new ApplicationFeatureDomainService(_mediator, _applicationFeatureRepository, _featureRepository);
        }

        [Fact]
        public async Task Should_add_application_feature()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isBlueButtonActive", true);
            var dto = new ApplicationFeatureDTO(feature, application, false);

            var appFeature = await _applicationFeatureDomainService.Add(dto);

            using (new AssertionScope())
            {
                _applicationFeatureRepository.Received(1).Add(appFeature);
                appFeature.Application.Name.Should().Be("App01");
                appFeature.Application.Version.Should().Be("0.1");
                appFeature.Feature.Name.Should().Be("isBlueButtonActive");
                appFeature.Feature.IsActive.Should().Be(true);
                appFeature.IsActive.Should().Be(false);
            }
        }

        [Fact]
        public async Task Should_should_raise_error_if_application_is_null_on_adding_application_feature()
        {
            var feature = new Feature("isBlueButtonActive", true);
            var dto = new ApplicationFeatureDTO(feature, null, false);

            var appFeature = await _applicationFeatureDomainService.Add(dto);

            using (new AssertionScope())
            {
                appFeature.Should().BeNull();
                _applicationFeatureRepository.Received(0).Add(appFeature);
                dto.ValidationResult.Errors.Should().NotBeEmpty();
                dto.ValidationResult.Errors.Any(d => d.ErrorMessage == DomainMessageError.ApplicationCannotBeNull);
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationCannotBeNull)));
            }
        }

        [Fact]
        public async Task Should_should_raise_error_if_feature_is_null_on_adding_application_feature()
        {
            var application = new Application("App01", "0.1");
            var dto = new ApplicationFeatureDTO(null, application, false);

            var appFeature = await _applicationFeatureDomainService.Add(dto);

            using (new AssertionScope())
            {
                appFeature.Should().BeNull();
                _applicationFeatureRepository.Received(0).Add(appFeature);
                dto.ValidationResult.Errors.Should().NotBeEmpty();
                dto.ValidationResult.Errors.Any(d => d.ErrorMessage == DomainMessageError.FeatureCannotBeNull);
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.FeatureCannotBeNull)));
            }
        }

        [Fact]
        public async Task Should_should_raise_error_if_both_application_and_feature_are_null_on_adding_application_feature()
        {
            var dto = new ApplicationFeatureDTO(null, null, false);

            var appFeature = await _applicationFeatureDomainService.Add(dto);

            using (new AssertionScope())
            {
                var errorMessages = new string[] { DomainMessageError.ApplicationCannotBeNull, DomainMessageError.FeatureCannotBeNull };
                appFeature.Should().BeNull();
                _applicationFeatureRepository.Received(0).Add(appFeature);
                dto.ValidationResult.Errors.Should().HaveCount(2);
                dto.ValidationResult.Errors.Any(d => errorMessages.Contains(d.ErrorMessage));
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationCannotBeNull)));
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.FeatureCannotBeNull)));
            }
        }

        [Fact]
        public async Task Should_update_feature_if_there_is_an_existing_feature_for_the_provided_application()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isBlueButtonActive", true);
            var dto = new ApplicationFeatureDTO(feature, application, false);
            var applicationFeature = new ApplicationFeature(feature, application, true);

            _applicationFeatureRepository.GetAsync(application.Id, feature.Id).Returns(applicationFeature);            

            var appFeature = await _applicationFeatureDomainService.Add(dto);

            using (new AssertionScope())
            {
                _applicationFeatureRepository.Received(0).Add(appFeature);
                appFeature.Application.Name.Should().Be("App01");
                appFeature.Application.Version.Should().Be("0.1");
                appFeature.Feature.Name.Should().Be("isBlueButtonActive");
                appFeature.Feature.IsActive.Should().Be(true);
                appFeature.IsActive.Should().Be(false);
            }
        }

        [Fact]
        public async Task Should_toggle_feature_as_inactive()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isBlueButtonActive", true);
            var dto = new ApplicationFeatureDTO(feature, application, false);
            var applicationFeature = new ApplicationFeature(feature, application, true);

            _applicationFeatureRepository.GetAsync(application.Id, feature.Id).Returns(applicationFeature);

            await _applicationFeatureDomainService.TogleApplicationFeature(dto);

            applicationFeature.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task Should_toggle_feature_as_active()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isBlueButtonActive", false);
            var dto = new ApplicationFeatureDTO(feature, application, true);
            var applicationFeature = new ApplicationFeature(feature, application, false);

            _applicationFeatureRepository.GetAsync(application.Id, feature.Id).Returns(applicationFeature);

            await _applicationFeatureDomainService.TogleApplicationFeature(dto);

            applicationFeature.IsActive.Should().BeTrue();
        }

        [Fact]
        public async Task Should_raise_error_when_toggling_an_nonexisting_feature_as_active()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isBlueButtonActive", false);
            var dto = new ApplicationFeatureDTO(feature, application, true);

            await _applicationFeatureDomainService.TogleApplicationFeature(dto);

            await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.NonExistentFeature)));
        }

        [Fact]
        public async Task Should_return_true_if_feature_is_eanabled_for_all_apps()
        {
            var feature = new Feature("isBlueButtonActive", true);
            _featureRepository.GetByName("isBlueButtonActive").Returns(feature);
            var result = await _applicationFeatureDomainService.CheckFeature("app01", "1.1", "isBlueButtonActive");

            result.Should().NotBeNull();
            result.Enabled.Should().BeTrue();
            result.Mesage.Should().Be(DomainMessage.FeatureEnabled);
        }

        [Fact]
        public async Task Should_return_false_if_feature_is_disabled_for_all_apps()
        {
            var feature = new Feature("isBlueButtonActive", false);
            _featureRepository.GetByName("isBlueButtonActive").Returns(feature);
            var result = await _applicationFeatureDomainService.CheckFeature("app01", "1.1", "isBlueButtonActive");

            result.Should().NotBeNull();
            result.Enabled.Should().BeFalse();
            result.Mesage.Should().Be(DomainMessage.FeatureDisabled);
        }

        [Fact]
        public async Task Should_return_true_if_feature_is_enabled_for_the_app()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isBlueButtonActive", false);
            var applicationFeature = new ApplicationFeature(feature, application, true);

            _applicationFeatureRepository.GetAsync("App01", "0.1", "isBlueButtonActive").Returns(applicationFeature);
            var result = await _applicationFeatureDomainService.CheckFeature("App01", "0.1", "isBlueButtonActive");

            result.Should().NotBeNull();
            result.Enabled.Should().BeTrue();
            result.Mesage.Should().Be(DomainMessage.FeatureEnabled);
        }

        [Fact]
        public async Task Should_return_true_if_feature_is_disabled_for_the_app()
        {
            var application = new Application("App01", "0.1");
            var feature = new Feature("isBlueButtonActive", true);
            var applicationFeature = new ApplicationFeature(feature, application, false);

            _applicationFeatureRepository.GetAsync("App01", "0.1", "isBlueButtonActive").Returns(applicationFeature);
            var result = await _applicationFeatureDomainService.CheckFeature("App01", "0.1", "isBlueButtonActive");

            result.Should().NotBeNull();
            result.Enabled.Should().BeFalse();
            result.Mesage.Should().Be(DomainMessage.FeatureDisabled);
        }
    }
}
