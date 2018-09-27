using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.Toggler.ApplicationService;
using Lab.Toggler.Domain.Interface.Notifications;
using Lab.Toggler.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Toggler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class FeatureController : ApiControllerBase
    {
        private readonly IFeatureAppService _featureAppService;

        public FeatureController(IErrorNotificationsManager errorNotificationsManager, IFeatureAppService featureAppService) : base(errorNotificationsManager)
        {
            _featureAppService = featureAppService;
        }

        /// <summary>
        /// Endpoint to add a new new feature
        /// </summary>
        /// <param name="model">Feature data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(FeatureModelResponse), 200)]
        public async Task<IActionResult> Post(FeatureModel model)
        {
            try
            {
                var app = await _featureAppService.Add(model);
                return Response(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint to alter feature data
        /// </summary>
        /// <param name="model">Application data</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Put(FeatureModel model)
        {
            try
            {
                await _featureAppService.ToggleFeature(model);
                return Response();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}