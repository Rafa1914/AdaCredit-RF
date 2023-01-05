using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace AdaCredit
{
    public class Login
    {
        private readonly EmployeeRepository _employeeRepository;

        public Login(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Show()
        {
            var loggedIn = false;

            do
            {
                Console.Clear();

                Console.Write("Digite o Nome do Usuário: ");
                var username = Console.ReadLine();

                Console.Write("Digite a Senha: "); //Trocar para char prompt ou getpass
                var password = Console.ReadLine();

                var employee = _employeeRepository.FindEmployee(username);
                if(employee is null)
                {
                    Console.WriteLine("Usuário não encontrado!");
                    Console.ReadKey();
                    continue;
                }

                var passwordMatch = Verify(password + employee.Salt, employee.HashedPassword);
                if (passwordMatch)
                {
                    if(!employee.Activate)
                    {
                        Console.WriteLine("Usuário inativo!");
                        Console.ReadKey();
                        continue;
                    }
                    loggedIn = true;
                    employee.UpdateDateTime(DateTime.Now);
                    Console.WriteLine(employee.LastLogin);
                    _employeeRepository.changeEmployee(employee, employee.Username);
                    Console.WriteLine("Login efetuado com sucesso!");
                    Console.WriteLine("Pressione uma tecla para continuar...") ;
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine("Senha Incorreta!");
                Console.ReadKey();
            } while (!loggedIn);
        }
    }
}
