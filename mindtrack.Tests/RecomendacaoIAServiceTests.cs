using mindtrack.DTO.Request;
using mindtrack.Model;
using mindtrack.Repository;
using mindtrack.Service;
using Moq;


namespace mindtrack.Tests
{
    public class RecomendacaoIAServiceTests
    {
       
        private readonly Mock<IRecomendacaoIARepository> _repositoryMock;
        private readonly RecomendacaoIAService _service;

        public RecomendacaoIAServiceTests()
        {
            _repositoryMock = new Mock<IRecomendacaoIARepository>();
            _service = new RecomendacaoIAService(_repositoryMock.Object);
        }

        [Fact]
        public async Task SaveAsync_DeveRetornarResponse_QuandoSucesso()
        {
            // Arrange (Preparação)
            var dto = new RecomendacaoIADto
            {
                Texto = "Teste IA",
                IdUser = 1,
                IdCheckin = 10,
                DataCriacao = DateTime.Now
            };

            // Simulamos que ao chamar AddAsync, o repositório retorna a entidade com ID preenchido
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<RecomendacaoIA>()))
                .ReturnsAsync((RecomendacaoIA entity) =>
                {
                    entity.IdRecomendacao = 99; // ID simulado do banco
                    return entity;
                });

            // Act (Ação)
            var result = await _service.SaveAsync(dto);

            // Assert (Verificação)
            Assert.NotNull(result);
            Assert.Equal(99, result.IdRecomendacao);
            Assert.Equal(dto.Texto, result.Texto);


            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<RecomendacaoIA>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarObjeto_QuandoEncontrado()
        {
   
            var entity = new RecomendacaoIA { IdRecomendacao = 1, Texto = "Teste" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

   
            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.IdRecomendacao);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {

            _repositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((RecomendacaoIA)null);

            var result = await _service.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarExcecao_QuandoIdNaoExiste()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((RecomendacaoIA)null);
            var dto = new RecomendacaoIADto { Texto = "Update" };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(1, dto));
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizar_QuandoIdExiste()
        {
            var existing = new RecomendacaoIA { IdRecomendacao = 1, Texto = "Antigo" };
            var dto = new RecomendacaoIADto { Texto = "Novo", IdUser = 2 };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<RecomendacaoIA>()))
                .ReturnsAsync((RecomendacaoIA r) => r); // Retorna o próprio objeto atualizado

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal("Novo", result.Texto);
            Assert.Equal(2, result.IdUser); // Verifica se mapeou os campos novos
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<RecomendacaoIA>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveChamarDelete_QuandoIdExiste()
        {
            // Arrange
            var existing = new RecomendacaoIA { IdRecomendacao = 1 };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(existing), Times.Once);
        }
    }
}
