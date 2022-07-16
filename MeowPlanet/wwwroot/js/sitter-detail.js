
//* 點擊日曆
$(document).ready(function () {
    const calendarControl = new CalendarControl()
});
$(".reserve-btn")[0].addEventListener("click", function (e) {
    $(".calender-rec").show();
}, false);
$(".calender-rec").click(function (e) {
    e.stopPropagation();
})

//* 日曆

let startend = [];
function CalendarControl() {
    const calendar = new Date();
    let localDate = new Date();
    localDate.setHours(0, 0, 0, 0);
    const calendarControl = {
        prevMonthLastDate: null,
        calWeekDays: ["日", "一", "二", "三", "四", "五", "六"],
        calMonthName: [
            "1月",
            "2月",
            "3月",
            "4月",
            "5月",
            "6月",
            "7月",
            "8月",
            "9月",
            "10月",
            "11月",
            "12月"

        ],
        //format Date
        formatDate: function (date) {
            let format_date = `${date.getFullYear()}年${date.getMonth() + 1}月${date.getDate()}日`;
            return format_date;
        },
        formatShortDate: function (date) {
            let format_date = `${date.getFullYear()}/${date.getMonth() + 1}/${date.getDate()}`;
            return format_date;
        },
        // 這個月的日數(number)
        daysInMonth: function (month, year) {
            return new Date(year, month, 0).getDate();
        },
        // 本月第一天的日期物件(date)
        firstDay: function () {
            return new Date(calendar.getFullYear(), calendar.getMonth(), 1);
        },
        lastDay: function () {
            return new Date(calendar.getFullYear(), calendar.getMonth() + 1, 0);
        },
        // 這個月第一天的星期數(number)
        firstDayNumber: function () {
            return calendarControl.firstDay().getDay() + 1;
        },
        lastDayNumber: function () {
            return calendarControl.lastDay().getDay() + 1;
        },

        getPreviousMonthLastDate: function () {
            let lastDate = new Date(
                calendar.getFullYear(),
                calendar.getMonth(),
                0
            ).getDate();
            return lastDate;
        },
        navigateToPreviousMonth: function (e) {
            calendar.setMonth(calendar.getMonth() - 1);
            calendarControl.attachEventsOnNextPrev();
            calendarControl.checkRenderOrNot();
            e.stopPropagation();

        },
        navigateToNextMonth: function (e) {
            calendar.setMonth(calendar.getMonth() + 1);
            calendarControl.attachEventsOnNextPrev();
            calendarControl.checkRenderOrNot();
            e.stopPropagation();
        },
        checkRenderOrNot: function () {
            let thisYear = calendar.getFullYear();
            let thisMonth = calendar.getMonth();
            let dateSelect = document.querySelectorAll(".number-item"); // div tag
            let dateNumber = document.querySelectorAll(".calendar .dateNumber");  //a tag

            if (startend.length === 0) {

            } else if (startend.length === 1) {
                if (startend[0].getFullYear() === thisYear && startend[0].getMonth() === thisMonth) {
                    dateNumber[startend[0].getDate() - 1].classList.add("calendar-today");
                }
            } else if (startend.length === 2) {
                if (startend[0].getTime() < calendarControl.firstDay().getTime()) {

                    if (startend[1].getTime() > calendarControl.firstDay().getTime() && startend[1].getTime() < calendarControl.lastDay().getTime()) {
                        console.log("S | E |")
                        //  S | E |
                        for (let i = 0; i < startend[1].getDate(); i++) {
                            if (i == startend[1].getDate() - 1) {
                                dateSelect[i].classList.add("calendar-during-end");
                                dateNumber[i].classList.add("calendar-today");
                            } else {
                                dateSelect[i].classList.add("calendar-during");
                            }
                        }

                    } else if (startend[1].getTime() > calendarControl.lastDay().getTime()) {
                        console.log("S | | E")
                        // S | | E
                        dateSelect.forEach(element => {
                            element.classList.add("calendar-during");
                        });
                    } else if (startend[1].getTime() < calendarControl.firstDay().getTime()) {
                        console.log(" S E | |")
                        // S E | |
                    }

                } else if (startend[0].getTime() > calendarControl.firstDay().getTime() && startend[0].getTime() < calendarControl.lastDay().getTime()) {
                    if (startend[1].getTime() < calendarControl.lastDay().getTime()) {
                        console.log(" |S E|")
                        //  |S E|
                        let night = (startend[1] - startend[0]) / (1000 * 3600 * 24);
                        for (let i = 0; i < night + 1; i++) {
                            if (i == 0) {
                                dateSelect[startend[0].getDate() - 1 + i].classList.add("calendar-during-start");
                                dateNumber[startend[0].getDate() - 1 + i].classList.add("calendar-today");
                            } else if (i == night) {
                                dateSelect[startend[0].getDate() - 1 + i].classList.add("calendar-during-end");
                                dateNumber[startend[0].getDate() - 1 + i].classList.add("calendar-today");
                            } else {
                                dateSelect[startend[0].getDate() - 1 + i].classList.add("calendar-during");
                            }
                        }
                    } else if (startend[1].getTime() > calendarControl.lastDay().getTime()) {
                        console.log("| S | E")
                        // | S | E
                        let night = calendarControl.lastDay().getDate() - startend[0].getDate()
                        for (let i = 0; i < night; i++) {
                            if (i == 0) {
                                dateSelect[startend[0].getDate() - 1 + i].classList.add("calendar-during-start");
                                dateNumber[startend[0].getDate() - 1 + i].classList.add("calendar-today");
                            } else {
                                dateSelect[startend[0].getDate() - 1 + i].classList.add("calendar-during");
                            }
                        }
                    }
                } else if (startend[0].getTime() > calendarControl.lastDay().getTime() &&
                    startend[1].getTime() > calendarControl.lastDay().getTime()) {
                    console.log("| | S E")
                    //  | | S E 
                }
            }
        },
        displayYear: function () {
            let yearLabel = document.querySelector(".calendar .calendar-year-label");
            yearLabel.innerHTML = calendar.getFullYear();
        },
        displayMonth: function () {
            let monthLabel = document.querySelector(
                ".calendar .calendar-month-label"
            );
            monthLabel.innerHTML = calendarControl.calMonthName[calendar.getMonth()];
        },

        // 月份選擇
        plotSelectors: function () {
            document.querySelector(".calendar").innerHTML +=
                `<div class="calendar-inner">
                            <div class="calendar-controls">
                                <div class="calendar-prev">
                                    <a >
                                        <svg xmlns="http://www.w3.org/2000/svg" width="128" height="128" viewBox="0 0 128 128">
                                            <path fill="#666" d="M88.2 3.8L35.8 56.23 28 64l7.8 7.78 52.4 52.4 9.78-7.76L45.58 64l52.4-52.4z" />
                                        </svg>
                                    </a>
                                </div>
                                <div class="calendar-year-month">
                                    <div class="calendar-year-label"></div>
                                    <div>-</div>
                                    <div class="calendar-month-label"></div>
                                </div>
                                <div class="calendar-next">
                                    <a >
                                        <svg xmlns="http://www.w3.org/2000/svg" width="128" height="128" viewBox="0 0 128 128">
                                            <path fill="#666"  d="M38.8 124.2l52.4-52.42L99 64l-7.77-7.78-52.4-52.4-9.8 7.77L81.44 64 29 116.42z" />
                                        </svg>
                                    </a>
                                </div>
                            </div>
                            <div class="calendar-body"></div>
                        </div>`;
        },
        // 輸出星期名字
        plotDayNames: function () {
            for (let i = 0; i < calendarControl.calWeekDays.length; i++) {
                document.querySelector(
                    ".calendar .calendar-body"
                ).innerHTML += `<div>${calendarControl.calWeekDays[i]}</div>`;
            }
        },
        //輸出日期
        plotDates: function () {
            document.querySelector(".calendar .calendar-body").innerHTML = "";
            calendarControl.plotDayNames();
            calendarControl.displayMonth();
            calendarControl.displayYear();
            let count = 1;
            let prevDateCount = 0;

            calendarControl.prevMonthLastDate = calendarControl.getPreviousMonthLastDate();
            let prevMonthDatesArray = [];
            let calendarDays = calendarControl.daysInMonth(
                calendar.getMonth() + 1,
                calendar.getFullYear()
            );
            // dates of current month
            for (let i = 1; i < calendarDays; i++) {
                if (i < calendarControl.firstDayNumber()) {
                    prevDateCount += 1;
                    document.querySelector(
                        ".calendar .calendar-body"
                    ).innerHTML += `<div class="prev-dates"></div>`;
                } else {
                    document.querySelector(
                        ".calendar .calendar-body"
                    ).innerHTML +=
                        `<div class="number-item">
                                    <a class="dateNumber" data-dateid="${calendar.getFullYear()}/${calendar.getMonth() + 1}/${count}">${count++}</a>
                                </div>`;
                }
            }
            //remaining dates after month dates
            for (let j = 0; j < prevDateCount + 1; j++) {
                document.querySelector(
                    ".calendar .calendar-body"
                ).innerHTML +=
                    `<div class="number-item" >
                                <a class="dateNumber" data-dateid="${calendar.getFullYear()}/${calendar.getMonth() + 1}/${count}">${count++}</a>
                            </div>`;
            }
        },
        attachEvents: function () {
            let prevBtn = document.querySelector(".calendar .calendar-prev a");
            let nextBtn = document.querySelector(".calendar .calendar-next a");
            let todayDate = document.querySelector(".calendar .calendar-today-date");
            let dateNumber = document.querySelectorAll(".calendar .dateNumber");
            let clearSelect = document.querySelector(".clear-select");
            let closeCalendar = document.querySelector(".close-calendar");

            prevBtn.addEventListener("click", calendarControl.navigateToPreviousMonth);
            nextBtn.addEventListener("click", calendarControl.navigateToNextMonth);
            for (var i = 0; i < dateNumber.length; i++) {
                dateNumber[i].addEventListener(
                    "click",
                    calendarControl.selectDate,
                    false
                );
            }
            clearSelect.addEventListener("click", calendarControl.clearSelect);
            closeCalendar.addEventListener("click", calendarControl.closeCalendar);
        },
        closeCalendar: function (e) {
            if (startend[0] !== undefined) {
                $("#check-in").html(calendarControl.formatShortDate(startend[0]));
            } else {
                $("#check-in").html("輸入日期");
            }
            if (startend[1] !== undefined) {
                $("#check-out").html(calendarControl.formatShortDate(startend[1]));
            } else {
                $("#check-out").html("輸入日期");

            }

            $(".calender-rec").hide();
        },
        clearSelect: function (e) {
            let dateSelect = document.querySelectorAll(".number-item"); // div tag
            let dateNumber = document.querySelectorAll(".calendar .dateNumber");  //a tag
            dateSelect.forEach(element => { element.classList.remove("calendar-during") });
            dateSelect.forEach(element => { element.classList.remove("calendar-during-end") });
            dateSelect.forEach(element => { element.classList.remove("calendar-during-start") });
            dateNumber.forEach(element => { element.classList.remove("calendar-today") });
            document.querySelector(".night").innerHTML = "選擇日期";
            document.querySelector(".calender-header span:nth-child(1)").innerHTML = "";
            document.querySelector(".calender-header span:nth-child(2)").innerHTML = "";
            startend = [];
            e.stopPropagation();

        },

        selectDate: function (e) {
            let selectDate = new Date(e.target.dataset.dateid);
            // 本日後才能點
            if (localDate.getTime() <= selectDate.getTime()) {
                if (startend.length === 0) {
                    startend.push(selectDate);
                    e.target.classList.add("calendar-today");
                    document.querySelector(".calender-header span:nth-child(1)").innerHTML = calendarControl.formatDate(selectDate);
                    document.querySelector(".night").innerHTML = "選擇結束日期";
                    //! disable prev date
                } else if (startend.length === 1) {
                    if (startend[0] < selectDate) {
                        startend.push(selectDate);
                        e.target.classList.add("calendar-today");
                        document.querySelector(".calender-header span:nth-child(2)").innerHTML = calendarControl.formatDate(selectDate);
                        console.log(startend[1], startend[0])
                        let night = (startend[1] - startend[0]) / (1000 * 3600 * 24);
                        document.querySelector(".night").innerHTML = `${night}晚`;
                        if (startend[0].getTime() > calendarControl.firstDay().getTime()) {
                            for (let i = 0; i < night + 1; i++) {
                                let dateSelect = document.querySelectorAll(".number-item")[startend[0].getDate() - 1 + i];
                                if (i == 0) {
                                    dateSelect.classList.add("calendar-during-start");
                                } else if (i == night) {
                                    dateSelect.classList.add("calendar-during-end");
                                } else {
                                    dateSelect.classList.add("calendar-during");
                                }
                            }
                        } else {
                            for (let i = 0; i < startend[1].getDate(); i++) {
                                let dateSelect = document.querySelectorAll(".number-item")[0 + i];
                                if (i == startend[1].getDate() - 1) {
                                    dateSelect.classList.add("calendar-during-end");
                                } else {
                                    dateSelect.classList.add("calendar-during");
                                }
                            }
                        }

                    }
                } else if (startend.length === 2) {
                    // document.querySelector(".calender-header span:nth-child(1)").innerHTML = "";
                    // document.querySelector(".calender-header span:nth-child(2)").innerHTML = "";
                    // document.querySelector(".night").innerHTML = "選擇開始日期";
                    // startend = [];
                    // calendarControl.selectDate(e);
                }
            }
            e.stopPropagation();
        },

        attachEventsOnNextPrev: function () {
            calendarControl.plotDates();
            calendarControl.attachEvents();
        },
        init: function () {
            calendarControl.plotSelectors();
            calendarControl.plotDates();
            calendarControl.attachEvents();
        }
    };
    calendarControl.init();
}

//* 彈出評論



//* 地圖
function doFirst() {
    let area = document.querySelector('.map');
    const option = {
        zoom: 14,
        center: new google.maps.LatLng(22.664285, 120.343922),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    let map = new google.maps.Map(area, option);

    let marker = new google.maps.Marker({
        position: new google.maps.LatLng(22.664285, 120.343922),
        map: map,
        icon: '',
        title: '這是哪裡?',
    });

}
window.addEventListener('load', doFirst)