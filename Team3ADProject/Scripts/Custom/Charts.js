/* 
 * Chart.js usage:
 * Just add in <div id="testChart" customAttribute="value"></div> in the html file
 * Create <div id="chartMessage" to output chart messages
 * 
 * 
 * 
 * testChart: A simple chart to test whether canvas.js is running
 * requisitionOrderStatusChart: A doughnut chart that shows portion of RO Status
 * requisitionOrderDateChart: A column chart that shows dates of requisition orders made
 * purchaseQuantityByItemQuantityBarChart: A Column chart that shows stationaries purchased by category
 *  - monthsParam: Sets the number of months to look back up to present
 *  pendingPurchaseOrderCountBySupplierChart: A Column chart showing pending purchase orders by each supplier
 */
$(document).ready(function () {

    // var hostname = window.location.host;
    var hostname = window.location.host + "/LogicUniversity";

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
		$.getJSON("http://" + hostname + "/Services/Service.svc/RequisitionOrder/List/" + department, {},
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
                        text: "Chart of percentage between Approved, Pending, and Rejected requisition orders in department " + department
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
    if ($("#requisitionOrderDateChart").length == 1) {
        $.getJSON("http://" + hostname + "/Services/Service.svc/RequisitionOrder/List", {},
            function (data) {
                console.log(data[0].RequisitionDate);
                console.log(ConvertToDatetime(data[0].RequisitionDate));

                // Prepare data
                var dataPoints = [];

                var chart = new CanvasJS.Chart("requisitionOrderDateChart",
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
                            title: "Ordered"
                        },
                        data: [
                            {
                                type: "scatter",
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

    /* purchaseQuantityByItemCategory BarChart
     * Generates a bar chart stationary purchased grouped by categories.
     */
    if ($("#purchaseQuantityByItemCategoryBarChart").length == 1) {

        // Prepare data
        var dataPoints = [];

        var startDate = document.getElementById("startDate").value;
        var endDate = document.getElementById("endDate").value;

        if (startDate == undefined || endDate == undefined) {
            startDate = "01-01-1965";
            endDate = "12-31-3000";
        }

        // Fetch current month data
        $.getJSON("http://" + hostname + "/Services/Service.svc/Chart/PurchaseQuantityByItemCategory/" + startDate + "/" + endDate, {},
            function (data) {

                // Place data on the chart
                $.each(data, function (key, value) {
                    dataPoints.push({ y: value.quantity, label: value.category });
                });
                
                // Render the chart
                var chart = new CanvasJS.Chart("purchaseQuantityByItemCategoryBarChart",
                    {
                        title: {
                            text: "Stationary categories purchased from " + startDate + " to " + endDate
                        },
                        theme: "theme2",
                        animationEnabled: true,
                        axisX: {
                            title: "Item Category",
                            gridThickness: 2
                        },
                        axisY: {
                            title: "Quantity"
                        },
                        data: [
                            {
                                type: "column",
                                dataPoints: dataPoints
                            }
                        ]
                    });
                chart.render();
            });


    }

    // A chart that displays items requested by each department based on a given time.
    if ($("#requisitionQuantityByDepartmentChart").length == 1) {



        // Prepare data
        var dataPoints = [];

        var startDate = document.getElementById("startDate").value;
        var endDate = document.getElementById("endDate").value;

        if (startDate == undefined || endDate == undefined) {
            startDate = "01-01-1965";
            endDate = "12-31-3000";
        }

        alert("http://" + hostname + "/Services/Service.svc/Chart/getRequisitionQuantityByDepartmentWithinTime/" + startDate + "/" + endDate);

        $.getJSON("http://" + hostname + "/Services/Service.svc/Chart/getRequisitionQuantityByDepartmentWithinTime/" + startDate + "/" + endDate, {},
            function (data) {
                // Place data on the chart
                $.each(data, function (key, value) {
                    dataPoints.push({ y: value.ItemRequestQuantity, label: value.DepartmentId });
                });
                
                // Render the chart
                var chart = new CanvasJS.Chart("requisitionQuantityByDepartmentChart",
                    {
                        title: {
                            text: "Total stationeries requested by each department from " + startDate + " to " + endDate
                        },
                        theme: "theme2",
                        animationEnabled: true,
                        axisX: {
                            title: "Department",
                            gridThickness: 2
                        },
                        axisY: {
                            title: "Quantity"
                        },
                        data: [
                            {
                                type: "column",
                                dataPoints: dataPoints
                            }
                        ]
                    });
                chart.render();
            });
    }

    // A test chart to show that the javascript is running
    if ($("#pendingPurchaseOrderCountBySupplierChart").length == 1) {

        // Prepare data
        var dataPoints = [];

        $.getJSON("http://" + hostname + "/Services/Service.svc/Chart/getPendingPurchaseOrderCountBySupplier", {},
            function (data) {
                // Place data on the chart
                $.each(data, function (key, value) {
                    dataPoints.push({ y: value.Count, label: value.SupplierId });
                });

                // Render the chart
                var chart = new CanvasJS.Chart("pendingPurchaseOrderCountBySupplierChart",
                    {
                        title: {
                            text: "Number of Pending purchase orders by suppliers"
                        },
                        theme: "theme2",
                        animationEnabled: true,
                        axisX: {
                            title: "Department",
                            gridThickness: 2
                        },
                        axisY: {
                            title: "Quantity",
                        },
                        data: [
                            {
                                type: "column",
                                dataPoints: dataPoints
                            }
                        ]
                    });
                chart.render();

            });
    }



});









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