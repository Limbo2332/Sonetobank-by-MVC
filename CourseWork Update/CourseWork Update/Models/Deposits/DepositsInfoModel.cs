using System.ComponentModel.DataAnnotations;

namespace CourseWork_Update.Models.Deposits
{
    public class DepositsInfoModel
    {
        [Key]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double MinSumOfDeposit { get; set; }

        [Required]
        public double MaxSumOfDeposit { get; set; }

        [Required]
        public int Term { get; set; }

        [Required]
        public double PercentBeforeTax { get; set; }

        [Required]
        public double PercentAfterTax { get; set; }

        [Required]
        public int DepositInfoId { get; set; }
    }
}
