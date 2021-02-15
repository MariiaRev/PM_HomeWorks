namespace DesignPatterns.ChainOfResponsibility
{
    public class ToUpperMutator : AbstractMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.ToUpper());
        }
    }
}