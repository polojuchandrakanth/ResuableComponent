using System.Collections;
using System.Data;
using Feature.DAL.Repositories.Interfaces;
using Feature.Entity.Entities;
using Feature.Services.Abstract;

namespace Feature.Services.Concrete
{
    public class DALService : IDALService
    {
        private IDALRepository _dalRepository;
        public DALService(IDALRepository dalRepository)
        {
            _dalRepository = dalRepository;
        }
        public void CreateUser(UserProfile userProfile)
        {
            _dalRepository.CreateUser(userProfile);
        }
        public IEnumerable GetUserById(string userId)
        {
            var result = _dalRepository.GetUserById(userId);
            return result;
        }
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            var result = _dalRepository.GetUserProfiles();
            return result;
        }
        public void UpdateUserProfile(UserProfile userProfile)
        {
            _dalRepository.UpdateUserProfile(userProfile);
        }
        public void DeleteUserProfile(string userId)
        {
            _dalRepository.DeleteUserProfile(userId);
        }
    }
}
