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
    public class UserServiceDapper : IUserServiceDapper
    {
        private readonly IUserRepositoryDapper _userRepository;
        public UserServiceDapper(IUserRepositoryDapper userRepository)
        {
            _userRepository = userRepository;
            
        }
        public IEnumerable<UserProfile> GetAll()
        {
            return _userRepository.GetAll();
        }
        public UserProfile GetById(object id)
        {
            return _userRepository.GetById(id);
        }
        public void Insert(UserProfile obj)
        {
            _userRepository.Insert(obj);

        }
        public void Update(UserProfile obj)
        {
            _userRepository.Update(obj);

        }
        public void Delete(object id)
        {
            _userRepository.Delete(id);
        }


    }
}
