using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseWork_Update.Data;

public enum EmploymentType
{
    [Display(Name = "Безробітний")]
    Unemployed,
    [Display(Name = "Військовослужбовець")]
    Serviceman,
    [Display(Name = "Власне господарство")]
    Own_household,
    [Display(Name = "Найманий працівник")]
    Employee,
    [Display(Name = "Особа з інвалідністю")]
    Disabled,
    [Display(Name = "Пенсіонер")]
    Pensioner,
    [Display(Name = "Працюючкий пенсіонер")]
    Working_Pensioner,
    [Display(Name = "Працюю неофіційно")]
    Informally_Worker,
    [Display(Name = "Студент")]
    Student,
    [Display(Name = "Приватний підприємець")]
    Private_Entrepreneur
}

public enum BirthMonth
{
    Січень = 1,
    Лютий,
    Березень,
    Квітень,
    Травень,
    Червень,
    Липень,
    Серпень,
    Вересень,
    Жовтень,
    Листопад,
    Грудень
}


namespace CourseWork_Update.Models
{
    public class CreditModel
    {
        [Key]
        [ValidateNever]
        public string CreditId { get; set; }
        [ForeignKey("IdentityUser")]
        [ValidateNever]
        public string IdentityUserId { get; set; }
        [Required]
        public double CreditSum { get; set; }
        [Required]
        public int Term { get; set; }

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
        [Remote(action: "CheckDate", controller: "Home", AdditionalFields = "BirthDay, BirthMonth, BirthYear", ErrorMessage = "Такої дати не існує")]
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public int BirthDay { get; set; }
        [NotMapped]
        public BirthMonth BirthMonth { get; set; }
        [NotMapped]
        public int BirthYear { get; set; }
        [Required]
        public EmploymentType EmploymentType { get; set; }

    }

}
