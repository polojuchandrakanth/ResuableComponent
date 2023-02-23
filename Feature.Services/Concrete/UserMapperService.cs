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
    public class UserMapperService : IUserMapperService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserMapperService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public IEnumerable<UserProfileViewModel> GetAll()
        {
            var users= _userRepository.GetAll();
            var getUsers = _mapper.Map<IEnumerable<UserProfileViewModel>>(users);
            return getUsers;
        }
        public async Task<UserProfileViewModel> GetById(int id)
        {
            var user = _userRepository.GetById(id);
            var getUser = _mapper.Map<UserProfileViewModel>(user);
            return getUser;
        }
        public void Insert(UserProfileViewModel profile)
        {
            var userprofile = _mapper.Map<UserProfile>(profile);

            _userRepository.Insert(userprofile);
        }
        public void Update(UserProfileViewModel profile)
        {
            var userprofile = _mapper.Map<UserProfile>(profile);

            _userRepository.Update(userprofile);
        }
        public void Delete(object id)
        {
            _userRepository.Delete(id);
        }
       
    }
}
