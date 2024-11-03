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
    public class UsuarioControllerTests
    {
        private readonly UsuarioController _controller;
        private readonly Mock<IUsuarioService> _mockUsuarioService;

        public UsuarioControllerTests()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _controller = new UsuarioController(_mockUsuarioService.Object);
        }

        [Fact]
        public async Task Get_ReturnsAllUsuarios()
        {
            // Arrange
            var mockUsuarios = new List<UsuarioDto>
            {
                new UsuarioDto { UsuarioId = 1, Nome = "João Silva", NomeUsuario = "joaosilva", EmpresaId = 1, RazaoSocialEmpresa = "Empresa A" },
                new UsuarioDto { UsuarioId = 2, Nome = "Maria Santos", NomeUsuario = "mariasantos", EmpresaId = 1, RazaoSocialEmpresa = "Empresa A" }
            };

            _mockUsuarioService.Setup(service => service.GetAllAsync()).ReturnsAsync(mockUsuarios);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsuarios = Assert.IsAssignableFrom<IEnumerable<UsuarioDto>>(okResult.Value);
            Assert.Equal(2, ((List<UsuarioDto>)returnUsuarios).Count);
        }

        [Fact]
        public async Task GetById_ReturnsUsuario_WhenUsuarioExists()
        {
            // Arrange
            var usuarioId = 1;
            var mockUsuario = new UsuarioDto { UsuarioId = usuarioId, Nome = "João Silva", NomeUsuario = "joaosilva", EmpresaId = 1, RazaoSocialEmpresa = "Empresa A" };

            _mockUsuarioService.Setup(service => service.GetByIdAsync(usuarioId)).ReturnsAsync(mockUsuario);

            // Act
            var result = await _controller.GetById(usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsuario = Assert.IsType<UsuarioDto>(okResult.Value);
            Assert.Equal(usuarioId, returnUsuario.UsuarioId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            var usuarioId = 1;
            _mockUsuarioService.Setup(service => service.GetByIdAsync(usuarioId)).ReturnsAsync((UsuarioDto?)null);

            // Act
            var result = await _controller.GetById(usuarioId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WhenUsuarioIsCreated()
        {
            // Arrange
            var usuarioDto = new UsuarioDto { Nome = "João Silva", NomeUsuario = "joaosilva", Senha = "senha123", EmpresaId = 1, RazaoSocialEmpresa = "Empresa A" };

            _mockUsuarioService.Setup(service => service.AddAsync(usuarioDto)).Returns(Task.CompletedTask)
                .Callback(() => { usuarioDto.UsuarioId = 1; });

            // Act
            var result = await _controller.Create(usuarioDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnUsuario = Assert.IsType<UsuarioDto>(createdAtActionResult.Value);
            Assert.Equal(1, returnUsuario.UsuarioId);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenUsuarioIsUpdated()
        {
            // Arrange
            var usuarioId = 1;
            var usuarioDto = new UsuarioDto { UsuarioId = usuarioId, Nome = "João Silva Atualizado", NomeUsuario = "joaosilva", Senha = "novaSenha123", EmpresaId = 1, RazaoSocialEmpresa = "Empresa A" };

            _mockUsuarioService.Setup(service => service.UpdateAsync(usuarioDto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(usuarioId, usuarioDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var usuarioId = 1;
            var usuarioDto = new UsuarioDto { UsuarioId = 2, Nome = "João Silva", NomeUsuario = "joaosilva", Senha = "senha123", EmpresaId = 1, RazaoSocialEmpresa = "Empresa A" };

            // Act
            var result = await _controller.Update(usuarioId, usuarioDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID da URL não coincide com o ID do usuário.", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenUsuarioIsDeleted()
        {
            // Arrange
            var usuarioId = 1;
            _mockUsuarioService.Setup(service => service.DeleteAsync(usuarioId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(usuarioId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}