myapp.controller('course-controller', function ($scope, courseService) {


    $scope.getCourse = function () {
        //Loads all course records when page loads
      
        getCourse();
 
    }
    $scope.getCourseDetails = function () {
        //Loads all course records when page loads
        getCourseDetails();
    }
    $scope.onloadFun = function () {
        var id = $("#CourseId").val();
        if (id > 0) {
            getCourseToUpdate(id);
        }
    }

    function getCourse() {

        var courseRecords = courseService.getCourse();
        courseRecords.then(function (d) {
            //success
            $scope.courses = d.data;
        },
            function () {
                AppUtil.ShowNotification("error", "Error occured while fetching Course list...");
            });
    }


    function getCourseDetails() {
        var CourseDetailsRecords = courseService.getCourseDetails();
        CourseDetailsRecords.then(function (d) {
            //success
            $scope.CourseDetails = d.data;
        },
            function () {
                alert("Error occured while fetching course list...");
            });
    }



    function getCourseToUpdate(id) {
        debugger;
        var CourseRecords = courseService.getCourseToUpdate(id);
        debugger;
        CourseRecords.then(function (d) {
            //success
            $scope.CourseId = d.data.CourseId;
            $scope.CourseCode = d.data.CourseCode;
            $scope.CourseName = d.data.CourseName;
            $scope.CourseCredit = d.data.CourseCredit;
        },
            function () {
                
                AppUtil.ShowNotification("error", "Error occured while fetching Course list...");
            });
    }


    $scope.getCourseById = function (searchBy) {
        if (searchBy === undefined) {
            getCourse();
        }
        else {
            var courseRecords = courseService.getCourseById(searchBy);
            courseRecords.then(function (d) {
                //success
                $scope.courses = d.data;
            },
                function () {
                    AppUtil.ShowNotification("error", "Error occured while fetching Course list...");
                });
        }

    }

    //save course data
    $scope.save = function () {
        debugger;
        var course = {
            CourseId: 0,
            CourseCode: $scope.CourseCode,
            CourseName: $scope.CourseName,
            CourseCredit: $scope.CourseCredit,

        };
        var saveData = courseService.save(course);
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
                AppUtil.ShowNotification("error", "Error occurred while adding course.");

            });
    }
    //reset controls after save operation
    $scope.resetData = function () {
        $scope.CourseId = '';
        $scope.CourseCode = '';
        $scope.CourseName = '';
        $scope.CourseCredit = '';

    }

    //update course data
    $scope.update = function () {
        var course = {
            CourseId: $scope.CourseId,
            CourseCode: $scope.CourseCode,
            CourseName: $scope.CourseName,
            CourseCredit: $scope.CourseCredit
        };
        var updateData = courseService.update(course);
        updateData.then(function (d) {
            if (d.data.Result === true) {
                AppUtil.ShowNotification("success", d.data.Message);

                //$scope.resetUpdate();
                //$window.location.href = '/Course/Index';
            }
            else {
                AppUtil.ShowNotification("error", d.data.Message);
            }
        },
            function () {
                AppUtil.ShowNotification("error", "Error occurred while updating record.");
            });
    }

    //delete Employee record
    $scope.delete = function (courseId) {
        if (window.confirm('Are you sure you want to delete this Id = ' + courseId + '?'))//Popup window will open to confirm
        {
            var deleteData = courseService.delete(courseId);
            deleteData.then(function (d) {
                if (d.data.Result === true) {
                    getCourse();
                    AppUtil.ShowNotification("success", d.data.Message);
                }
                else {
                    AppUtil.ShowNotification("error", d.data.Message);
                }
            });

        }

    }

    //get single record by ID
    $scope.getForUpdate = function (course) {
        $scope.CourseId = course.CourseId;
        $scope.CourseCode = course.CourseCode;
        $scope.CourseName = course.CourseName;
        $scope.CourseCredit = course.CourseCredit;
    }

    //get data for delete confirmation
    $scope.getForDelete = function (Employee) {
        $scope.CourseId = Employee.CourseId;
        $scope.CourseCode = Employee.CourseCode;
        $scope.CourseName = Employee.CourseName;
        $scope.CourseCredit = Employee.CourseCredit;

    }
    $scope.orderByMe = function (x) {
        $scope.myOrderBy = x;
    }
});