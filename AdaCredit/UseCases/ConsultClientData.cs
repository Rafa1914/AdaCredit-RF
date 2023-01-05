using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class ConsultClientData
    {
        public static void Execute()
        {
            Console.Write("Digite o CPF do cliente desejado (sem formatação): ");
            var document = Console.ReadLine();
            document = document.Insert(3, ".");
            document = document.Insert(7, ".");
            document = document.Insert(11, "-");

            var clientRepository = new ClientRepository();

            var client = clientRepository.FindClient(document);

            if(client is null)
            {
                Console.WriteLine("Não foi possível encontrar o cliente.");
                Console.ReadKey();
                return;
            }
            client.WriteClientData();
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
