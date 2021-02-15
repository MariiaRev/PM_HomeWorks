using System;

namespace DesignPatterns.ChainOfResponsibility
{
    public class InvertMutator : AbstractMutator
    {
        public override string Mutate(string str)
        {
            var array = str.ToCharArray();
            Array.Reverse(array);

            return base.Mutate(new string(array));
        }
    }
}