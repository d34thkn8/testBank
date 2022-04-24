using Account.Api.model;
using Microsoft.EntityFrameworkCore;
using Quoting.Database.Database;
using Quoting.Database.Model.Maintenance;
using QuotingApi.ViewModel;
using System.Threading.Tasks;

namespace Account.Api.repository
{
    public interface IAccountRepository
    {
        Task<GenericResponse<AccountModel>> Add(AccountRequestModel acc);
        Task<GenericResponse<AccountModel>> Get(int AccountId);
        Task<GenericResponse> Delete(int AccountId);
        Task<GenericResponse<AccountModel>> Modify(AccountModifyModel acc);
    }
    public class AccountRepository : IAccountRepository
    {
        public ChallengeDatabase Db { get; }
        public AccountRepository(ChallengeDatabase exampleDatabase)
        {
            Db = exampleDatabase;
        }


        public async Task<GenericResponse<AccountModel>> Add(AccountRequestModel account)
        {
            try
            {
                var _acc = new AccountModel
                {
                    AccountNumber = account.AccountNumber,
                    AccountType = account.AccountType,
                    Balance = account.Balance,
                    ClientId = account.ClientId,
                    Status=true
                };
                await Db.Accounts.AddAsync(_acc);
                await Db.SaveChangesAsync();
                return new GenericResponse<AccountModel>
                {
                    Message = "Success",
                    Result = _acc,
                    Success = true
                };
            }
            catch (System.Exception ex)
            {
                return new GenericResponse<AccountModel>
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<AccountModel>> Get(int AccountId)
        {
            try
            {
                var _acc=await Db.Accounts.FindAsync(AccountId);
                return new GenericResponse<AccountModel> { 
                    Success= _acc!=null,
                    Result = _acc,
                    Message = _acc != null ? "Success":"Acount not found"
                };

            }
            catch (System.Exception ex)
            {
                return new GenericResponse<AccountModel>
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse> Delete(int AccountId)
        {
            try
            {
                var _acc = await Db.Accounts.FindAsync(AccountId);
                if (_acc != null) {
                    _acc.Status=false;
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
                        Message = "Acount not found"
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

        public async Task<GenericResponse<AccountModel>> Modify(AccountModifyModel acc)
        {
            try
            {
                var _acc = await Db.Accounts.FindAsync(acc.AccountId);
                if (_acc != null)
                {
                    _acc.Status = acc.Status;
                    _acc.Balance = acc.Balance;
                    _acc.AccountType= acc.AccountType;
                    await Db.SaveChangesAsync();
                    return new GenericResponse<AccountModel>
                    {
                        Success = true,
                        Result = _acc,
                        Message = "Success"
                    };
                }
                else
                {
                    return new GenericResponse<AccountModel>
                    {
                        Success = false,
                        Message = "Acount not found"
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new GenericResponse<AccountModel>
                {
                    Message = ex.Message
                };
            }
        }
    }
}
