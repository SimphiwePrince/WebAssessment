using Restul_Web_Assessment.Repository.Models;
using Restul_Web_Assessment.Repository.PostModels;

namespace Restul_Web_Assessment.Interfaces
{
    public interface IAccount
    {
        Account GetAccount(int accountNumber);
        List<Account> GetUserAccounts(int userID);
        Account PostAccount(AccountDTO accountInfo);
        bool Withdraw(int accountNumber, int withdrawlAmount);
        bool Deposit(int accountNumber, int depositAmount);
    }
}
