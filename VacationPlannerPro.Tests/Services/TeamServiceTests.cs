using FluentAssertions;
using Moq;
using VacationPlannerPro.Business.DTOs.TeamDTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Tests.Mocks;
using Xunit;

namespace VacationPlannerPro.Tests.Services
{
    public class TeamServiceTests : TestBase
    {
        private readonly TeamService _service;

        public TeamServiceTests()
        {
            _service = new TeamService(UnitOfWorkMock.Object, MapperMock.Object);
        }

        [Fact]
        public async Task GetTeamsAsync_ShouldReturnPaginatedTeams()
        {
            var teams = new List<Team>
            {
                new Team { Name = "Development Team" },
                new Team { Name = "Marketing Team" }
            };

            UnitOfWorkMock.Setup(uow => uow.Teams.GetTeamsPaginatedAsync(1, 5, null))
                .ReturnsAsync((teams, teams.Count));

            MapperMock.Setup(map => map.Map<List<TeamDTO>>(teams))
                      .Returns(new List<TeamDTO>
                      {
                          new TeamDTO { Name = "Development Team" },
                          new TeamDTO { Name = "Marketing Team" }
                      });

            var result = await _service.GetTeamsAsync(1, 5);

            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.Items.First().Name.Should().Be("Development Team");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTeam_WhenExists()
        {
            var teamId = Guid.NewGuid();
            var team = new Team { Id = teamId, Name = "Development Team" };

            UnitOfWorkMock.Setup(uow => uow.Teams.GetTeamByIdWithDetailsAsync(teamId))
                .ReturnsAsync(team);

            MapperMock.Setup(map => map.Map<TeamDTO>(team))
                      .Returns(new TeamDTO { Name = "Development Team" });

            var result = await _service.GetByIdAsync(teamId);

            result.Should().NotBeNull();
            result.Name.Should().Be("Development Team");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            UnitOfWorkMock.Setup(uow => uow.Teams.GetTeamByIdWithDetailsAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Team)null);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldAddTeam()
        {
            var createDto = new CreateTeamDTO
            {
                Name = "Development Team",
                WorkerIds = new List<Guid> { Guid.NewGuid() }
            };

            var team = new Team { Name = "Development Team" };

            MapperMock.Setup(map => map.Map<Team>(createDto)).Returns(team);

            await _service.CreateAsync(createDto);

            UnitOfWorkMock.Verify(uow => uow.Teams.AddAsync(It.IsAny<Team>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTeam_WhenExists()
        {
            var teamId = Guid.NewGuid();
            var team = new Team { Id = teamId, Name = "Development Team", TeamWorkers = new List<TeamWorker>() };

            var updateDto = new UpdateTeamDTO
            {
                Id = teamId,
                Name = "Updated Development Team",
                WorkerIds = new List<Guid> { Guid.NewGuid() }
            };

            UnitOfWorkMock.Setup(uow => uow.Teams.GetTeamByIdWithDetailsAsync(teamId))
                .ReturnsAsync(team);

            await _service.UpdateAsync(updateDto);

            UnitOfWorkMock.Verify(uow => uow.Teams.UpdateAsync(It.IsAny<Team>()), Times.Once);
            team.Name.Should().Be("Updated Development Team");
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenTeamNotFound()
        {
            var updateDto = new UpdateTeamDTO { Id = Guid.NewGuid() };

            UnitOfWorkMock.Setup(uow => uow.Teams.GetTeamByIdWithDetailsAsync(updateDto.Id))
                .ReturnsAsync((Team)null);

            Func<Task> act = async () => await _service.UpdateAsync(updateDto);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Team not found.");
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteTeam_WhenExists()
        {
            var teamId = Guid.NewGuid();
            var team = new Team { Id = teamId };

            UnitOfWorkMock.Setup(uow => uow.Teams.GetByIdAsync(teamId))
                .ReturnsAsync(team);

            await _service.DeleteAsync(teamId);

            UnitOfWorkMock.Verify(uow => uow.Teams.DeleteAsync(team), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenTeamNotFound()
        {
            var teamId = Guid.NewGuid();

            UnitOfWorkMock.Setup(uow => uow.Teams.GetByIdAsync(teamId))
                .ReturnsAsync((Team)null);

            Func<Task> act = async () => await _service.DeleteAsync(teamId);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage("Team not found.");
        }

        [Fact]
        public async Task GetTeamWithWorkersByIdAsync_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            UnitOfWorkMock.Setup(uow => uow.Teams.GetTeamByIdWithDetailsAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Team)null);

            // Act
            var result = await _service.GetTeamWithWorkersByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeNull();
        }
    }
}
