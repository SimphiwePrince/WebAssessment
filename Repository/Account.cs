using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Restul_Web_Assessment.Controllers;
using Restul_Web_Assessment.IEnumerables;
using Restul_Web_Assessment.Interfaces;
using Restul_Web_Assessment.Repository.Models;
using Restul_Web_Assessment.Repository.PostModels;

namespace Restul_Web_Assessment.Repository
{
    public class Account: IAccount
    {
        //private readonly ILogger<AccountController> _logger;
        private readonly BankingDbContext _db;
        public Account(BankingDbContext db) 
        { 
            _db = db;
        }

        public Models.Account GetAccount(int accountNumber)
        {
            try
            { 
                return _db.Accounts.SingleOrDefault(acc => acc.AccountNumber == accountNumber);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<Models.Account> GetUserAccounts(int userID)
        {
            try
            {
                return _db.Accounts.Where(acc => acc.UserId == userID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Models.Account PostAccount(AccountDTO accountInfo)
        {
            try
            {
                var account = new Models.Account
                {
                    AccountHolder = accountInfo.AccountHolder,
                    AccountType = accountInfo.AccountType,
                    IsActive = "True",                          //by default it should be true
                    DateModified = DateTime.UtcNow,             //last updated datetime
                    Balance = 0,                                //by default it should be 0
                    UserId = accountInfo.UserId
                };
                _db.Accounts.Add(account);
                _db.SaveChanges();
                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + " The user ID does not exist");
                return null;
            }
            
        }

        public bool Withdraw(int accountNumber, int withdrawlAmount)
        {
            try
            {
                var account = _db.Accounts.SingleOrDefault(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    if (account.Balance < withdrawlAmount)
                        throw new Exception("Insufficient funds");
                    else if (withdrawlAmount <= 0)
                        throw new Exception("Cannot withdraw an ammount less than or equal to zero");
                    else if (account.IsActive == "False")
                        throw new Exception("Cannot withdraw from an Inactive account");
                    else if (account.Balance == withdrawlAmount)
                    {
                        if (account.AccountType == "Fixed Deposit")
                            account.Balance -= withdrawlAmount;
                        else
                            throw new Exception("You can only withdraw 100% on the Fixed Deposit account");
                    }
                    else
                        account.Balance -= withdrawlAmount;
                    _db.SaveChanges();
                }
                else
                    throw new Exception("Account with account number " +  accountNumber + " does not exist");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString() + " Invalid withdrawal request");
                return false;
            }
            return true;
        }

        public bool Deposit(int accountNumber, int depositAmount)
        {
            try
            {

                var account = _db.Accounts.SingleOrDefault(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    if (depositAmount <= 0)
                        throw new Exception("Cannot deposit an amount less than or equal zero");
                    else if (account.IsActive != "True")
                        throw new Exception("Cannot deposit into an inactive account");
                    else
                        account.Balance += depositAmount;
                    _db.SaveChanges();
                }
                else
                    throw new Exception("Account with account number: " + accountNumber + " was not found");
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString() + " invalid deposit request");
                return false;
            }
            return true;
        }
    }
}
