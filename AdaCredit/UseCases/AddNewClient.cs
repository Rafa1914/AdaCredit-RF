using AdaCredit.Entities;
using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class AddNewClient
    {
        public static void Execute()
        {
            Console.Write("Digite o Nome do Cliente: ");
            var name = Console.ReadLine();

            Console.Write("Digite o CPF (sem formatação): ");
            //long.TryParse(Console.ReadLine(), out long document);
            var document = Console.ReadLine();

            document = document.Insert(3, ".");
            document = document.Insert(7, ".");
            document = document.Insert(11, "-");

            var client = new Client(name, document, true);

            var clientRepository = new ClientRepository();
            var result = clientRepository.AddClient(client);
            
            if (result)
            {
                Console.WriteLine("Cliente cadastrado com sucesso!");
                clientRepository.Save();
            }
            else
                Console.WriteLine("Falha no cadastro!");

            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
