namespace DesignPatterns.Builder
{
    public class CustomStringBuilderOld : ICustomStringBuilder
    {
        private string _currentString;
        public CustomStringBuilderOld()
        {
            _currentString = string.Empty;
        }

        public CustomStringBuilderOld(string text)
        {
            _currentString = text;
        }

        public ICustomStringBuilder Append(string str)
        {
            _currentString += str;
            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            _currentString += ch;
            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            _currentString += "\n";
            return this;
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            _currentString += str + "\n";
            return this;
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            _currentString += ch + "\n";
            return this;
        }

        public string Build()
        {
            return _currentString;
        }
    }
}