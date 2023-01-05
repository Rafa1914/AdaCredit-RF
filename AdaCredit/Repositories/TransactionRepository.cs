using AdaCredit.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace AdaCredit.Repositories
{
    public class TransactionRepository
    {
        private List<Transaction> _transactions = new List<Transaction>();
        private string transactionsDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Transactions");
        public TransactionRepository()
        {
            string transactionsFileName = @"nome-do-banco-parceiro-aaaammdd-completed.csv";
            string transactionsPath = Path.Combine(transactionsDirectoryPath, transactionsFileName);
            //Verificação da Existência do Arquivo:
            if (File.Exists(transactionsPath))
            {
                //Configuração do CsvHelper:
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    Delimiter = ",",
                    HasHeaderRecord = true,
                };

                //Leitura do Arquivo
                using var reader = new StreamReader(transactionsPath);
                using var csv = new CsvParser(reader, config);

                while (csv.Read())
                {
                    _transactions.Add(new Transaction(csv.Record[0], csv.Record[1], csv.Record[2],
                        csv.Record[3], csv.Record[4], csv.Record[5],
                        csv.Record[6], int.Parse(csv.Record[7]), decimal.Parse(csv.Record[8],new CultureInfo("en-US"))));
                    _transactions.Last().Validate();
                 }

            }
            else
            {
                //string transactionsDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Transactions");
                DirectoryInfo di = new DirectoryInfo(transactionsDirectoryPath);
                if(!di.Exists)
                {
                    di.Create();
                }
                using var writer = new StreamWriter(transactionsPath);
            }
        }

        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }

        public void SaveCompleted()
        {
            string transactionsCompletedDirectoryPath = Path.Combine(transactionsDirectoryPath, @"Completed");
            string transactionsCompletedFileName = @"nome-do-banco-parceiro-aaaammdd-completed.csv";
            string transactionsCompletedPath = Path.Combine(transactionsCompletedDirectoryPath, transactionsCompletedFileName);
            DirectoryInfo di = new DirectoryInfo(transactionsCompletedDirectoryPath);

            if(!di.Exists)
            {
                di.Create();
            }

            using var writer = new StreamWriter(transactionsCompletedPath);
            foreach(var transaction in _transactions)
            {
                if(transaction.Valid)
                {
                    writer.WriteLine(transaction.ToString());
                }
                    
            }
        }

        public void SaveFailed()
        {
            string transactionsFailedDirectoryPath = Path.Combine(transactionsDirectoryPath, @"Failed");
            string transactionsFailedFileName = @"nome-do-banco-parceiro-aaaammdd-failed.csv";
            string transactionsFailedPath = Path.Combine(transactionsFailedDirectoryPath, transactionsFailedFileName);
            DirectoryInfo di = new DirectoryInfo(transactionsFailedDirectoryPath);

            if (!di.Exists)
            {
                di.Create();
            }
                
            using var writer = new StreamWriter(transactionsFailedPath);
            foreach (var transaction in _transactions)
            {
                if (!transaction.Valid)
                    writer.WriteLine(transaction.ToString()+$",{transaction.ErrorMessage}");
            }
        }

        public void GenerateFakeTransactions(int numberOfFakes)
        {
            var clientRepository = new ClientRepository();
            var _clients = clientRepository.GetClients();
            string transactionsFileName = @"nome-do-banco-parceiro-aaaammdd-completed.csv";
            string transactionsPath = Path.Combine(transactionsDirectoryPath, transactionsFileName);
            using var writer = new StreamWriter(transactionsPath);
            //Transações Quaisquer
            var fakeTransactions = new Faker<Transaction>()
                .RuleFor(t => t.OriginBank, f => f.Random.ReplaceNumbers("###"))
                .RuleFor(t => t.OriginBranch, f => f.Random.ReplaceNumbers("####"))
                .RuleFor(t => t.OriginAccountNumber, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(t => t.DestinyBank, f => f.Random.ReplaceNumbers("###"))
                .RuleFor(t => t.DestinyBranch, f => f.Random.ReplaceNumbers("####"))
                .RuleFor(t => t.DestinyAccountNumber, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(t => t.Type, f => f.PickRandom("TED", "DOC", "TEF"))
                .RuleFor(t => t.Direction, f => f.PickRandom(1, 0))
                .RuleFor(t => t.Amount, f => Math.Round(f.Random.Decimal(0, 100000),2));
            
            foreach(var transaction in fakeTransactions.Generate(numberOfFakes))
            {
                writer.WriteLine(transaction.ToString());
            }

            //Transações Com Destino a Ada 
            fakeTransactions = new Faker<Transaction>()
                .RuleFor(t => t.OriginBank, f => f.Random.ReplaceNumbers("###"))
                .RuleFor(t => t.OriginBranch, f => f.Random.ReplaceNumbers("####"))
                .RuleFor(t => t.OriginAccountNumber, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(t => t.DestinyBank, f => "777")
                .RuleFor(t => t.DestinyBranch, f => "0001")
                .RuleFor(t => t.DestinyAccountNumber, f => _clients[new Faker().Random.Int(0,_clients.Count-1)].Account.Number)
                .RuleFor(t => t.Type, f => f.PickRandom("TED", "DOC", "TEF"))
                .RuleFor(t => t.Direction, f => f.PickRandom(1, 0))
                .RuleFor(t => t.Amount, f => Math.Round(f.Random.Decimal(0, 100000), 2));

            foreach (var transaction in fakeTransactions.Generate(numberOfFakes))
            {
                writer.WriteLine(transaction.ToString());
            }

            //Transações Com Origem a Ada 
            fakeTransactions = new Faker<Transaction>()
                .RuleFor(t => t.OriginBank, f => "777")
                .RuleFor(t => t.OriginBranch, f => "0001")
                .RuleFor(t => t.OriginAccountNumber, f => _clients[new Faker().Random.Int(0, _clients.Count - 1)].Account.Number)
                .RuleFor(t => t.DestinyBank, f => f.Random.ReplaceNumbers("###"))
                .RuleFor(t => t.DestinyBranch, f => f.Random.ReplaceNumbers("####"))
                .RuleFor(t => t.DestinyAccountNumber, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(t => t.Type, f => f.PickRandom("TED", "DOC", "TEF"))
                .RuleFor(t => t.Direction, f => f.PickRandom(1, 0))
                .RuleFor(t => t.Amount, f => Math.Round(f.Random.Decimal(0, 100000), 2));

            foreach (var transaction in fakeTransactions.Generate(numberOfFakes))
            {
                writer.WriteLine(transaction.ToString());
            }

            //Transações Com Entre Contas Ada 
            fakeTransactions = new Faker<Transaction>()
                .RuleFor(t => t.OriginBank, f => "777")
                .RuleFor(t => t.OriginBranch, f => "0001")
                .RuleFor(t => t.OriginAccountNumber, f => _clients[new Faker().Random.Int(0, _clients.Count - 1)].Account.Number)
                .RuleFor(t => t.DestinyBank, f => "777")
                .RuleFor(t => t.DestinyBranch, f => "0001")
                .RuleFor(t => t.DestinyAccountNumber, f => _clients[new Faker().Random.Int(0, _clients.Count - 1)].Account.Number)
                .RuleFor(t => t.Type, f => f.PickRandom("TED", "DOC", "TEF"))
                .RuleFor(t => t.Direction, f => f.PickRandom(1, 0))
                .RuleFor(t => t.Amount, f => Math.Round(f.Random.Decimal(0, 100000), 2));

            foreach (var transaction in fakeTransactions.Generate(numberOfFakes))
            {
                writer.WriteLine(transaction.ToString());
            }

            //Transações Com Entre Contas Ada Que Podem Não Existir 
            fakeTransactions = new Faker<Transaction>()
                .RuleFor(t => t.OriginBank, f => "777")
                .RuleFor(t => t.OriginBranch, f => "0001")
                .RuleFor(t => t.OriginAccountNumber, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(t => t.DestinyBank, f => "777")
                .RuleFor(t => t.DestinyBranch, f => "0001")
                .RuleFor(t => t.DestinyAccountNumber, f => f.Random.ReplaceNumbers("######"))
                .RuleFor(t => t.Type, f => f.PickRandom("TED", "DOC", "TEF"))
                .RuleFor(t => t.Direction, f => f.PickRandom(1, 0))
                .RuleFor(t => t.Amount, f => Math.Round(f.Random.Decimal(0, 100000), 2));

            foreach (var transaction in fakeTransactions.Generate(numberOfFakes))
            {
                writer.WriteLine(transaction.ToString());
            }
        }

    }
}
