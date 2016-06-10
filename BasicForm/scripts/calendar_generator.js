var selday;
var d;
var mon;
var ye;


var Calendar = function (o) {
    //Store div id
    this.divId = o.ParentID;

    // Days of week, starting on Sunday
    this.DaysOfWeek = o.DaysOfWeek;

    console.log("this.DaysOfWeek == ", this.DaysOfWeek)

    // Months, stating on January
    this.Months = o.Months;

    console.log("this.Months == ", this.Months)

    // Set the current month, year
    d = new Date();

    console.log("d == ", d)

    this.CurrentMonth = d.getMonth();

    console.log("this.CurrentMonth == ", this.CurrentMonth);

    this.CurrentYear = d.getFullYear();

    console.log("this.CurrentYear == ", this.CurrentYear);

    var f = o.Format;

    console.log("o == ", o);

    console.log("f == ", f);

    //this.f = typeof(f) == 'string' ? f.charAt(0).toUpperCase() : 'M';

    if (typeof (f) == 'string') {
        this.f = f.charAt(0).toUpperCase();
    } else {
        this.f = 'M';
    }

    console.log("this.f == ", this.f);

    mon = this.CurrentMonth;
    ye = this.CurrentYear;
};

// Goes to next month
Calendar.prototype.nextMonth = function () {
    console.log("Calendar.prototype.nextMonth = function() {");

    if (this.CurrentMonth == 11) {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = 0;

        console.log("this.CurrentMonth == ", this.CurrentMonth);

        console.log("this.CurrentYear == ", this.CurrentYear);

        this.CurrentYear = this.CurrentYear + 1;

        console.log("this.CurrentYear == ", this.CurrentYear);
    } else {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = this.CurrentMonth + 1;

        console.log("this.CurrentMonth + 1 == ", this.CurrentMonth);
    }

    this.showCurrent();
    mon = this.CurrentMonth;
    ye = this.CurrentYear;
};

// Goes to previous month
Calendar.prototype.previousMonth = function () {
    console.log("Calendar.prototype.previousMonth = function() {");

    if (this.CurrentMonth == 0) {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = 11;

        console.log("this.CurrentMonth == ", this.CurrentMonth);

        console.log("this.CurrentYear == ", this.CurrentYear);

        this.CurrentYear = this.CurrentYear - 1;

        console.log("this.CurrentYear == ", this.CurrentYear);
    } else {
        console.log("this.CurrentMonth == ", this.CurrentMonth);

        this.CurrentMonth = this.CurrentMonth - 1;

        console.log("this.CurrentMonth - 1 == ", this.CurrentMonth);
    }

    this.showCurrent();
    mon = this.CurrentMonth;
    ye = this.CurrentYear;
};

// 
Calendar.prototype.previousYear = function () {
    console.log(" ");

    console.log("Calendar.prototype.previousYear = function() {");

    console.log("this.CurrentYear == " + this.CurrentYear);

    this.CurrentYear = this.CurrentYear - 1;

    console.log("this.CurrentYear - 1 i.e. this.CurrentYear == " + this.CurrentYear);
   
    this.showCurrent();
    mon = this.CurrentMonth;
    ye = this.CurrentYear;
}


// 
Calendar.prototype.nextYear = function () {
    console.log(" ");

    console.log("Calendar.prototype.nextYear = function() {");

    console.log("this.CurrentYear == " + this.CurrentYear);

    this.CurrentYear = this.CurrentYear + 1;

    console.log("this.CurrentYear - 1 i.e. this.CurrentYear == " + this.CurrentYear);

    this.showCurrent();
    mon = this.CurrentMonth;
    ye = this.CurrentYear;
}

// Show current month
Calendar.prototype.showCurrent = function () {
    console.log(" ");

    console.log("Calendar.prototype.showCurrent = function() {");

    console.log("this.CurrentYear == ", this.CurrentYear);

    console.log("this.CurrentMonth == ", this.CurrentMonth);

    this.Calendar(this.CurrentYear, this.CurrentMonth);
    mon = this.CurrentMonth;
    ye = this.CurrentYear;
};

