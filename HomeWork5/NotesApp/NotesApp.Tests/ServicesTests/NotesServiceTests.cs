using System;
using Xunit;
using Moq;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;

namespace NotesApp.Tests.ServicesTests
{
    public class NotesServiceTests
    {
        [Fact]
        public void AddNote_Should_Throw_IfNoteIsNull()
        {
            //arrange + moq
            Note note = null;
            var userId = It.IsAny<int>();
            
            var mock1 = new Mock<INotesStorage>();
            var mock2 = new Mock<INoteEvents>();
            mock1.Setup(st => st.AddNote(note, userId));
            mock2.Setup(ev => ev.NotifyAdded(note, userId));

            var sut = new NotesService(mock1.Object, mock2.Object);

            //act + assert
            Assert.Throws<ArgumentNullException>(() => sut.AddNote(note, userId));
        }

        [Fact]
        public void AddNote_Should_NotifyAdded_IfNoteWasAdded()
        {
            //arrange + moq
            var note = new Note();
            var mock1 = new Mock<INotesStorage>();
            var mock2 = new Mock<INoteEvents>();
            mock1.Setup(m => m.AddNote(note, It.IsAny<int>()));
            mock2.Setup(m => m.NotifyAdded(note, It.IsAny<int>()));

            var sut = new NotesService(mock1.Object, mock2.Object);

            //act
            sut.AddNote(note, It.IsAny<int>());

            //assert
            mock2.Verify(mock => mock.NotifyAdded(note, It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void AddNote_Should_NotNotifyAdded_IfNoteWasNotAdded()
        {
            //arrange + moq
            var note = new Note();
            var mock1 = new Mock<INotesStorage>();
            var mock2 = new Mock<INoteEvents>();
            mock1.Setup(m => m.AddNote(note, It.IsAny<int>())).Throws<Exception>();
            mock2.Setup(m => m.NotifyAdded(note, It.IsAny<int>()));

            var sut = new NotesService(mock1.Object, mock2.Object);

            //act + assert
            Assert.Throws<Exception>(() => sut.AddNote(note, It.IsAny<int>()));
            mock2.Verify(mock => mock.NotifyAdded(note, It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void DeleteNote_Should_NotifyDeleted_IfNoteWasDeleted()
        {
            //arrange + moq
            var noteId = new Guid();
            var mock1 = new Mock<INotesStorage>();
            var mock2 = new Mock<INoteEvents>();

            mock1.Setup(m => m.DeleteNote(noteId)).Returns(true);
            mock2.Setup(m => m.NotifyDeleted(noteId, It.IsAny<int>()));

            var sut = new NotesService(mock1.Object, mock2.Object);

            //act
            sut.DeleteNote(noteId, It.IsAny<int>());

            //assert
            mock2.Verify(m => m.NotifyDeleted(noteId, It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void DeleteNote_Should_NotNotifyDeleted_IfNoteWasNotDeleted()
        {
            //arrange + moq
            var noteId = new Guid();
            var mock1 = new Mock<INotesStorage>();
            var mock2 = new Mock<INoteEvents>();

            mock1.Setup(m => m.DeleteNote(noteId)).Returns(false);
            mock2.Setup(m => m.NotifyDeleted(noteId, It.IsAny<int>()));

            var sut = new NotesService(mock1.Object, mock2.Object);

            //act
            sut.DeleteNote(noteId, It.IsAny<int>());

            //assert
            mock2.Verify(m => m.NotifyDeleted(noteId, It.IsAny<int>()), Times.Never);
        }
    }
}
