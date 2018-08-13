using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using Model.Domain.MachineData.Press;
using Model.Mappers.MachineData.HP;
using Xunit;

namespace Model.Test.Mappers.MachineData.HP
{
    public class HpMachinePaperMapperTests
    {
        private readonly Fixture _fixture;

        public HpMachinePaperMapperTests() 
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Mapper_ShouldBeNull_WhenAllPropertiesAreNull()
        {
            //Arrange
            IHpMachinePaperMapper sut = new HpMachinePaperMapper();

            //Act
            var result = sut.ShouldBeNull(new PaperData());

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Mapper_ShouldNotBeNull_WhenAnyPropertiesArePopulated()
        {
            //Arrange
            IHpMachinePaperMapper sut = new HpMachinePaperMapper();
            var samplePaperData = _fixture.Create<PaperData>();

            //Act
            var result = sut.ShouldBeNull(samplePaperData);

            //Assert
            Assert.False(result);
        }

    }
}
