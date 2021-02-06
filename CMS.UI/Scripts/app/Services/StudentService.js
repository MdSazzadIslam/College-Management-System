//Service to get data from employee mvc controller
myapp.service('studentService', function ($http) {

    this.getStudent = function () {

        return $http.get("/Student/GetStudent");
    }
    this.getStudentDetails = function () {

        return $http.get("/Student/GetStudentDetailsList");
    }
    this.getStudentById = function (searchBy) {

        return $http.get('/Student/GetStudentById/', { params: { searchBy: searchBy } });
    }

    this.getStudentToUpdate = function (id) {

        return $http.get('/Student/getStudentToUpdate/', { params: { id: id } });
    }
    //add new employee
    this.save = function (Student) {
        var request = $http({
            method: 'post',
            url: '/Student/Insert',
            data: Student
        });
        return request;
    }

    //update Employee records
    this.update = function (Student) {
        var updaterequest = $http({
            method: 'post',
            url: '/Student/Insert',
            data: Student
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (StudentId) {
        return $http.post('/Student/Delete/' + StudentId);
    }
});