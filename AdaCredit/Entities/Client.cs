using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Entities
{
    public sealed class Client
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public bool Activate { get; private set; }
        public Account Account { get; private set; }

        public Client()
        {

        }

        public Client(string name, string document)
        {
            Name = name;
            Document = document;
            Account = null;
            Activate = true;
        }

        public Client(string name, string document, bool active)
        {
            Name= name;
            Document = document;
            Account = null;
            Activate = active;
        }

        public Client(string name, string document, Account account, bool active)
        {
            Name = name;
            Document = document;
            Account = account;
            Activate = active;
        }

        public override string ToString()
        {
            return $"{Name},{Document},{Account.Number},{Activate.ToString()}";
        }

        public void Deactivate()
        {
            Activate = false;
        }

        public void WriteClientData()
        {
            Console.WriteLine();
            Console.WriteLine($"Nome Completo: {Name}");
            Console.WriteLine($"CPF: {Document}");
            Console.WriteLine($"Número da Conta: {Account.Number.Insert(5, "-")}");
            if (Activate)
            {
                Console.WriteLine($"Saldo do Cliente: R$ {Math.Round(Account.Balance, 2)}");
                Console.WriteLine($"Status do Cliente: Cliente ativo");
            }
            else
            {
                Console.WriteLine($"Saldo do Cliente: - ");
                Console.WriteLine($"Status do Cliente: Cliente inativo");
            }
            Console.WriteLine("----------------------------");
        }

        
    }
}
