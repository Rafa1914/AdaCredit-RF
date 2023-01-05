using Bogus;
using AdaCredit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace AdaCredit.Repositories
{
    public class AccountRepository
    {
        private List<Account> _accounts = new List<Account>();
        private string accountsPath = Path.Combine(Environment.CurrentDirectory, @"Accounts.txt");

        public AccountRepository() 
        {
            //Verificação da Existência do Arquivo:
            if (File.Exists(accountsPath))
            {
                //Configuração do CsvHelper:
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    Delimiter = ",",
                    HasHeaderRecord = true,
                };

                //Leitura do Arquivo
                using var reader = new StreamReader(accountsPath);
                using var csv = new CsvParser(reader, config);

                if (!csv.Read())
                    return;

                while (csv.Read())
                {
                    _accounts.Add(new Account(csv.Record[1], decimal.Parse(csv.Record[2],new CultureInfo("en-US"))));
                }
            }
            else
            {
                using var writer = new StreamWriter(accountsPath);
                WriteHeader(writer);
            }
        }

        private static void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("Branch,Account_Number,Balance");
        }

        public string GetNewUnique()
        {
            var exists = false;
            var accountNumber = String.Empty;
            do
            {
                accountNumber = new Faker().Random.ReplaceNumbers("######");
                exists = _accounts.Any(x => x.Number.Equals(accountNumber));
            } while (exists);

            _accounts.Add(new Account(accountNumber));

            Save();

            return accountNumber;
        }

        public Account GetAccount(string number)
        {
            return _accounts.FirstOrDefault(x=>x.Number.Equals(number));
        }

        private void Save()
        {
            using var writer = new StreamWriter(accountsPath);
            WriteHeader(writer);
            foreach (var account in _accounts)
            {
                writer.WriteLine(account.ToString());
            }
        }

        public void ChangeAccount(Account account)
        {
            var index = _accounts.FindIndex(x=>x.Number.Equals(account.Number));
            _accounts[index] = account;
            Save();
        }
    }
}
