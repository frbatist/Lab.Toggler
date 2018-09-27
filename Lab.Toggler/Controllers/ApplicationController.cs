using System;
using System.Threading.Tasks;
using Lab.Toggler.ApplicationService;
using Lab.Toggler.Domain.Interface.Notifications;
using Lab.Toggler.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Toggler.Controllers
{
    /// <summary>
    /// Application API, provides endpoints for application management
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ApplicationController : ApiControllerBase
    {
        private readonly IApplicationAppService _applicationAppService;

        public ApplicationController(IErrorNotificationsManager errorNotificationsManager, IApplicationAppService applicationAppService) : base(errorNotificationsManager)
        {
            _applicationAppService = applicationAppService;
        }

        /// <summary>
        /// Endpoint to add a new application for feature management
        /// </summary>
        /// <param name="model">Application data</param>
        /// <returns></returns>        
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationResponseModel), 200)]
        public async Task<IActionResult> Post(ApplicationModel model)
        {
            try
            {
                var app = await _applicationAppService.Add(model);
                return Response(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}