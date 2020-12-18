using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    interface ISupportWithdrawal
    {
        void StartWithdrawal(decimal amount, string currency);
    }
}
