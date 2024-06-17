using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalutinisProjektas.Server.Controllers;
using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.HATEOASResourceModels;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Service;
using Microsoft.EntityFrameworkCore;

namespace ReqNrollTests.StepDefinitions
{
    [TestFixture]
    [Binding]
    public class IATACodesControllerStepDefinitions
    {
        private Mock<DbSet<IATACodes>> _dbSetMock;
        private Mock<ModeldbContext> _dbContextMock;
        private IATACodesService _service;
        private Mock<IMemoryCache> _memoryCacheMock;
        private IATACodesController _controller;
        private ActionResult<IEnumerable<IATACodesResponse>> _listResult;
        private ActionResult<IATACodesResponse> _singleResult;

        public IATACodesControllerStepDefinitions()
        {
            _dbSetMock = new Mock<DbSet<IATACodes>>();
            _dbContextMock = new Mock<ModeldbContext>();
            _service = new IATACodesService(_dbContextMock.Object);
            _memoryCacheMock = new Mock<IMemoryCache>();
            _controller = new IATACodesController(_service, _memoryCacheMock.Object);
            _listResult = null;
            _singleResult = null;
        }

        [SetUp]
        public void Setup()
        {
            _dbSetMock = new Mock<DbSet<IATACodes>>();
            _dbContextMock = new Mock<ModeldbContext>();
            _service = new IATACodesService(_dbContextMock.Object);
            _memoryCacheMock = new Mock<IMemoryCache>();
            _controller = new IATACodesController(_service, _memoryCacheMock.Object);
            _listResult = null;
            _singleResult = null;
        }

        [Given("the IATA codes service is available")]
        public void GivenTheIATACodesServiceIsAvailable()
        {
            // Setup or initialization if needed
        }

        [When("I request all IATA codes")]
        public async Task WhenIRequestAllIATACodes()
        {
            _listResult = await _controller.GetIATACodes();
        }

        [Then("the response should be successful and contain the IATA codes")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheIATACodes()
        {
            Assert.IsNotNull(_listResult);
            Assert.IsInstanceOf<OkObjectResult>(_listResult.Result);

            var okResult = _listResult.Result as OkObjectResult;
            var iataCodes = okResult?.Value as IEnumerable<IATACodesResponse>;

            Assert.IsNotNull(iataCodes);
            Assert.IsTrue(iataCodes.Any());
        }

        [Given("the IATA code with ID {int} is available in the service")]
        public void GivenTheIATACodeWithIDIsAvailableInTheService(int id)
        {
            var iataCode = new IATACodes
            {
                Id = id,
                IATA = "LAX",
                City = "Los Angeles",
                Country = "USA",
                AirportName = "Los Angeles International Airport"
            };

            _dbSetMock.Setup(m => m.FindAsync(id)).ReturnsAsync(iataCode);
            _dbContextMock.Setup(c => c.IATACodes).Returns(_dbSetMock.Object);
        }

        [When("I request the IATA code by ID {int}")]
        public async Task WhenIRequestTheIATACodeByID(int id)
        {
            var actionResult = await _controller.GetIATACodes(id);
            _singleResult = ConvertToIATACodesResponse(actionResult);
        }

        [Then("the response should be successful and contain the IATA code")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheIATACode()
        {
            Assert.IsNotNull(_singleResult);
            Assert.IsInstanceOf<OkObjectResult>(_singleResult.Result);

            var okResult = _singleResult.Result as OkObjectResult;
            var iataCode = okResult?.Value as IATACodesResponse;

            Assert.IsNotNull(iataCode);
            Assert.AreEqual(1, iataCode.Id);
        }

        [Given("the IATA code with code {string} is available in the service")]
        public void GivenTheIATACodeWithCodeIsAvailableInTheService(string code)
        {
            var iataCode = new IATACodes
            {
                Id = 1,
                IATA = code,
                City = "Los Angeles",
                Country = "USA",
                AirportName = "Los Angeles International Airport"
            };

            _dbSetMock.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Func<IATACodes, bool>>())).ReturnsAsync(iataCode);
            _dbContextMock.Setup(c => c.IATACodes).Returns(_dbSetMock.Object);
        }   
        //nnn
        [When("I request the IATACode by code {string}")]
        public async Task WhenIRequestTheIATACodeByCode(string code)
        {
            var actionResult = await _controller.GetByIATA(code);
            _singleResult = ConvertToIATACodesResponse(actionResult);
        }

        [Then("the response should be successful and contain the IATACode by code")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheIATACodeByCode()
        {
            Assert.IsNotNull(_singleResult);
            Assert.IsInstanceOf<OkObjectResult>(_singleResult.Result);

            var okResult = _singleResult.Result as OkObjectResult;
            var iataCode = okResult?.Value as IATACodesResponse;

            Assert.IsNotNull(iataCode);
            Assert.AreEqual("LAX", iataCode.IATA);
        }

        // Helper method for setting up the memory cache mock
        private static IMemoryCache SetupMemoryCacheMock()
        {
            var memoryCacheMock = new Mock<IMemoryCache>();
            var cacheEntryMock = new Mock<ICacheEntry>();
            memoryCacheMock.Setup(mc => mc.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);
            return memoryCacheMock.Object;
        }

        // Helper method to convert ActionResult<IATACodes> to ActionResult<IATACodesResponse>
        private ActionResult<IATACodesResponse> ConvertToIATACodesResponse(ActionResult<IATACodes> actionResult)
        {
            if (actionResult.Result is OkObjectResult okResult)
            {
                var iataCode = okResult.Value as IATACodes;
                var iataCodeResponse = new IATACodesResponse
                {
                    Id = iataCode.Id,
                    IATA = iataCode.IATA,
                    City = iataCode.City,
                    Country = iataCode.Country,
                    AirportName = iataCode.AirportName,
                    Links = new List<HATEOASLink>()
                };

                return new OkObjectResult(iataCodeResponse);
            }

            return new ActionResult<IATACodesResponse>(actionResult.Result);
        }
    }
}
