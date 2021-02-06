//Service to get data from employee mvc controller
myapp.service('gradeService', function ($http) {

    this.getGrade = function () {

        return $http.get("/Grade/GetGrade");
    }

    this.getGradeById = function (searchBy) {

        return $http.get('/Grade/GetGradeById/', { params: { searchBy: searchBy } });
    }
    this.getGradeToUpdate = function (id) {

        return $http.get('/Grade/getGradeToUpdate/', { params: { id: id } });
    }

    //add new employee
    this.save = function (Grade) {
        var request = $http({
            method: 'post',
            url: '/Grade/Insert',
            data: Grade
        });
        return request;
    }

    //update Employee records
    this.update = function (Grade) {
        var updaterequest = $http({
            method: 'post',
            url: '/Grade/Insert',
            data: Grade
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (GradeId) {
        return $http.post('/Grade/Delete/' + GradeId);
    }
});