using AdaCredit.Entities;
using AdaCredit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class UpdateAccount
    {
        public static void Execute(Account account)
        {
            var accountRepository = new AccountRepository();
            accountRepository.ChangeAccount(account);
        }
    }
}
