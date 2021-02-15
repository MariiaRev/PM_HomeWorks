namespace DesignPatterns.ChainOfResponsibility
{
    public class TrimMutator : AbstractMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.Trim());
        }
    }
}