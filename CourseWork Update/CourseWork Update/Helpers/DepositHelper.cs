using CourseWork_Update.Models.Deposits;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace CourseWork_Update.Helpers
{
    public static class DepositHelper
    {
        public static HtmlString CreateLine(this IHtmlHelper html, string photoNumber, string photoDescription)
        {
            var result = "<div class='line'>";
            result += $"<img src='/images/DepositsPage/{photoNumber}.png' alt='{photoNumber}' />";
            result += $"<p>{photoDescription}</p>";
            result += "</div>" ;

            return new HtmlString(result);
        }

        public static HtmlString CreateContent(this IHtmlHelper html, string titleName, string contentText)
        {
            var result = "<div class=\"content\">";
            result += $"<h3> \"{titleName.ToUpper()}\" </h3>";
            result += $"<p>{contentText}</p>";
            result += "</div>";

            return new HtmlString(result);
        }

        public static HtmlString CreateMainSection(this IHtmlHelper html, string titleName, string contentText, IEnumerable<string> photoNumbers, IEnumerable<string> photoDescriptions, DepositsInfoModel model)
        {
            var result = "<main class= \"main\">";
            result += CreateContent(html, titleName, contentText).ToString();
            result += "<div class=\"terms\">";
            result += "<h4 class=\"text-center\">Умови продукту:</h4>";
            result += "<div class=\"for-grid\">";

            result += CreateLine(html, photoNumbers.ElementAt(0), photoDescriptions.ElementAt(0) + model.Term + " місяців").ToString();
            result += CreateLine(html, photoNumbers.ElementAt(4), photoDescriptions.ElementAt(4)).ToString();
            result += CreateLine(html, photoNumbers.ElementAt(1), photoDescriptions.ElementAt(1) + model.MinSumOfDeposit + " грн").ToString();
            result += CreateLine(html, photoNumbers.ElementAt(5), photoDescriptions.ElementAt(5)).ToString();
            result += CreateLine(html, photoNumbers.ElementAt(2), photoDescriptions.ElementAt(2) + model.PercentBeforeTax +"%").ToString();
            result += CreateLine(html, photoNumbers.ElementAt(6), photoDescriptions.ElementAt(6)).ToString();
            result += CreateLine(html, photoNumbers.ElementAt(3), photoDescriptions.ElementAt(3) + model.MaxSumOfDeposit + " грн").ToString();
            result += CreateLine(html, photoNumbers.ElementAt(7), photoDescriptions.ElementAt(7)).ToString();

            result += "</div>";
            result += "<button class=\"btn btn-scroll\">ОФОРМИТИ</button>";
            result += "</div>";
            result += "</main>";

            return new HtmlString(result);
        }

        public static HtmlString CreateMyRow(this IHtmlHelper html, string additionalRowClass, string additionalValueClass, string myRowNameText, string id, string idValue)
        {
            var result = $"<div class=\"my-row {additionalRowClass}\">";
            result += "<div class= \"my-row-name\" >";
            result += $"<p>{myRowNameText}</p>";
            result += "</div>";
            result += $"<div class= \"my-row-value {additionalValueClass}\" >";
            result += $"<p id='{id}'>{idValue}</p>";
            result += "</div>";
            result += "</div>";

            return new HtmlString(result);
        }

        public static HtmlString CreateIncomeSection(this IHtmlHelper html, double depositSum, DepositsInfoModel model)
        {
            double sumBeforeTax = Math.Round(depositSum * model.PercentBeforeTax * model.Term * 30.5 / 36500, 2);
            double tax = Math.Round(sumBeforeTax * 0.18, 2);
            double military = Math.Round(sumBeforeTax * 0.015, 2);
            double sumAfterTax = Math.Round(sumBeforeTax - tax - military, 2);

            var result = "<div class=\"offset-lg-2 col-lg-8\">";
            result += "<div class=\"income\">";
            result += "<h4 class=\"text-center\">Орієнтовний розрахунок доходів:</h4>";
            result += "<div class=\"range\">";
            result += "<div class=\"sum-of-credit\">";
            result += "<p>Бажана сума депозиту, грн</p>";
            result += "<div class=\"sum\" width =\"190\">";
            result += "<input type =\"text\" name =\"sum-value\" class=\"sum-value\" id =\"sum-value\" value = \"15000\" />";
            result += "<p class= \"currency\" >ГРН</p>";
            result += "</div>";
            result += "</div>";
            result += "<div class=\"scroll\">";
            result += $"<input type=\"range\" name=\"scroll\" id=\"big-scroll\" min =\"{model.MinSumOfDeposit}\" max = \"{model.MaxSumOfDeposit}\" value = \"15000\" step =\"1\" />";
            result += "</div>";
            result += "<div class= \"min-and-max\">";
            result += $"<p>{model.MinSumOfDeposit}</p>";
            result += $"<p>{model.MaxSumOfDeposit}</p>";
            result += "</div>";
            result += "</div>";
            result += "<div class=\"table\">";

            result += CreateMyRow(html, "", "", "Строк вкладу(міс.)", "term", model.Term.ToString());
            result += CreateMyRow(html, "", "", "Відсоткова ставка(до сплати податків)", "percent", model.PercentBeforeTax.ToString() + "%");
            result += CreateMyRow(html, "", "fw-bold", "Сума доходу до оподаткування", "before-tax", sumBeforeTax.ToString() +" грн");
            result += CreateMyRow(html, "", "", "Податок на доходи фізичних осіб (18%)", "tax", tax.ToString() + " грн");
            result += CreateMyRow(html, "", "", "Військовий збір(1.5 %)", "military", military.ToString() + " грн");
            result += CreateMyRow(html, "", "", "Сума доходу після оподаткування", "after-tax", sumAfterTax.ToString() + " грн");
            result += CreateMyRow(html, "border-0", "", "Відсоткова ставка після сплати податків", "", model.PercentAfterTax.ToString() + "%");

            result += "<button class= \"btn btn-scroll\" > ОФОРМИТИ </ button >";
            result += "</div>";
            result += "</div>";
            result += "</div>";

            return new HtmlString(result);
        }
    }
}

