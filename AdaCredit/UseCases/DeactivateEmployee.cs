using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class DeactivateEmployee
    {
        public static void Execute()
        {
            Console.Write("Digite o usuário do funcionário que deseja desativar: ");
            var user = Console.ReadLine();

            var employeeRepository = new EmployeeRepository();

            var employee = employeeRepository.FindEmployee(user);

            if (employee is null)
            {
                Console.WriteLine("Usuário não encontrado.");
            }
            else
            {
                if(employee.Activate)
                {
                    employee.Deactivate();
                    employeeRepository.changeEmployee(employee, user);
                    Console.WriteLine("Usuário desativado!");
                }
            }          
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
