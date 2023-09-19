
var app = angular.module("CheckInApp", []);

app.controller("CheckInController", function ($scope, $http) {


    angular.element(document).ready(function () {

        SetFocusToCheckInput();
        GetTotalCount();
    });

    $scope.EmployeeNumber = "";
    $scope.EmployeeNotFound = false;
    $scope.EmployeeAlreadyCheck = false;

    // Init Variables
    $scope.VisitAge = 0;

    $scope.VisitType = "";

    $scope.VisitSex = "";

    $scope.LessThanOne = "";

    $scope.total_visits = 0;

    var VisitObject = [];

    $scope.ShowEmployeeCheck = true;
    $scope.ShowVisitCheck = false;
    $scope.ShowVisitAge = false;

    $scope.CurrentVisitCount = VisitObject.length;

    $scope.CheckEmployeeAndCount = async function () {

        $scope.EmployeeNotFound = false;
        $scope.EmployeeAlreadyCheck = false;
        $scope.EmployeeCheckSucess = false;

        CheckEmployee();

        $scope.EmployeeNumber = "";

    }

    $scope.GetEmpName = function (keyEvent) {
        if (keyEvent.keyCode == 13) {   // '13' is the key code for enter
            CheckEmployee();           
        }
    }

    $scope.ReturnEmployeeCheck = function () {
        $scope.ShowEmployeeCheck = true;
        $scope.ShowVisitCheck = false;
        $scope.EmployeeCheckSucess = false;
    }

    $scope.ShowVisitCard = function () {
        $scope.ShowEmployeeCheck = false;
        $scope.ShowVisitCheck = true;
        $scope.EmployeeCheckSucess = false;
    }

    $scope.CheckEmployee = function () {

        if ($scope.EmployeeName.length > 0) {
            Swal.fire({
                position: 'top',
                icon: 'success',
                title: 'Registro Exitoso',
                showConfirmButton: false,
                timer: 1000
            })

            SetFocusToCheckInput();
        }
        else {
            CheckEmployee();
        }

        $scope.ShowEmployeeCheck = true;
        $scope.ShowVisitCheck = false;
        $scope.EmployeeNumber = "";
        $scope.EmployeeName = "";
    }


    $scope.SetVisitType = function (visit_type) {

        if (visit_type == 1) {
            $scope.VisitType = "ADULTO";
        }
        if (visit_type == 2) {
            $scope.VisitType = "INFANTE";
        }
    }

    $scope.RestoreVisitType = function () {
        $scope.VisitType = "";
    }

    $scope.SetVisitSex = function (visit_sex) {
        
        if (visit_sex == 1) {
            $scope.VisitSex = "MASCULINO";
        }
        if (visit_sex == 2) {
            $scope.VisitSex = "FEMENINO";
        }

        $scope.ShowVisitAge = false;
        $scope.ShowVisitCheck = true;
    }

    $scope.RestoretVisitSex = function () {
        $scope.VisitSex = "";
    }

    $scope.SumAge = function (age_sum) {
        
        if (age_sum == -1) {
            $scope.RestoreAge();
            $scope.LessThanOne = "Menor a 1";
        }
        else {
            if ($scope.VisitAge == 0) {
                $scope.VisitAge = age_sum
                $scope.LessThanOne = "";
            }
            else {
                $scope.VisitAge = $scope.VisitAge + "" + age_sum;
            }
        }

    }

    $scope.RestoreAge = function () {
        $scope.VisitAge = 0;
        $scope.LessThanOne = "";
    }

    $scope.AddVisitObject = function () {
        let visit = {};
        visit.visit_emp_num = $scope.EmployeeNumber;
        visit.visit_type = $scope.VisitType;
        visit.visit_sex = $scope.VisitSex;
        visit.visit_age = 0;
       
        VisitObject.push(visit);
        $scope.CurrentVisitCount = VisitObject.length;

        $scope.RestoreVisitType();
        $scope.RestoretVisitSex();
        $scope.RestoreAge();
        $scope.ShowVisitAge = false;
        $scope.ShowVisitCheck = true;
        SetFocusToCheckInput();
    }

    $scope.RestoreVisitObject = function () {
        VisitObject = [];
        $scope.CurrentVisitCount = VisitObject.length;
    }

    $scope.SetEmployeeVisits = function () {
        
        if (VisitObject.length > 0) {

            if ($scope.VisitSex.length > 0) {

                $scope.AddVisitObject();
                SaveEmployeeVisits();
                
            }
            else {
                SaveEmployeeVisits();
            }
        }
        else {
            if ($scope.VisitSex.length > 0) {

                $scope.AddVisitObject();
                SaveEmployeeVisits();
                
            }
            else {
                CheckEmployee();
            }

        }

        $scope.ShowEmployeeCheck = true;
        $scope.ShowVisitCheck = false;
        $scope.ShowVisitAge = false;
        $scope.EmployeeNumber = "";
    }

    function CheckEmployee() {
        // Call Fields validation 
        var employee = {
            employeenumber: $scope.EmployeeNumber,
        }
      
        // start httpRequest 
        $http({
            method: "POST",
            url: "/Check/SetEmployeeAsChecked",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            data: employee
        }).then(function (response) {
            
            $scope.validatechecked = response.data[0]; //Get First element of response
            if ($scope.validatechecked.was_updated === true) {
                $scope.EmployeeName = $scope.validatechecked.emp_name_check;
               // $scope.total_visits = $scope.validatechecked.total_count;
            }
            else {
                $scope.EmployeeNumber = "";
                $scope.EmployeeName = "";

                Swal.fire({
                    position: 'top',
                    icon: 'success',
                    title: 'Empleado No Encontrado',
                    showConfirmButton: false,
                    timer: 1000
                })
            }
            GetTotalCount();
            SetFocusToCheckInput();
            RestoreAllValues();

        }, function errorCallBack(response) {
            console.error("Error in server");
            console.log(response.data);
            Swal.fire({
                position: 'top',
                icon: 'success',
                title: response.data,
                showConfirmButton: false,
                timer: 1000
            })
        })
        // end httpRequest

    }

    function SaveEmployeeVisits() {
        // Call Fields validation 

        let visits = VisitObject;
        // start httpRequest 
        $http({
            method: "POST",
            url: "/Check/SaveEmployeeVisit",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            data: visits
        }).then(function (response) {

            $scope.validatechecked = response.data[0]; //Get First element of response
            if ($scope.validatechecked.was_updated === true) {
                //$scope.total_visits = $scope.validatechecked.total_count;
                Swal.fire({
                    position: 'top',
                    icon: 'success',
                    title: 'Registro Exitoso!',
                    showConfirmButton: false,
                    timer: 1000
                })

            }
            else {

                Swal.fire({
                    position: 'top',
                    icon: 'success',
                    title: 'Empleado No Encontrado!',
                    showConfirmButton: false,
                    timer: 1000
                })
            }
            GetTotalCount();
            RestoreAllValues();
            SetFocusToCheckInput();
        }, function errorCallBack(response) {
            console.error("Error in server");
            console.log(response.data);
            Swal.fire({
                position: 'top',
                icon: 'success',
                title: response.data,
                showConfirmButton: false,
                timer: 1000
            })
            SetFocusToCheckInput();
        })
        // end httpRequest
    }

    function GetTotalCount() {

        // start httpRequest 
        $http({
            method: "POST",
            url: "/Check/GetTotalCount",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            data: {}
        }).then(function (response) {

            $scope.total_count_object = response.data[0]; //Get First element of response
            $scope.total_visits = $scope.total_count_object.total_count;

        }, function errorCallBack(response) {
            console.error("Error in server");
            console.log(response.data);
            Swal.fire({
                position: 'top',
                icon: 'success',
                title: response.data,
                showConfirmButton: false,
                timer: 1000
            })
        })
        // end httpRequest

    }

    function SetFocusToCheckInput() {
        document.getElementById("numeroempleado").focus();
    }

    function RestoreAllValues() {
        $scope.RestoreVisitType();
        $scope.RestoretVisitSex();
        $scope.RestoreAge();
        $scope.RestoreVisitObject();

        $scope.ShowEmployeeCheck = true;
        $scope.ShowVisitCheck = false;
        $scope.ShowVisitAge = false;

    }
});

$(document).ready(function () {



})