using Microsoft.AspNetCore.Mvc;
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
    public class FuelTypesControllerStepDefinitions
    {
        private DbContextOptions<ModeldbContext> _dbContextOptions;
        private ModeldbContext _dbContext;
        private FuelTypesService _service;
        private Mock<IMemoryCache> _memoryCacheMock;
        private Mock<IUrlHelper> _urlHelperMock;
        private FuelTypesController _controller;
        private ActionResult<IEnumerable<FuelTypes>> _listResult;
        private ActionResult<FuelTypesResponse> _singleResult;

        private void SetupInMemoryDatabase()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ModeldbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ModeldbContext(_dbContextOptions);
            _memoryCacheMock = new Mock<IMemoryCache>();
            _urlHelperMock = new Mock<IUrlHelper>();

            _service = new FuelTypesService(_dbContext);
            _controller = new FuelTypesController(_service, _memoryCacheMock.Object)
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
            _dbContext.FuelTypes.AddRange(
                new FuelTypes { Id = 1, FuelName = "Diesel", FuelType = "diesel", FuelUnit = "liter" },
                new FuelTypes { Id = 2, FuelName = "Petrol", FuelType = "petrol", FuelUnit = "liter" }
            );
            _dbContext.SaveChanges();
        }

        [Given("the fuel types service is available")]
        public void GivenTheFuelTypesServiceIsAvailable()
        {
            if (_dbContext == null)
            {
                SetupInMemoryDatabase();
            }
        }

        [When("I request all fuel types")]
        public async Task WhenIRequestAllFuelTypes()
        {
            _listResult = await _controller.GetFuelTypes();
        }

        [Then("the response should be successful and contain the fuel types")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFuelTypes()
        {
            Assert.IsNotNull(_listResult, "_listResult is null");
            Assert.IsInstanceOf<ObjectResult>(_listResult.Result, "_listResult.Result is not an ObjectResult");

            var okResult = _listResult.Result as ObjectResult;
            Assert.IsNotNull(okResult, "okResult is null");

            var fuelTypes = okResult.Value as IEnumerable<FuelTypesResponse>;
            Assert.IsNotNull(fuelTypes, "fuelTypes is null");
            Assert.IsTrue(fuelTypes.Any(), "fuelTypes is empty");
        }

        [Given("the fuel type with ID {int} is available in the service")]
        public void GivenTheFuelTypeWithIDIsAvailableInTheService(int id)
        {
            if (_dbContext == null)
            {
                SetupInMemoryDatabase();
            }
        }

        [When("I request the fuel type by ID {int}")]
        public async Task WhenIRequestTheFuelTypeByID(int id)
        {
            _singleResult = await ConvertToFuelTypesResponse(_controller.GetFuelTypes(id));
        }

        [Then("the response should be successful and contain the fuel type")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFuelType()
        {
            Assert.IsNotNull(_singleResult, "_singleResult is null");
            Assert.IsInstanceOf<ObjectResult>(_singleResult.Result, "_singleResult.Result is not an OkObjectResult");

            var okResult = _singleResult.Result as ObjectResult;
            Assert.IsNotNull(okResult, "okResult is null");

            var fuelType = okResult.Value as FuelTypesResponse;
            Assert.IsNotNull(fuelType, "fuelType is null");
            Assert.AreEqual(1, fuelType.Id, "fuelType.Id is not 1");
        }

        [Given("the fuel type with name {string} is available in the service")]
        public void GivenTheFuelTypeWithNameIsAvailableInTheService(string fuelTypeName)
        {
            if (_dbContext == null)
            {
                SetupInMemoryDatabase();
            }

            var existingFuelType = _dbContext.FuelTypes.FirstOrDefault(ft => ft.FuelName == fuelTypeName);
            if (existingFuelType == null)
            {
                var fuelType = new FuelTypes
                {
                    Id = _dbContext.FuelTypes.Max(ft => ft.Id) + 1, // Ensure unique ID
                    FuelName = fuelTypeName,
                    FuelType = fuelTypeName.ToLower(),
                    FuelUnit = "liter"
                };

                _dbContext.FuelTypes.Add(fuelType);
                _dbContext.SaveChanges();
            }
        }

        [When("I request the fuel type by name {string}")]
        public async Task WhenIRequestTheFuelTypeByName(string name)
        {
            _singleResult = await ConvertToFuelTypesResponse(_controller.GetByFuelType(name));
        }

        [Then("the response should be successful and contain the fuel type by name")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFuelTypeByName()
        {
            Assert.IsNotNull(_singleResult, "_singleResult is null");
            Assert.IsInstanceOf<ObjectResult>(_singleResult.Result, "_singleResult.Result is not an OkObjectResult");

            var okResult = _singleResult.Result as ObjectResult;
            Assert.IsNotNull(okResult, "okResult is null");

            var fuelType = okResult.Value as FuelTypesResponse;
            Assert.IsNotNull(fuelType, "fuelType is null");
            Assert.AreEqual("diesel", fuelType.FuelType, "fuelType.FuelType is not diesel");
        }

        private async Task<ActionResult<FuelTypesResponse>> ConvertToFuelTypesResponse(Task<ActionResult<FuelTypes>> task)
        {
            var actionResult = await task;
            if (actionResult.Result is OkObjectResult okResult && okResult.Value is FuelTypes fuelType)
            {
                var fuelTypeResponse = new FuelTypesResponse
                {
                    Id = fuelType.Id,
                    FuelName = fuelType.FuelName,
                    FuelType = fuelType.FuelType,
                    FuelUnit = fuelType.FuelUnit,
                    Links = null
                };

                return new OkObjectResult(fuelTypeResponse);
            }

            return actionResult.Result != null ? new ActionResult<FuelTypesResponse>(actionResult.Result) : null;
        }
    }
}
