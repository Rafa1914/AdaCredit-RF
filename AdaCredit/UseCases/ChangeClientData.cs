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
            document = document.Insert(3, ".");
            document = document.Insert(7, ".");
            document = document.Insert(11, "-");

            Console.WriteLine("Digite os dados corrigidos:");
            Console.Write("Nome: ");
            var nameCorrected = Console.ReadLine();
            Console.Write("CPF (sem formatação): ");
            var documentCorrected = Console.ReadLine();
            documentCorrected = documentCorrected.Insert(3, ".");
            documentCorrected = documentCorrected.Insert(7, ".");
            documentCorrected = documentCorrected.Insert(11, "-");

            var clientCorrected = new Client(nameCorrected, documentCorrected);

            if(clientRepository.FindClient(document) is null)
            {
                Console.WriteLine("Cliente não encontrado.");
            }
            else
            {
                clientRepository.ChangeClient(document, clientCorrected);
                Console.WriteLine("Dados alterados com sucesso!");
            }
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();

        }
    }
}
