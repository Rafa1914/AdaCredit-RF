using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class GetReport
    {
        
        
        

        public static void ActiveClients()
        {
            var _clientRepository = new ClientRepository();
            foreach(var client in _clientRepository.GetClients())
            {
                if(client.Activate)
                {
                    client.WriteClientData();
                }
            }
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

        public static void InactiveClients()
        {
            var _clientRepository = new ClientRepository();
            foreach (var client in _clientRepository.GetClients())
            {
                if (!client.Activate)
                {
                    client.WriteClientData();
                }
            }
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

        public static void ActiveEmployees()
        {
            var _employeeRepository = new EmployeeRepository();
            foreach (var employee in _employeeRepository.GetEmployees())
            {
                if (employee.Activate)
                {
                    employee.WriteEmployeeData();
                }
            }
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

        public static void FailedTransactions()
        {
            var _transactionRepository = new TransactionRepository();
            foreach(var transaction in _transactionRepository.GetTransactions())
            {
                if(!transaction.Valid)
                {
                    transaction.WriteTransactionData();
                }
            }
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

    }
}
