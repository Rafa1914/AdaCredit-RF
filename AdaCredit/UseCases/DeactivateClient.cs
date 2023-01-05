using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class DeactivateClient
    {
        public static void Execute()
        {
            Console.Write("Digite o CPF do cliente que deseja desativar (sem formatação): ");
            var document = Console.ReadLine();

            var clientRepository = new ClientRepository();

            var client = clientRepository.FindClient(document);
            client.Deactivate();

            clientRepository.ChangeClient(document,client);
        }
    }
}
