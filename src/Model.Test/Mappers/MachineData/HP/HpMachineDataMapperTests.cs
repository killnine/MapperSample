using System;
using Model.External.HP;
using Model.Mappers.HP;
using Model.Press;
using Xunit;
using AutoFixture;
using Model.Mappers.MachineData.HP;

namespace Model.Test.Mappers.MachineData.HP
{
    public class HpMachineDataMapperTests
    {
        private Fixture _fixture;

        public HpMachineDataMapperTests() 
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Mapper_ShouldSucceed_WithNoErrors()
        {
            //Arrange
            HpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

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
            var result = sut.Sync(destination, source);

            //Assert
            Assert.True(result.Success);
            Assert.Empty(result.Errors);
        }

        //[Fact]
        //public void Mapper_ShouldNotSucceed_WhenErrorsArePresent()
        //{
        //    //Arrange
        //    HpMachineDataMapper sut = new HpMachineDataMapper(new HpMachinePaperMapper());

        //    PressMachineData destination = new PressMachineData();

        //    messageSubmissionStatistics statistic = _fixture.Build<messageSubmissionStatistics>()
        //                                                     .With(x => x.copiesprintedok, "invalidNumber")
        //                                                     .Create();
        //    messageSubmission submission = _fixture.Build<messageSubmission>()
        //                                           .With(x => x.statistics, new[] { statistic })
        //                                           .Create();
        //    HpSpecification source = _fixture.Build<HpSpecification>()
        //                                     .With(x => x.submission, new[] { submission })
        //                                     .Create();

        //    //Act
        //    var result = sut.Sync(destination, source);

        //    //Assert
        //    Assert.False(result.Success);
        //    Assert.Empty(result.Errors);
        //}
    }
}
