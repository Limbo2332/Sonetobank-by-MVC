using CourseWork_Update.Data;
using CourseWork_Update.Extensions;
using CourseWork_Update.Interfaces;
using CourseWork_Update.Models;
using CourseWork_Update.Models.Deposits;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseWork_Update.Controllers
{
    public class DepositController : Controller
    {
        private readonly ILogger<DepositController> _logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPutDataService _dataService;

        public DepositController(ILogger<DepositController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, IPutDataService dataService)
        {
            _logger = logger;
            db = context;
            _userManager = userManager;
            _dataService = dataService;
        }

        public async Task PutData(string title)
        {
            var phoneNumber = await _userManager.GetPhoneNumber(User);

            ViewBag.PhoneNumber = phoneNumber;
            ViewBag.Days = _dataService.GetDays();
            ViewBag.Years = _dataService.GetYears();
            ViewData["DepositSum"] = 15000;

            var model = await db.DepositsInfoModels.FirstAsync(x => x.Title == title);

            IEnumerable<int> photoNumbersIds = db.DepositPhotoInfo
                .Where(p => p.DepositInfoId == model.DepositInfoId)
                .Select(p => p.PhotoInfoId);

            IEnumerable <PhotoInfosModel> photoNumbersValues = db.PhotoInfos.ToList();

            IList<string> photoNumbers = new List<string>();
            IList<string> photoDescriptions = new List<string>();

            foreach (var photoNumber in photoNumbersValues)
            {
                foreach (var photoNumberId in photoNumbersIds)
                {
                    if (photoNumber.Id == photoNumberId)
                    {
                        photoNumbers.Add(photoNumber.PhotoNumberPath);
                        photoDescriptions.Add(photoNumber.Description);
                        break;
                    }
                }
            }

            ViewData["Title"] = model.Title;
            ViewData["Description"] = model.Description;
            ViewBag.photoNumbers = photoNumbers;
            ViewBag.photoDescriptions = photoDescriptions;
            ViewBag.model = model;
        }

        public void UpdateDepositModel(ref DepositModel depositModel, DepositsInfoModel model)
        {
            depositModel.DepositId = Guid.NewGuid().ToString();
            var arr = Enum.GetValues(typeof(DepositName));

            // Отримуємо рядкові значення enum DepositName та конвертуємо їх в int

            for (int i = 0; i < arr.Length; i++)
            {
                if(arr.GetValue(i)!.ToString() == model.Title)
                {
                    depositModel.DepositName = (DepositName)i;
                    break;
                }
            }
            depositModel.Term = model.Term;
            depositModel.PercentBeforeTax = model.PercentBeforeTax;
            depositModel.PercentAfterTax = model.PercentAfterTax;
            depositModel.DepositSum = double.Parse(Request.Cookies["depositSum"]!);
        }

        [HttpGet]
        [Route("Deposits")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Perspective()
        {
            await PutData("Перспективний");           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Perspective(DepositModel depositModel)
        {
            await PutData("Перспективний");

            var user = await _userManager.GetUserAsync(User);
            var model = await db.DepositsInfoModels.FirstAsync(x => x.Title == "Перспективний");

            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            try
            {
                depositModel.BirthDate = new DateTime(depositModel.BirthYear, ((int)depositModel.BirthMonth), depositModel.BirthDay);
            }
            catch (Exception)
            {
                ModelState.AddModelError("BirthDate", "Такої дати не існує");
            }

            if (await db.Credits.FirstOrDefaultAsync(x => x.IdentityUserId == user.Id) != null)
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            if (await db.Deposits.FirstOrDefaultAsync(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований депозит.");
            if (await db.Deposits.FirstOrDefaultAsync(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований депозит.");
            if (await db.Credits.FirstOrDefaultAsync(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований кредит.");
            if (await db.Credits.FirstOrDefaultAsync(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований кредит.");

            if (db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id) == null)
            {
                UpdateDepositModel(ref depositModel, model);

                depositModel.IdentityUserId = db.Users.First(x => x.Id == user.Id).Id.ToString();

                if (user.PhoneNumber != null)
                {
                    depositModel.PhoneNumber = user.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }

                if (ModelState.IsValid)
                {
                    Response.Cookies.Delete("toscroll");
                    db.Deposits.Add(depositModel);
                    await db.SaveChangesAsync();
                    return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
                }
                else
                    Response.Cookies.Append("toscroll", "true", new CookieOptions { Expires = DateTimeOffset.Now.AddSeconds(5) });
            }
            else
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });

            return View(depositModel);
        }

        [HttpGet]
        public async Task<IActionResult> Strokoviy()
        {
            await PutData("Строковий");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Strokoviy(DepositModel depositModel)
        {
            await PutData("Строковий");

            var user = await _userManager.GetUserAsync(User);
            var model = await db.DepositsInfoModels.FirstAsync(x => x.Title == "Строковий");

            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            try
            {
                depositModel.BirthDate = new DateTime(depositModel.BirthYear, ((int)depositModel.BirthMonth), depositModel.BirthDay);
            }
            catch (Exception)
            {
                ModelState.AddModelError("BirthDate", "Такої дати не існує");
            }

            if (db.Credits.FirstOrDefault(x => x.IdentityUserId == user.Id) != null)
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            if (db.Deposits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований депозит.");
            if (db.Deposits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований депозит.");
            if (db.Credits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований кредит.");
            if (db.Credits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований кредит.");

            if (db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id) == null)
            {
                UpdateDepositModel(ref depositModel, model);

                if (user.PhoneNumber != null)
                {
                    depositModel.PhoneNumber = user.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }

                depositModel.IdentityUserId = db.Users.First(x => x.Id == user.Id).Id.ToString();

                if (ModelState.IsValid)
                {
                    Response.Cookies.Delete("toscroll");
                    db.Deposits.Add(depositModel);
                    await db.SaveChangesAsync();
                    return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
                }
                else
                    Response.Cookies.Append("toscroll", "true", new CookieOptions { Expires = DateTimeOffset.Now.AddSeconds(5) });
            }
            else
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            return View(depositModel);
        }

        [HttpGet]
        public async Task<IActionResult> Oshchadniy()
        {
            await PutData("Ощадний");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Oshchadniy(DepositModel depositModel)
        {
            await PutData("Ощадний");

            var user = await _userManager.GetUserAsync(User);
            var model = await db.DepositsInfoModels.FirstAsync(x => x.Title == "Ощадний");

            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            try
            {
                depositModel.BirthDate = new DateTime(depositModel.BirthYear, ((int)depositModel.BirthMonth), depositModel.BirthDay);
            }
            catch (Exception)
            {
                ModelState.AddModelError("BirthDate", "Такої дати не існує");
            }

            if (db.Credits.FirstOrDefault(x => x.IdentityUserId == user.Id) != null)
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            if (db.Deposits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований депозит.");
            if (db.Deposits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований депозит.");
            if (db.Credits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований кредит.");
            if (db.Credits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований кредит.");

            if (db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id) == null)
            {
                UpdateDepositModel(ref depositModel, model);

                if(user.PhoneNumber != null)
                {
                    depositModel.PhoneNumber = user.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }

                depositModel.IdentityUserId = db.Users.First(x => x.Id == user.Id).Id.ToString();

                if (ModelState.IsValid)
                {
                    Response.Cookies.Delete("toscroll");
                    db.Deposits.Add(depositModel);
                    await db.SaveChangesAsync();
                    return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
                }
                else
                    Response.Cookies.Append("toscroll", "true", new CookieOptions { Expires = DateTimeOffset.Now.AddSeconds(5) });
            }
            else
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            return View(depositModel);
        }
        [HttpGet]
        public async Task<IActionResult> Nakopychuvalnyi()
        {
            await PutData("Накопичувальний");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Nakopychuvalnyi(DepositModel depositModel)
        {
            await PutData("Накопичувальний");
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var model = await db.DepositsInfoModels.FirstAsync(x => x.Title == "Накопичувальний");
            try
            {
                depositModel.BirthDate = new DateTime(depositModel.BirthYear, ((int)depositModel.BirthMonth), depositModel.BirthDay);
            }
            catch (Exception)
            {
                ModelState.AddModelError("BirthDate", "Такої дати не існує");
            }

            if (db.Credits.FirstOrDefault(x => x.IdentityUserId == user.Id) != null)
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            if (db.Deposits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований депозит.");
            if (db.Deposits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований депозит.");
            if (db.Credits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований кредит.");
            if (db.Credits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований кредит.");

            if (db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id) == null)
            {
                UpdateDepositModel(ref depositModel, model);

                depositModel.IdentityUserId = db.Users.First(x => x.Id == user.Id).Id.ToString();

                if (user.PhoneNumber != null)
                {
                    depositModel.PhoneNumber = user.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }

                if (ModelState.IsValid)
                {
                    Response.Cookies.Delete("toscroll");
                    db.Deposits.Add(depositModel);
                    await db.SaveChangesAsync();
                    return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
                }
                else
                    Response.Cookies.Append("toscroll", "true", new CookieOptions { Expires = DateTimeOffset.Now.AddSeconds(5) });
            }
            else
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            return View(depositModel);
        }

        [HttpGet]
        public async Task<IActionResult> Mobile()
        {
            await PutData("Мобільні заощадження");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Mobile(DepositModel depositModel)
        {
            await PutData("Мобільні заощадження");

            var user = await _userManager.GetUserAsync(User);
            var model = await db.DepositsInfoModels.FirstAsync(x => x.Title == "Мобільні заощадження");

            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            try
            {
                depositModel.BirthDate = new DateTime(depositModel.BirthYear, ((int)depositModel.BirthMonth), depositModel.BirthDay);
            }
            catch (Exception)
            {
                ModelState.AddModelError("BirthDate", "Такої дати не існує");
            }

            if (db.Credits.FirstOrDefault(x => x.IdentityUserId == user.Id) != null)
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            if (db.Deposits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований депозит.");
            if (db.Deposits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований депозит.");
            if (db.Credits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований кредит.");
            if (db.Credits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований кредит.");
            if (db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id) == null)
            {
                UpdateDepositModel(ref depositModel, model);

                depositModel.IdentityUserId = db.Users.First(x => x.Id == user.Id).Id.ToString();

                if (user.PhoneNumber != null)
                {
                    depositModel.PhoneNumber = user.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }

                if (ModelState.IsValid)
                {
                    Response.Cookies.Delete("toscroll");
                    db.Deposits.Add(depositModel);
                    await db.SaveChangesAsync();
                    return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
                }
                else
                    Response.Cookies.Append("toscroll", "true", new CookieOptions { Expires = DateTimeOffset.Now.AddSeconds(5) });
            }
            else
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            return View(depositModel);
        }

        [HttpGet]
        public async Task<IActionResult> Kids()
        {
            await PutData("Дитячий");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Kids(DepositModel depositModel)
        {
            await PutData("Дитячий");

            var user = await _userManager.GetUserAsync(User);
            var model = await db.DepositsInfoModels.FirstAsync(x => x.Title == "Дитячий");

            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            try
            {
                depositModel.BirthDate = new DateTime(depositModel.BirthYear, ((int)depositModel.BirthMonth), depositModel.BirthDay);
            }
            catch (Exception)
            {
                ModelState.AddModelError("BirthDate", "Такої дати не існує");
            }

            if (db.Credits.FirstOrDefault(x => x.IdentityUserId == user.Id) != null)
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
            if (db.Deposits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований депозит.");
            if (db.Deposits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований депозит.");
            if (db.Credits.FirstOrDefault(x => x.PhoneNumber == depositModel.PhoneNumber) != null)
                ModelState.AddModelError("PhoneNumber", "На даний номер телефону вже зареєстрований кредит.");
            if (db.Credits.FirstOrDefault(x => x.PassportNumber == depositModel.PassportNumber) != null)
                ModelState.AddModelError("PassportNumber", "На даний номер паспорту вже зареєстрований кредит.");
            if (db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id) == null)
            {
                UpdateDepositModel(ref depositModel, model);

                depositModel.IdentityUserId = db.Users.First(x => x.Id == user.Id).Id.ToString();

                if (user.PhoneNumber != null)
                {
                    depositModel.PhoneNumber = user.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }

                if (ModelState.IsValid)
                {
                    Response.Cookies.Delete("toscroll");
                    db.Deposits.Add(depositModel);
                    await db.SaveChangesAsync();
                    return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });
                }
                else
                    Response.Cookies.Append("toscroll", "true", new CookieOptions { Expires = DateTimeOffset.Now.AddSeconds(5) });
            }
            else
                return RedirectToPage("/Account/Manage/HasCreditOrDeposit", new { area = "Identity" });

            return View(depositModel);
        }
    }
}
