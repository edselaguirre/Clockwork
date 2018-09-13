(
    function () {
        var app = angular.module('APIModule', []);
        app.controller('Controller', ['$scope', 'serviceClass', Controller]);
        app.service('serviceClass', serviceClass);
    }()
);