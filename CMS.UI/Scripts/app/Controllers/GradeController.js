myapp.controller('grade-controller', function ($scope, gradeService) {

    $scope.onloadFun = function () {
        var id = $("#GradeId").val();
        if (id > 0) {
            getGradeToUpdate(id);
        }
    }

    $scope.getGrade = function () {
        //Loads all Grade records when page loads
        getGrade();
    }

    function getGrade() {
        var GradeRecords = gradeService.getGrade();
        GradeRecords.then(function (d) {
            //success
            $scope.Grades = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Grade list...");
            });
    }

    function getGradeToUpdate(id) {
        var GradeRecords = gradeService.getGradeToUpdate(id);
        GradeRecords.then(function (d) {
            //success
            $scope.GradeId = d.data.GradeId;
            $scope.GradeName = d.data.GradeName;
            $scope.GradePoint = d.data.GradePoint;
            $scope.ScoreFrom = d.data.ScoreFrom;
            $scope.ScoreTo = d.data.ScoreTo;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Grade list...");
            });
    }

    $scope.getGradeById = function (searchBy) {
        if (searchBy === undefined) {
            getGrade();
        }
        else {
            var GradeRecords = gradeService.getGradeById(searchBy);
            GradeRecords.then(function (d) {
                //success
                $scope.Grades = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching Grade list...");
                });
        }

    }



    //save Grade data
    $scope.save = function () {
        debugger;
        var Grade = {
            GradeId: 0,
            GradeName: $scope.GradeName,
            ScoreFrom: $scope.ScoreFrom,
            ScoreTo: $scope.ScoreTo,
            GradePoint: $scope.GradePoint,
        };
        var saveData = gradeService.save(Grade);
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
                AppUtil.ShowNotification("error", "Error occurred while adding Grade.");
            });
    }
    //reset controls after save operation
    $scope.resetData = function () {
        $scope.GradeId = '';
        $scope.GradeName = '';
        $scope.ScoreTo = '';
        $scope.ScoreFrom = '';
        $scope.GradePoint = '';
    }

    //update Grade data
    $scope.update = function () {
        var Grade = {
            GradeId: $scope.GradeId,
            GradeName: $scope.GradeName,
            ScoreFrom: $scope.ScoreFrom,
            ScoreTo: $scope.ScoreTo,
            GradePoint: $scope.GradePoint,
        };
        var updateData = gradeService.update(Grade);
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
                AppUtil.ShowNotification("error", "Error occurred while updating employee record");
            });
    }
    //reset controls after update
    $scope.resetUpdate = function () {
        $scope.GradeId = '';
        $scope.GradeName = '';
        $scope.ScoreFrom = '';
        $scope.ScoreTo = '';
        $scope.GradePoint = '';
    }

    //delete Employee record
    $scope.delete = function (GradeId) {
        if (window.confirm('Are you sure you want to delete this Id = ' + GradeId + '?'))//Popup window will open to confirm
        {
            var deleteData = gradeService.delete(GradeId);
            deleteData.then(function (d) {
                if (d.data.Result === true) {
                    getGrade();
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