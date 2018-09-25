using FluentAssertions;
using FluentAssertions.Execution;
using Lab.Toggler.ApplicationService;
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
    public class ApplicationAppServiceTest : SqliteIntegrationTest
    {
        private readonly IMediator _mediator;

        public ApplicationAppServiceTest()
        {
            _mediator = Substitute.For<IMediator>();
        }

        [Fact]
        public async Task Should_add_new_service()
        {

            var newApplication = await ExecuteCommand((context) =>
            {
                var uow = new UnitOfWork(context);
                var applicationDomainService = new ApplicationDomainService(_mediator, new ApplicationRepository(context));

                var applicationAppService = new ApplicationAppService(uow, applicationDomainService);
                return applicationAppService.Add(new ApplicationModel { Name = "App01", Version = "0.1" });
            });

            using (new AssertionScope())
            {
                newApplication.Name.Should().Be("App01");
                newApplication.Version.Should().Be("0.1");
            }
        }

        [Fact]
        public async Task Should_return_null_and_cast_error_when_invalid_data_is_provided()
        {
            var newApplication = await ExecuteCommand((context) =>
            {
                var uow = new UnitOfWork(context);
                var applicationDomainService = new ApplicationDomainService(_mediator, new ApplicationRepository(context));

                var applicationAppService = new ApplicationAppService(uow, applicationDomainService);
                return applicationAppService.Add(new ApplicationModel { Name = "", Version = "0.1" });
            });

            using (new AssertionScope())
            {
                newApplication.Should().BeNull();
                await _mediator.Received(1).Publish(Arg.Is<ErrorNotification>(d => d.Error.Equals(DomainMessageError.ApplicationNameCannotBeNullOrEmpty)));
            }
        }
    }
}
