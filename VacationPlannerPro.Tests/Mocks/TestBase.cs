using AutoMapper;
using Moq;
using VacationPlannerPro.Data.Interfaces;

namespace VacationPlannerPro.Tests.Mocks
{
    public class TestBase
    {
        protected Mock<IUnitOfWork> UnitOfWorkMock { get; }

        protected Mock<IMapper> MapperMock { get; }

        public TestBase()
        {
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            MapperMock = new Mock<IMapper>();
        }
    }
}
