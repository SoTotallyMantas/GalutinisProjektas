using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace ReqNrollTests.StepDefinitions
{
    [TestFixture]
    [Binding]
    public class IATACodesControllerStepDefinitions
    {
        private DbContextOptions<ModeldbContext> _dbContextOptions;
        private ModeldbContext _dbContext;
        private IATACodesService _service;
        private Mock<IMemoryCache> _memoryCacheMock;
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

            _service = new IATACodesService(_dbContext);
            _controller = new IATACodesController(_service, _memoryCacheMock.Object);
            _listResult = null;
            _singleResult = null;

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
        }

        [Given("the IATA codes service is available")]
        public void GivenTheIATACodesServiceIsAvailable()
        {
            SetupInMemoryDatabase();
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
            Assert.IsInstanceOf<ObjectResult>(_listResult.Result);

            var okResult = _listResult.Result as ObjectResult;
            var iataCodes = okResult?.Value as IEnumerable<IATACodesResponse>;

            Assert.IsNotNull(iataCodes);
            Assert.IsTrue(iataCodes.Any());
        }

        [Given("the IATA code with ID {int} is available in the service")]
        public void GivenTheIATACodeWithIDIsAvailableInTheService(int id)
        {
            // No need to set up as it is already seeded in the database
        }

        [When("I request the IATA code by ID {int}")]
        public async Task WhenIRequestTheIATACodeByID(int id)
        {
            _singleResult = await ConvertToIATACodesResponse(_controller.GetIATACodes(id));
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
            // No need to set up as it is already seeded in the database
        }

        [When("I request the IATACode by code {string}")]
        public async Task WhenIRequestTheIATACodeByCode(string code)
        {
            _singleResult = await ConvertToIATACodesResponse(_controller.GetByIATA(code));
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

        // Consolidated helper method to convert ActionResult to ActionResult<IATACodesResponse>
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
                    Links = new List<HATEOASLink>()
                };

                return new OkObjectResult(iataCodeResponse);
            }

            return actionResult.Result != null ? new ActionResult<IATACodesResponse>(actionResult.Result) : null;
        }
    }
}