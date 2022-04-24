using Account.Api.model;
using Account.Api.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quoting.Database.Model.Maintenance;
using QuotingApi.ViewModel;
using System.Threading.Tasks;

namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(IAccountRepository repository)
        {
            Repository = repository;
        }

        public IAccountRepository Repository { get; }



        [HttpGet("[action]")]
        public async Task<ActionResult<GenericResponse<AccountModel>>> Get(int id)
        {
            return Ok(await Repository.Get(id));
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<GenericResponse<AccountModel>>> Create(AccountRequestModel request)
        {
            var valid = request.IsValid();
            if (valid.Item1)
            {
                return Ok(await Repository.Add(request));
            }
            else
            {
                return Ok(new GenericResponse<AccountModel> { 
                    Message =valid.Item2
                });
            }
        }



        [HttpPut("[action]")]
        public async Task<ActionResult<GenericResponse<AccountModel>>> Modify(AccountModifyModel request)
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
