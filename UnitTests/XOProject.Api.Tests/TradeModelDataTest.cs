using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XOProject.Api.Model;

namespace XOProject.Api.Tests
{
    public class TradeModelDataTest
    {
        [Test]
        public void HourlyShareRateValidateData()
        {
            var TradeModelNotValid = new TradeModel
            {
                Symbol = "REL",
                NoOfShares = 10,
                PortfolioId = 1,
                Action="OUT"
            };

            var context1 = new ValidationContext(TradeModelNotValid, null, null);
            var results1 = new List<ValidationResult>();
            var isModelStateNotValid = Validator.TryValidateObject(TradeModelNotValid, context1, results1, true);

            var TradeModelValid = new TradeModel
            {
                Symbol = "REL",
                NoOfShares = 10,
                PortfolioId = 1,
                Action = "BUY"
            };

            var context = new ValidationContext(TradeModelValid, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(TradeModelValid, context, results, true);


            // Assert 
            Assert.False(isModelStateNotValid);
            Assert.True(isModelStateValid);
        }
    }
}
