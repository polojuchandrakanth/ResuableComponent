using AutoMapper;
using Feature.BusinessModel.ViewModel;
using Feature.Entity.Entities;
using Feature.Repository.Interface.Interfaces;
using Feature.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Services.Concrete
{
    public class RoleMapperService : IRoleMapperService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleMapperService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public IEnumerable<RoleModel> GetAll()
        {
            var users = _roleRepository.GetAll();
            var getUsers = _mapper.Map<IEnumerable<RoleModel>>(users);
            return getUsers;
        }
        public async Task<RoleModel> GetById(int id)
        {
            var user = _roleRepository.GetById(id);
            var getUser = _mapper.Map<RoleModel>(user);
            return getUser;
        }
        public void Insert(RoleModel profile)
        {
            var userprofile = _mapper.Map<Role>(profile);

            _roleRepository.Insert(userprofile);
        }
        public void Update(RoleModel profile)
        {
            var userprofile = _mapper.Map<Role>(profile);

            _roleRepository.Update(userprofile);
        }
        public void Delete(object id)
        {
            _roleRepository.Delete(id);
        }
    }
}

