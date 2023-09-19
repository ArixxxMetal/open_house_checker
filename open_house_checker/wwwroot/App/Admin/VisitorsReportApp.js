
var app = angular.module("VisitorsReportApp", []);

app.controller("VisitorsReportController", function ($scope, $http) {

    angular.element(document).ready(function () {

        CheckEmployee();
        GetTotalCheck()
    });

    $scope.CheckFilterModel = "";

    $scope.generalSearchFilter = function (CheckedEmployeeObject) {

        if ($scope.CheckFilterModel === "") {

            return (CheckedEmployeeObject);
        }
        if ($scope.CheckFilterModel === "CHECKED") {

            return (CheckedEmployeeObject.ischecked === $scope.CheckFilterModel);
        }
        if ($scope.CheckFilterModel === "NOTCHECKED") {

            return (CheckedEmployeeObject.ischecked === null);
        }
    }


    $scope.DownloadReport = function () {

        // Validate if data was filtered
        var Query = "SELECT * INTO XLSX('Visitas-OpenHouse.xlsx',{headers:true}) FROM ?";

        // To download the recor to excel
        alasql(Query, [$scope.filtered]);

    }
    // End function

    $scope.CheckEmployeeAndCount = function () {

        $scope.EmployeeNotFound = false;
        $scope.EmployeeAlreadyCheck = false;

        CheckEmployee();
        //GetEmployeesChecked();
        $scope.EmployeeNumber = "";
    }

    async function CheckEmployee() {
        // start httpRequest 
        $http({
            method: "POST",
            url: "/Check/GetVisitorsReport",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            data: {}
        }).then(function (response) {

            $scope.VisitList = response.data;

        }, function errorCallBack(response) {
            console.error("Error to delete");
            console.log(response.data);
        })
        // end httpRequest

    }

    async function GetTotalCheck() {

        // start httpRequest 
        $http({
            method: "POST",
            url: "/Check/GetTotalCount",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            data: {}
        }).then(function (response) {
            console.table(response.data);
            $scope.TotalChecked = response.data[0];

            //$scope.ApprovedPercentage = (($scope.TotalChecked.total_employees_approved / $scope.TotalChecked.total_employees) * 100).toFixed(2);
            //$scope.AssistPercentage = (($scope.TotalChecked.total_employees_checkin / $scope.TotalChecked.total_employees) * 100).toFixed(2);

        }, function errorCallBack(response) {
            console.error("Error to delete");
            console.log(response.data);
        })
        // end httpRequest

    }
});

$(document).ready(function () {

})