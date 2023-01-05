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
            Console.Write("Deseja gerar dados Fakes para Funcionários (S/N): ");
            if(Console.ReadLine().ToUpper().Equals("S"))
            {
                employeeRepository.GenerateFakeEmployees(20);
                Console.WriteLine("Funcionários gerados.");
                Console.ReadKey();
            }
            
            Console.Write("Deseja gerar dados Fakes para Clientes (S/N): ");
            if (Console.ReadLine().ToUpper().Equals("S"))
            {  
                clientRepository.GenerateFakeClients(20);
                Console.WriteLine("Clientes gerados.");
                Console.ReadKey();
            }
            Console.Write("Deseja gerar dados Fakes para Transações (S/N): ");
            if (Console.ReadLine().ToUpper().Equals("S"))
            {
                transactionRepository.GenerateFakeTransactions();
                Console.WriteLine("Transações geradas.");
                Console.ReadKey();
            }
            Console.Clear();
            //Logar o Usuário:
            
            var loginScreen = new Login(employeeRepository);
            loginScreen.Show();          
            
            //Inicializando o Menu:
            Menu.Show();









            //Console.WriteLine("Teste");
        }
    }
}