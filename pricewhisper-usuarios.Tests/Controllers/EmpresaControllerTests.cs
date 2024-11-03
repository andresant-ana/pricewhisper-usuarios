using Application.DTOs;
using Application.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace pricewhisper_usuarios.Tests.Controllers
{
    public class EmpresaControllerTests
    {
        private readonly EmpresaController _controller;
        private readonly Mock<IEmpresaService> _mockEmpresaService;

        public EmpresaControllerTests()
        {
            _mockEmpresaService = new Mock<IEmpresaService>();
            _controller = new EmpresaController(_mockEmpresaService.Object);
        }

        [Fact]
        public async Task Get_ReturnsAllEmpresas()
        {
            // Arrange
            var mockEmpresas = new List<EmpresaDto>
            {
                new EmpresaDto { EmpresaId = 1, CNPJ = "12345678901234", RazaoSocial = "Empresa A", NomeFantasia = "Fantasia A", Usuarios = new List<UsuarioDto>() },
                new EmpresaDto { EmpresaId = 2, CNPJ = "98765432109876", RazaoSocial = "Empresa B", NomeFantasia = "Fantasia B", Usuarios = new List<UsuarioDto>() }
            };

            _mockEmpresaService.Setup(service => service.GetAllAsync()).ReturnsAsync(mockEmpresas);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnEmpresas = Assert.IsAssignableFrom<IEnumerable<EmpresaDto>>(okResult.Value);
            Assert.Equal(2, ((List<EmpresaDto>)returnEmpresas).Count);
        }

        [Fact]
        public async Task GetById_ReturnsEmpresa_WhenEmpresaExists()
        {
            // Arrange
            var empresaId = 1;
            var mockEmpresa = new EmpresaDto { EmpresaId = empresaId, CNPJ = "12345678901234", RazaoSocial = "Empresa A", NomeFantasia = "Fantasia A", Usuarios = new List<UsuarioDto>() };

            _mockEmpresaService.Setup(service => service.GetByIdAsync(empresaId)).ReturnsAsync(mockEmpresa);

            // Act
            var result = await _controller.GetById(empresaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnEmpresa = Assert.IsType<EmpresaDto>(okResult.Value);
            Assert.Equal(empresaId, returnEmpresa.EmpresaId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenEmpresaDoesNotExist()
        {
            // Arrange
            var empresaId = 1;
            _mockEmpresaService.Setup(service => service.GetByIdAsync(empresaId)).ReturnsAsync((EmpresaDto?)null);

            // Act
            var result = await _controller.GetById(empresaId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WhenEmpresaIsCreated()
        {
            // Arrange
            var empresaDto = new EmpresaDto { CNPJ = "12345678901234", RazaoSocial = "Empresa A", NomeFantasia = "Fantasia A", Usuarios = new List<UsuarioDto>() };

            _mockEmpresaService.Setup(service => service.AddAsync(empresaDto)).Returns(Task.CompletedTask).Callback(() => empresaDto.EmpresaId = 1);

            // Act
            var result = await _controller.Create(empresaDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnEmpresa = Assert.IsType<EmpresaDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnEmpresa.EmpresaId);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenEmpresaIsUpdated()
        {
            // Arrange
            var empresaId = 1;
            var empresaDto = new EmpresaDto { EmpresaId = empresaId, CNPJ = "12345678901234", RazaoSocial = "Empresa A", NomeFantasia = "Fantasia A", Usuarios = new List<UsuarioDto>() };

            _mockEmpresaService.Setup(service => service.UpdateAsync(empresaDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(empresaId, empresaDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var empresaId = 1;
            var empresaDto = new EmpresaDto { EmpresaId = 2, CNPJ = "12345678901234", RazaoSocial = "Empresa A", NomeFantasia = "Fantasia A", Usuarios = new List<UsuarioDto>() };

            // Act
            var result = await _controller.Update(empresaId, empresaDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID da URL não coincide com o ID da empresa.", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenEmpresaIsDeleted()
        {
            // Arrange
            var empresaId = 1;
            _mockEmpresaService.Setup(service => service.DeleteAsync(empresaId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(empresaId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}