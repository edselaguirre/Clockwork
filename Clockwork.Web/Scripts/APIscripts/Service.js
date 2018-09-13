
var serviceClass = function ($http) {
    var service = {}

    service.getNextPrevPage = function (pageno) {
        console.log("API page = " + pageno);
        return $http.get('http://localhost:5000/api/page/' + '?pageNo=' + pageno);
    }

    service.getResult = function (returnUserTimeId) {
        return $http.get('http://localhost:5000/api/currenttime/' + '?userTimezoneId=' + returnUserTimeId);
    }

    service.getTimezoneInfoList = function () {
        return $http.get('http://localhost:5000/api/TimezoneInfo/');
    }

    return service;
}

