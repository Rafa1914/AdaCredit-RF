using AdaCredit.Entities;
using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class ChangeClientData
    {
        public static void Execute()
        {
            var clientRepository = new ClientRepository();

            Console.Write("Digite o CPF do cliente que deseja alterar (sem formatação): ");
            var document = Console.ReadLine();

            Console.WriteLine("Digite os dados corrigidos:");
            Console.Write("Nome: ");
            var nameCorrected = Console.ReadLine();
            Console.Write("CPF (sem formatação): ");
            var documentCorrected = Console.ReadLine();

            var clientCorrected = new Client(nameCorrected, documentCorrected);

            clientRepository.ChangeClient(document, clientCorrected);

            Console.WriteLine("Dados alterados com sucesso!");
            Console.ReadKey();

        }
    }
}
