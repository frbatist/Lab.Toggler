using System;
using System.Threading.Tasks;
using Lab.Toggler.ApplicationService;
using Lab.Toggler.Domain.Interface.Notifications;
using Lab.Toggler.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Toggler.Controllers
{
    [Route("api/application-feature")]
    [ApiController]
    [Authorize]
    public class ApplicationFeatureController : ApiControllerBase
    {
        private readonly IFeatureAppService _featureAppService;

        public ApplicationFeatureController(IErrorNotificationsManager errorNotificationsManager, IFeatureAppService featureAppService) : base(errorNotificationsManager)
        {
            _featureAppService = featureAppService;
        }

        /// <summary>
        /// Endpoint to add a new new application feature
        /// </summary>
        /// <param name="model">Application feature data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationFeatureResponseModel), 200)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post(ApplicationFeatureModel model)
        {
            try
            {
                var app = await _featureAppService.AddApplicationFeature(model);
                return Response(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint to alter application feature data
        /// </summary>
        /// <param name="model">Application feature data</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(ApplicationFeatureModel model)
        {
            try
            {
                await _featureAppService.ToggleApplicationFeature(model);
                return Response();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint to get feature data, shows if it's enabled or disabled
        /// </summary>
        /// <param name="featureCheckModel">Application feature data</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FeatureCheckModelResponse), 200)]
        public async Task<IActionResult> Get([FromQuery]FeatureCheckModel featureCheckModel)
        {
            try
            {
                var response = await _featureAppService.Check(featureCheckModel.ApplicationName, featureCheckModel.ApplicationVersion, featureCheckModel.Feature);
                return Response(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); ;
            }
        }

        /// <summary>
        /// Endpoint to get all feature data for a application
        /// </summary>
        /// <param name="application"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(FeatureModelResponse), 200)]
        public async Task<IActionResult> Get(string application, string version)
        {
            try
            {
                var response = await _featureAppService.GetAll(application, version);
                return Response(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); ;
            }
        }
    }
}