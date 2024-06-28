using ComicLoreApi.Controllers;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;
using ComicLoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicAPITests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UsersController(_mockUserService.Object);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var userDto = new UserRegisterDto { UserName = "testuser", PassWord = "password123" };
            _mockUserService.Setup(service => service.Register(userDto))
                            .ReturnsAsync(new User { Id = 1, UserName = userDto.UserName });

            // Act
            var result = await _controller.Register(userDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var loginDto = new UserLoginDto { UserName = "testuser", PassWord = "password123" };
            var token = "mocked-jwt-token";
            _mockUserService.Setup(service => service.Login(loginDto))
                            .ReturnsAsync((true, token));

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(token, okResult.Value); // Ensure token returned
        }
    }
}
