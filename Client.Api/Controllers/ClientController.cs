using Client.Api.model;
using Client.Api.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quoting.Database.Model;
using Quoting.Database.Model.Maintenance;
using QuotingApi.ViewModel;
using System.Threading.Tasks;

namespace Client.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public ClientController(IClientRepository repository)
        {
            Repository = repository;
        }

        public IClientRepository Repository { get; }



        [HttpGet("[action]")]
        public async Task<ActionResult<GenericResponse<ClientModel>>> Get(int id)
        {
            return Ok(await Repository.Get(id));
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<GenericResponse<ClientModel>>> Create(ClientRequestModel request)
        {
            var valid = request.IsValid();
            if (valid.Item1)
            {
                return Ok(await Repository.Add(request));
            }
            else
            {
                return Ok(new GenericResponse<AccountModel>
                {
                    Message = valid.Item2
                });
            }
        }



        [HttpPut("[action]")]
        public async Task<ActionResult<GenericResponse<ClientModel>>> Modify(ClientModifyModel request)
        {
            var valid = request.IsValid();
            if (valid.Item1)
            {
                return Ok(await Repository.Modify(request));
            }
            else
            {
                return Ok(new GenericResponse<AccountModel>
                {
                    Message = valid.Item2
                });
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<GenericResponse>> Delete(int id)
        {
            return Ok(await Repository.Delete(id));
        }
    }
}
