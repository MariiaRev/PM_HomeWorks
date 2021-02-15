using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Builder
{
    public class CustomStringBuilder : ICustomStringBuilder
    {
        private readonly List<char> _storage;
        public CustomStringBuilder()
        {
            _storage = new List<char>(1);
        }

        public CustomStringBuilder(string text)
        {
            _storage = text.ToCharArray().ToList();
        }

        public ICustomStringBuilder Append(string str)
        {
            _storage.AddRange(str.ToCharArray());
            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            _storage.Add(ch);
            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            _storage.Add('\n');
            return this;
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            _storage.AddRange(str.ToCharArray());
            _storage.Add('\n');
            return this;
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            _storage.Add(ch);
            _storage.Add('\n');
            return this;
        }

        public string Build()
        {
            return string.Join("", _storage);
        }
    }
}