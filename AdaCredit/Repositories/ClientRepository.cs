using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AdaCredit.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System.Reflection.Metadata;
using Bogus;
using Bogus.Extensions.Brazil;

namespace AdaCredit.Repositories
{
    public class ClientRepository
    {
        private List<Client> _clients = new List<Client>();
        private string clientsPath = Path.Combine(Environment.CurrentDirectory, @"Clients.txt");

        public ClientRepository()
        {
            //Verificação da Existência do Arquivo:
            if (File.Exists(clientsPath))
            {
                //Leitura do Banco de Dacos das COntas:
                var accountRepository = new AccountRepository();

                //Configuração do CsvHelper:
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    Delimiter = ",",
                    HasHeaderRecord = true,
                };

                //Leitura do Arquivo
                using var reader = new StreamReader(clientsPath);
                using var csv = new CsvParser(reader, config);

                if (!csv.Read())
                    return;

                while (csv.Read())
                {
                    var account = accountRepository.GetAccount(csv.Record[2]);
                    _clients.Add(new Client(csv.Record[0], csv.Record[1], account, bool.Parse(csv.Record[3])));
                }
            }
            else
            {
                using var writer = new StreamWriter(clientsPath);
                WriteHeader(writer);
            }            
        }

        private void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("Name,Document,Account_Number,Activate");
        }

        public bool AddClient(Client client)
        {
            if (_clients.Any(x => x.Document.Equals(client.Document)))
            {
                Console.WriteLine("Cliente já cadastrado!");
                Console.ReadKey();

                return false;
            }

            var accountRepository = new AccountRepository();
            

            var entity = new Client(client.Name, client.Document, new Account(accountRepository.GetNewUnique()), client.Activate);
            
            _clients.Add(entity);

            return true;
        }

        public Client FindClient(string? document)
        {
            return _clients.FirstOrDefault(x=> x.Document.Equals(document));
        }

        public void ChangeClient(string? document, Client client)
        {
            var index = _clients.FindIndex(x => x.Document.Equals(document));                     
            var accountNumber = _clients[index].Account.Number;
            var accountRepository = new AccountRepository();

            _clients[index] = new Client(client.Name, client.Document, accountRepository.GetAccount(accountNumber), client.Activate);            

            Save();
        }

        public void Save()
        {
            using var write = new StreamWriter(clientsPath);
            WriteHeader(write);
            foreach(var client in _clients)
            {
                write.WriteLine(client.ToString());
            }
        }

        public Client GetClientByAccount(string accountNumber)
        {
            return _clients.FirstOrDefault(x => x.Account.Number.Equals(accountNumber));
        }

        public void GenerateFakeClients(int numberOfFakes)
        {
            var clientFaker = new Faker<Client>("pt_BR")
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Document, f => f.Person.Cpf())
                .RuleFor(c => c.Activate, f => f.PickRandomParam(new bool[] { true, false}));

            foreach (var client in clientFaker.Generate(numberOfFakes))
            {
                AddClient(client);
                var balanceFaker = new Faker();
                _clients.Last().Account.Deposit(Math.Round(balanceFaker.Random.Decimal(0, 100000),2));
            }

            Save();
        }

        public List<Client> GetClients()
        {
            return _clients;
        }

        public void ChangeClientByAccount(string accountNumber, Client client)
        {
            var index = _clients.FindIndex(x => x.Account.Number.Equals(accountNumber));
            var accountRepository = new AccountRepository();
            _clients[index] = new Client(client.Name, client.Document, accountRepository.GetAccount(accountNumber), client.Activate);

            Save();
        }
    }
}
