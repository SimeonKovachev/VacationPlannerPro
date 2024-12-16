using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using VacationPlannerPro.Business.DTOs.WorkerDTOs;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Tests.Mocks;
using Xunit;

namespace VacationPlannerPro.Tests.Services
{
    public class WorkerServiceTests : TestBase
    {
        private readonly WorkerService _service;

        public WorkerServiceTests()
        {
            _service = new WorkerService(UnitOfWorkMock.Object, MapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllWorkers()
        {
            var workers = new List<Worker>
            {
                new Worker { FullName = "John Doe" },
                new Worker { FullName = "Jane Smith" }
            };

            UnitOfWorkMock.Setup(uow => uow.Workers.GetWorkersWithProfessionsAsync())
                          .ReturnsAsync(workers);

            MapperMock.Setup(map => map.Map<IEnumerable<WorkerDTO>>(workers))
                      .Returns(new List<WorkerDTO>
                      {
                          new WorkerDTO { FullName = "John Doe" },
                          new WorkerDTO { FullName = "Jane Smith" }
                      });

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().FullName.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnWorker_WhenExists()
        {
            var workerId = Guid.NewGuid();
            var worker = new Worker { Id = workerId, FullName = "John Doe" };

            UnitOfWorkMock.Setup(uow => uow.Workers.GetWorkerWithProfessionByIdAsync(workerId))
                          .ReturnsAsync(worker);

            MapperMock.Setup(map => map.Map<WorkerDTO>(worker))
                      .Returns(new WorkerDTO { FullName = "John Doe" });

            var result = await _service.GetByIdAsync(workerId);

            result.Should().NotBeNull();
            result.FullName.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenWorkerDoesNotExist()
        {
            UnitOfWorkMock.Setup(uow => uow.Workers.GetWorkerWithProfessionByIdAsync(It.IsAny<Guid>()))
                          .ReturnsAsync((Worker)null);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldAddWorker()
        {
            var createDto = new CreateWorkerDTO { FullName = "John Doe", Age = 30 };
            var worker = new Worker { FullName = "John Doe", Age = 30 };

            MapperMock.Setup(map => map.Map<Worker>(createDto)).Returns(worker);

            await _service.CreateAsync(createDto);

            UnitOfWorkMock.Verify(uow => uow.Workers.AddAsync(It.IsAny<Worker>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenWorkerNotFound()
        {
            var updateDto = new UpdateWorkerDTO { Id = Guid.NewGuid() };

            UnitOfWorkMock.Setup(uow => uow.Workers.GetByIdAsync(updateDto.Id))
                          .ReturnsAsync((Worker)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(updateDto));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateWorker()
        {
            var workerId = Guid.NewGuid();
            var worker = new Worker { Id = workerId, FullName = "Old Name" };

            var updateDto = new UpdateWorkerDTO
            {
                Id = workerId,
                FullName = "New Name"
            };

            UnitOfWorkMock.Setup(uow => uow.Workers.GetByIdAsync(workerId))
                          .ReturnsAsync(worker);

            await _service.UpdateAsync(updateDto);

            UnitOfWorkMock.Verify(uow => uow.Workers.UpdateAsync(It.IsAny<Worker>()), Times.Once);
            worker.FullName.Should().Be("New Name");
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteWorker()
        {
            var workerId = Guid.NewGuid();
            var worker = new Worker { Id = workerId };

            UnitOfWorkMock.Setup(uow => uow.Workers.GetByIdAsync(workerId))
                          .ReturnsAsync(worker);

            await _service.DeleteAsync(workerId);

            UnitOfWorkMock.Verify(uow => uow.Workers.DeleteAsync(worker), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenWorkerNotFound()
        {
            var workerId = Guid.NewGuid();

            UnitOfWorkMock.Setup(uow => uow.Workers.GetByIdAsync(workerId))
                          .ReturnsAsync((Worker)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(workerId));
        }

        [Fact]
        public async Task GetWorkersAsync_ShouldReturnPaginatedWorkers()
        {
            var workers = new List<Worker>
            {
                new Worker { FullName = "John Doe" },
                new Worker { FullName = "Jane Smith" }
            };

            UnitOfWorkMock.Setup(uow => uow.Workers.GetPaginatedWithIncludeAsync(
                1,
                5,
                It.IsAny<Expression<Func<Worker, bool>>>(),
                null,
                It.IsAny<Expression<Func<Worker, object>>>()))
            .ReturnsAsync((workers, workers.Count));

            MapperMock.Setup(map => map.Map<List<WorkerDTO>>(workers))
                      .Returns(new List<WorkerDTO>
                      {
                          new WorkerDTO { FullName = "John Doe" },
                          new WorkerDTO { FullName = "Jane Smith" }
                      });

            var result = await _service.GetWorkersAsync(1, 5);

            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.Items.First().FullName.Should().Be("John Doe");
        }
    }
}
