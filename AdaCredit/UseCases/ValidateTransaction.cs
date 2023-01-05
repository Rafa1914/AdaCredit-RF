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
                var client = clientRepository.GetClientByAccount(transaction.OriginAccountNumber);
                if (client is null)
                {
                    transaction.ErrorMessage = "Conta de origem inexistente";
                    return false;
                }
                if(!client.Activate)
                {
                    transaction.ErrorMessage = "Conta de origem está desativada";
                    return false;
                }
            }

            if (transaction.DestinyBank == "777")
            {
                var client = clientRepository.GetClientByAccount(transaction.DestinyAccountNumber);
                if (client is null)
                {
                    transaction.ErrorMessage = "Conta de destino inexistente";
                    return false;
                }
                if(!client.Activate)
                {
                    transaction.ErrorMessage = "Conta de destino está desativada";
                    return false;
                }
            }

            return true;
        }
    }
}
