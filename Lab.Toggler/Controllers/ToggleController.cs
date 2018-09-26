using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lab.Toggler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ToggleController : ControllerBase
    {
        //[Authorize(Roles = "admin")]
        //public IActionResult Get()
        //{
        //    return Ok(User.Claims.Where(d=>d.Type.Equals("client_id")).FirstOrDefault().Value);
        //}

        //[Authorize]
        //[HttpGet]
        //[Route("not-admin")]
        //public IActionResult NotAdmin()
        //{
        //    return Ok(User.Claims.Where(d => d.Type.Equals("client_id")).FirstOrDefault().Value);
        //}
    }
}