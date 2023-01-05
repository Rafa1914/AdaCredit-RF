using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class ProcessesTransactions
    {
        public static void Execute()
        {
            var transactionRepository = new TransactionRepository();
            var clientRepository = new ClientRepository();

            Console.WriteLine("Processando transações...");

            foreach (var transaction in transactionRepository.GetTransactions())
            {
                
                
                if (!transaction.Valid)
                    continue;

                if (transaction.OriginBank=="777")
                {
                    var client = clientRepository.GetClientByAccount(transaction.OriginAccountNumber);
                    bool isPossible = client.Account.Withdraw(transaction.Fee,transaction.Amount);
                    if(!isPossible)
                    {
                        transaction.Valid = false;
                        transaction.ErrorMessage = "Saldo Insuficiente";
                    }
                }

                if (transaction.DestinyBank.Equals("777"))
                {
                    if (transaction.Valid)
                    {
                        var client = clientRepository.GetClientByAccount(transaction.DestinyAccountNumber);
                        client.Account.Deposit(transaction.Amount);
                    }
                }
            }

            transactionRepository.SaveCompleted();
            
            transactionRepository.SaveFailed();

            Console.WriteLine("Transações Processadas.");
            Console.ReadKey();

        }
    }
}
