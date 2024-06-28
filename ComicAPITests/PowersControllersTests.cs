using AutoMapper;
using ComicLoreApi.Controllers;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;
using ComicLoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicAPITests
{
    public class PowersControllerTests
    {
        private readonly Mock<IPowerRepository> _mockPowerRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<PowersController>> _mockLogger;
        private readonly PowersController _controller;

        public PowersControllerTests()
        {
            _mockPowerRepository = new Mock<IPowerRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<PowersController>>();

            _controller = new PowersController(
                _mockPowerRepository.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task GetPowers_ReturnsOkWithPowerDtos()
        {
            // Arrange
            var mockPowers = new List<Power>
            {
                new Power { Id = 1, Name = "Flight" },
                new Power { Id = 2, Name = "Super Strength" }
            };

            _mockPowerRepository.Setup(repo => repo.getAllPowersAsync()).ReturnsAsync(mockPowers);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PowerDto>>(mockPowers)).Returns(new List<PowerDto>
            {
                new PowerDto { Id = 1, Name = "Flight" },
                new PowerDto { Id = 2, Name = "Super Strength" }
            });

            // Act
            var result = await _controller.GetPowers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var powerDtos = Assert.IsAssignableFrom<IEnumerable<PowerDto>>(okResult.Value);
        }

        [Fact]
        public async Task GetPowerById_ExistingId_ReturnsOkWithPowerDto()
        {
            // Arrange
            var powerId = 1;
            var mockPower = new Power { Id = powerId, Name = "Flight" };

            _mockPowerRepository.Setup(repo => repo.getPowerByIdAsync(powerId)).ReturnsAsync(mockPower);
            _mockMapper.Setup(mapper => mapper.Map<PowerDto>(mockPower)).Returns(new PowerDto
            {
                Id = powerId,
                Name = "Flight"
            });

            // Act
            var result = await _controller.GetPowerById(powerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var powerDto = Assert.IsType<PowerDto>(okResult.Value);
            Assert.Equal(mockPower.Id, powerDto.Id);
            Assert.Equal(mockPower.Name, powerDto.Name);
        }

        [Fact]
        public async Task GetPowerById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var powerId = 999; // Assuming this ID does not exist

            _mockPowerRepository.Setup(repo => repo.getPowerByIdAsync(powerId)).ReturnsAsync((Power)null);

            // Act
            var result = await _controller.GetPowerById(powerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddPower_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var powerDto = new PowerDto { Name = "Telekinesis" };
            var createdPower = new Power { Id = 1, Name = "Telekinesis" };

            _mockMapper.Setup(mapper => mapper.Map<Power>(powerDto)).Returns(createdPower);

            // Act
            var result = await _controller.AddPower(powerDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result); // Ensure to access .Result here
        }

        [Fact]
        public async Task UpdatePower_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var powerId = 1;
            var updatedPower = new Power { Id = powerId, Name = "Updated Power" };

            _mockPowerRepository.Setup(repo => repo.getPowerByIdAsync(powerId)).ReturnsAsync(updatedPower);

            // Act
            var result = await _controller.UpdatePower(powerId, updatedPower);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdatePower_NonExistingId_ReturnsBadRequest()
        {
            // Arrange
            var powerId = 999; // Assuming this ID does not exist
            var updatedPower = new Power { Id = 1, Name = "Updated Power" };

            // Act
            var result = await _controller.UpdatePower(powerId, updatedPower);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletePower_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var powerId = 1;
            var mockPower = new Power { Id = powerId, Name = "Flight" };

            _mockPowerRepository.Setup(repo => repo.getPowerByIdAsync(powerId)).ReturnsAsync(mockPower);

            // Act
            var result = await _controller.DeletePower(powerId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePower_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var powerId = 999; // Assuming this ID does not exist

            _mockPowerRepository.Setup(repo => repo.getPowerByIdAsync(powerId)).ReturnsAsync((Power)null);

            // Act
            var result = await _controller.DeletePower(powerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
