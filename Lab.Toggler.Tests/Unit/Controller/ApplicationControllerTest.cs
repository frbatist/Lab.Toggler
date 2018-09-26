using FluentAssertions;
using Lab.Toggler.ApplicationService;
using Lab.Toggler.Controllers;
using Lab.Toggler.Domain.Interface.Notifications;
using Lab.Toggler.Model;
using Lab.Toggler.Tests.Unit.Controller.Contract;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Unit.Controller
{
    public class ApplicationControllerTest
    {
        private readonly IApplicationAppService _applicationAppService;
        private readonly IErrorNotificationsManager _errorNotificationsManager;
        private readonly ApplicationController _applicationController;

        public ApplicationControllerTest()
        {
            _applicationAppService = Substitute.For<IApplicationAppService>();
            _errorNotificationsManager = Substitute.For<IErrorNotificationsManager>();
            _applicationController = new ApplicationController(_errorNotificationsManager, _applicationAppService);
        }

        [Fact]
        public async Task Should_post_application_data()
        {
            var contract = new ApplicationResponseModelContract
            {
                Id = 1,
                Name = "App01",
                Version = "0.1"
            };

            var model = new ApplicationModel
            {
                Name = "App01",
                Version = "0.1"
            };

            _applicationAppService.Add(model).Returns(new ApplicationResponseModel
            {
                Id = 1,
                Name = "App01",
                Version = "0.1"
            });

            var result = await _applicationController.Post(model);
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var applicationResponseModel = okResult.Value.Should().BeAssignableTo<ApplicationResponseModel>().Subject;
            contract.Should().BeEquivalentTo(applicationResponseModel);
        }
    }
}
