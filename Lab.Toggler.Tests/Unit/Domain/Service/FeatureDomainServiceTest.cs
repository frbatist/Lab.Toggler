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
    public class FeatureDomainServiceTest
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IFeatureDomainService _featureDomainService;
        private readonly IMediator _mediator;

        public FeatureDomainServiceTest()
        {
            _featureRepository = Substitute.For<IFeatureRepository>();
            _mediator = Substitute.For<IMediator>();
            _featureDomainService = new FeatureDomainService(_mediator, _featureRepository);            
        }

        [Fact]
        public async Task Should_add_feature()
        {
            var dto = new FeatureDTO(name: "buttonIsBlue", isActive: false);

            var feature = await _featureDomainService.AddFeature(dto);

            using (new AssertionScope())
            {
                dto.ValidationResult.Errors.Should().BeEmpty();
                _featureRepository.Received(1).Add(feature);
            }
        }

        [Fact]
        public async Task Should_raise_error_if_feature_is_invalid()
        {
            var dto = new FeatureDTO(name: "", isActive: false);

            var feature = await _featureDomainService.AddFeature(dto);

            using (new AssertionScope())
            {
                dto.ValidationResult.Errors.Should().NotBeEmpty();
                _featureRepository.Received(0).Add(feature);
                dto.ValidationResult.Errors.Select(d => d.ErrorMessage).ToArray().Contains(DomainMessageError.FeatureNameCannotBeNulllOrEmpty);
            }
        }

        [Fact]
        public async Task Should_not_add_feature_if_there_is_an_existing_feature_with_same_name()
        {
            var dto = new FeatureDTO(name: "buttonIsBlue", isActive: false);
            var entity = new Feature("buttonIsBlue", true);
            _featureRepository.GetByName("buttonIsBlue").Returns(entity);

            var feature = await _featureDomainService.AddFeature(dto);

            _featureRepository.Received(0).Add(feature);
        }

        [Fact]
        public async Task Should_update_feature_if_there_is_an_existing_feature_with_same_name()
        {
            var dto = new FeatureDTO(name: "buttonIsBlue", isActive: false);
            var entity = new Feature("buttonIsBlue", true);
            _featureRepository.GetByName("buttonIsBlue").Returns(entity);

            var feature = await _featureDomainService.AddFeature(dto);

            feature.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task Should_toggle_feature_as_inactive()
        {
            var dto = new FeatureDTO(name: "buttonIsBlue", isActive: false);

            var existingFeature = new Feature(name: "buttonIsBlue", isActive: true);
            _featureRepository.GetByName("buttonIsBlue").Returns(existingFeature);

            await _featureDomainService.TogleFeature(dto);

            existingFeature.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task Should_toggle_feature_as_active()
        {
            var dto = new FeatureDTO(name: "buttonIsBlue", isActive: true);

            var existingFeature = new Feature(name: "buttonIsBlue", isActive: false);
            _featureRepository.GetByName("buttonIsBlue").Returns(existingFeature);

            await _featureDomainService.TogleFeature(dto);

            existingFeature.IsActive.Should().BeTrue();
        }

        [Fact]
        public async Task Should_raise_error_when_toggling_an_nonexisting_feature_as_active()
        {
            var dto = new FeatureDTO(name: "buttonIsBlue", isActive: true);

            await _featureDomainService.TogleFeature(dto);

            await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.NonExistentFeature)));
        }
    }
}
