using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class GetTransactionFee
    {
        
        public static decimal Execute(string type,decimal amount)
        {
            Dictionary<string, decimal> _fees = new Dictionary<string, decimal>()
            {
                {"DOC",1.0M+0.01M*amount},
                {"TED",5.0M},
                {"TEF",0M}
            };
            if (_fees[type] <= 5.0M)
                return _fees[type];
            else
                return 5.0M;
    }
    }
}
