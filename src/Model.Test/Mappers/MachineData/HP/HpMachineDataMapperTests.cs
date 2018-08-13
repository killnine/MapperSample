using System;
using Model.External.HP;
using Xunit;
using AutoFixture;
using Model.Domain.MachineData.Press;
using Model.Mappers.MachineData.HP;
using Model.Types;
using Moq;

namespace Model.Test.Mappers.MachineData.HP
{
    public class HpMachineDataMapperTests
    {
        private readonly Fixture _fixture;

        public HpMachineDataMapperTests() 
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Mapper_ShouldSucceed_WithNoErrors()
        {
            //Arrange
            IHpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

            PressMachineData destination = new PressMachineData();
            messageSubmissionStatistics statistic = _fixture.Build<messageSubmissionStatistics>()
                                                             .With(x => x.copiesprintedok, 100)
                                                             .With(x => x.copiesprintederror, 0)
                                                             .Create();
            messageSubmission submission = _fixture.Build<messageSubmission>()
                                                   .With(x => x.statistics, new[] { statistic })
                                                   .Create();
            HpSpecification source = _fixture.Build<HpSpecification>()
                                             .With(x => x.submission, new [] { submission })
                                             .Create();

            //Act
            var result = sut.Map(destination, source);

            //Assert
            Assert.True(result.Success);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Mapper_ShouldMap_JobNumber()
        {
            //Arrange
            IHpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

            PressMachineData destination = new PressMachineData();
            HpSpecification source = _fixture.Create<HpSpecification>();

            //Act
            sut.Map(destination, source);

            //Assert
            Assert.Equal(source.customerjobid, destination.JobNumber);
        }

        [Fact]
        public void Mapper_ShouldMap_GrossCount()
        {
            //Arrange
            IHpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

            PressMachineData destination = new PressMachineData();
            HpSpecification source = _fixture.Create<HpSpecification>();

            //Act
            sut.Map(destination, source);

            //Assert
            Assert.Equal(source.submission[0].statistics[0].copiesprintederror + source.submission[0].statistics[0].copiesprintedok, destination.GrossCount);
        }

        [Fact]
        public void Mapper_ShouldMap_NetCount()
        {
            //Arrange
            IHpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

            PressMachineData destination = new PressMachineData();
            HpSpecification source = _fixture.Create<HpSpecification>();

            //Act
            sut.Map(destination, source);

            //Assert
            Assert.Equal(source.submission[0].statistics[0].copiesprintedok, destination.NetCount);
        }

        [Fact]
        public void Mapper_ShouldMap_WasteCount()
        {
            //Arrange
            IHpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

            PressMachineData destination = new PressMachineData();
            HpSpecification source = _fixture.Create<HpSpecification>();

            //Act
            sut.Map(destination, source);

            //Assert
            Assert.Equal(source.submission[0].statistics[0].copiesprintederror, destination.WasteCount);
        }

        [Fact]
        public void Mapper_ShouldMap_Unit()
        {
            //Arrange
            IHpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

            PressMachineData destination = new PressMachineData();
            HpSpecification source = _fixture.Create<HpSpecification>();

            //Act
            sut.Map(destination, source);

            //Assert
            Assert.Equal(CountUnitType.Copies, destination.Unit);
        }


        [Fact]
        public void Mapper_ShouldNotSucceed_WhenErrorsArePresent()
        {
            //Arrange
            Mock<IHpMachinePaperMapper> mockPaperMapper = new Mock<IHpMachinePaperMapper>();
            mockPaperMapper.Setup(x => x.Map(It.IsAny<PaperData>(), It.IsAny<HpSpecification>())).Throws(new Exception("Boom goes the dynamite"));
            
            IHpMachineDataMapper sut = new HpMachineDataMapper(mockPaperMapper.Object);

            PressMachineData destination = new PressMachineData();

            HpSpecification source = _fixture.Create<HpSpecification>();

            //Act
            var result = sut.Map(destination, source);

            //Assert
            Assert.False(result.Success);
            Assert.Empty(result.Errors);
        }
    }
}
