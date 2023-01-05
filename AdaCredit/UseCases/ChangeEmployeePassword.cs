using AdaCredit.Repositories;
using AdaCredit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace AdaCredit.UseCases
{
    public static class ChangeEmployeePassword
    {
        public static void Execute()
        {
            Console.Write("Qual usuário deseja trocar a senha: ");
            var user = Console.ReadLine();

            var employeeRepository = new EmployeeRepository();
            var employee = employeeRepository.FindEmployee(user);

            if (employee is null)
            {
                Console.WriteLine("Usuário não encontrado.");
                Console.ReadKey();
                return;
            }

            var passwordMatch = false;

            do
            {
                Console.Write("Digite a senha atual: ");
                passwordMatch = Verify(Console.ReadLine()+employee.Salt, employee.HashedPassword);
                if (!passwordMatch)
                {
                    Console.WriteLine("Senha Incorreta");
                    Console.ReadKey();
                    Console.Clear();
                }
                    
            } while (!passwordMatch);

            Console.Write("Digite a nova senha: ");
            var pass = Console.ReadLine();

            employee = new Employee(user, pass, true);

            employeeRepository.changeEmployee(employee,user);

            Console.WriteLine("Senha alterada com sucesso.");
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
