myapp.controller('subject-controller', function ($scope, subjectService) {
    $scope.getSubject = function () {
        getSubject();
    }

    $scope.getSubjectDetails = function () {
        getSubjectDetails();
    }

    
    $scope.onloadFun = function () {
        getCourse();
        var id = $("#SubjectId").val();
        if (id > 0) {
            getSubjectToUpdate(id);
        }
    }
    //Loads all Subject records when page loads
    function getSubject() {
        var SubjectRecords = subjectService.getSubject();
        SubjectRecords.then(function (d) {
            //success
            $scope.Subjects = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Subject list...");
            });
    }

    function getSubjectDetails() {
        var SubjectDetailRecords = subjectService.getSubjectDetails();
        SubjectDetailRecords.then(function (d) {
            //success
            $scope.SubjectDetails = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Subject list...");
            });
    }

    function getSubjectToUpdate(id) {
        var SubjectRecords = subjectService.getSubjectToUpdate(id);
        SubjectRecords.then(function (d) {
            //success
            $scope.SubjectId = d.data.SubjectId;
            $scope.SubjectCode = d.data.SubjectCode;
            $scope.SubjectName = d.data.SubjectName;
            $scope.CourseId = d.data.CourseId;
            $scope.SubjectCredit = d.data.SubjectCredit;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Subject list...");
            });
    }


    function getCourse() {
        var CourseRecords = subjectService.getCourse();
        CourseRecords.then(function (d) {
            //success
            $scope.Courses = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Subject list...");
            });
    }


    $scope.getSubjectById = function (searchBy) {
        if (searchBy === undefined) {
            getSubject();
        }
        else {
            var SubjectRecords = subjectService.getSubjectById(searchBy);
            SubjectRecords.then(function (d) {
                //success
                $scope.Subjects = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching Subject list...");
                });
        }

    }


    //save Subject data
    $scope.save = function () {
        debugger;
        if ($scope.CourseId === "") {
            AppUtil.ShowNotification("error", "Course name is required!!"); 
            return;
        }
        var Subject = {
            SubjectId: 0,
            SubjectCode: $scope.SubjectCode,
            SubjectName: $scope.SubjectName,
            CourseId: $scope.CourseId,
            SubjectCredit: $scope.SubjectCredit
        };
        var saveData = subjectService.save(Subject);
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
                AppUtil.ShowNotification("error", "Error occurred while adding Subject.");
            });
    }
    //reset controls after save operation
    $scope.resetData = function () {
        $scope.SubjectId = '';
        $scope.SubjectCode = '';
        $scope.SubjectName = '';
        $scope.CourseId = "";
        $scope.SubjectCredit = "";

    }

    //update Subject data
    $scope.update = function () {
        var Subject = {
            SubjectId: $scope.SubjectId,
            SubjectCode: $scope.SubjectCode,
            SubjectName: $scope.SubjectName,
            CourseId: $scope.CourseId,
            SubjectCredit: $scope.SubjectCredit
        };
        var updateData = subjectService.update(Subject);
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
                AppUtil.ShowNotification("error", "Error occured while updating employee record");
            });
    }


    //delete Employee record
    $scope.delete = function (SubjectId) {
        if (window.confirm('Are you sure you want to delete this Id = ' + SubjectId + '?'))//Popup window will open to confirm
        {
            var deleteData = subjectService.delete(SubjectId);
            deleteData.then(function (d) {
                if (d.data.Result === true) {
                    getSubject();
                    AppUtil.ShowNotification("success", d.data.Message);
                }
                else {
                    AppUtil.ShowNotification("error", d.data.Message);
                }
            });

        }

    }

    //get single record by ID
    $scope.getForUpdate = function (subject) {
        $scope.SubjectId = subject.SubjectId;
        $scope.SubjectCode = subject.SubjectCode;
        $scope.SubjectName = subject.SubjectName;
        $scope.CourseId = subject.CourseId;
        $scope.SubjectCredit = subject.SubjectCredit;
    }

    //get data for delete confirmation
    $scope.getForDelete = function (subject) {
        $scope.SubjectId = subject.SubjectId;
        $scope.SubjectCode = subject.SubjectCode;
        $scope.SubjectName = subject.SubjectName;
        $scope.CourseId = subject.CourseId;
        $scope.SubjectCredit = subject.SubjectCredit;

    }
    $scope.orderByMe = function (x) {
        $scope.myOrderBy = x;
    }
});