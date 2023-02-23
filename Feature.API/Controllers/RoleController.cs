using Feature.API.Logger;
using Feature.Entity.Entities;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feature.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {


        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {

            _roleService = roleService;
        }

        [Route("GetAllRoles")]
        [HttpGet]
        public IEnumerable<Role> GetAll()
        {
            return _roleService.GetAll();
        }

        [Route("GetById")]
        [HttpGet]
        public Task<Role> GetById(int id)
        {
            return _roleService.GetById(id);
        }

        [Route("Create Role")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(Role role)
        {
            _roleService.Insert(role);
            return Ok();
        }

        [Route("Update Role")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(Role role)
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

