using FluentAssertions;
using Moq;
using VacationPlannerPro.Business.DTOs.ProjectDTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Tests.Mocks;
using Xunit;

namespace VacationPlannerPro.Tests.Services
{
    public class ProjectServiceTests : TestBase
    {
        private readonly ProjectService _service;

        public ProjectServiceTests()
        {
            _service = new ProjectService(UnitOfWorkMock.Object, MapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProjects()
        {
            var projects = new List<Project>
            {
                new Project { Name = "Project A" },
                new Project { Name = "Project B" }
            };

            UnitOfWorkMock.Setup(uow => uow.Projects.GetAllAsync())
                          .ReturnsAsync(projects);

            MapperMock.Setup(map => map.Map<IEnumerable<ProjectDTO>>(projects))
                      .Returns(new List<ProjectDTO>
                      {
                          new ProjectDTO { Name = "Project A" },
                          new ProjectDTO { Name = "Project B" }
                      });

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(p => p.Name == "Project A");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProject_WhenExists()
        {
            var projectId = Guid.NewGuid();
            var project = new Project { Id = projectId, Name = "Project A" };

            UnitOfWorkMock.Setup(uow => uow.Projects.GetByIdAsync(projectId))
                          .ReturnsAsync(project);

            MapperMock.Setup(map => map.Map<ProjectDTO>(project))
                      .Returns(new ProjectDTO { Name = "Project A" });

            var result = await _service.GetByIdAsync(projectId);

            result.Should().NotBeNull();
            result.Name.Should().Be("Project A");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProjectDoesNotExist()
        {
            UnitOfWorkMock.Setup(uow => uow.Projects.GetByIdAsync(It.IsAny<Guid>()))
                          .ReturnsAsync((Project)null);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldAddProject()
        {
            var createDto = new CreateProjectDTO { Name = "New Project", Description = "Test description" };
            var project = new Project { Name = "New Project", Description = "Test description" };

            MapperMock.Setup(map => map.Map<Project>(createDto)).Returns(project);

            await _service.CreateAsync(createDto);

            UnitOfWorkMock.Verify(uow => uow.Projects.AddAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenProjectNotFound()
        {
            var updateDto = new UpdateProjectDTO { Id = Guid.NewGuid(), Name = "Updated Project" };

            UnitOfWorkMock.Setup(uow => uow.Projects.GetByIdAsync(updateDto.Id))
                          .ReturnsAsync((Project)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(updateDto));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProject()
        {
            var projectId = Guid.NewGuid();
            var project = new Project { Id = projectId, Name = "Old Project Name" };

            var updateDto = new UpdateProjectDTO
            {
                Id = projectId,
                Name = "Updated Project Name"
            };

            UnitOfWorkMock.Setup(uow => uow.Projects.GetByIdAsync(projectId))
                          .ReturnsAsync(project);

            await _service.UpdateAsync(updateDto);

            UnitOfWorkMock.Verify(uow => uow.Projects.UpdateAsync(It.IsAny<Project>()), Times.Once);
            project.Name.Should().Be("Updated Project Name");
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteProject()
        {
            var projectId = Guid.NewGuid();
            var project = new Project { Id = projectId };

            UnitOfWorkMock.Setup(uow => uow.Projects.GetByIdAsync(projectId))
                          .ReturnsAsync(project);

            await _service.DeleteAsync(projectId);

            UnitOfWorkMock.Verify(uow => uow.Projects.DeleteAsync(project), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenProjectNotFound()
        {
            var projectId = Guid.NewGuid();

            UnitOfWorkMock.Setup(uow => uow.Projects.GetByIdAsync(projectId))
                          .ReturnsAsync((Project)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(projectId));
        }
    }
}
