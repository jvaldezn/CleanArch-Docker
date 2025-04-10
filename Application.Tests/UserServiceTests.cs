using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interface;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Enums;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Transversal.Common;
using Transversal.Common.Helper;
using Transversal.Common.Interfaces;

namespace Application.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger<UserService>>();
            _userService = new UserService(_mockUnitOfWork.Object, _mockMapper.Object, _mockUserRepository.Object, _mockLogger.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsEmptyList()
        {
            _mockUserRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(new List<User>());
            var result = await _userService.GetAllUsers();

            Assert.True(result.IsSuccess);
            Assert.Equal(Messages.NoUsersFound, result.Message);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser()
        {
            var user = new User { Id = 1, Username = "admin", Email = "admin@admin.com", Password = "Admin123!", DateOfBirth = new DateTime(1990, 1, 1), Role = UserRole.Admin };
            var userDto = new UserDto { Id = 1, Username = "admin", Email = "admin@admin.com", Password = "Admin123!", DateOfBirth = new DateTime(1990, 1, 1), Role = UserRole.Admin };

            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);
            _mockUserRepository.Setup(u => u.GetByIdAsync(1)).ReturnsAsync(user);

            var result = await _userService.GetUserById(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(userDto.Username, result.Data?.Username);
        }

        [Fact]
        public async Task CreateUser_ReturnsSuccess()
        {
            var user = new User { Id = 1, Username = "admin", Email = "admin@admin.com", Password = "Admin123!", DateOfBirth = new DateTime(1990, 1, 1), Role = UserRole.Admin };
            var userDto = new UserDto { Id = 1, Username = "admin", Email = "admin@admin.com", Password = "Admin123!", DateOfBirth = new DateTime(1990, 1, 1), Role = UserRole.Admin };

            var mockTransaction = new Mock<IDbContextTransaction>();

            _mockUnitOfWork.Setup(u => u.BeginTransactionAsync()).ReturnsAsync(mockTransaction.Object);

            _mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);
            _mockUserRepository.Setup(r => r.AddAsync(user)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(1);
            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            var result = await _userService.CreateUser(userDto);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal(Messages.UserCreated, result.Message);

            _mockUnitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
