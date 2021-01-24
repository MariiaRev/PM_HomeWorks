using System;
using Xunit;
using NotesApp.Tools;

namespace NotesApp.Tests.ToolsTests
{
    public class ShortGuidTests
    {
        [Fact]
        public void FromShortId_Should_ConvertCorrectly_GuidAfterToShortId()
        {
            //arrange
            var guid = new Guid();

            //act
            var shortGuid = ShortGuid.ToShortId(guid);
            var fromShort = ShortGuid.FromShortId(shortGuid);

            //assert
            Assert.Equal(guid, fromShort);
        }

        [Fact]
        public void FromShortId_Should_ConvertCorrectly_GuidAfterToShortIdIfItEndsWithDoubleEqualSign()
        {
            //arrange
            var guid = new Guid();

            //act
            var shortGuid = ShortGuid.ToShortId(guid);
            var fromShort = ShortGuid.FromShortId(shortGuid + "==");

            //assert
            Assert.Equal(guid, fromShort);
        }

        [Fact]
        public void FromShortId_Should_ConvertCorrectly_StringWithGuidToGuid()
        {
            //arrange
            var guid = new Guid();

            //act
            var fromShort = ShortGuid.FromShortId(guid.ToString());

            //assert
            Assert.Equal(guid, fromShort);
        }

        [Fact]
        public void FromShortId_Should_Throw_IfShortIdLengthIsInvalid()
        {
            //arrange
            var guid = new Guid();
            var invalidLength4 = guid.ToString()[..4];
            var invalidLength21 = guid.ToString()[..21];
            var invalidLength25 = guid.ToString()[..25];

            //act + assert
            Assert.Throws<FormatException>(() => ShortGuid.FromShortId(invalidLength4));
            Assert.Throws<FormatException>(() => ShortGuid.FromShortId(invalidLength21));
            Assert.Throws<FormatException>(() => ShortGuid.FromShortId(invalidLength25));
        }

        [Fact]
        public void FromShortId_Should_ReturnNull_IfInputIsNull()
        {
            //act
            var fromShortId = ShortGuid.FromShortId(null);
            
            //assert
            Assert.True(fromShortId == null);
        }
    }
}
