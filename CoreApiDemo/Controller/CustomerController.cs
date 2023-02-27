using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingSystem.Data;
using BankingSystem.Models;
using NuGet.Versioning;
using BankingSystem.Model;

namespace BankingSystem.Controller
{
    [ApiController]
    [Route("api/customers")]

    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CustomerController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(CheckCustomerBalance))]
        public async Task<string> CheckCustomerBalance(string UserName)
        {
            string message = string.Empty;
            if (_context.customers == null)
            {
                message = "No Data found";
            }
            var customerDetails = (from custAccount in _context.customerAccounts
                                   join cust in _context.customers on custAccount.CustomerId equals cust.CustomerId
                                   where cust.UserName == UserName
                                   select custAccount).FirstOrDefault();

            if (customerDetails == null)
            {
                message = "Customer does not exists";
            }
            else
            {
                message = "Customer current balance is :" + customerDetails.CurrentBalance;
            }
            return message;
        }


        // GET: api/Customers
        [HttpGet(nameof(GetTransactionHistory))]
        public async Task<List<TransactionList>> GetTransactionHistory(string UserName, DateTime fromDate, DateTime toDate)
        {
            if (_context.customers == null)
            {
                return null;
            }
            var transactionList = (from transactions in _context.transactions
                                   join custAccount in _context.customerAccounts on transactions.AccountId equals custAccount.AccountId
                                   join transType in _context.transactionTypes on transactions.TransactionTypeId equals transType.TransactionTypeId
                                   join cust in _context.customers on custAccount.CustomerId equals cust.CustomerId
                                   where cust.UserName == UserName && (transactions.TransactionDate >= fromDate && transactions.TransactionDate <= toDate)
                                   select new TransactionList
                                   {
                                       CustomerName = cust.FirstName,
                                       Amount = transactions.Amount,
                                       TransactionType = transType.TransactionName,
                                       TransactionDate = transactions.TransactionDate,
                                       Remarks = transactions.Remarks
                                   } into result
                                   select result);

            return transactionList.ToList();
        }


        

        [HttpPost(nameof(TransferFunds))]
        public async Task<string> TransferFunds(string fromAccountnumber, string toAccountNumber, decimal amount, string Remarks)
        {
            bool ok = false;
            string message = string.Empty;
            try
            {
                var fromCustomerData = (from fromcust in _context.customerAccounts where fromcust.AccountNumber == fromAccountnumber select fromcust).FirstOrDefault();
                var toCustomerData = (from tocust in _context.customerAccounts where tocust.AccountNumber == toAccountNumber select tocust).FirstOrDefault();

                if (fromCustomerData == null)
                {
                    return "Customer  not found";
                }
                if (toCustomerData == null)
                {
                    return "Customer  is not valid";
                }

                #region Check customer balance before transfer

                if (fromCustomerData.CurrentBalance >= amount)
                {
                    #region Debit from accout
                    var debitTransaction = (from trans in _context.transactionTypes where trans.TransactionName == "Debit" select trans.TransactionTypeId).FirstOrDefault();
                    Transaction debitTrans = new Transaction();
                    debitTrans.AccountId = fromCustomerData.AccountId;
                    debitTrans.TransactionDate = DateTime.Now;
                    debitTrans.Amount = amount;
                    debitTrans.Remarks = Remarks;
                    debitTrans.TransactionTypeId = debitTransaction;
                    debitTrans.CreatedDate = DateTime.Now;
                    debitTrans.Updateddate = DateTime.Now;
                    debitTrans.CreatedBy = fromCustomerData.CreatedById;

                    await _context.transactions.AddAsync(debitTrans);

                    #region Update debited balance
                    _context.customerAccounts.Attach(fromCustomerData);
                    fromCustomerData.CurrentBalance = fromCustomerData.CurrentBalance - amount;
                    #endregion


                    #endregion

                    #region Credit to account

                    var creditTransaction = (from trans in _context.transactionTypes where trans.TransactionName == "Credit" select trans.TransactionTypeId).FirstOrDefault();
                    Transaction creditTrans = new Transaction();
                    creditTrans.AccountId = toCustomerData.AccountId;
                    creditTrans.TransactionDate = DateTime.Now;
                    creditTrans.Amount = amount;
                    creditTrans.Remarks = Remarks;
                    creditTrans.TransactionTypeId = creditTransaction;
                    creditTrans.CreatedDate = DateTime.Now;
                    creditTrans.Updateddate = DateTime.Now;
                    creditTrans.CreatedBy = fromCustomerData.CreatedById;

                    var result = _context.transactions.AddAsync(creditTrans);

                    #region Update debited balance
                    _context.customerAccounts.Attach(toCustomerData);
                    toCustomerData.CurrentBalance = toCustomerData.CurrentBalance + amount;
                    #endregion

                    _context.SaveChanges();
                    #endregion
                    if (result.IsCompleted)
                    {
                        ok = true;
                        message = "Transaction completed successfully!";
                    }
                }
                else
                {
                    message = "Customer do not have enough balance";
                }
                #endregion


            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                _context.Dispose();
            }
            return message;
        }

        //private bool CustomerExists(int id)
        //{
        //    return (_context.customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        //}
    }
}
