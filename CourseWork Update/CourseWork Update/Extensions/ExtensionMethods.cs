using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CourseWork_Update.Extensions
{
    static public class ExtensionMethods
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            if(htmlHelper == null || controller == null || action == null)
            {
                return "";
            }
            var routeData = htmlHelper.ViewContext.RouteData;

            if(routeData == null)
            {
                return "";
            }

            var routeAction = routeData.Values["action"]?.ToString();
            var routeController = routeData.Values["controller"]?.ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return returnActive ? "active" : "";
        }

        public static async Task<string> GetPhoneNumber(this UserManager<IdentityUser> userManager, ClaimsPrincipal User)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null && user?.PhoneNumber == null)
            {
                return "";
            }
            else
            {
                return user.PhoneNumber;
            }
        }
    }
}
