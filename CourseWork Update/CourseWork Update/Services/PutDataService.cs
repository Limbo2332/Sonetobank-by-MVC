using CourseWork_Update.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork_Update.Services
{
    public class PutDataService : IPutDataService
    {

        public SelectList GetDays()
        {
            List<int> Days = new List<int>();

            for (int i = 1; i <= 31; i++)
                Days.Add(i);

            return new SelectList(Days);
        }

        public SelectList GetYears()
        {
            List<int> Years = new List<int>();

            for (int i = DateTime.Now.Year - 65; i <= DateTime.Now.Year - 18; i++)
                Years.Add(i);

            return new SelectList(Years);
        }
    }
}
