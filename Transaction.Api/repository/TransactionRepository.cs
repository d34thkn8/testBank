using Microsoft.EntityFrameworkCore;
using Quoting.Database.Database;
using Quoting.Database.Model.Maintenance;
using QuotingApi.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Api.model;

namespace Transaction.Api.repository
{
    public interface ITransactionRepository
    {
        Task<GenericResponse<TransactionModel>> Add(TransactionRequestModel acc);
        Task<GenericResponse<TransactionModel>> Get(int AccountId);
        Task<GenericResponse> Delete(int AccountId);
        Task<GenericResponse<TransactionModel>> Modify(TransactionModifyModel acc);
        Task<GenericResponse<List<TransactionReportModel>>> GetAll(TransactionReportQuery Query);
    }
    public class TransactionRepository : ITransactionRepository
    {
        public ChallengeDatabase Db { get; }
        public TransactionRepository(ChallengeDatabase exampleDatabase)
        {
            Db = exampleDatabase;
        }


        public async Task<GenericResponse<TransactionModel>> Add(TransactionRequestModel transaction)
        {
            try
            {
                var _account=await Db.Accounts.FindAsync(transaction.AccountId);
                if (_account != null)
                {
                    if (transaction.Value > 0)
                    {
                        var _transaction = new TransactionModel
                        {
                            AccountId = transaction.AccountId,
                            Value = transaction.Value,
                            ClientId = _account.ClientId,
                            Date=System.DateTime.Now,
                            Balance=_account.Balance+transaction.Value,
                            Status = true,
                            TypeId = 1,
                            Type ="Deposit"
                    };
                        _account.Balance = _account.Balance + transaction.Value;
                        await Db.Transactions.AddAsync(_transaction);
                        await Db.SaveChangesAsync();
                        return new GenericResponse<TransactionModel>
                        {
                            Message = "Success",
                            Result = _transaction,
                            Success = true
                        };
                    }
                    else
                    {
                        if(transaction.Value >= -1000)
                        {
                            if (transaction.Value < 0)
                            {
                                if (_account.Balance > 0)
                                {
                                    var withdrawals = await Db.Transactions.Where(tr => tr.Date.Date == System.DateTime.Now.Date && tr.AccountId == transaction.AccountId && tr.TypeId == 2).ToListAsync();
                                    decimal sum = 0;
                                    foreach (var withdrawal in withdrawals)
                                    {
                                        sum += withdrawal.Value;
                                    }
                                    sum += (transaction.Value * -1);
                                    if (sum <= 1000)
                                    {
                                        var _transaction = new TransactionModel
                                        {
                                            AccountId = transaction.AccountId,
                                            Value = transaction.Value * -1,
                                            ClientId = _account.ClientId,
                                            Date = System.DateTime.Now,
                                            Balance = _account.Balance + transaction.Value,
                                            Status = true,
                                            Type = "Withdrawal",
                                            TypeId=2
                                        };
                                        _account.Balance = _account.Balance + transaction.Value;
                                        await Db.Transactions.AddAsync(_transaction);
                                        await Db.SaveChangesAsync();
                                        return new GenericResponse<TransactionModel>
                                        {
                                            Message = "Success",
                                            Result = _transaction,
                                            Success = true
                                        };
                                    }
                                    else
                                    {
                                        return new GenericResponse<TransactionModel>
                                        {
                                            Message = "Maximun Withdrawal daily amount exceeded"
                                        };
                                    }
                                }
                                else
                                {
                                    return new GenericResponse<TransactionModel>
                                    {
                                        Message = "No balance available"
                                    };
                                }
                                
                            }
                            else
                            {
                                return new GenericResponse<TransactionModel>
                                {
                                    Message = "Sorry, Transaction value can't be 0"
                                };
                            }
                            
                        }
                        else
                        {
                            return new GenericResponse<TransactionModel>
                            {
                                Message = "Sorry, Withdrawal amount limit is $1000"
                            };
                        }
                        
                    }
                    
                }
                else
                {
                    return new GenericResponse<TransactionModel>
                    {
                        Message = "Account not found"
                    };
                }
                
            }
            catch (System.Exception ex)
            {
                return new GenericResponse<TransactionModel>
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<TransactionModel>> Get(int TransactionId)
        {
            try
            {
                var _transaction = await Db.Transactions.FindAsync(TransactionId);
                return new GenericResponse<TransactionModel>
                {
                    Success = _transaction != null,
                    Result = _transaction,
                    Message = _transaction != null ? "Success" : "Transaction not found"
                };

            }
            catch (System.Exception ex)
            {
                return new GenericResponse<TransactionModel>
                {
                    Message = ex.Message
                };
            }
        }
        public async Task<GenericResponse<List<TransactionReportModel>>> GetAll(TransactionReportQuery Query)
        {
            try
            {
                var accounts = await Db.Accounts.Where(acc=>acc.ClientId==Query.ClientId).ToListAsync();
                var client=await Db.Clients.FindAsync(Query.ClientId);
                if (client != null)
                {
                    var output = new List<TransactionReportModel>();
                    foreach (var account in accounts)
                    {
                        var transactions = await Db.Transactions.Where(
                            tr => tr.AccountId == account.AccountId && tr.Date.Date >= Query.StartDate.Date && tr.Date.Date <= Query.EndDate.Date).ToListAsync();
                        foreach (var transaction in transactions)
                        {
                            output.Add(new TransactionReportModel
                            {
                                AccountNumber = account.AccountNumber,
                                AccountType = account.AccountType,
                                Client=client.Name,
                                Date = transaction.Date,
                                FinalBalance=transaction.Balance,
                                Status = transaction.Status,
                                TransactionValue = transaction.Value,
                                Transaction = (transaction.TypeId==1 ? "Deposit" : "Withdrawal") + ": "+ transaction.Value,
                            });
                        }
                    }
                    return new GenericResponse<List<TransactionReportModel>>
                    {
                        Success = output != null && output.Count>0,
                        Result = output,
                        Message = output != null && output.Count > 0 ? "Success" : "No records found"
                    };
                }
                else
                {
                    return new GenericResponse<List<TransactionReportModel>>
                    {
                        Message = "Client not found"
                    };
                }
                

            }
            catch (System.Exception ex)
            {
                return new GenericResponse<List<TransactionReportModel>>
                {
                    Message = ex.Message
                };
            }
        }
        public async Task<GenericResponse> Delete(int AccountId)
        {
            try
            {
                var _acc = await Db.Transactions.FindAsync(AccountId);
                if (_acc != null)
                {
                    _acc.Status = false;
                    await Db.SaveChangesAsync();
                    return new GenericResponse
                    {
                        Success = true,
                        Message = "Success"
                    };
                }
                else
                {
                    return new GenericResponse
                    {
                        Success = false,
                        Message = "Transaction not found"
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new GenericResponse
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<TransactionModel>> Modify(TransactionModifyModel transaction)
        {
            try
            {
                var _transaction = await Db.Transactions.FindAsync(transaction.TransactionId);
                if (_transaction != null)
                {
                    _transaction.Status = transaction.Status;
                    _transaction.Balance = transaction.Balance;
                    _transaction.Date = transaction.Date;
                    _transaction.Value = transaction.Value;

                    await Db.SaveChangesAsync();
                    return new GenericResponse<TransactionModel>
                    {
                        Success = true,
                        Result = _transaction,
                        Message = "Success"
                    };
                }
                else
                {
                    return new GenericResponse<TransactionModel>
                    {
                        Success = false,
                        Message = "Transaction not found"
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new GenericResponse<TransactionModel>
                {
                    Message = ex.Message
                };
            }
        }
    }
}
