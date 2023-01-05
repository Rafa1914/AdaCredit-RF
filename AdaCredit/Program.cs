using AdaCredit.Entities;
using AdaCredit.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AdaCredit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Declaração de Repositórios:
            var employeeRepository = new EmployeeRepository();
            var clientRepository = new ClientRepository();
            var transactionRepository = new TransactionRepository();

            //Geração de Dados Fakes:
            Console.Write("Deseja gerar dados Fakes (S/N): ");
            if (Console.ReadLine().ToUpper().Equals("S"))
            {
                Console.Clear();
                Console.Write("Deseja gerar dados Fakes para Funcionários (S/N): ");
                if (Console.ReadLine().ToUpper().Equals("S"))
                {
                    Console.Write("Quantos funcionários fakes deseja criar: ");
                    employeeRepository.GenerateFakeEmployees(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Funcionários gerados.");
                    Console.WriteLine("Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }

                Console.Clear();
                Console.Write("Deseja gerar dados Fakes para Clientes (S/N): ");
                if (Console.ReadLine().ToUpper().Equals("S"))
                {
                    Console.Write("Quantos clientes fakes deseja criar: ");
                    clientRepository.GenerateFakeClients(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Clientes gerados.");
                    Console.WriteLine("Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }

                Console.Clear();
                Console.Write("Deseja gerar dados Fakes para Transações (S/N): ");
                if (Console.ReadLine().ToUpper().Equals("S"))
                {
                    Console.Write("Quantas transações fakes com cada regra deseja criar: ");
                    transactionRepository.GenerateFakeTransactions(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Transações geradas.");
                    Console.WriteLine("Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
                
            }
            //Logar o Usuário:
            Console.Clear();
            var loginScreen = new Login(employeeRepository);
            loginScreen.Show();          
            
            //Inicializando o Menu:
            Menu.Show();
        }
    }
}