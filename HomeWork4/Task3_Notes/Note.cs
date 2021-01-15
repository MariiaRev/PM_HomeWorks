using System;
using Newtonsoft.Json;

namespace Task3_Notes
{
    public class Note : INote
    {
        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public string Title { get; private set; }
        [JsonProperty]
        public string Text { get; private set; }
        [JsonProperty]
        public DateTime CreatedOn { get; private set; }

        const int _titleLength = 32;

        public Note() { }
        public Note (string data, int lastId)
        {
            Id = lastId + 1;

            if (data.Length > _titleLength)
                Title = data[.._titleLength];
            else
                Title = data;

            Text = data;
            CreatedOn = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"{Id, 4}{"",  3}{Title, _titleLength} {CreatedOn, 20:G}";
        }
    }
}
