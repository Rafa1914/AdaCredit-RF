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

                //var password = Console.ReadLine();
                var password = String.Empty;
                ConsoleKey key;
                Console.Write("Digite a Senha: "); //Trocar para char prompt ou getpass
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password = password[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        password += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                Console.WriteLine();


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
