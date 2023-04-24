using CourseWork_Update.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;
using System.Linq;
using CourseWork_Update.Data;

namespace CourseWork_Update.Areas.Identity.Pages.Account.Manage
{
    public class HasCreditOrDepositModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext db;
        public HasCreditOrDepositModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var credit = db.Credits.FirstOrDefault(x => x.IdentityUserId == user.Id);

            if (credit != null)
            {
                ViewData["Info"] = "credit";
                ViewData["Credit"] = credit;
                return Page();
            }
            var deposit = db.Deposits.FirstOrDefault(x => x.IdentityUserId == user.Id);

            if (deposit != null)
            {
                ViewData["Info"] = "deposit";
                ViewData["Deposit"] = deposit;
                return Page();
            }

            ViewData["Info"] = "null";
            return Page();
        }
    }
}
