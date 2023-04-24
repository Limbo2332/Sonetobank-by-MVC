let form = $("#form"),
    day = $("#day"),
    month = $("#month"),
    year = $("#year"),
    spanValidation = $("#BirthDate");

form.on("submit", function () {
    let dayValue = day.val(),
        monthValue = month.val(),
        yearValue = year.val();

    if (dayValue == "День" || monthValue == "Місяць" || yearValue == "Рік" || !isValidDate(Number(dayValue), Number(monthValue), Number(yearValue))) {
        spanValidation.text("Такої дати не існує");
        console.log(1);
        return false;
    } else {
        console.log(2);
        return true;
    }
})
