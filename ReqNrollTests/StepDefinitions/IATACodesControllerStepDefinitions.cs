using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalutinisProjektas.Server.Controllers;
using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.HATEOASResourceModels;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Service;

namespace ReqNrollTests.StepDefinitions
{
    [Binding]
    public class IATACodesControllerStepDefinitions
    {
        private DbContextOptions<ModeldbContext> _dbContextOptions;
        private ModeldbContext _dbContext;
        private IATACodesService _service;
        private Mock<IMemoryCache> _memoryCacheMock;
        private Mock<IUrlHelper> _urlHelperMock;
        private IATACodesController _controller;
        private ActionResult<IEnumerable<IATACodes>> _listResult;
        private ActionResult<IATACodesResponse> _singleResult;

        private void SetupInMemoryDatabase()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ModeldbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ModeldbContext(_dbContextOptions);
            _memoryCacheMock = new Mock<IMemoryCache>();
            _urlHelperMock = new Mock<IUrlHelper>();

            _service = new IATACodesService(_dbContext);
            _controller = new IATACodesController(_service, _memoryCacheMock.Object)
            {
                Url = _urlHelperMock.Object
            };
            _listResult = null;
            _singleResult = null;
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _dbContext.IATACodes.AddRange(
                new IATACodes { Id = 1, IATA = "LAX", City = "Los Angeles", Country = "USA", AirportName = "Los Angeles International Airport" },
                new IATACodes { Id = 2, IATA = "JFK", City = "New York", Country = "USA", AirportName = "John F. Kennedy International Airport" }
            );
            _dbContext.SaveChanges();

            
            var count = _dbContext.IATACodes.Count();
            
            Assert.AreEqual(2, count, "Database seeding failed.");
        }

        [Given("the IATA codes service is available")]
        public void GivenTheIATACodesServiceIsAvailable()
        {
            if (_dbContext == null)
            {
                SetupInMemoryDatabase();

                // Setup mock UrlHelper
                _urlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                              .Returns((UrlActionContext context) => $"{context.Action}/{context.Controller}");

                // Setup mock MemoryCache
                object cacheValue;
                _memoryCacheMock.Setup(m => m.TryGetValue(It.IsAny<object>(), out cacheValue))
                                .Returns(false);
                _memoryCacheMock.Setup(m => m.CreateEntry(It.IsAny<object>()))
                                .Returns(Mock.Of<ICacheEntry>());
            }
        }

        [When("I request all IATA codes")]
        public async Task WhenIRequestAllIATACodes()
        {
            _listResult = await _controller.GetIATACodes();
           
        }

        [Then("the response should be successful and contain the IATA codes")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheIATACodes()
        {
            Assert.IsNotNull(_listResult, "_listResult is null");
            Assert.IsInstanceOf<ObjectResult>(_listResult.Result, "_listResult.Result is not an ObjectResult");

            var okResult = _listResult.Result as ObjectResult;
            Assert.IsNotNull(okResult, "okResult is null");

            var iataCodes = okResult.Value as IEnumerable<IATACodesResponse>;
            Assert.IsNotNull(iataCodes, "iataCodes is null");
            Assert.IsTrue(iataCodes.Any(), "iataCodes is empty");
        }

        [Given("the IATA code with ID {int} is available in the service")]
        public void GivenTheIATACodeWithIDIsAvailableInTheService(int p0)
        {
            if(_dbContext == null)
            {
                SetupInMemoryDatabase();

                // Setup mock UrlHelper
                _urlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                              .Returns((UrlActionContext context) => $"{context.Action}/{context.Controller}");

                // Setup mock MemoryCache
                object cacheValue;
                _memoryCacheMock.Setup(m => m.TryGetValue(It.IsAny<object>(), out cacheValue))
                                .Returns(false);
                _memoryCacheMock.Setup(m => m.CreateEntry(It.IsAny<object>()))
                                .Returns(Mock.Of<ICacheEntry>());
            }
        }

        [When("I request the IATA code by ID {int}")]
        public async Task WhenIRequestTheIATACodeByID(int id)
        {
            _singleResult = await ConvertToIATACodesResponse(_controller.GetIATACodes(id));
           
        }

        [Then("the response should be successful and contain the IATA code")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheIATACode()
        {
            Assert.IsNotNull(_singleResult, "_singleResult is null");
            Assert.IsInstanceOf<OkObjectResult>(_singleResult.Result, "_singleResult.Result is not an OkObjectResult");

            var okResult = _singleResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "okResult is null");

            var iataCode = okResult.Value as IATACodesResponse;
            Assert.IsNotNull(iataCode, "iataCode is null");
            Assert.AreEqual(1, iataCode.Id, "iataCode.Id is not 1");
        }

        [Given("the IATA code with code {string} is available in the service")]
        public void GivenTheIATACodeWithCodeIsAvailableInTheService(string lAX)
        {
            SetupInMemoryDatabase();

            // Setup mock UrlHelper
            _urlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                          .Returns((UrlActionContext context) => $"{context.Action}/{context.Controller}");

            // Setup mock MemoryCache
            object cacheValue;
            _memoryCacheMock.Setup(m => m.TryGetValue(It.IsAny<object>(), out cacheValue))
                            .Returns(false);
            _memoryCacheMock.Setup(m => m.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>());
        }

        [When("I request the IATA code by code {string}")]
        public async Task WhenIRequestTheIATACodeByCode(string code)
        {
            _singleResult = await ConvertToIATACodesResponse(_controller.GetByIATA(code));
            
        }

        [Then("the response should be successful and contain the IATA code by code")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheIATACodeByCode()
        {
            Assert.IsNotNull(_singleResult, "_singleResult is null");
            Assert.IsInstanceOf<OkObjectResult>(_singleResult.Result, "_singleResult.Result is not an OkObjectResult");

            var okResult = _singleResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "okResult is null");

            var iataCode = okResult.Value as IATACodesResponse;
            Assert.IsNotNull(iataCode, "iataCode is null");
            Assert.AreEqual("LAX", iataCode.IATA, "iataCode.IATA is not LAX");
        }
        //  helper method to convert ActionResult to ActionResult<IATACodesResponse>
        private async Task<ActionResult<IATACodesResponse>> ConvertToIATACodesResponse(Task<ActionResult<IATACodes>> task)
        {
            var actionResult = await task;
            if (actionResult.Result is OkObjectResult okResult && okResult.Value is IATACodes iataCode)
            {
                var iataCodeResponse = new IATACodesResponse
                {
                    Id = iataCode.Id,
                    IATA = iataCode.IATA,
                    City = iataCode.City,
                    Country = iataCode.Country,
                    AirportName = iataCode.AirportName,
                    Links = null
                };

                return new OkObjectResult(iataCodeResponse);
            }

            return actionResult.Result != null ? new ActionResult<IATACodesResponse>(actionResult.Result) : null;
        }
    }
}
