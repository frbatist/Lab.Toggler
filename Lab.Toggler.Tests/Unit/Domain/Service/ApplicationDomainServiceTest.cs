using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Resources;
using Lab.Toggler.Domain.Service;
using MediatR;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Unit.Domain.Service
{
    public class ApplicationDomainServiceTest
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMediator _mediator;
        private readonly IApplicationDomainService _applicationDomainService;

        public ApplicationDomainServiceTest()
        {
            _applicationRepository = Substitute.For<IApplicationRepository>();
            _mediator = Substitute.For<IMediator>();
            _applicationDomainService = new ApplicationDomainService(_mediator, _applicationRepository);
        }

        [Fact]
        public async Task Should_add_new_application()
        {
            var dto = new ApplicationDTO
            {
                Name = "App01",
                Version = "0.1"
            };

            var newApplication = await _applicationDomainService.Add(dto);

            using (new AssertionScope())
            {
                _applicationRepository.Received(1).Add(newApplication);
                newApplication.Name.Should().Be("App01");
                newApplication.Version.Should().Be("0.1");
            }
        }

        [Fact]
        public async Task Should_not_add_new_application_if_application_with_same_name_and_version_already_exists()
        {
            var application = new Application("App01", "0.1");

            _applicationRepository.GetAsync("App01", "0.1").Returns(application);

            var dto = new ApplicationDTO
            {
                Name = "App01",
                Version = "0.1"
            };

            var newApplication = await _applicationDomainService.Add(dto);

            using (new AssertionScope())
            {
                _applicationRepository.Received(0).Add(newApplication);
                newApplication.Should().BeNull();
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationAlreadyExists)));
            }
        }

        [Fact]
        public async Task Should_raise_error_when_application_name_is_empty()
        {
            var dto = new ApplicationDTO
            {
                Name = "",
                Version = "0.1"
            };

            var newApplication = await _applicationDomainService.Add(dto);

            using (new AssertionScope())
            {
                dto.ValidationResult.Errors.Should().NotBeEmpty();
                _applicationRepository.Received(0).Add(newApplication);
                dto.ValidationResult.Errors.Any(d => d.ErrorMessage == DomainMessageError.ApplicationNameCannotBeNullOrEmpty);
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationNameCannotBeNullOrEmpty)));
            }
        }

        [Fact]
        public async Task Should_raise_error_when_application_version_is_empty()
        {
            var dto = new ApplicationDTO
            {
                Name = "App01",
                Version = ""
            };

            var newApplication = await _applicationDomainService.Add(dto);

            using (new AssertionScope())
            {
                dto.ValidationResult.Errors.Should().NotBeEmpty();
                _applicationRepository.Received(0).Add(newApplication);
                dto.ValidationResult.Errors.Any(d => d.ErrorMessage == DomainMessageError.ApplicationVersionCannotBeNullOrEmpty);
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationVersionCannotBeNullOrEmpty)));
            }
        }

        [Fact]
        public async Task Should_raise_error_when_both_application_name_and_version_are_empty()
        {
            var dto = new ApplicationDTO
            {
                Name = "",
                Version = ""
            };

            var newApplication = await _applicationDomainService.Add(dto);

            using (new AssertionScope())
            {
                var errorMessages = new string[] { DomainMessageError.ApplicationVersionCannotBeNullOrEmpty, DomainMessageError.ApplicationNameCannotBeNullOrEmpty };
                dto.ValidationResult.Errors.Should().HaveCount(2);
                _applicationRepository.Received(0).Add(newApplication);
                dto.ValidationResult.Errors.Any(d => errorMessages.Contains(d.ErrorMessage));
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationNameCannotBeNullOrEmpty)));
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationVersionCannotBeNullOrEmpty)));
            }
        }
    }
}
