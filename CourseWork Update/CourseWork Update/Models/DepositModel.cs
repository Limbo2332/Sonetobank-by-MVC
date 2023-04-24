using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseWork_Update.Models
{
    public enum DepositName
    {
        Перспективний = 0,
        Накопичувальний,
        Ощадний,
        Дитячий,
        Мобільні_заощадження,
        Строковий
    }

    public class DepositModel
    {
        [Key]
        [ValidateNever]
        public string DepositId { get; set; }

        [Required]
        public DepositName DepositName { get; set; }
        [ForeignKey("IdentityUser")]
        [ValidateNever]
        public string IdentityUserId { get; set; }
        [Required]
        public double DepositSum { get; set; }
        [Required]
        public int Term { get; set; }
        [Required]
        public double PercentBeforeTax { get; set; }
        [Required]
        public double PercentAfterTax { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [RegularExpression(@"\D*", ErrorMessage = "Ім'я має бути написано лише кирилицею.")]
        [MinLength(2, ErrorMessage = "Мінімальна довжина імені - 2 символи.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [RegularExpression(@"\D*", ErrorMessage = "Прізвище має бути написано лише кирилицею.")]
        [MinLength(3, ErrorMessage = "Мінімальна довжина прізвища - 3 символи.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [Phone]
        [RegularExpression(@"^\+380[0-9]{9}", ErrorMessage = "Некоректний номер телефону.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Некоректний номер паспорту.")]
        public string PassportNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public int BirthDay { get; set; }
        [NotMapped]
        public BirthMonth BirthMonth { get; set; }
        [NotMapped]
        public int BirthYear { get; set; }

    }
}
