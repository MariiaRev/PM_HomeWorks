using System;
using Xunit;
using NotesApp.Tools;

namespace NotesApp.Tests.ToolsTests
{
    public class NumberGeneratorTests
    {
        [Fact]
        public void GeneratePositiveLong_Should_Throw_IfInputIsInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(-16));
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(19));
        }

        [Fact]
        public void GeneratePositiveLong_Should_ReturnANumberWithSetLength_IfInputIsValid()
        {
            //act
            var length2 = NumberGenerator.GeneratePositiveLong(2);
            var length5 = NumberGenerator.GeneratePositiveLong(5);
            var length15 = NumberGenerator.GeneratePositiveLong(15);
            var length18 = NumberGenerator.GeneratePositiveLong(18);

            //assert
            Assert.True(length2.ToString().Length == 2);
            Assert.True(length5.ToString().Length == 5);
            Assert.True(length15.ToString().Length == 15);
            Assert.True(length18.ToString().Length == 18);
        }
    }
}
