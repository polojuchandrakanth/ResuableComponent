using System.Net;
using Feature.API.Controllers;
using Feature.BusinessModel.Common;
using Feature.Entity.Entities;
using Feature.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Feature.UnitTests
{
    public class UserServiceTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<UserController>> _logger;
        private UserController _userControllerTest;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _logger = new Mock<ILogger<UserController>>();
            _userControllerTest = new UserController(_logger.Object, _userServiceMock.Object);
        }
        [Test]
        public async Task GetAllReturnsSuccess()
        {
            List<UserProfile> mockUsers = new List<UserProfile>();
            mockUsers.Add(new UserProfile {
                Id = 1,
                UserId = "UserId1",
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Password = "Password",
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "",
                CreatedOn = DateTime.Now,
                LastModifiedBy = "",
                LastModifiedOn = DateTime.Now,
                RefreshToken = "RefreshToken"
            });
            _userServiceMock.Setup(m => m.GetAll()).Returns(mockUsers);
            var result = _userControllerTest.GetAll();
            Assert.IsTrue(result.Any());
        }
        [Test]
        public async Task GetAll_NotReturnsSuccessfully()
        {
            _userServiceMock.Setup(m => m.GetAll());
            var result = _userControllerTest.GetAll();
            Assert.IsTrue(!result.Any());
        }
        [Test]
        public async Task GetById_SuccessfullyReturnsUser()
        {
            _userServiceMock.Setup(m => m.GetById(1)).Returns(Task.FromResult(GetById(1)));
            var result = await _userControllerTest.GetById(1);
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetById_NoContent()
        {
            _userServiceMock.Setup(m => m.GetById(15));
            var result = await _userControllerTest.GetById(15);
            Assert.That(result, Is.Null);
        }
        [Test]
        public async Task CreateUser_SuccessfullyReturnsOkObject()
        {
            var user = new UserProfile()
            {
                Id = 1,
                UserId = "UserId1",
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Password = "Password",
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "FirstName",
                CreatedOn = DateTime.Now,
                LastModifiedBy = "FirstName",
                LastModifiedOn = DateTime.Now,
                RefreshToken = "RefreshToken"
            };
            _userServiceMock.Setup(m => m.Insert(It.IsAny<UserProfile>()));
            var result = await _userControllerTest.CreateUser(user);
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public async Task CreateUser_Duplicated()
        {
            var user = new UserProfile()
            {
                Id = 1,
                UserId = "UserId1",
                FirstName = "FirstName",
                LastName = "LastName"
            };
            //_userServiceMock.Setup(m => m.Insert(It.IsAny<UserProfile>()));
           await _userControllerTest.CreateUser(user);//Creatin user 1 time            
            try
            {
                var result = await _userControllerTest.CreateUser(user);
                Assert.Fail("");
            }
            catch (Exception ex) 
            { 
                Assert.Pass(""); 
            }

            
        }
        [Test]
        public async Task CreateUser_NotReturnsSuccessfully()
        {
            var user = new UserProfile()
            {
                Id = 1,
                UserId = "UserId1",
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Password = "Password",
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "",
                CreatedOn = DateTime.Now,
                LastModifiedBy = "",
                LastModifiedOn = DateTime.Now,
                RefreshToken = "RefreshToken"
            };
            _userServiceMock.Setup(m => m.Insert(It.IsAny<UserProfile>()));
            var result = await _userControllerTest.CreateUser(user);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task UpdateUser_ReturnsSuccessfully()
        {
            var user = new UserProfile()
            {
                Id = 1,
                FirstName = "FirstName1",
                LastName = "LastName1"
            };
            _userServiceMock.Setup(m => m.Update(It.IsAny<UserProfile>()));
            var result = await _userControllerTest.UpdateUser(user);
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task UpdateUser_ReturnsNotSuccessfull()
        {
            var user = new UserProfile()
            {
                Id = 11,
                FirstName = "FirstName1",
                LastName = "LastName1"
            };
            _userServiceMock.Setup(m => m.Update(It.IsAny<UserProfile>()));
            var result = await _userControllerTest.UpdateUser(user);
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task DeleteUser_ReturnsSuccessful()
        {
            _userServiceMock.Setup(m => m.Delete(5));
            var result = await _userControllerTest.DeleteUser(5);
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public async Task DeleteUser_NotReturnsSuccessful()
        {
            _userServiceMock.Setup(m => m.Delete(1));
            var result = await _userControllerTest.DeleteUser(1);
            Assert.That(result,Is.Not.Null);
        }
        public UserProfile GetById(int id)
        {
            return new UserProfile()
            {
                Id = 1,
                UserId = "QE1234",
                FirstName = "Sraddha",
                LastName = "Chaganti",
                Email = "sraddha.chaganti@gmail.com",
                Password = "Siri@1234",
                RefreshToken = null,
                IsActive = true,
                IsDeleted = false,
                CreatedOn = DateTime.Now,
                CreatedBy = "",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = ""
            };
        }
    }
}