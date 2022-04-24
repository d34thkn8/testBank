using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quoting.Database.Model;
using Quoting.Database.Repository;
using QuotingApi.ViewModel;

namespace QuotingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public ClientController(IGenericRepository<ClientModel> repository)
        {
            Repository = repository;
        }

        public IGenericRepository<ClientModel> Repository { get; }

        [HttpPost]
        public ActionResult<GenericResponse<ClientModel>>AddClient([FromBody] ClientModel client)
        {
            Repository.Add(client);
            return Ok(new GenericResponse { Message = "OK", Success = true });
        }
    }
}
