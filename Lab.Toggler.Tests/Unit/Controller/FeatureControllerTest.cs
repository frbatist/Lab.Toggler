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
    public class FeatureControllerTest
    {
        private readonly IFeatureAppService _featureAppService;
        private readonly IErrorNotificationsManager _errorNotificationsManager;
        private readonly FeatureController _featureController;

        public FeatureControllerTest()
        {
            _featureAppService = Substitute.For<IFeatureAppService>();
            _errorNotificationsManager = Substitute.For<IErrorNotificationsManager>();
            _featureController = new FeatureController(_errorNotificationsManager, _featureAppService);
        }

        [Fact]
        public async Task Should_post_feature_data()
        {
            var contract = new FeatureModelResponseContract
            {
                Id = 1,
                Name = "isButtonPurple",
                IsActive = true
            };

            var model = new FeatureModel
            {
                Name = "isButtonPurple",
                IsActive = true
            };

            _featureAppService.Add(model).Returns(new FeatureModelResponse(1, "isButtonPurple", true));

            var response = await _featureController.Post(model);

            response.Should().NotBeNull();
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            var applicationResponseModel = okResult.Value.Should().BeAssignableTo<FeatureModelResponse>().Subject;
            contract.Should().BeEquivalentTo(applicationResponseModel);
        }

        [Fact]
        public async Task Should_put_feature_data()
        {
            var model = new FeatureModel
            {
                Name = "isButtonPurple",
                IsActive = true
            };

            var response = await _featureController.Put(model);

            response.Should().NotBeNull();
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
        }
    }
}
