using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace AdaCredit.Entities
{
    public sealed class Employee
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public string HashedPassword { get; private set; }
        public bool Activate { get; private set; }
        public DateTime LastLogin { get; private set; }

        public Employee()
        {

        }

        public Employee(string user, string pass, bool active)
        {
            Username = user;
            Password = pass;
            Salt = new Faker().Random.ReplaceNumbers("######");
            HashedPassword = HashPassword(pass+Salt);
            Activate = active;
            LastLogin = new DateTime(0);
        }

        public Employee(string user, string pass, string salt, string hash)
        {
            Username = user;
            Password = pass;
            Salt = salt;
            HashedPassword = hash;
            Activate = true;
            LastLogin = new DateTime(0);
        }

        public Employee(string user, string pass, string salt, string hash, bool active, DateTime lastLogin)
        {
            Username = user;
            Password = pass;
            Salt = salt;
            HashedPassword = hash;
            Activate = active;
            LastLogin = lastLogin;
        }

        public override string ToString()
        {
            return $"{Username},{Password},{Salt},{HashedPassword},{Activate.ToString()},{LastLogin}";
        }

        public void Deactivate()
        {
            Activate = false;
        }

        public void UpdateDateTime(DateTime now)
        {
            LastLogin = now;
        }

        public void WriteEmployeeData()
        {
            Console.WriteLine();
            Console.WriteLine($"Nome do Usuário: {Username}");
            Console.WriteLine($"Último Acesso: {LastLogin}");
            Console.WriteLine("------------------------------");
        }
    }
}
