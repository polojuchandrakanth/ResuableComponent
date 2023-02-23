using Feature.API.Logger;
using Feature.BusinessModel.ViewModel;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILogger<LoginController> _seriLogger;
        private readonly ILoggerExtention _nlogLogger;
        private readonly IUserService _userService;
        public LoginController(ILogger<LoginController> logger, ILogger<LoginController> seriLogger, ILoggerExtention nlogLogger, IUserService userService)
        {
            _seriLogger = seriLogger;
            _nlogLogger = nlogLogger;
            _logger = logger;
            _userService = userService;
        }
        /// <summary>
        /// User Login
        /// </summary>
        /// <returns>True or False</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogins userLogins)
        {
            try
            {
                LoginResponse result = await _userService.Login(userLogins);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex);
                return BadRequest(ex);
            }
            finally
            {
                _logger.LogInformation("Login action execution end");
            }
        }
    }
}
