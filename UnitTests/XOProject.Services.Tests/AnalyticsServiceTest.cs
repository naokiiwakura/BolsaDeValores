using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;
using XOProject.Services.Tests.Helpers;

namespace XOProject.Services.Tests
{
    public class AnalyticsServiceTests
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();

        private readonly AnalyticsService _analyticsService;

        public AnalyticsServiceTests()
        {
            _analyticsService = new AnalyticsService(_shareRepositoryMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _shareRepositoryMock.Reset();
        }

        [Test]
        public async Task DailySummaryTest()
        {
            // Arrange
            ArrangeRates();

            // Act
            var result = await _analyticsService.GetDailyAsync("CBI", 2018, 08, 17);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(300, result.Open);
            Assert.AreEqual(400, result.High);
            Assert.AreEqual(200, result.Low);
            Assert.AreEqual(210, result.Close);
        }
        [Test]
        public async Task WeeklySummaryTest()
        {
            // Arrange
            ArrangeRates();

            // Act
            var result = await _analyticsService.GetWeeklyAsync("CBI", 2018, 33);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(320, result.Open);
            Assert.AreEqual(500, result.High);
            Assert.AreEqual(200, result.Low);
            Assert.AreEqual(220, result.Close);
        }

        [Test]
        public async Task MonthlySummaryTest()
        {
            // Arrange
            ArrangeRates();

            // Act
            var result = await _analyticsService.GetMonthlyAsync("CBI", 2018, 08);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(320, result.Open);
            Assert.AreEqual(600, result.High);
            Assert.AreEqual(200, result.Low);
            Assert.AreEqual(600, result.Close);
        }

        private void ArrangeRates()
        {
            var rates = new[]
            {
                new HourlyShareRate
                {
                    Id = 1,
                    Symbol = "CBI",
                    Rate = 310.0M,
                    TimeStamp = new DateTime(2017, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 2,
                    Symbol = "CBI",
                    Rate = 320.0M,
                    TimeStamp = new DateTime(2018, 08, 16, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 3,
                    Symbol = "REL",
                    Rate = 300.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 4,
                    Symbol = "CBI",
                    Rate = 300.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 5,
                    Symbol = "CBI",
                    Rate = 400.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 6, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 6,
                    Symbol = "IBM",
                    Rate = 300.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                 new HourlyShareRate
                {
                    Id = 7,
                    Symbol = "CBI",
                    Rate = 200.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 7, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 8,
                    Symbol = "CBI",
                    Rate = 210.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 8, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 9,
                    Symbol = "CBI",
                    Rate = 210.0M,
                    TimeStamp = new DateTime(2018, 08, 18, 1, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 10,
                    Symbol = "CBI",
                    Rate = 210.0M,
                    TimeStamp = new DateTime(2018, 08, 18, 2, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 11,
                    Symbol = "CBI",
                    Rate = 500.0M,
                    TimeStamp = new DateTime(2018, 08, 18, 3, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 12,
                    Symbol = "CBI",
                    Rate = 220.0M,
                    TimeStamp = new DateTime(2018, 08, 18, 4, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 13,
                    Symbol = "CBI",
                    Rate = 600.0M,
                    TimeStamp = new DateTime(2018, 08, 30, 4, 0, 0)
                },
            };

            _shareRepositoryMock
                .Setup(mock => mock.Query())
                .Returns(new AsyncQueryResult<HourlyShareRate>(rates));
        }
    }
}
