
/* 
 * Chart.js usage:
 * Just add in <div id="testChart"></div> in the html file
 * 
 */
window.onload = function () {

    // A test chart to show that the javascript is running
    if ($("#testChart").length == 1) {
        var testChart = new CanvasJS.Chart("testChart", {
            theme: "theme2",
            animationEnabled: true,
            title: {
                text: "Simple Column Chart in ASP.NET MVC"
            },
            subtitles: [
                { text: "Try Resizing the Browser" }
            ],
            data: [
                {
                    type: "column", //change type to bar, line, area, pie, etc
                    dataPoints: [
                        { x: 10, y: 71 },
                        { x: 20, y: 55 },
                        { x: 30, y: 50 },
                        { x: 40, y: 65 },
                        { x: 50, y: 95 },
                        { x: 60, y: 68 },
                        { x: 70, y: 28 },
                        { x: 80, y: 34 },
                        { x: 90, y: 14 }
                    ]
                }
            ]
        });
        testChart.render();
    }

    /* requisitionOrderStatusChart
     * Generates a doughnut chart that compares between status of requisition orders.
     */
    if ($("#requisitionOrderStatusChart").length == 1) {

        // Fetch a list of requisition orders
        $.getJSON("http://localhost:61187/Services/Service.svc/RequisitionOrder/List", {},
            function (data) {

                var approvedCount = 0;
                var pendingCount = 0;
                var rejectedCount = 0;

                data.forEach(function (obj) {
                    if (obj.RequisitionStatus.trim() == "Approved") {
                        approvedCount++;
                    }

                    else if (obj.RequisitionStatus.trim() == "Pending") {
                        pendingCount++;
                    }

                    else if (obj.RequisitionStatus.trim() == "Rejected") {
                        rejectedCount++;
                    }
                });


                var requisitionOrderStatusChart = new CanvasJS.Chart("requisitionOrderStatusChart", {
                    animationEnabled: true,
                    theme: "theme2",
                    title: {
                        text: "Chart of percentage between Approved, Pending, and Rejected requisition orders"
                    },
                    data: [
                        {
                            type: "doughnut", //change type to bar, line, area, pie, etc
                            dataPoints: [
                                { y: approvedCount, indexLabel: "Approved Count" },
                                { y: pendingCount, indexLabel: "Pending Count" },
                                { y: rejectedCount, indexLabel: "Rejected Count" },
                            ]
                        }
                    ]
                });
                requisitionOrderStatusChart.render();
            });
    }

    /* requisitionOrderDateChart
     * Generates a scatter plot that shows the date when requisition orders are made.
     */
    if ($("#chartContainer").length == 1) {
        $.getJSON("http://localhost:61187/Services/Service.svc/RequisitionOrder/List", {},
            function (data) {
                console.log(data[0].RequisitionDate);
                console.log(ConvertToDatetime(data[0].RequisitionDate));

                // Prepare data
                var dataPoints = [];

                var chart = new CanvasJS.Chart("chartContainer",
                    {
                        title: {
                            text: "Simple Date-Time Chart"
                        },
                        theme: "theme2",
                        animationEnabled: true,
                        axisX: {
                            title: "timeline",
                            gridThickness: 2
                        },
                        axisY: {
                            title: "Downloads"
                        },
                        data: [
                            {
                                type: "area",
                                dataPoints: dataPoints
                            }
                        ]
                    });


                $.each(data, function (key, value) {
                    dataPoints.push({ x: ConvertToDatetime(value.RequisitionDate), y: 1 });
                });	

                chart.render();
            });


    }


};




// Utilities
// Converts JSON epoch time to a Date object
function ConvertToDatetime(dateValue) {
    var regex = /-?\d+/;
    var match = regex.exec(dateValue);
    var date = new Date(parseInt(match[0]));
    // Shift timezone to +8
    date.setTime(date.getTime() + (8 * 60 * 60 * 1000));
    return date;
}