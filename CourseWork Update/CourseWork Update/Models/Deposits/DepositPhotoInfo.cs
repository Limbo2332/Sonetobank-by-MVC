using System.ComponentModel.DataAnnotations;

namespace CourseWork_Update.Models.Deposits
{
    
    public class DepositPhotoInfo
    {
        [Required]
        public int DepositInfoId { get; set; }

        [Required]
        public int PhotoInfoId { get; set; }
    }
}
