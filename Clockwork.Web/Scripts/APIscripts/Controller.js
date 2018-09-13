var Controller = function ($scope, serviceClass) {   

    var data = this;
    $scope.currentPage = 0;
    $scope.totalPage = 0;

    //get the time button
    $scope.getRes = function () {
        serviceClass.getResult($scope.returnUserTime.id)
            .then(function callResponse(response) {
                console.log(response);
                data.returnData = response.data.displayedData;
                $scope.hasNext = response.data.pageNext;
                $scope.hasPrev = response.data.pagePrev;
                $scope.totalPage = response.data.totalPage
                $scope.currentPage = 1;
                $scope.nextPage = $scope.currentPage + 1;
                $scope.prevPage = $scope.currentPage - 1;              
            });
    }
    //pages next previous
    $scope.getNextPrev = function (pageno) {
        serviceClass.getNextPrevPage(pageno)
            .then(function callResponse(response) {
                console.log('pagenum = ' + pageno);
                data.returnData = response.data.displayedData;
                $scope.hasNext = response.data.pageNext;
                $scope.hasPrev = response.data.pagePrev;
                $scope.totalPage = response.data.totalPage
                $scope.currentPage = pageno;
                $scope.nextPage = $scope.currentPage + 1;
                $scope.prevPage = $scope.currentPage - 1;                
            });
    }
    //timezone list
    $scope.getTimezoneList = function () {
        serviceClass.getTimezoneInfoList()
            .then(function callResponse(response) {
                data.timeList = response.data.timezoneList;
                $scope.returnUserTime = response.data.userTimezone;                
            });
    }

    //for initial value
    $scope.getNextPrev(1);
    $scope.getTimezoneList();
}