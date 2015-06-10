var app = angular.module('Reclamation', []);

app.controller('SiteVisitController', ['$scope', '$http', function ($scope, $http) {


    $scope.rawDataTypes = [
        { name: 'Desktop Review', selected: true, url: '/Data/CoordinatesD' },
        { name: 'Site Visit', selected: false, url: '/rawdata/sitevisit', action: '/RawData/SiteVisitReport' , formType: "SiteVisit"},
        { name: 'Site Prep', selected: false },
        { name: 'Vegetation Control', selected: false },
        { name: 'Revegetation', selected: false }
    ];

    $scope.getData = function (event, type) {

        if (type.selected) {

            if (type.name === "Desktop Review") {
                $http.get(type.url).success(function (data) {
                    showMarkersOnMap(data);
                });
            } else {
                $http.get(type.url).success(function (data) {
                    onRawDataSuccess(data, type);
                });
            }



        } else {
            if (type.name === "Desktop Review") {
                clearMarkers();
            } else {
                clearRawMarker();
            }

            
        }
    }
        
}]);

app.controller('DesktopReviewController', ['$scope', '$http', function ($scope, $http){


}]);
