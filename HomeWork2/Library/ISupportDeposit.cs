using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    interface ISupportDeposit
    {
        void StartDeposit(decimal amount, string currency);
    }
}
