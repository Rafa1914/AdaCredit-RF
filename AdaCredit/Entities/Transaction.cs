using AdaCredit.UseCases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.Entities
{
    public sealed class Transaction
    {
        public string OriginBank { get; private set; }
        public string OriginBranch { get; private set; }
        public string OriginAccountNumber { get; private set; }
        public string DestinyBank { get; private set; }
        public string DestinyBranch { get; private set; }
        public string DestinyAccountNumber { get; private set; }
        public string Type { get; private set; }
        public int Direction { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Fee { get; private set; }
        public string ErrorMessage { get; set; }
        public bool Valid { get; set; }

        public Transaction()
        {

        }

        public Transaction(string originBank, string originBranch, string originAccount,
            string destinyBank, string destinyBranch, string destinyAccount,
            string type, int direction, decimal amount)
        {
            OriginBank = originBank;
            OriginBranch = originBranch;
            OriginAccountNumber = originAccount;
            DestinyBank = destinyBank;
            DestinyBranch = destinyBranch;
            DestinyAccountNumber = destinyAccount;
            Type = type;
            Direction = direction;
            Amount = amount;
            if (DateTime.Compare(new DateTime(2022, 11, 30), DateTime.Today) > 0)
                Fee = 0;
            else
                Fee = GetTransactionFee.Execute(type, amount);
            Valid = true;
            ErrorMessage = "";
        }

        public void Validate()
        {
            Valid = ValidateTransaction.Execute(this);
        }

        public override string ToString()
        {
            return $"{OriginBank},{OriginBranch},{OriginAccountNumber},{DestinyBank},{DestinyBranch},{DestinyAccountNumber},{Type},{Direction},{Amount.ToString(new CultureInfo("en-US"))}";
        }

        public void WriteTransactionData()
        {
            Console.WriteLine();
            Console.WriteLine($"Banco de Origem: {OriginBank}");
            Console.WriteLine($"Agência de Origem: {OriginBranch}");
            Console.WriteLine($"Conta de Origem: {OriginAccountNumber.Insert(5,"-")}");
            Console.WriteLine($"Banco de Destino: {DestinyBank}");
            Console.WriteLine($"Agência de Destino: {DestinyBranch}");
            Console.WriteLine($"Conta de Destino: {DestinyAccountNumber.Insert(5,"-")}");
            Console.WriteLine($"Tipo da Transação: {Type}");
            Console.WriteLine($"Valor da Transação: R$ {Amount}");
            Console.WriteLine($"Motivo da Falha: {ErrorMessage}");
            Console.WriteLine("------------------------------------");
        }
    }
}
