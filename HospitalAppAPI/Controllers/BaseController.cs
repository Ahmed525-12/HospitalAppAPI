using HospitalDomain.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<AccountController> _logger;

        public BaseController(ILogger<AccountController> logger, SignInManager<Account> signInManagerAccount = null)
        {
            _logger = logger;
        }
    }
}