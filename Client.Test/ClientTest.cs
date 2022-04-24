using Client.Api.Controllers;
using Client.Api.model;
using Client.Api.repository;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quoting.Database.Model;
using QuotingApi.ViewModel;
using System.Threading.Tasks;

namespace Client.Test
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public async Task AddOk()
        {
            var repo = A.Fake<IClientRepository>();
            var request = new ClientRequestModel
            {
                Age = 25,
                Direction = "Direccion de prueba",
                Genre = "Masculino",
                IdCard = "0928450995",
                Name = "Nombre prueba Unitaria",
                Password = "9173.*Prueba",
                Phone = "0982070158"
            };
            var response = new GenericResponse<ClientModel> { 
                Success=true,
                Message="Success"
            };
            A.CallTo(() => repo.Add(request)).Returns(response);
            var controller= new ClientController(repo);
            var actionResult = await controller.Create(request);
            var result = actionResult.Result as OkObjectResult;
            var restResponse=result.Value as GenericResponse<ClientModel>;
            Assert.AreEqual(response.Success, restResponse.Success);
        }
        [TestMethod]
        public async Task getOk()
        {
            var repo = A.Fake<IClientRepository>();
            var request = new ClientRequestModel
            {
                Age = 25,
                Direction = "Direccion de prueba",
                Genre = "Masculino",
                IdCard = "0928450995",
                Name = "Nombre prueba Unitaria",
                Password = "9173.*Prueba",
                Phone = "0982070158"
            };
            var response = new GenericResponse<ClientModel>
            {
                Success = false,
                Message = "Client not found"
            };
            A.CallTo(() => repo.Get(1)).Returns(response);

            var controller = new ClientController(repo);
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var restResponse = result.Value as GenericResponse<ClientModel>;
            Assert.AreEqual(response.Message, restResponse.Message);
        }
    }
}
