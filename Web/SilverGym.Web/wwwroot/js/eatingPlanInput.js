var mealPlansIndex = 0;
var prevsDays = [];
var mealIndex = 0;
function AddMoreMeals(dayIndex) {
    if (prevsDays[dayIndex] == undefined) {
        prevsDays[dayIndex] = 0;
        mealIndex = 0;
    } else {
        mealIndex = prevsDays[dayIndex];
    }
    $("#MealsContainer" + dayIndex + "")
        .append("<div class='form-control'>Име: <input type='text' required name='MealPlans[" + dayIndex + "]" +
            ".Meals[" + mealIndex + "].Name' /> </div>" +
            "<div class='form-control'> Количество: <input required type='text' name='MealPlans[" + dayIndex + "]" +
            ".Meals[" + mealIndex + "].GramsOrMil' />"
            + " </div>");
    prevsDays[dayIndex]++;
}

function AddMoreMealPlans() {
    $("#MealPlansContainer")
        .append("<div class='form-control'>Име: <input type='text' required name='MealPlans[" + mealPlansIndex + "]" +
            ".Name' /> </div>" +
            "<div class='form-control'>Време: <input type='text' required name='MealPlans[" + mealPlansIndex + "]" +
            ".Time' /> </div>" +
            "<div class='form-group clearfix' id='MealsContainer" + mealPlansIndex + "'>" +
            "<label name='MealPlans' class='suls-text-color'>Храни</label >" +
            "<a class='btn btn-success' onclick='AddMoreMeals(" + mealPlansIndex + ")'>+</a>" +
            "</div>");
    mealPlansIndex++;
}