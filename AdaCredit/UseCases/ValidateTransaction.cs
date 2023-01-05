using AdaCredit.Entities;
using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class ValidateTransaction
    {
        public static bool Execute(Transaction transaction)
        {
            var clientRepository = new ClientRepository();
            //var accountRepository = new AccountRepository();

            if(transaction.Type == "TEF")
            {
                if (transaction.OriginBank != transaction.DestinyBank)
                {
                    transaction.ErrorMessage = "Não é possível realizar TEF entre bancos distintos";
                    return false;
                }
            }

            if (transaction.OriginBank == "777")
            {
                if (clientRepository.GetClientByAccount(transaction.OriginAccountNumber) is null)
                {
                    transaction.ErrorMessage = "Conta de origem inexistente";
                    return false;
                }
            }

            if (transaction.DestinyBank == "777")
            {
                if (clientRepository.GetClientByAccount(transaction.DestinyAccountNumber) is null)
                {
                    transaction.ErrorMessage = "Conta de destino inexistente";
                    return false;
                }
            }

            return true;
        }
    }
}