//        < div class= "table" >
//            < div class= "my-row" >
//                < div class= "my-row-name" >
//                    < p > Строк вкладу(міс.) </ p >
//                </ div >
//                < div class= "my-row-value" >
//                    < p id = "term" > 36 </ p >
//                </ div >
//            </ div >
//            < div class= "my-row" >
//                < div class= "my-row-name" >
//                    < p > Відсоткова ставка(до сплати податків) </ p >
//                </ div >
//                < div class= "my-row-value" >
//                    < p id = "percent" > 5 %</ p >
//                </ div >
//            </ div >
//            < div class= "my-row" >
//                < div class= "my-row-name" >
//                    < p > Сума доходу до оподаткування</p>
//                </div>
//                <div class= "my-row-value fw-bold" >
//                    < p id = "before-tax" > 2256.16 грн </ p >
//                </ div >
//            </ div >
//            < div class= "my-row" >
//                < div class= "my-row-name" >
//                    < p > Податок на доходи фізичних осіб (18%)</ p >
//                </ div >
//                < div class= "my-row-value" >
//                    < p id = "tax" > 406.11 грн </ p >
//                </ div >
//            </ div >
//            < div class= "my-row" >
//                < div class= "my-row-name" >
//                    < p > Військовий збір(1.5 %) </ p >
//                </ div >
//                < div class= "my-row-value" >
//                    < p id = "military" > 33.84 грн </ p >
//                </ div >
//            </ div >
//            < div class= "my-row" >
//                < div class= "my-row-name" >
//                    < p > Сума доходу після оподаткування</p>
//                </div>
//                <div class= "my-row-value" >
//                    < p id = "after-tax" > 1816.21 грн </ p >
//                </ div >
//            </ div >
//            < div class= "my-row border-0" >
//                < div class= "my-row-name" >
//                    < p > Відсоткова ставка після сплати податків</p>
//                </div>
//                <div class= "my-row-value" >
//                    < p > 4.03 %</ p >
//                </ div >
//            </ div >
//            < button class= "btn btn-scroll" > ОФОРМИТИ </ button >
//        </ div >
//    </ div >
//</ div >