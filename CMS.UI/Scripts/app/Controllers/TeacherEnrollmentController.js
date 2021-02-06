myapp.controller('teacherEnrollment-controller', function ($scope, teacherEnrollmentService) {
    $scope.getTeacherEnrollment = function () {
        getTeacherEnrollment();
    }
    $scope.onloadFun = function () {
        getTeacher();
        getCourse();

        var id = $("#TeacherEnrollmentId").val();
        if (id > 0) {
            getTeacherEnrollmentToUpdate(id);
        }
    }
    //Loads all TeacherEnrollment records when page loads
    function getTeacherEnrollment() {
        var TeacherEnrollmentRecords = teacherEnrollmentService.getTeacherEnrollment();
        TeacherEnrollmentRecords.then(function (d) {
            //success
            $scope.TeacherEnrollments = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching TeacherEnrollment list...");
            });
    }

    function getTeacherEnrollmentToUpdate(id) {
        var TeacherEnrollmentRecords = teacherEnrollmentService.getTeacherEnrollmentToUpdate(id);
        TeacherEnrollmentRecords.then(function (d) {
            //success
            $scope.TeacherEnrollmentId = d.data.TeacherEnrollmentId;
            $scope.TeacherId = d.data.TeacherId;
            $scope.CourseId = d.data.CourseId;
            $scope.SubjectId = d.data.SubjectId;
            $scope.GetSubjectByCourseId(d.data.CourseId);
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching TeacherEnrollment list...");
            });
    }


    function getCourse() {
        var CourseRecords = teacherEnrollmentService.getCourse();
        CourseRecords.then(function (d) {
            //success
            $scope.Courses = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching TeacherEnrollment list...");
            });
    }
    function getTeacher() {
        var TeacherRecords = teacherEnrollmentService.getTeacher();
        TeacherRecords.then(function (d) {
            //success
            $scope.Teachers = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching TeacherEnrollment list...");
            });
    }
    $scope.GetSubjectByCourseId = function (id) {
        if (id === undefined) {
            AppUtil.ShowNotification("error", "CourseId not found");
        }
        else {
            var SubjectRecords = teacherEnrollmentService.GetSubjectByCourseId(id);
            SubjectRecords.then(function (d) {
                //success
                $scope.Subjects = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching TeacherEnrollment list...");
                });
        }

    }

    $scope.getTeacherEnrollmentById = function (searchBy) {
        if (searchBy === undefined || searchBy === "") {
            getTeacherEnrollment();
        }
        else {
            var TeacherEnrollmentRecords = teacherEnrollmentService.getTeacherEnrollmentById(searchBy);
            TeacherEnrollmentRecords.then(function (d) {
                //success
                $scope.TeacherEnrollments = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching TeacherEnrollment list...");
                });
        }

    }


    //save TeacherEnrollment data
    $scope.save = function () {
        debugger;
        if ($scope.CourseId === "") {
            AppUtil.ShowNotification("error", "Course name is required!!");
            return;
        }
        var TeacherEnrollment = {
            TeacherEnrollmentId: 0,
            TeacherId: $scope.TeacherId,
            CourseId: $scope.CourseId,
            SubjectId: $scope.SubjectId
           
        };
        var saveData = teacherEnrollmentService.save(TeacherEnrollment);
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
                AppUtil.ShowNotification("error", "Error occurred while adding TeacherEnrollment.");
            });
    }
    //reset controls after save operation
    $scope.resetData = function () {
        $scope.TeacherEnrollmentId = '',
            $scope.TeacherId = '',
            $scope.CourseId = '',
            $scope.SubjectId = '',
            $scope.GradeId = ''

    }

    //update TeacherEnrollment data
    $scope.update = function () {
        var TeacherEnrollment = {
            TeacherEnrollmentId: $scope.TeacherEnrollmentId,
            TeacherId: $scope.TeacherId,
            CourseId: $scope.CourseId,
            SubjectId: $scope.SubjectId,
            GradeId: $scope.GradeId
        };
        var updateData = teacherEnrollmentService.update(TeacherEnrollment);
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
    //reset controls after update
    $scope.resetUpdate = function () {
        $scope.TeacherEnrollmentId = '',
            $scope.TeacherId = '',
            $scope.CourseId = '',
            $scope.SubjectId = '',
            $scope.GradeId = ''

    }

    //delete Employee record
    $scope.delete = function (TeacherEnrollmentId) {
        if (window.confirm('Are you sure you want to delete this Id = ' + TeacherEnrollmentId + '?'))//Popup window will open to confirm
        {
            var deleteData = teacherEnrollmentService.delete(TeacherEnrollmentId);
            deleteData.then(function (d) {
                if (d.data.Result === true) {
                    getTeacherEnrollment();
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