
var app = angular.module("EmployeesReportApp", []);

app.controller("EmployeesReportController", function ($scope, $http) {

    angular.element(document).ready(function () {

        CheckEmployee();
        GetTotalCheckEmployee();
    });

    //$scope.filteredTodos = [];
    //$scope.itemsPerPage = 30;
    //$scope.currentPage = 4;

    //$scope.figureOutTodosToDisplay = function () {
    //    var begin = (($scope.currentPage - 1) * $scope.itemsPerPage);
    //    var end = begin + $scope.itemsPerPage;
    //    $scope.filteredTodos = $scope.filtered.slice(begin, end);
    //};

    //$scope.figureOutTodosToDisplay();

    //$scope.pageChanged = function () {
    //    $scope.figureOutTodosToDisplay();
    //};

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
        var Query = "SELECT id_rh, nombre_empleado, numero_puesto, nombre_puesto, numero_departamento, nombre_departamento, area, bum, checada_entrada, fecha_checada_entrada, checada_aprobada, fecha_checada_aprobada INTO XLSX('Sindicato-Reporte.xlsx',{headers:true}) FROM ?";

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
            url: "/Home/GetCheckSindicateReport",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            data: {}
        }).then(function (response) {

            $scope.EmployeeList = response.data;

        }, function errorCallBack(response) {
            console.error("Error to delete");
            console.log(response.data);
        })
        // end httpRequest

    }

    async function GetTotalCheckEmployee() {

        // start httpRequest 
        $http({
            method: "POST",
            url: "/Home/GetCheckCount",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            data: {}
        }).then(function (response) {
            console.table(response.data);
            $scope.TotalChecked = response.data[0];

            $scope.ApprovedPercentage = (($scope.TotalChecked.total_employees_approved / $scope.TotalChecked.total_employees) * 100).toFixed(2);
            $scope.AssistPercentage = (($scope.TotalChecked.total_employees_checkin / $scope.TotalChecked.total_employees) * 100).toFixed(2);

        }, function errorCallBack(response) {
            console.error("Error to delete");
            console.log(response.data);
        })
        // end httpRequest

    }
});

$(document).ready(function () {

})