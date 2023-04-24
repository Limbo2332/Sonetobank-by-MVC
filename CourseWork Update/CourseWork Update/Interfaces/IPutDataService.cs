using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWork_Update.Interfaces
{
    public interface IPutDataService
    {
        public SelectList GetDays();
        public SelectList GetYears();
    }
}
