using FluentAssertions;
using Moq;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Tests.Mocks;
using Xunit;

namespace VacationPlannerPro.Tests.Services
{
    public class ProfessionServiceTests : TestBase
    {
        private readonly ProfessionService _service;

        public ProfessionServiceTests()
        {
            _service = new ProfessionService(UnitOfWorkMock.Object, MapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrderedProfessions()
        {
            var professions = new List<Profession>
            {
                new Profession { Name = "Data Scientist" },
                new Profession { Name = "Software Engineer" }
            };
            UnitOfWorkMock.Setup(uow => uow.Professions.GetAllAsync()).ReturnsAsync(professions);

            var orderedDtos = new List<ProfessionDTO>
            {
                new ProfessionDTO { Name = "Data Scientist" },
                new ProfessionDTO { Name = "Software Engineer" }
            };
            MapperMock.Setup(map => map.Map<IEnumerable<ProfessionDTO>>(It.IsAny<IEnumerable<Profession>>()))
                      .Returns(orderedDtos);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Data Scientist", result.First().Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProfession_WhenExists()
        {
            var profession = new Profession { Id = Guid.NewGuid(), Name = "Software Engineer" };
            UnitOfWorkMock.Setup(uow => uow.Professions.GetByIdAsync(profession.Id)).ReturnsAsync(profession);

            MapperMock.Setup(map => map.Map<ProfessionDTO>(profession))
                      .Returns(new ProfessionDTO { Name = "Software Engineer" });

            var result = await _service.GetByIdAsync(profession.Id);

            Assert.NotNull(result);
            Assert.Equal("Software Engineer", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            UnitOfWorkMock.Setup(uow => uow.Professions.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Profession)null);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddProfession()
        {
            var createDto = new ProfessionDTO { Name = "Test Profession" };
            var profession = new Profession { Name = "Test Profession" };
            MapperMock.Setup(map => map.Map<Profession>(createDto)).Returns(profession);

            await _service.CreateAsync(createDto);

            UnitOfWorkMock.Verify(uow => uow.Professions.AddAsync(It.IsAny<Profession>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProfession()
        {
            var existingProfession = new Profession { Id = Guid.NewGuid(), Name = "Old Profession" };
            var updateDto = new ProfessionDTO { Id = existingProfession.Id, Name = "Updated Profession" };

            UnitOfWorkMock.Setup(uow => uow.Professions.GetByIdAsync(existingProfession.Id)).ReturnsAsync(existingProfession);

            await _service.UpdateAsync(updateDto);

            UnitOfWorkMock.Verify(uow => uow.Professions.UpdateAsync(existingProfession), Times.Once);
            existingProfession.Name.Should().Be("Updated Profession");
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_IfProfessionNotFound()
        {
            var updateDto = new ProfessionDTO { Id = Guid.NewGuid(), Name = "Updated Profession" };
            UnitOfWorkMock.Setup(uow => uow.Professions.GetByIdAsync(updateDto.Id)).ReturnsAsync((Profession)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(updateDto));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteProfession()
        {
            var profession = new Profession { Id = Guid.NewGuid(), Name = "Test Profession" };
            UnitOfWorkMock.Setup(uow => uow.Professions.GetByIdAsync(profession.Id)).ReturnsAsync(profession);

            await _service.DeleteAsync(profession.Id);

            UnitOfWorkMock.Verify(uow => uow.Professions.DeleteAsync(profession), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_IfProfessionNotFound()
        {
            UnitOfWorkMock.Setup(uow => uow.Professions.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Profession)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(Guid.NewGuid()));
        }
    }
}
