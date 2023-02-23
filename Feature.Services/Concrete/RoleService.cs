using Feature.Entity.Entities;
using Feature.Repository.Interface.Interfaces;
using Feature.Services.Abstract;
namespace Feature.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }
        public async Task<Role> GetById(int id)
        {
            return _roleRepository.GetById(id);
        }
        public void Insert(Role profile)
        {
            _roleRepository.Insert(profile);
        }
        public void Update(Role profile)
        {
            _roleRepository.Update(profile);
        }
        public void Delete(object id)
        {
            _roleRepository.Delete(id);
        }
    }
}

