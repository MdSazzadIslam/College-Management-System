myapp.controller('teacher-controller', function ($scope, teacherService) {
    $scope.getTeacher = function () {
        //Loads all Teacher records when page loads
        getTeacher();
     
    }
    $scope.onloadFun = function () {
        var id = $("#TeacherId").val();
        if (id > 0) {
            getTeacherToUpdate(id);
        }
    }

    function getTeacher() {
        var TeacherRecords = teacherService.getTeacher();
        TeacherRecords.then(function (d) {
            //success
            $scope.Teachers = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Teacher list...");
                
            });
    }


    function getTeacherToUpdate(id) {
        var TeacherRecords = teacherService.getTeacherToUpdate(id);
        TeacherRecords.then(function (d) {
            //success
            $scope.TeacherId = d.data.TeacherId;
            $scope.TeacherName = d.data.TeacherName;
            $scope.BirthDate = d.data.BirthDate;
            $scope.Salary = d.data.Salary;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Teacher list...");
            });
    }


    $scope.getTeacherById = function (searchBy) {
        if (searchBy === undefined) {
            getTeacher();
        }
        else {
            var TeacherRecords = teacherService.getTeacherById(searchBy);
            TeacherRecords.then(function (d) {
                //success
                $scope.Teachers = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching Teacher list...");
                });
        }

    }



    //save Teacher data
    $scope.save = function () {
        debugger;
        var Teacher = {
            TeacherId: 0,
            TeacherName: $scope.TeacherName,
            BirthDate: $scope.BirthDate, 
            Salary : $scope.Salary
        };
        var saveData = teacherService.save(Teacher);
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
                AppUtil.ShowNotification("error", "Error occurred while adding Teacher.");

            });
    }
    //reset controls after save operation
    $scope.resetData = function () {
        $scope.TeacherId ='';
        $scope.TeacherName = '';
        $scope.BirthDate = '';
        $scope.Salary ='' ;
    }

    //update Teacher data
    $scope.update = function () {
        var Teacher = {
            TeacherId: $scope.TeacherId,
            TeacherName: $scope.TeacherName,
            BirthDate: $scope.BirthDate,
            Salary: $scope.Salary
        };
        var updateData = teacherService.update(Teacher);
        updateData.then(function (d) {
            if (d.data.Result === true) {
                AppUtil.ShowNotification("success", d.data.Message);
                $scope.resetUpdate();
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
        $scope.TeacherId = '';
        $scope.TeacherName = '';
        $scope.BirthDate = '';
        $scope.Salary = '';
    }

    //delete Employee record
    $scope.delete = function (TeacherId) {
        if (window.confirm('Are you sure you want to delete this Id = ' + TeacherId + '?'))//Popup window will open to confirm
        {
            var deleteData = teacherService.delete(TeacherId);
            deleteData.then(function (d) {
                if (d.data.Result === true) {
                    getTeacher();
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