// Show month (year, month)
Calendar.prototype.Calendar = function (y, m) {
    console.log(" ");

    console.log("Calendar.prototype.Calendar = function(y,m){");

    typeof (y) == 'number' ? this.CurrentYear = y : null;

    console.log("this.CurrentYear == ", this.CurrentYear);

    typeof (y) == 'number' ? this.CurrentMonth = m : null;

    console.log("this.CurrentMonth == ", this.CurrentMonth);

    // 1st day of the selected month
    var firstDayOfCurrentMonth = new Date(y, m, 1).getDay();

    console.log("firstDayOfCurrentMonth == ", firstDayOfCurrentMonth);

    // Last date of the selected month
    var lastDateOfCurrentMonth = new Date(y, m + 1, 0).getDate();

    console.log("lastDateOfCurrentMonth == ", lastDateOfCurrentMonth);

    // Last day of the previous month
    console.log("m == ", m);

    var lastDateOfLastMonth = m == 0 ? new Date(y - 1, 11, 0).getDate() : new Date(y, m, 0).getDate();

    console.log("lastDateOfLastMonth == ", lastDateOfLastMonth);

    console.log("Print selected month and year.");

    // Write selected month and year. This HTML goes into <div id="year"></div>
    //var yearhtml = '<span class="yearspan">' + y + '</span>';

    // Write selected month and year. This HTML goes into <div id="month"></div>
    //var monthhtml = '<span class="monthspan">' + this.Months[m] + '</span>';

    // Write selected month and year. This HTML goes into <div id="month"></div>
    var monthandyearhtml = '<span id="monthandyearspan">' + this.Months[m] + ' - ' + y + '</span>';

    console.log("monthandyearhtml == " + monthandyearhtml);

    var html = '<table id="day">';

    // Write the header of the days of the week
    html += '<tr>';

    console.log(" ");

    console.log("Write the header of the days of the week");

    for (var i = 0; i < 7; i++) {
        console.log("i == ", i);

        console.log("this.DaysOfWeek[i] == ", this.DaysOfWeek[i]);

        html += '<th class="daysheader">' + this.DaysOfWeek[i] + '</th>';
    }

    html += '</tr>';

    console.log("Before conditional operator this.f == ", this.f);

    //this.f = 'X';

    var p = dm = this.f == 'M' ? 1 : firstDayOfCurrentMonth == 0 ? -5 : 2;

    /*var p, dm;
  
    if(this.f =='M') {
      dm = 1;
  
      p = dm;
    } else {
      if(firstDayOfCurrentMonth == 0) {
        firstDayOfCurrentMonth == -5;
      } else {
        firstDayOfCurrentMonth == 2;
      }
    }*/

    console.log("After conditional operator");

    console.log("this.f == ", this.f);

    console.log("p == ", p);

    console.log("dm == ", dm);

    console.log("firstDayOfCurrentMonth == ", firstDayOfCurrentMonth);

    var cellvalue;

    for (var d, i = 0, z0 = 0; z0 < 6; z0++) {
        html += '<tr>';

        console.log("Inside 1st for loop - d == " + d + " | i == " + i + " | z0 == " + z0);

        for (var z0a = 0; z0a < 7; z0a++) {
            console.log("Inside 2nd for loop");

            console.log("z0a == " + z0a);

            d = i + dm - firstDayOfCurrentMonth;

            console.log("d outside if statm == " + d);

            // Dates from prev month
            if (d < 1) {
                console.log("d < 1");

                console.log("p before p++ == " + p);

                cellvalue = lastDateOfLastMonth - firstDayOfCurrentMonth + p++;

                console.log("p after p++ == " + p);

                console.log("cellvalue == " + cellvalue);

                html += '<td id="prevmonthdates">' +
                      '<span id="cellvaluespan">' + (cellvalue) + '</span><br/>' +
                      '<ul id="cellvaluelist"></ul>' +
                    '</td>';

                // Dates from next month
            } else if (d > lastDateOfCurrentMonth) {
                console.log("d > lastDateOfCurrentMonth");

                console.log("p before p++ == " + p);

                html += '<td id="nextmonthdates">' + (p++) + '</td>';

                console.log("p after p++ == " + p);

                // Current month dates
            } else {
                html += '<td id=' + (d) + '>' + (d) + '</td>';

                console.log("d inside else { == " + d);

                p = 1;

                console.log("p inside } else { == " + p);
            }

            if (i % 7 == 6 && d >= lastDateOfCurrentMonth) {
                console.log("INSIDE if (i % 7 == 6 && d >= lastDateOfCurrentMonth) {");

                console.log("i == " + i);

                console.log("d == " + d);

                console.log("z0 == " + z0);

                z0 = 10; // no more rows
            }

            console.log("i before i++ == " + i);

            i++;

            console.log("i after i++ == " + i);
        }

        html += '</tr>';
    }

    // Closes table
    html += '</table>';

    // Write HTML to the div
    //document.getElementById("year").innerHTML = yearhtml;

    //document.getElementById("month").innerHTML = monthhtml;

    document.getElementById("monthandyear").innerHTML = monthandyearhtml;

    document.getElementById(this.divId).innerHTML = html;
   
};

// On Load of the window
window.onload = function () {

    // Start calendar
    var c = new Calendar({
        ParentID: "divcalendartable",

        DaysOfWeek: [
        'MON',
        'TUE',
        'WED',
        'THU',
        'FRI',
        'SAT',
        'SUN'
        ],

        Months: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],

        Format: 'dd/mm/yyyy'
    });

    c.showCurrent();

    // Bind next and previous button clicks
    getId('btnPrev').onclick = function () {
        c.previousMonth();
    };

    getId('btnPrevYr').onclick = function () {
        c.previousYear();
    };

    getId('btnNext').onclick = function () {
        c.nextMonth();
    };

    getId('btnNextYr').onclick = function () {
        c.nextYear();
    };
}

// Get element by id
function getId(id) {
    return document.getElementById(id);
}



/*
*   Selektivni funkce
*   mesic
*/
var days = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31'];
var day = document.getElementById('divcalendartable');
day.onclick = function (e) {

    for (var i = 0; i < days.length; i++) {
        var selday = e.target.id;
        var seleday = document.getElementById(selday);
        var myday = document.getElementById(days[i]);

        if (seleday == myday) {
            myday.style.background = 'red';
        } else {
            myday.style.background = 'lightblue';
        }
        var mont = (mon + 1);
        if (selday >=1 && selday <=31) {
            document.getElementById("date").placeholder = selday + '.' + mont + '.' + ye;
        } else {
            document.getElementById("date").placeholder = '-';
        }
        
       
    }

    
};