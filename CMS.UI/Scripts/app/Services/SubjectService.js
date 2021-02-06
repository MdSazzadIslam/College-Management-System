//Service to get data from employee mvc controller
myapp.service('subjectService', function ($http) {

    this.getSubject = function () {

        return $http.get("/Subject/GetSubject");
    }

    this.getSubjectDetails = function () {

        return $http.get("/Subject/GetSubjectDetailsList");
    }

    
    this.getCourse = function () {

        return $http.get("/Course/GetCourse");
    }
    
    this.getSubjectToUpdate = function (id) {

        return $http.get('/Subject/GetSubjectToUpdate/', { params: { id: id } });
    }

    this.getSubjectById = function (searchBy) {

        return $http.get('/Subject/GetSubjectById/', { params: { searchBy: searchBy } });
    }

    //add new employee
    this.save = function (Subject) {
        var request = $http({
            method: 'post',
            url: '/Subject/Insert',
            data: Subject
        });
        return request;
    }

    //update Employee records
    this.update = function (Subject) {
        var updaterequest = $http({
            method: 'post',
            url: '/Subject/Insert',
            data: Subject
        });
        return updaterequest;
    }

    //delete record
    this.delete = function (SubjectId) {
        return $http.post('/Subject/Delete/' + SubjectId);
    }
});