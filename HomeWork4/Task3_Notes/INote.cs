using System;

namespace Task3_Notes
{
    public interface INote
    {
        int Id { get; }
        string Title { get; }
        string Text { get; }
        DateTime CreatedOn { get; }
    }
}
