using System.Linq.Expressions;
using Feature.BusinessModel.ViewModel;
using Feature.Entity.Entities;
using Feature.Repository.Interface.Interfaces;
using Feature.Services.Abstract;

namespace Feature.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LoginResponse> Login(UserLogins userLogins)
        {
            return await _userRepository.Login(userLogins);
        }
        public IEnumerable<UserProfile> GetAll()
        {
            return _userRepository.GetAll();
        }
        public async Task<UserProfile> GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public void Insert(UserProfile profile)
        {
            _userRepository.Insert(profile);
        }
        public void Update(UserProfile profile)
        {
            _userRepository.Update(profile);
        }
        public void Delete(object id)
        {
            _userRepository.Delete(id);
        }
        public async Task<UserProfile> Filter(Expression<Func<UserProfile, bool>> Predicate)
        {
            return await _userRepository.Filter(Predicate);
        }
    }
}