using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Api.Controller;
using XOProject.Api.Model;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests
{
    public class PortfolioControllerTest
    {
        private readonly Mock<IPortfolioService> _portfolioServiceMock = new Mock<IPortfolioService>();

        private readonly PortfolioController _portifolioController;

        public PortfolioControllerTest()
        {
            _portifolioController = new PortfolioController(_portfolioServiceMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _portfolioServiceMock.Reset();
        }
        
        [Test]
        public async Task Post()
        {
            // Arrange
            var portifolio = new PortfolioModel
            {
               Name = "Elton Naoki Iwakura"
            };

            // Act
            var result = await _portifolioController.Post(portifolio);

            // Assert
            Assert.NotNull(result);

            // TODO: This unit test is broken, the result received from the Post method is correct.
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
    }
}
