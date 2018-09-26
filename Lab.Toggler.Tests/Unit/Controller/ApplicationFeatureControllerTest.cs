using FluentAssertions;
using Lab.Toggler.ApplicationService;
using Lab.Toggler.Controllers;
using Lab.Toggler.Domain.Interface.Notifications;
using Lab.Toggler.Model;
using Lab.Toggler.Tests.Unit.Controller.Contract;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lab.Toggler.Tests.Unit.Controller
{
    public class ApplicationFeatureControllerTest
    {
        private readonly IFeatureAppService _featureAppService;
        private readonly IErrorNotificationsManager _errorNotificationsManager;
        private readonly ApplicationFeatureController _applicationFeatureController;

        public ApplicationFeatureControllerTest()
        {
            _featureAppService = Substitute.For<IFeatureAppService>();
            _errorNotificationsManager = Substitute.For<IErrorNotificationsManager>();
            _applicationFeatureController = new ApplicationFeatureController(_errorNotificationsManager, _featureAppService);
        }

        [Fact]
        public async Task Should_post_application_feature_data()
        {
            var contract = new ApplicationFeatureResponseModelContract
            {
                Id = 1,
                ApplicationName = "App01",
                ApplicationVersion = "0.1",
                FeatureName = "isButtonPurple",
                IsActive = true
            };

            var model = new ApplicationFeatureModel("App01", "0.1", "isButtonPurple", true);

            _featureAppService.AddApplicationFeature(model).Returns(new ApplicationFeatureResponseModel(1, "App01", "0.1", "isButtonPurple", true));

            var response = await _applicationFeatureController.Post(model);

            response.Should().NotBeNull();
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            var applicationResponseModel = okResult.Value.Should().BeAssignableTo<ApplicationFeatureResponseModel>().Subject;
            contract.Should().BeEquivalentTo(applicationResponseModel);
        }

        [Fact]
        public async Task Should_put_feature_data()
        {
            var model = new ApplicationFeatureModel("App01", "0.1", "isButtonPurple", true);

            var response = await _applicationFeatureController.Put(model);

            response.Should().NotBeNull();
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Should_get_application_feature_data()
        {
            var contract = new FeatureCheckModelResponseContract
            {
                Enabled = false,
                Mesage = "Alguma coisa em portugues no código."
            };
            var model = new FeatureCheckModel { ApplicationName = "App01", ApplicationVersion = "0.1", Feature = "isButtonPurple" };

            _featureAppService.Check(model.ApplicationName, model.ApplicationVersion, model.Feature)
                .Returns(new FeatureCheckModelResponse { Enabled = false, Mesage = "Alguma coisa em portugues no código." });
            
            var response = await _applicationFeatureController.Get(model);

            response.Should().NotBeNull();
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            var applicationResponseModel = okResult.Value.Should().BeAssignableTo<FeatureCheckModelResponse>().Subject;
            contract.Should().BeEquivalentTo(applicationResponseModel);
        }
    }
}
