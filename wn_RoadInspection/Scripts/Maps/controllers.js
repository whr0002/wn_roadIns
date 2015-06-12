var app = angular.module('Reclamation', []);

app.controller('SiteVisitController', ['$scope', '$http', function ($scope, $http) {


    $scope.rawDataTypes = [
        { name: 'Road Inspection Data', selected: true, url: '/Data/CoordinatesD' },
    ];

    $scope.getData = function (event, type) {

        if (type.selected) {
            // Show data
            if (type.name === "Road Inspection Data") {
                $http.get(type.url).success(function (data) {
                    showMarkersOnMap(data);
                });
            }



        } else {
            // Clear data
            if (type.name === "Road Inspection Data") {
                clearMarkers();
                clearPaths();
            }

            
        }
    }
        
}]);

app.controller('DesktopReviewController', ['$scope', '$http', function ($scope, $http){


}]);
