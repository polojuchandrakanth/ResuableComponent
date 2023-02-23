using Feature.BusinessModel.ViewModel;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileMapperController : ControllerBase
    {
        private readonly IUserMapperService _userService;
        public UserProfileMapperController(IUserMapperService userService)
        {
            
            _userService = userService;
        }
        
        [Route("GetAllUsers")]
        [HttpGet]
        public IEnumerable<UserProfileViewModel> GetAll()
        {
            return _userService.GetAll();
        }
       
        [Route("GetById")]
        [HttpGet]
        public Task<UserProfileViewModel> GetById(int id)
        {
            return _userService.GetById(id);
        }
        
        [Route("Create User")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserProfileViewModel userProfile)
        {
            _userService.Insert(userProfile);
            return Ok();
        }
        
        [Route("Update User")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserProfileViewModel userProfile)
        {
            _userService.Update(userProfile);
            return Ok();
        }
       
        [Route("Delete User")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
