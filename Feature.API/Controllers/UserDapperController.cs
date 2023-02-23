using Feature.API.Logger;
using Feature.Entity.Entities;
using Feature.JWT;
using Feature.JWT.Interface;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserDapperController : ControllerBase
    {
        private readonly IUserServiceDapper _userService;
        private readonly ILoggerExtention _nlogLogger;

        public UserDapperController(IUserServiceDapper userService, ILoggerExtention nlogLogger)
        {
            _userService = userService;
            _nlogLogger = nlogLogger;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _nlogLogger.LogInformation("get all records");

            return new OkObjectResult(_userService.GetAll());
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int ID)
        {
            _nlogLogger.LogInformation("get by id information");

            return new OkObjectResult(_userService.GetById(ID));

        }
        [HttpPost("insert")]
        public IActionResult Insert(UserProfile obj)
        {
            _nlogLogger.LogInformation("Inserted record");

            _userService.Insert(obj);
            return Ok("inserted");

        }
        [HttpPost("update")]
        public  IActionResult Update(UserProfile obj)
        {
            _nlogLogger.LogInformation("update record");

            _userService.Update(obj);
           return  Ok("record updated successfully");
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int ID)
        {
            _nlogLogger.LogInformation("delete record");

            _userService.Delete(ID);
          return  Ok("Deleted");
        }
       

    }
}
 