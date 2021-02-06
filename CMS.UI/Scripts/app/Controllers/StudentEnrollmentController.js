myapp.controller('studentEnrollment-controller', function ($scope, studentEnrollmentService) {
    $scope.getStudentEnrollment = function () {
        getStudentEnrollment();
    }
    $scope.onloadFun = function () {
        getStudent();
        getGrade();
        getCourse();

        var id = $("#StudentEnrollmentId").val();
        if (id > 0) {
            getStudentEnrollmentToUpdate(id);
        }
    }
    //Loads all StudentEnrollment records when page loads
    function getStudentEnrollment() {
        var StudentEnrollmentRecords = studentEnrollmentService.getStudentEnrollment();
        StudentEnrollmentRecords.then(function (d) {
            //success
            $scope.StudentEnrollments = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching StudentEnrollment list...");
            });
    }

    function getStudentEnrollmentToUpdate(id) {
        var StudentEnrollmentRecords = studentEnrollmentService.getStudentEnrollmentToUpdate(id);
        StudentEnrollmentRecords.then(function (d) {
            //success
            $scope.StudentEnrollmentId = d.data.StudentEnrollmentId;
            $scope.StudentId = d.data.StudentId;
            $scope.CourseId = d.data.CourseId;
            $scope.SubjectId = d.data.SubjectId;
            $scope.GradeId = d.data.GradeId;
             $scope.GetSubjectByCourseId(d.data.CourseId);
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching StudentEnrollment list...");
            });
    }


    function getCourse() {
        var CourseRecords = studentEnrollmentService.getCourse();
        CourseRecords.then(function (d) {
            //success
            $scope.Courses = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching StudentEnrollment list...");
            });
    }
    function getStudent() {
        var StudentRecords = studentEnrollmentService.getStudent();
        StudentRecords.then(function (d) {
            //success
            $scope.Students = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching StudentEnrollment list...");
            });
    }

    function getGrade() {
        var GradeRecords = studentEnrollmentService.getGrade();
        GradeRecords.then(function (d) {
            //success
            $scope.Grades = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching StudentEnrollment list...");
            });
    }

    $scope.GetSubjectByCourseId = function (id) {
        if (id === undefined) {
            AppUtil.ShowNotification("error", "CourseId not found");
        }
        else {
            var SubjectRecords = studentEnrollmentService.GetSubjectByCourseId(id);
            SubjectRecords.then(function (d) {
                //success
                $scope.Subjects = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching StudentEnrollment list...");
                });
        }

    }

    $scope.getStudentEnrollmentById = function (searchBy) {
        if (searchBy === undefined || searchBy ==="") {
            getStudentEnrollment();
        }
        else {
            var StudentEnrollmentRecords = studentEnrollmentService.getStudentEnrollmentById(searchBy);
            StudentEnrollmentRecords.then(function (d) {
                //success
                $scope.StudentEnrollments = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching StudentEnrollment list...");
                });
        }

    }


    //save StudentEnrollment data
    $scope.save = function () {
        debugger;
        if ($scope.CourseId === "") {
            AppUtil.ShowNotification("error", "Course name is required!!");
            return;
        }
        var StudentEnrollment = {
            StudentEnrollmentId: 0,
            StudentId: $scope.StudentId,
            CourseId: $scope.CourseId,
            SubjectId: $scope.SubjectId,
            GradeId: $scope.GradeId
        };
        var saveData = studentEnrollmentService.save(StudentEnrollment);
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
                AppUtil.ShowNotification("error", "Error occurred while adding StudentEnrollment.");
            });
    }
    //reset controls after save operation
    $scope.resetData = function () {
             $scope.StudentEnrollmentId ='',
             $scope.StudentId = '',
             $scope.CourseId = '',
             $scope.SubjectId = '',
             $scope.GradeId = ''

    }

    //update StudentEnrollment data
    $scope.update = function () {
        var StudentEnrollment = {
            StudentEnrollmentId: $scope.StudentEnrollmentId,
            StudentId: $scope.StudentId,
            CourseId: $scope.CourseId,
            SubjectId: $scope.SubjectId,
            GradeId: $scope.GradeId
        };
        var updateData = studentEnrollmentService.update(StudentEnrollment);
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
            $scope.StudentEnrollmentId = '',
            $scope.StudentId = '',
            $scope.CourseId = '',
            $scope.SubjectId = '',
            $scope.GradeId = ''

    }

    //delete Employee record
    $scope.delete = function (StudentEnrollmentId) {
        if (window.confirm('Are you sure you want to delete this Id = ' + StudentEnrollmentId + '?'))//Popup window will open to confirm
        {
            var deleteData = studentEnrollmentService.delete(StudentEnrollmentId);
            deleteData.then(function (d) {
                if (d.data.Result === true) {
                    getStudentEnrollment();
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