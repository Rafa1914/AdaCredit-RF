using AdaCredit.Entities;
using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class AddNewEmployee
    {
        public static void Execute()
        {
            Console.Write("Digite o Usuário do Funcionário: ");
            var user = Console.ReadLine();

            Console.Write("Digite a senha do usuário: ");
            var password = Console.ReadLine();

            var employee = new Employee(user,password,true);

            var employeeRepository = new EmployeeRepository();
            var result = employeeRepository.AddEmployee(employee);

            if (result)
            {
                Console.WriteLine("Usuário cadastrado com sucesso!");
                employeeRepository.Save();
            }
                
            else
                Console.WriteLine("Falha no cadastro!");

            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
