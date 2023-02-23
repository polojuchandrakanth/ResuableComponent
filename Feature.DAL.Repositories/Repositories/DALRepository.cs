using System.Collections;
using System.Data;
using Feature.ADONET.DAL.Interfaces;
using Feature.DAL.Repositories.Interfaces;
using Feature.Entity.Entities;

namespace Feature.DAL.Repositories.Repositories
{
    public class DALRepository : IDALRepository
    {
        private readonly IDataBaseManager _dataBaseManager;
        private readonly IDataParameterHandler _dataParameterHandler;

        public DALRepository(IDataBaseManager dataBaseManager, IDataParameterHandler dataParameterHandler)
        {
            _dataBaseManager = dataBaseManager;
            _dataParameterHandler = dataParameterHandler;
        }
        public void CreateUser(UserProfile userProfile)
        {
            string sql = "[dbo].[SP_User_Insert]";

            IDbDataParameter[] parameter = new[]
            {
             _dataBaseManager.CreateParameter("UserId", userProfile.@UserId,DbType.String),
             _dataBaseManager.CreateParameter("FirstName", userProfile.@FirstName, DbType.String),
             _dataBaseManager.CreateParameter("LastName", userProfile.@LastName, DbType.String),
             _dataBaseManager.CreateParameter("Email", userProfile.@Email,DbType.String),
             _dataBaseManager.CreateParameter("Password",userProfile.Password,DbType.String),
             _dataBaseManager.CreateParameter("IsActive", userProfile.@IsActive,DbType.Byte),
             _dataBaseManager.CreateParameter("IsDeleted",userProfile.IsDeleted,DbType.Byte),
             _dataBaseManager.CreateParameter("CreatedOn", userProfile.@CreatedOn,DbType.DateTime),
             _dataBaseManager.CreateParameter("CreatedBy", userProfile.@CreatedBy,DbType.String),
             _dataBaseManager.CreateParameter("LastModifiedOn",userProfile.@LastModifiedOn,DbType.DateTime),
             _dataBaseManager.CreateParameter("LastModifiedBy",userProfile.@LastModifiedBy,DbType.String),
             _dataBaseManager.CreateParameter("RefreshToken", userProfile.@RefreshToken,DbType.String)
            };
            _dataBaseManager.Insert(sql, CommandType.StoredProcedure, parameter);
        }
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            string sql = $"Select * from userProfileTbl";
            var dataTable = _dataBaseManager.GetDataTable(sql, CommandType.Text, null);
            return ConversionHelper.ConvertToObject<UserProfile>(dataTable); 
        }
        public IEnumerable GetUserById(string userId)
        {
            UserProfile userProfile = new UserProfile();
            string sql = $"Select * from userProfileTbl where UserId = @userId";
            IDbDataParameter[] parameter = new[]
            {
                _dataBaseManager.CreateParameter("UserId", @userId, DbType.String)
            };
            var dataTable = _dataBaseManager.GetDataTable(sql, CommandType.Text, parameter);
            return ConversionHelper.ConvertToObject<UserProfile>(dataTable);
        }
        public void UpdateUserProfile(UserProfile userProfile)
        {
            string sql = "[dbo].[SP_User_Update]";
            IDbDataParameter[] parameters = new[]
            {
               _dataBaseManager.CreateParameter("FirstName", userProfile.@FirstName, DbType.String),
               _dataBaseManager.CreateParameter("LastName", userProfile.@LastName, DbType.String),
               _dataBaseManager.CreateParameter("Email", userProfile.@Email,DbType.String),
               //_dataBaseManager.CreateParameter("Passsword", userProfile.@Password,DbType.String)
           };
            _dataBaseManager.Update(sql, CommandType.StoredProcedure, parameters);
        }
        public void DeleteUserProfile(string userId)
        {
            string sql = $"Delete from userProfileTbl where UserId= @userId";
            IDbDataParameter[] parameters = new[]
            {
                _dataBaseManager.CreateParameter("UserId",@userId,DbType.String)
            };
            _dataBaseManager.Delete(sql, CommandType.Text, parameters);
        }
    }
}
