using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Feature.Entity.Entities;
using Feature.Repositories.ADONET.DataAccessLayer;
using Feature.Repositories.ADONET.Interfaces;
using Feature.Repositories.DALRepositories.Interfaces;

namespace Feature.Repositories.DALRepositories.Repositories
{
    public class DALRepository : IDALRepository
    { 
        private readonly DataBaseManager _dataBaseManager;

        public DALRepository(DataBaseManager dataBaseManager)
        {
            _dataBaseManager = dataBaseManager;
        }
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>User Profile</returns>
        public UserProfile CreateUser(UserProfile userProfile)
        {
            
            using (var connection = _dataBaseManager.GetDbConnection())
            {
                string sql = $"Insert into UserProfile(Id,UserId,FirstName,LastName,Emai,Password) Values ('{userProfile.Id}',{userProfile.UserId}',{userProfile.FirstName}','{userProfile.LastName}','{userProfile.Email}','{userProfile.Password}')";
                using (SqlCommand command = new SqlCommand(sql, (SqlConnection)connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    return userProfile;
                }
            }
        }
    }
}
