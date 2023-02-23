using System.Collections;
using System.Data;
using Feature.DAL.Repositories;
using Feature.Entity.Entities;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Feature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DALController : ControllerBase
    {
        private readonly IDALService _dalService;
        private readonly ILogger<DALController> _logger;    
        public DALController(IDALService dalService, ILogger<DALController> logger)
        {
            _dalService = dalService;
            _logger = logger;
        }
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>User Profile</returns>
        [Route("CreateUser")]
        [HttpPost]
        public void CreateUserProfile(UserProfile userProfile)
        {
            _logger.LogInformation("Logs for Creating User");
            _dalService.CreateUser(userProfile);
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>List of User Profiles</returns>
        [Route("GetAllUsers")]
        [HttpGet]
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            _logger.LogInformation("Get All User Profiles");
            return _dalService.GetUserProfiles();
        }
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Profileu</returns>
        [Route("GetUserById")]
        [HttpGet]
        public IEnumerable GetUserById(string userId)
        {
            _logger.LogInformation("Get User Profiles By Id");
            return _dalService.GetUserById(userId);
            //return ConversionHelper.ConvertToObject<UserProfile>(result);
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userProfile"></param>
        /// <param name="id"></param>
        /// <returns>User Profile</returns>
        [Route("UpdateUser")]
        [HttpPost]
        public void UpdateUserProfile(UserProfile userProfile)
        {
            _logger.LogInformation("Update Users");
            _dalService.UpdateUserProfile(userProfile);
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Profile</returns>
        [Route("DeleteUser")]
        [HttpDelete]
        public void DeleteUserProfile(string userId)
        {
            _logger.LogInformation("Delete Users");
            _dalService.DeleteUserProfile(userId);
        }
    }
}
