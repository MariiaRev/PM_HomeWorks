namespace DesignPatterns.ChainOfResponsibility
{
    public abstract class AbstractMutator : IStringMutator
    {
        private IStringMutator _next;
        public IStringMutator SetNext(IStringMutator next)
        {
            _next = next;
            return _next;
        }

        public virtual string Mutate(string str)
        {
            if (_next == null)
            {
                return str;
            }
            else
            {
                return _next.Mutate(str);
            }
        }
    }
}
