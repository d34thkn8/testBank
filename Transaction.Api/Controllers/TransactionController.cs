using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quoting.Database.Model.Maintenance;
using QuotingApi.ViewModel;
using System.Threading.Tasks;
using Transaction.Api.model;
using Transaction.Api.repository;

namespace Transaction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionController(ITransactionRepository repository)
        {
            Repository = repository;
        }

        public ITransactionRepository Repository { get; }



        [HttpGet("[action]")]
        public async Task<ActionResult<GenericResponse<AccountModel>>> Get([FromQuery] int id)
        {
            return Ok(await Repository.Get(id));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<GenericResponse<AccountModel>>> GetClientReport([FromQuery] TransactionReportQuery query)
        {
            var valid = query.IsValid();
            if (valid.Item1)
            {
                return Ok(await Repository.GetAll(query));
            }
            else
            {
                return Ok(new GenericResponse<AccountModel>
                {
                    Message = valid.Item2
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<GenericResponse<AccountModel>>> Create(TransactionRequestModel request)
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
        public async Task<ActionResult<GenericResponse<AccountModel>>> Modify(TransactionModifyModel request)
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
