using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace XOProject.Api.Model
{
    public class TradeModel
    {
        [Required]
        public string Symbol { get; set; }

        [Required]
        public int NoOfShares { get; set; }

        [Required]
        public int PortfolioId { get; set; }

        [StringRange(AllowableValues = new[] { "BUY", "SELL" }, ErrorMessage = "only 'Buy' ou 'SELL' actions accepted")]
        public string Action { get; set; }
    }

    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            var msg = $"Please enter one of the allowable values: {string.Join(", ", (AllowableValues ?? new string[] { "No allowable values found" }))}.";
            return new ValidationResult(msg);
        }
    }
}