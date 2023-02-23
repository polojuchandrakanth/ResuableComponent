using System.Linq.Expressions;
using Feature.Entity.Entities;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feature.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>List of Users</returns>
        [Route("GetAllUsers")]
        [HttpGet]
        public IEnumerable<UserProfile> GetAll()
        { 
            _logger.LogInformation("Get All Users");
            var result =  _userService.GetAll();
            return result;
        }
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Details</returns>
        [Route("GetById")]
        [HttpGet]
        public Task<UserProfile> GetById(int id)
        {
            _logger.LogInformation("Get By id");
            return _userService.GetById(id);
        }
        /// <summary>
        /// Creating User
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>User Profile</returns>
        [Route("Create User")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserProfile userProfile)
        {
            _logger.LogInformation("Create User");
            _userService.Insert(userProfile);
            return Ok();
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>Updated User</returns>
        [Route("Update User")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserProfile userProfile)
        {
            _logger.LogInformation("Update User");
            _userService.Update(userProfile);
            return Ok();
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted User</returns>
        [Route("Delete User")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Delete User");
            _userService.Delete(id);
            return Ok();
        }
        /// <summary>
        /// Filter User
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Filtered User</returns>
        [Route("Filter User")]
        [HttpGet]
        public Task<UserProfile> Filter(Expression<Func<UserProfile, bool>> Predicate)
        {
            _logger.LogInformation("Get User as per requirement");
            return _userService.Filter(Predicate);
        }
    }
}