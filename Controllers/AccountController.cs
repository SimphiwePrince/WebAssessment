using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Restul_Web_Assessment.IEnumerables;
using Restul_Web_Assessment.Interfaces;
using Restul_Web_Assessment.Repository.Models;
using Restul_Web_Assessment.Repository.PostModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restul_Web_Assessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _execute;

        public AccountController(IAccount execute)
        {
            _execute = execute;
        }

        [HttpGet("GetAccount")]
        public IActionResult GetAccount(int accountNumber)
        {
            var results = _execute.GetAccount(accountNumber);
            if (results != null)
                return Ok(results);
            else
                return NotFound();
        }

        [HttpGet("GetUserAccounts")]
        public IActionResult GetUserAccounts(int userID)
        {
            var results = _execute.GetUserAccounts(userID);
            if (results.Count != 0)
                return Ok(results);
            else
                return NotFound();
        }

        [HttpPost("CreateAccount")]
        public IActionResult PostAccount(AccountDTO account)
        {
            var results = _execute.PostAccount(account);
            if (results != null)
                return Ok(results);
            else
                return BadRequest();
        }

        [HttpPut("Withdraw")]
        public IActionResult Withdraw(int accountnumber, int withdrawalamount)
        {
            var results = _execute.Withdraw(accountnumber, withdrawalamount);
            if(results)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("Deposit")]
        public IActionResult Deposit(int accountnumber, int depositamount)
        {
            var results = _execute.Deposit(accountnumber, depositamount);
            if(results)
                return Ok();
            else 
                return BadRequest();
        }
    }
}
