using AutoMapper;
using FluentAssertions;
using Moq;
using VacationPlannerPro.Business.DTOs.VacationDTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;
using Xunit;

namespace VacationPlannerPro.Tests.Services
{
    public class VacationServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly VacationService _service;

        public VacationServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new VacationService(_unitOfWorkMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task UpdateAsync_WhenVacationDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var updateDto = new UpdateVacationDTO { Id = Guid.NewGuid(), StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) };
            _unitOfWorkMock.Setup(uow => uow.Vacations.GetByIdAsync(updateDto.Id)).ReturnsAsync((Vacation)null);

            // Act
            Func<Task> act = () => _service.UpdateAsync(updateDto);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Vacation not found.");
        }

    }
}
