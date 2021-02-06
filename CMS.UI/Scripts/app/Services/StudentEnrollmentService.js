//Service to get data from employee mvc controller
myapp.service('studentEnrollmentService', function ($http) {

    this.getStudentEnrollment = function () {

        return $http.get("/StudentEnrollment/GetStudentEnrollment");
    }
    this.getCourse = function () {

        return $http.get("/Course/GetCourse");
    }
    this.getGrade = function () {

        return $http.get("/Grade/GetGrade");
    }
    this.getStudent = function () {

        return $http.get("/Student/GetStudent");
    }

    this.getStudentEnrollmentToUpdate = function (id) {

        return $http.get('/StudentEnrollment/GetStudentEnrollmentToUpdate/', { params: { id: id } });
    }

    this.GetSubjectByCourseId = function (id) {

        return $http.get('/Subject/GetSubjectByCourseId/', { params: { id: id } });
    }

    this.getStudentEnrollmentById = function (searchBy) {

        return $http.get('/StudentEnrollment/GetStudentEnrollmentById/', { params: { searchBy: searchBy } });
    }

    //add new employee
    this.save = function (StudentEnrollment) {
        var request = $http({
            method: 'post',
            url: '/StudentEnrollment/Insert',
            data: StudentEnrollment
        });
        return request;
    }

    //update Employee records
    this.update = function (StudentEnrollment) {
        var updaterequest = $http({
            method: 'post',
            url: '/StudentEnrollment/Insert',
            data: StudentEnrollment
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (StudentEnrollmentId) {
        return $http.post('/StudentEnrollment/Delete/' + StudentEnrollmentId);
    }
});