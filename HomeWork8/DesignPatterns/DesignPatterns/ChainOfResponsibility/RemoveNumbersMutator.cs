using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class RemoveNumbersMutator : AbstractMutator
    {
        public override string Mutate(string str)
        {
            str = new string(str.Where(ch => !char.IsNumber(ch)).ToArray());

            return base.Mutate(str);
        }
    }
}