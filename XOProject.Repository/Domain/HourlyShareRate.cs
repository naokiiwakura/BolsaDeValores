using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XOProject.Repository.Domain
{
    public class HourlyShareRate
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Symbol { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [GreaterThanZero]
        public decimal Rate { get; set; }
    }

    public class GreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var x = (decimal)value;
            return x > 0;
        }
    }
}
