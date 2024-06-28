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
    public class SupesControllerTests
    {
        // Unit tests for SupesController actions

        private readonly Mock<ISupeRepository> _mockSupeRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ISupeService> _mockSupeService;
        private readonly Mock<ILogger<SupesController>> _mockLogger;
        private readonly SupesController _controller;

        public SupesControllerTests()
        {
            _mockSupeRepository = new Mock<ISupeRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockSupeService = new Mock<ISupeService>();
            _mockLogger = new Mock<ILogger<SupesController>>();

            _controller = new SupesController(
                _mockSupeRepository.Object,
                _mockMapper.Object,
                _mockSupeService.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task AddSupe_ValidInput_ReturnsCreatedAtAction()
        {
            // Arrange
            var supeForCreationDto = new SupeForUpdateDto
            {
                FirstName = "Clark",
                LastName = "Kent",
                Alias = "Superman",
                DateOfBirth = new System.DateTime(1938, 6, 1),
                Origin = "Krypton"
            };

            var expectedSupe = new Supe
            {
                Id = 1,
                FirstName = supeForCreationDto.FirstName,
                LastName = supeForCreationDto.LastName,
                Alias = supeForCreationDto.Alias,
                DateOfBirth = supeForCreationDto.DateOfBirth,
                Origin = supeForCreationDto.Origin
            };

            _mockMapper.Setup(mapper => mapper.Map<Supe>(supeForCreationDto)).Returns(expectedSupe);

            // Act
            var result = await _controller.AddSupe(supeForCreationDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var supeDto = Assert.IsType<Supe>(createdAtActionResult.Value);
            Assert.Equal(expectedSupe.Id, supeDto.Id);
            Assert.Equal(expectedSupe.FirstName, supeDto.FirstName);
            Assert.Equal(expectedSupe.LastName, supeDto.LastName);
            Assert.Equal(expectedSupe.Alias, supeDto.Alias);

            // Optionally, verify the route values
            Assert.Equal(nameof(SupesController.GetSupeById), createdAtActionResult.ActionName);
            Assert.Equal(expectedSupe.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task GetSupes_ReturnsOkWithSupes()
        {
            // Arrange
            var mockSupes = new List<Supe>
            {
                new Supe { Id = 1, FirstName = "Clark", LastName = "Kent", Alias = "Superman", DateOfBirth = new DateTime(1938, 6, 1), Origin = "Krypton" },
                new Supe { Id = 2, FirstName = "Bruce", LastName = "Wayne", Alias = "Batman", DateOfBirth = new DateTime(1939, 5, 1), Origin = "Gotham City" }
            };
            _mockSupeRepository.Setup(repo => repo.getAllSupesAsync()).ReturnsAsync(mockSupes);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<SupeWithPowersDto>>(mockSupes)).Returns(new List<SupeWithPowersDto>
            {
                new SupeWithPowersDto { Id = 1, FirstName = "Clark", LastName = "Kent", Alias = "Superman", DateOfBirth = new DateTime(1938, 6, 1), Origin = "Krypton" },
                new SupeWithPowersDto { Id = 2, FirstName = "Bruce", LastName = "Wayne", Alias = "Batman", DateOfBirth = new DateTime(1939, 5, 1), Origin = "Gotham City" }
            });

            // Act
            var result = await _controller.GetSupes();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var supesDto = Assert.IsAssignableFrom<IEnumerable<SupeWithPowersDto>>(okResult.Value);
            Assert.Multiple(() =>
            {
                Assert.Equal(2, supesDto.Count());
                Assert.Equal("Clark", supesDto.First().FirstName);
                Assert.Equal("Kent", supesDto.First().LastName);
                Assert.Equal("Superman", supesDto.First().Alias);
                Assert.Equal(new DateTime(1938, 6, 1), supesDto.First().DateOfBirth);
                Assert.Equal("Krypton", supesDto.First().Origin);
            });
        }

        [Fact]
        public async Task GetSupes_ReturnsNotFoundWhenNoSupes()
        {
            // Arrange
            _mockSupeRepository.Setup(repo => repo.getAllSupesAsync()).ReturnsAsync((List<Supe>)null);

            // Act
            var result = await _controller.GetSupes();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetSupeById_ReturnsOkWithSupeDto()
        {
            // Arrange
            var supeId = 1;
            var mockSupe = new Supe { Id = 1, FirstName = "Clark", LastName = "Kent", Alias = "Superman", DateOfBirth = new DateTime(1938, 6, 1), Origin = "Krypton" };
            var expectedSupeDto = new SupeWithPowersDto
            {
                Id = 1,
                FirstName = "Clark",
                LastName = "Kent",
                Alias = "Superman",
                DateOfBirth = new DateTime(1938, 6, 1),
                Origin = "Krypton"
            };

            _mockSupeRepository.Setup(repo => repo.getSupeByIdAsync(supeId)).ReturnsAsync(mockSupe);
            _mockMapper.Setup(mapper => mapper.Map<SupeWithPowersDto>(mockSupe)).Returns(expectedSupeDto);

            // Act
            var result = await _controller.GetSupeById(supeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var supeDto = Assert.IsType<SupeWithPowersDto>(okResult.Value);
            Assert.Equal(expectedSupeDto.Id, supeDto.Id);
            Assert.Equal(expectedSupeDto.FirstName, supeDto.FirstName);
            Assert.Equal(expectedSupeDto.LastName, supeDto.LastName);
            Assert.Equal(expectedSupeDto.Alias, supeDto.Alias);
            Assert.Equal(expectedSupeDto.DateOfBirth, supeDto.DateOfBirth);
            Assert.Equal(expectedSupeDto.Origin, supeDto.Origin);
        }

        [Fact]
        public async Task GetSupeById_ReturnsNotFoundWhenSupeNotFound()
        {
            // Arrange
            var supeId = 1;
            _mockSupeRepository.Setup(repo => repo.getSupeByIdAsync(supeId)).ReturnsAsync((Supe)null);

            // Act
            var result = await _controller.GetSupeById(supeId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateSupe_ValidInput_ReturnsNoContent()
        {
            // Arrange
            var supeId = 1;
            var supeForUpdateDto = new SupeForUpdateDto
            {
                Alias = "Updated Alias"
            };

            var existingSupe = new Supe
            {
                Id = supeId,
                Alias = "Original Alias"
                // Include other properties as needed for your entity
            };

            _mockSupeRepository.Setup(repo => repo.getSupeByIdAsync(supeId)).ReturnsAsync(existingSupe);

            // Act
            var result = await _controller.UpdateSupe(supeId, supeForUpdateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(supeForUpdateDto.Alias, existingSupe.Alias); // Verify the update was applied
        }

        [Fact]
        public async Task UpdateSupe_NonExistingSupe_ReturnsNotFound()
        {
            // Arrange
            var supeId = 999; // Assuming this ID does not exist
            var supeForUpdateDto = new SupeForUpdateDto
            {
                Alias = "Updated Alias"
            };

            _mockSupeRepository.Setup(repo => repo.getSupeByIdAsync(supeId)).ReturnsAsync((Supe)null);

            // Act
            var result = await _controller.UpdateSupe(supeId, supeForUpdateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteSupe_ExistingSupe_ReturnsNoContent()
        {
            // Arrange
            var supeId = 1;
            _mockSupeRepository.Setup(repo => repo.getSupeByIdAsync(supeId)).ReturnsAsync(new Supe { Id = supeId });

            // Act
            var result = await _controller.DeleteSupe(supeId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSupe_NonExistingSupe_ReturnsNotFound()
        {
            // Arrange
            var supeId = 1;
            _mockSupeRepository.Setup(repo => repo.getSupeByIdAsync(supeId)).ReturnsAsync((Supe)null);

            // Act
            var result = await _controller.DeleteSupe(supeId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Example for testing error handling
        [Fact]
        public async Task GetSupeById_NonExistingSupe_ReturnsNotFound()
        {
            // Arrange
            var supeId = 999; // Assuming this ID does not exist
            _mockSupeRepository.Setup(repo => repo.getSupeByIdAsync(supeId)).ReturnsAsync((Supe)null);

            // Act
            var result = await _controller.GetSupeById(supeId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
