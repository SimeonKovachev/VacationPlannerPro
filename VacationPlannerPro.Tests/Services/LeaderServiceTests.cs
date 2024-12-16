using AutoMapper;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;
using VacationPlannerPro.Tests.Mocks;
using Xunit;

namespace VacationPlannerPro.Tests.Services
{
    public class LeaderServiceTests : TestBase
    {
        private readonly LeaderService _service;

        public LeaderServiceTests()
        {
            _service = new LeaderService(UnitOfWorkMock.Object, MapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllLeaders()
        {
            var leaders = new List<Leader> { new Leader { FullName = "Test Leader" } };
            UnitOfWorkMock.Setup(uow => uow.Leaders.GetAllAsync()).ReturnsAsync(leaders);
            MapperMock.Setup(map => map.Map<IEnumerable<LeaderDTO>>(leaders))
                      .Returns(new List<LeaderDTO> { new LeaderDTO { FullName = "Test Leader" } });

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Leader", result.First().FullName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnLeader_WhenExists()
        {
            var leader = new Leader { Id = Guid.NewGuid(), FullName = "Test Leader" };
            UnitOfWorkMock.Setup(uow => uow.Leaders.GetEntityWithNavigationPropertyByIdAsync<Leader, Profession>(
                leader.Id, It.IsAny<Expression<Func<Leader, Profession>>>())).ReturnsAsync(leader);

            MapperMock.Setup(map => map.Map<LeaderDTO>(leader))
                      .Returns(new LeaderDTO { FullName = "Test Leader" });

            var result = await _service.GetByIdAsync(leader.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Leader", result.FullName);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddLeader()
        {
            var createDto = new CreateLeaderDTO { FullName = "Test Leader", Age = 30 };
            var leader = new Leader { FullName = "Test Leader", Age = 30 };
            MapperMock.Setup(map => map.Map<Leader>(createDto)).Returns(leader);

            await _service.CreateAsync(createDto);

            UnitOfWorkMock.Verify(uow => uow.Leaders.AddAsync(It.IsAny<Leader>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_IfLeaderNotFound()
        {
            var updateDto = new UpdateLeaderDTO { Id = Guid.NewGuid() };
            UnitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(updateDto.Id)).ReturnsAsync((Leader)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(updateDto));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteLeader()
        {
            var leader = new Leader { Id = Guid.NewGuid() };
            UnitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(leader.Id)).ReturnsAsync(leader);

            await _service.DeleteAsync(leader.Id);

            UnitOfWorkMock.Verify(uow => uow.Leaders.DeleteAsync(leader), Times.Once);
        }

    }
}
