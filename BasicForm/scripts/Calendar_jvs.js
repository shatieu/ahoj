

/*
*   Selektivni funkce
*   mesic

var days = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31'];
var day = document.getElementById('c_days_new');



day.onclick = function(e) {

    for (var i = 0; i < days.length; i++) {
        var selday = e.target.id;
       
        var seleday = document.getElementById(selday);
        var myday = document.getElementById(days[i]);

        if (seleday == myday) {
            myday.style.background = 'red';
        } else {
            myday.style.background = 'lightblue';
        }
    }
};
*/