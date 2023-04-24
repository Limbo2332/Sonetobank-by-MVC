using CourseWork_Update.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CourseWork_Update.Data;
using CourseWork_Update.Services;
using CourseWork_Update.Extensions;
using CourseWork_Update.Interfaces;

namespace CourseWork_Update.Controllers
{
    public class CreditController : Controller
    {
        private readonly ILogger<CreditController> _logger;
        private ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPutDataService _dataService;

        public CreditController(ILogger<CreditController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, IPutDataService dataService)
        {
            _logger = logger;
            db = context;
            _userManager = userManager;
            _dataService = dataService;
        }

        public async Task PutData()
        {
            var phoneNumber = await _userManager.GetPhoneNumber(User);

            ViewBag.PhoneNumber = phoneNumber;
            ViewBag.Days = _dataService.GetDays();
            ViewBag.Years = _dataService.GetYears();
        }

        [HttpGet]
        [Route("Credits")]
        public async Task<IActionResult> Index()
        {
            await PutData();
            return View();
        }

        [HttpPost]
        [Route("Credits")]
        public async Task<IActionResult> Index(CreditModel creditModel)
        {
            await PutData();

            creditModel.CreditId = Guid.NewGuid().ToString();
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            creditModel.BirthDate = new DateTime(creditModel.BirthYear, ((int)creditModel.BirthMonth), creditModel.BirthDay);

            if (db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id) != null)
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            if (db.Deposits.FirstOrDefault(x => x.PhoneNumber == creditModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований депозит.");
            if (db.Deposits.FirstOrDefault(x => x.PassportNumber == creditModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований депозит.");
            if (db.Credits.FirstOrDefault(x => x.PhoneNumber == creditModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований кредит.");
            if (db.Credits.FirstOrDefault(x => x.PassportNumber == creditModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований кредит.");

            if (db.Credits.FirstOrDefault(x => x.IdentityUserId == user.Id) == null)
            {
                creditModel.IdentityUserId = db.Users.First(x => x.Id == user.Id).Id;

                if (ModelState.IsValid)
                {
                    Response.Cookies.Delete("toscroll");
                    db.Credits.Add(creditModel);
                    await db.SaveChangesAsync();
                    return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
                }
                else
                    Response.Cookies.Append("toscroll", "true", new CookieOptions { Expires = DateTimeOffset.Now.AddSeconds(5) });
            }
            else
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            return View(creditModel);
        }
    }
}
