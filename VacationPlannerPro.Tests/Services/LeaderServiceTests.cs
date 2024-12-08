using AutoMapper;
using FluentAssertions;
using Moq;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace VacationPlannerPro.Tests.Services
{
    public class LeaderServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LeaderService _service;

        public LeaderServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _service = new LeaderService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

       /* #region GetAllAsync
        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedLeaders()
        {
            // Arrange
            var leaders = new List<Leader>
            {
                new Leader { Id = Guid.NewGuid(), FullName = "John Doe", Age = 35, ProfessionId = 1 }
            };
            _unitOfWorkMock.Setup(uow => uow.Leaders.GetAllAsync()).ReturnsAsync(leaders);

            var leaderDTOs = new List<LeaderDTO>
            {
                new LeaderDTO { Id = leaders[0].Id, FullName = "John Doe", Age = 35, ProfessionId = 1 }
            };
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<LeaderDTO>>(leaders)).Returns(leaderDTOs);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(leaderDTOs);
            _unitOfWorkMock.Verify(uow => uow.Leaders.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoLeadersExist()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.Leaders.GetAllAsync()).ReturnsAsync(new List<Leader>());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
            _unitOfWorkMock.Verify(uow => uow.Leaders.GetAllAsync(), Times.Once);
        }
        #endregion

        #region GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_ShouldReturnMappedLeader_WhenLeaderExists()
        {
            // Arrange
            var leaderId = Guid.NewGuid();
            var leader = new Leader { Id = leaderId, FullName = "John Doe", Age = 35, ProfessionId = 1 };
            _unitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(leaderId)).ReturnsAsync(leader);

            var leaderDTO = new LeaderDTO { Id = leaderId, FullName = "John Doe", Age = 35, ProfessionId = 1 };
            _mapperMock.Setup(mapper => mapper.Map<LeaderDTO>(leader)).Returns(leaderDTO);

            // Act
            var result = await _service.GetByIdAsync(leaderId);

            // Assert
            result.Should().BeEquivalentTo(leaderDTO);
            _unitOfWorkMock.Verify(uow => uow.Leaders.GetByIdAsync(leaderId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenLeaderDoesNotExist()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Leader)null);

            // Act
            var result = await _service.GetByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }
        #endregion

        #region CreateAsync
        [Fact]
        public async Task CreateAsync_ShouldAddLeader()
        {
            // Arrange
            var createDto = new CreateLeaderDTO { FullName = "John Doe", Age = 35, ProfessionId = 1 };
            var leaderEntity = new Leader { Id = Guid.NewGuid(), FullName = "John Doe", Age = 35, ProfessionId = 1 };

            _mapperMock.Setup(m => m.Map<Leader>(createDto)).Returns(leaderEntity);

            // Act
            await _service.CreateAsync(createDto);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.Leaders.AddAsync(It.Is<Leader>(l => l.FullName == "John Doe")), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenMappingFails()
        {
            // Arrange
            var createDto = new CreateLeaderDTO { FullName = "John Doe", Age = 35, ProfessionId = 1 };
            _mapperMock.Setup(m => m.Map<Leader>(createDto)).Throws(new Exception("Mapping failed"));

            // Act
            Func<Task> act = async () => await _service.CreateAsync(createDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Mapping failed");
        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task UpdateAsync_ShouldUpdateLeader_WhenLeaderExists()
        {
            // Arrange
            var updateDto = new UpdateLeaderDTO { Id = Guid.NewGuid(), FullName = "John Updated", Age = 40, ProfessionId = 2 };
            var existingLeader = new Leader { Id = updateDto.Id, FullName = "John Doe", Age = 35, ProfessionId = 1 };

            _unitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(updateDto.Id)).ReturnsAsync(existingLeader);

            // Act
            await _service.UpdateAsync(updateDto);

            // Assert
            _mapperMock.Verify(m => m.Map(updateDto, existingLeader), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Leaders.UpdateAsync(existingLeader), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenLeaderDoesNotExist()
        {
            // Arrange
            var updateDto = new UpdateLeaderDTO { Id = Guid.NewGuid(), FullName = "Non-Existent Leader", Age = 40, ProfessionId = 2 };
            _unitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(updateDto.Id)).ReturnsAsync((Leader)null);

            // Act
            Func<Task> act = async () => await _service.UpdateAsync(updateDto);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Leader not found.");
        }
        #endregion

        #region DeleteAsync
        [Fact]
        public async Task DeleteAsync_ShouldDeleteLeader_WhenLeaderExists()
        {
            // Arrange
            var leaderId = Guid.NewGuid();
            var leader = new Leader { Id = leaderId, FullName = "John Doe", Age = 35, ProfessionId = 1 };

            _unitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(leaderId)).ReturnsAsync(leader);

            // Act
            await _service.DeleteAsync(leaderId);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.Leaders.DeleteAsync(leader), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenLeaderDoesNotExist()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.Leaders.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Leader)null);

            // Act
            Func<Task> act = async () => await _service.DeleteAsync(Guid.NewGuid());

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Leader not found.");
        }
        #endregion*/
    }
}
