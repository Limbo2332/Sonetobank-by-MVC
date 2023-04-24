using System.ComponentModel.DataAnnotations;

namespace CourseWork_Update.Models.Deposits
{
    public class PhotoInfosModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string PhotoNumberPath { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
