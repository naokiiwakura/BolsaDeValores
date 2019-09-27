using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using XOProject.Api.Controller;
using XOProject.Api.Model;
using XOProject.Api.Model.Analytics;
using XOProject.Repository.Domain;
using XOProject.Services.Domain;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests
{
    public class AnalyticsControllerTest
    {
        private readonly Mock<IAnalyticsService> _analyticsServiceMock = new Mock<IAnalyticsService>();

        private readonly AnalyticsController _analyticsController;

        public AnalyticsControllerTest()
        {
            _analyticsController = new AnalyticsController(_analyticsServiceMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _analyticsServiceMock.Reset();
        }

        [Test]
        public async Task DailyTest()
        {
            // Arrange
            _analyticsServiceMock
                .Setup(mock => mock.GetDailyAsync("CBI", 2018, 08, 17))
                .Returns(Task.FromResult(new AnalyticsPrice {
                    Open=3,
                    High = 4,
                    Low = 1,
                    Close = 2,
                }));

            // Act
            var result = await _analyticsController.Daily("CBI", 2018, 08, 17);

            // Assert
            Assert.NotNull(result);
            
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);

            var model = okObjectResult.Value as DailyModel;

            Assert.AreEqual("CBI", model.Symbol);
            Assert.AreEqual(new DateTime(), model.Day);
            Assert.NotNull(model.Price);
        }
        [Test]
        public async Task WeeklyTest()
        {
            // Arrange
            _analyticsServiceMock
                .Setup(mock => mock.GetWeeklyAsync("CBI", 2018, 33))
                .Returns(Task.FromResult(new AnalyticsPrice
                {
                    Open = 3,
                    High = 4,
                    Low = 1,
                    Close = 2,
                }));

            // Act
            var result = await _analyticsController.Weekly("CBI", 2018,33);

            // Assert
            Assert.NotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);

            var model = okObjectResult.Value as WeeklyModel;

            Assert.AreEqual("CBI", model.Symbol);
            Assert.AreEqual(2018, model.Year);
            Assert.AreEqual(33, model.Week);
            Assert.NotNull(model.Price);
        }

        [Test]
        public async Task MonthlyTest()
        {

            // Arrange
            _analyticsServiceMock
                .Setup(mock => mock.GetMonthlyAsync("CBI", 2018, 8))
                .Returns(Task.FromResult(new AnalyticsPrice
                {
                    Open = 3,
                    High = 4,
                    Low = 1,
                    Close = 2,
                }));

            // Act
            var result = await _analyticsController.Monthly("CBI", 2018, 8);

            // Assert
            Assert.NotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);

            var model = okObjectResult.Value as MonthlyModel;

            Assert.AreEqual("CBI", model.Symbol);
            Assert.AreEqual(2018, model.Year);
            Assert.AreEqual(8, model.Month);
            Assert.NotNull(model.Price);
        }

        private void ArrangeRates()
        {
            _analyticsServiceMock
                .Setup(mock => mock.GetDailyAsync("CBI", 2018, 08, 17))
                .Returns(Task.FromResult<AnalyticsPrice>(null));
        }
    }
}
