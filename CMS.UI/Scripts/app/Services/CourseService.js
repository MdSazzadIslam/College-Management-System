//Service to get data from employee mvc controller
myapp.service('courseService', function ($http) {

    this.getCourse = function () {

        return $http.get("/Course/GetCourse");
    }
    this.getCourseDetails = function () {

        return $http.get("/Course/GetCourseDetailsList");
    }

    this.getCourseById = function (searchBy) {

        return $http.get('/Course/GetCourseById/', { params: { searchBy: searchBy } });
    }

    this.getCourseToUpdate = function (id) {

        return $http.get('/Course/getCourseToUpdate/', { params: { id: id } });
    }
    //add new employee
    this.save = function (Course) {
        var request = $http({
            method: 'post',
            url: '/Course/Insert',
            data: Course
        });
        return request;
    }

    //update Employee records
    this.update = function (Course) {
        var updaterequest = $http({
            method: 'post',
            url: '/Course/Insert',
            data: Course
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (CourseId) {
        return $http.post('/Course/Delete/' + CourseId);
    }
});