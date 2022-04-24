using Client.Api.model;
using Microsoft.EntityFrameworkCore;
using Quoting.Database.Database;
using Quoting.Database.Model;
using Quoting.Database.Model.Maintenance;
using QuotingApi.ViewModel;
using System.Threading.Tasks;

namespace Client.Api.repository
{
    public interface IClientRepository
    {
        Task<GenericResponse<ClientModel>> Add(ClientRequestModel Client);
        Task<GenericResponse<ClientModel>> Get(int Id);
        Task<GenericResponse> Delete(int Id);
        Task<GenericResponse<ClientModel>> Modify(ClientModifyModel Client);
    }
    public class ClientRepository : IClientRepository
    {
        public ChallengeDatabase Db { get; }
        public ClientRepository(ChallengeDatabase exampleDatabase)
        {
            Db = exampleDatabase;
        }


        public async Task<GenericResponse<ClientModel>> Add(ClientRequestModel client)
        {
            try
            {
                var clientX = await Db.Clients.FirstOrDefaultAsync(cli => cli.IdCard.Equals(client.IdCard) && cli.Status);
                if (clientX == null)
                {
                    var _client = new ClientModel
                    {
                        IdCard = client.IdCard,
                        Name = client.Name,
                        Genre = client.Genre,
                        Age = client.Age,
                        Direction = client.Direction,
                        Phone = client.Phone,
                        Password = client.Password,
                        Status = true
                    };
                    await Db.Clients.AddAsync(_client);
                    await Db.SaveChangesAsync();
                    return new GenericResponse<ClientModel>
                    {
                        Message = "Success",
                        Result = _client,
                        Success = true
                    };
                }
                else
                {
                    return new GenericResponse<ClientModel>
                    {
                        Message="Identification card already exists"
                    };
                }
                
            }
            catch (System.Exception ex)
            {
                return new GenericResponse<ClientModel>
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse<ClientModel>> Get(int ClientId)
        {
            try
            {
                var _acc = await Db.Clients.FindAsync(ClientId);
                return new GenericResponse<ClientModel>
                {
                    Success = _acc != null,
                    Result = _acc,
                    Message = _acc != null ? "Success" : "Client not found"
                };

            }
            catch (System.Exception ex)
            {
                return new GenericResponse<ClientModel>
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<GenericResponse> Delete(int AccountId)
        {
            try
            {
                var _client = await Db.Clients.FindAsync(AccountId);
                if (_client != null)
                {
                    _client.Status = false;
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
                        Message = "Client not found"
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

        public async Task<GenericResponse<ClientModel>> Modify(ClientModifyModel account)
        {
            try
            {
                var _client = await Db.Clients.FindAsync(account.ClientId);
                if (_client != null)
                {
                    _client.IdCard = account.IdCard;
                    _client.Name = account.Name;
                    _client.Genre = account.Genre;
                    _client.Age = account.Age;
                    _client.Direction = account.Direction;
                    _client.Phone = account.Phone;
                    _client.Password = account.Password;
                    _client.Status = account.Status;
                    await Db.SaveChangesAsync();
                    return new GenericResponse<ClientModel>
                    {
                        Success = true,
                        Result = _client,
                        Message = "Success"
                    };
                }
                else
                {
                    return new GenericResponse<ClientModel>
                    {
                        Success = false,
                        Message = "Client not found"
                    };
                }
            }
            catch (System.Exception ex)
            {
                return new GenericResponse<ClientModel>
                {
                    Message = ex.Message
                };
            }
        }
    }
}
