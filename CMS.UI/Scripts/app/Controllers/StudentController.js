myapp.controller('student-controller', function ($scope, studentService) {
    $scope.getStudent = function () {
        //Loads all Student records when page loads
        getStudent();
    }
    $scope.getStudentDetails = function () {
        //Loads all Student records when page loads
        getStudentDetails();
    }
    $scope.onloadFun = function () {
        var id = $("#StudentId").val();
        if (id > 0) {
            getStudentToUpdate(id);
        }
    }

    function getStudent() {
        var StudentRecords = studentService.getStudent();
        StudentRecords.then(function (d) {
            //success
           // $scope.Data = $filter("date")(new Date(d.data.BirthDate), "MM/dd/yyyy h:mm")
            //console.log($scope.Data );
            $scope.Students = d.data;
            console.log($scope.Students);
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Student list...");
            });
    }


    function getStudentDetails() {
        var StudentDetailsRecords = studentService.getStudentDetails();
        StudentDetailsRecords.then(function (d) {
            //success
            $scope.StudentDetails = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Student list...");
            });
    }


    function getStudentToUpdate(id) {
        var StudentRecords = studentService.getStudentToUpdate(id);
        StudentRecords.then(function (d) {
            //success
            $scope.StudentId = d.data.StudentId;
            $scope.StudentName = d.data.StudentName;
            $scope.BirthDate = d.data.BirthDate;
            $scope.RegistrationNo = d.data.RegistrationNo
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Student list...");
            });
    }


    $scope.getStudentById = function (searchBy) {
        if (searchBy === undefined) {
            getStudent();
        }
        else {
            var StudentRecords = studentService.getStudentById(searchBy);
            StudentRecords.then(function (d) {
                //success
                $scope.Students = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching Student list...");
                });
        }

    }



    //save Student data
    $scope.save = function () {
        debugger;
        var Student = {
            StudentId: 0,
            BirthDate: $scope.BirthDate,
            StudentName: $scope.StudentName,
            RegistrationNo: $scope.RegistrationNo
        };
        var saveData = studentService.save(Student);
        saveData.then(function (d) {
            if (d.data.Result === true) {
                AppUtil.ShowNotification("success", d.data.Message);
                $scope.resetData();
            }
            else {
                AppUtil.ShowNotification("error", d.data.Message);
            }
        },
            function () {
                AppUtil.ShowNotification("error", "Error occurred while adding Student.");

            });
    }
    //reset controls after save operation
    $scope.resetData = function () {
        $scope.StudentId = '';
        $scope.BirthDate = '';
        $scope.StudentName = '';
        $scope.RegistrationNo = '';
    }

    //update Student data
    $scope.update = function () {
        var Student = {
            StudentId: $scope.StudentId,
            BirthDate: $scope.BirthDate,
            StudentName: $scope.StudentName,
            RegistrationNo: $scope.RegistrationNo
        };
        var updateData = studentService.update(Student);
        updateData.then(function (d) {
            if (d.data.Result === true) {
                AppUtil.ShowNotification("success", d.data.Message);
                $scope.resetUpdate();
                $window.location.href = '/Student/Index';
            }
            else {
                AppUtil.ShowNotification("error", d.data.Message);
            }
        },
            function () {
                AppUtil.ShowNotification("error", "Error occurred while updating record.");
            });
    }
    //reset controls after update
    $scope.resetUpdate = function () {
        $scope.StudentId = '';
        $scope.BirthDate = '';
        $scope.StudentName = '';
        $scope.RegistrationNo = '';
    }

    //delete Employee record
    $scope.delete = function (StudentId) {
        if (window.confirm('Are you sure you want to delete this Id = ' + StudentId + '?'))//Popup window will open to confirm
        {
            var deleteData = studentService.delete(StudentId);
            deleteData.then(function (d) {
                if (d.data.Result === true) {
                    getStudent();
                    AppUtil.ShowNotification("success", d.data.Message);
                }
                else {
                    AppUtil.ShowNotification("error", d.data.Message);
                }
            });

        }

    }
    $scope.orderByMe = function (x) {
        $scope.myOrderBy = x;
    }
});