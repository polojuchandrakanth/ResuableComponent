using Feature.BusinessModel.ViewModel;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMapperController : ControllerBase
    {
        private readonly IRoleMapperService _roleService;
        public RoleMapperController(IRoleMapperService roleService)
        {

            _roleService = roleService;
        }

        [Route("GetAllRoles")]
        [HttpGet]
        public IEnumerable<RoleModel> GetAll()
        {
            return _roleService.GetAll();
        }

        [Route("GetById")]
        [HttpGet]
        public Task<RoleModel> GetById(int id)
        {
            return _roleService.GetById(id);
        }

        [Route("Create Role")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel role)
        {
            _roleService.Insert(role);
            return Ok();
        }

        [Route("Update Role")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleModel role)
        {
            _roleService.Update(role);
            return Ok();
        }

        [Route("Delete Role")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id)
        {
            _roleService.Delete(id);
            return Ok();
        }

    }
}

