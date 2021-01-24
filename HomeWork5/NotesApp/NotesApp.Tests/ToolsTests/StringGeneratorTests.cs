using System;
using Xunit;
using NotesApp.Tools;

namespace NotesApp.Tests.ToolsTests
{
    public class StringGeneratorTests
    {
        [Fact]
        public void GenerateNumbersString_Should_ReturnEmptyString_IfLengthEqualsZero()
        {
            //act
            var stringNumber1 = StringGenerator.GenerateNumbersString(0, false);
            var stringNumber2 = StringGenerator.GenerateNumbersString(0, true);

            //assert
            Assert.True(stringNumber1 == "");
            Assert.True(stringNumber2 == "");
        }

        [Fact]
        public void GenerateNumbersString_Should_Throw_IfInputLengthIsInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StringGenerator.GenerateNumbersString(-10, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => StringGenerator.GenerateNumbersString(-1, true));
        }

        [Fact]
        public void GenerateNumbersString_Should_ReturnStringWithoutLeadingNumber_IfNotAllowLeadingZero()
        {
            //act
            var stringNumber1 = StringGenerator.GenerateNumbersString(1, false);
            var stringNumber2 = StringGenerator.GenerateNumbersString(3, false);
            var stringNumber3 = StringGenerator.GenerateNumbersString(16, false);

            //assert
            Assert.True(stringNumber1[0] != 0);
            Assert.True(stringNumber2[0] != 0);
            Assert.True(stringNumber3[0] != 0);
        }

        [Fact]
        public void GenerateNumbersString_Should_ReturnStringWithSetLength_IfInputLengthIsValid()
        {
            //act
            var stringNumber1 = StringGenerator.GenerateNumbersString(5, false);
            var stringNumber2 = StringGenerator.GenerateNumbersString(5, true);
            var stringNumber3 = StringGenerator.GenerateNumbersString(7, false);
            var stringNumber4 = StringGenerator.GenerateNumbersString(7, true);

            //assert
            Assert.True(stringNumber1.Length == 5);
            Assert.True(stringNumber2.Length == 5);
            Assert.True(stringNumber3.Length == 7);
            Assert.True(stringNumber4.Length == 7);
        }

        [Fact]
        public void GenerateNumbersString_Should_ReturnNumberWithSetLength_IfInputLengthIsValid()
        {
            //act
            var stringNumber1 = StringGenerator.GenerateNumbersString(5, false);
            var stringNumber2 = StringGenerator.GenerateNumbersString(5, true);

            //assert
            Assert.True(stringNumber1.Length == 5 && long.TryParse(stringNumber1, out _));
            Assert.True(stringNumber2.Length == 5 && long.TryParse(stringNumber2, out _));

            Assert.True(stringNumber1.Length == 5 && int.TryParse(stringNumber1, out _));
            Assert.True(stringNumber2.Length == 5 && int.TryParse(stringNumber2, out _));

            Assert.True(stringNumber1.Length == 5 && double.TryParse(stringNumber1, out _));
            Assert.True(stringNumber2.Length == 5 && double.TryParse(stringNumber2, out _));

            Assert.True(stringNumber1.Length == 5 && decimal.TryParse(stringNumber1, out _));
            Assert.True(stringNumber2.Length == 5 && decimal.TryParse(stringNumber2, out _));
        }
    }
}
