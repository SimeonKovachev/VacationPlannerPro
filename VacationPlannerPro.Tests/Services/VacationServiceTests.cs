using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using VacationPlannerPro.Business.DTOs.VacationDTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Tests.Mocks;
using Xunit;

namespace VacationPlannerPro.Tests.Services
{
    public class VacationServiceTests : TestBase
    {
        private readonly VacationService _service;

        public VacationServiceTests()
        {
            _service = new VacationService(UnitOfWorkMock.Object, MapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllVacations()
        {
            var vacations = new List<Vacation>
            {
                new Vacation { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(5) },
                new Vacation { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-5) }
            };

            UnitOfWorkMock.Setup(uow => uow.Vacations.GetAllAsync()).ReturnsAsync(vacations);

            var vacationDtos = new List<VacationDTO>
            {
                new VacationDTO { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(5) },
                new VacationDTO { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-5) }
            };

            MapperMock.Setup(map => map.Map<IEnumerable<VacationDTO>>(vacations)).Returns(vacationDtos);

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnVacation_WhenExists()
        {
            var vacationId = Guid.NewGuid();
            var vacation = new Vacation { Id = vacationId, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) };

            UnitOfWorkMock.Setup(uow => uow.Vacations.GetEntityWithNavigationPropertyByIdAsync<Vacation, Worker>(
                vacationId, It.IsAny<Expression<Func<Vacation, Worker>>>())).ReturnsAsync(vacation);

            var vacationDto = new VacationDTO { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) };
            MapperMock.Setup(map => map.Map<VacationDTO>(vacation)).Returns(vacationDto);

            var result = await _service.GetByIdAsync(vacationId);

            result.Should().NotBeNull();
            result.StartDate.Should().Be(vacation.StartDate);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenVacationDoesNotExist()
        {
            UnitOfWorkMock.Setup(uow => uow.Vacations.GetEntityWithNavigationPropertyByIdAsync<Vacation, Worker>(
                It.IsAny<Guid>(), It.IsAny<Expression<Func<Vacation, Worker>>>())).ReturnsAsync((Vacation)null);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldAddVacation()
        {
            var createDto = new CreateVacationDTO { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) };
            var vacation = new Vacation { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) };

            MapperMock.Setup(map => map.Map<Vacation>(createDto)).Returns(vacation);

            await _service.CreateAsync(createDto);

            UnitOfWorkMock.Verify(uow => uow.Vacations.AddAsync(It.IsAny<Vacation>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateVacation_WhenVacationExists()
        {
            var vacationId = Guid.NewGuid();
            var vacation = new Vacation { Id = vacationId, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) };
            var updateDto = new UpdateVacationDTO { Id = vacationId, StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(4) };

            UnitOfWorkMock.Setup(uow => uow.Vacations.GetByIdAsync(vacationId)).ReturnsAsync(vacation);

            await _service.UpdateAsync(updateDto);

            UnitOfWorkMock.Verify(uow => uow.Vacations.UpdateAsync(vacation), Times.Once);
            vacation.StartDate.Should().Be(updateDto.StartDate);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenVacationDoesNotExist()
        {
            var updateDto = new UpdateVacationDTO { Id = Guid.NewGuid(), StartDate = DateTime.Now };

            UnitOfWorkMock.Setup(uow => uow.Vacations.GetByIdAsync(updateDto.Id)).ReturnsAsync((Vacation)null);

            Func<Task> act = async () => await _service.UpdateAsync(updateDto);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Vacation not found.");
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteVacation_WhenVacationExists()
        {
            var vacationId = Guid.NewGuid();
            var vacation = new Vacation { Id = vacationId };

            UnitOfWorkMock.Setup(uow => uow.Vacations.GetByIdAsync(vacationId)).ReturnsAsync(vacation);

            await _service.DeleteAsync(vacationId);

            UnitOfWorkMock.Verify(uow => uow.Vacations.DeleteAsync(vacation), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenVacationDoesNotExist()
        {
            var vacationId = Guid.NewGuid();

            UnitOfWorkMock.Setup(uow => uow.Vacations.GetByIdAsync(vacationId)).ReturnsAsync((Vacation)null);

            Func<Task> act = async () => await _service.DeleteAsync(vacationId);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Vacation not found.");
        }

        [Fact]
        public async Task GetVacationsAsync_ShouldReturnPaginatedVacations()
        {
            var vacations = new List<Vacation>
            {
                new Vacation { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(5) },
                new Vacation { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-5) }
            };

            UnitOfWorkMock.Setup(uow => uow.Vacations.GetPaginatedWithIncludeAsync(
                1, 5, It.IsAny<Expression<Func<Vacation, bool>>>(), v => v.StartDate, v => v.Worker))
                .ReturnsAsync((vacations, vacations.Count));

            var vacationDtos = new List<VacationDTO>
            {
                new VacationDTO { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(5) },
                new VacationDTO { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-5) }
            };

            MapperMock.Setup(map => map.Map<List<VacationDTO>>(vacations)).Returns(vacationDtos);

            var result = await _service.GetVacationsAsync(1, 5);

            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
        }
    }
}
