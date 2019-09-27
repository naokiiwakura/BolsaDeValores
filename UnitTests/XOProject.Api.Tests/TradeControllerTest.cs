using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Api.Controller;
using XOProject.Api.Model;
using XOProject.Repository.Domain;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests
{
    public class TradeControllerTest
    {
        private readonly Mock<ITradeService> _tradeServiceMock = new Mock<ITradeService>();

        private readonly TradeController _tradeController;

        public TradeControllerTest()
        {
            _tradeController = new TradeController(_tradeServiceMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _tradeServiceMock.Reset();
        }

        [Test]
        public async Task Post()
        {
            // Arrange
            var trade = new TradeModel
            {
                Symbol = "NAS",
                NoOfShares = 10,
                PortfolioId = 1,
                Action = "BUY"
            };

            _tradeServiceMock
                .Setup(mock => mock.BuyOrSell(1,"NAS",10,"BUY"))
                .Returns(Task.FromResult(new Trade{
                }));

            // Act
            var result = await _tradeController.Post(trade);

            // Assert
            Assert.NotNull(result);

            // TODO: This unit test is broken, the result received from the Post method is correct.
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }

    }
}
