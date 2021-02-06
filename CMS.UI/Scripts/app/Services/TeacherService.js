//Service to get data from employee mvc controller
myapp.service('teacherService', function ($http) {

    this.getTeacher = function () {

        return $http.get("/Teacher/GetTeacher");
    }

    this.getTeacherById = function (searchBy) {

        return $http.get('/Teacher/GetTeacherById/', { params: { searchBy: searchBy } });
    }

    this.getTeacherToUpdate = function (id) {

        return $http.get('/Teacher/getTeacherToUpdate/', { params: { id: id } });
    }
    //add new employee
    this.save = function (Teacher) {
        var request = $http({
            method: 'post',
            url: '/Teacher/Insert',
            data: Teacher
        });
        return request;
    }

    //update Employee records
    this.update = function (Teacher) {
        var updaterequest = $http({
            method: 'post',
            url: '/Teacher/Insert',
            data: Teacher
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (TeacherId) {
        return $http.post('/Teacher/Delete/' + TeacherId);
    }
});