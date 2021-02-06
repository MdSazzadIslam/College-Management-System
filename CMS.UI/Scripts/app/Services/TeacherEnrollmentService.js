//Service to get data from employee mvc controller
myapp.service('teacherEnrollmentService', function ($http) {

    this.getTeacherEnrollment = function () {

        return $http.get("/TeacherEnrollment/GetTeacherEnrollment");
    }
    this.getCourse = function () {

        return $http.get("/Course/GetCourse");
    }
    this.getTeacher = function () {

        return $http.get("/Teacher/GetTeacher");
    }

    this.getTeacherEnrollmentToUpdate = function (id) {

        return $http.get('/TeacherEnrollment/GetTeacherEnrollmentToUpdate/', { params: { id: id } });
    }

    this.GetSubjectByCourseId = function (id) {

        return $http.get('/Subject/GetSubjectByCourseId/', { params: { id: id } });
    }

    this.getTeacherEnrollmentById = function (searchBy) {

        return $http.get('/TeacherEnrollment/GetTeacherEnrollmentById/', { params: { searchBy: searchBy } });
    }

    //add new employee
    this.save = function (TeacherEnrollment) {
        var request = $http({
            method: 'post',
            url: '/TeacherEnrollment/Insert',
            data: TeacherEnrollment
        });
        return request;
    }

    //update Employee records
    this.update = function (TeacherEnrollment) {
        var updaterequest = $http({
            method: 'post',
            url: '/TeacherEnrollment/Insert',
            data: TeacherEnrollment
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (TeacherEnrollmentId) {
        return $http.post('/TeacherEnrollment/Delete/' + TeacherEnrollmentId);
    }
});