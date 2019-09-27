using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XOProject.Repository.Domain;

namespace XOProject.Repository.Tests
{
    public class SharesModelDataTest
    {
        [Test]
        public void HourlyShareRateValidateData()
        {
            var HourlyShareRateNotValid = new HourlyShareRate
            {
                TimeStamp = new DateTime(),
                Symbol = "REL",
                Rate = -1
            };

            var context1 = new ValidationContext(HourlyShareRateNotValid, null, null);
            var results1 = new List<ValidationResult>();
            var isModelStateNotValid = Validator.TryValidateObject(HourlyShareRateNotValid, context1, results1, true);

            var HourlyShareRateValid = new HourlyShareRate
            {
                TimeStamp = new DateTime(),
                Symbol = "REL",
                Rate = 10
            };

            var context = new ValidationContext(HourlyShareRateValid, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(HourlyShareRateValid, context, results, true);


            // Assert 
            Assert.False(isModelStateNotValid);
            Assert.True(isModelStateValid);
        }
    }
}
