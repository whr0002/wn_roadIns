google.maps.event.addDomListener(window, 'load', getCoords);
// Get only coordinates and Risk levels
function getCoords() {
    //var actionUrl = '@Url.Action("Coordinates", "Data")';
    var actionUrl = "/Data/CoordinatesD";
    $.getJSON(actionUrl, showMarkersOnMap);
    
}


// Adding markers on map
var markers = [];
var selectedMarkers = [];
var selectedRealMarkers = [];
var allCPaths = [];
var selectedLocations = [];
var map;
var CMarker = function (id, lat, lng) {
    this.ID = id;
    this.Latitude = lat;
    this.Longitude = lng;
    this.LatLng = new google.maps.LatLng(this.Latitude, this.Longitude);
}
var CPath = function (id, date, path) {
    this.ID = id;
    this.Date = convertMilliToDate(date);;
    this.Path = path;
    this.LocationList = parsePath(path);
    this.Color = getRandomColor();
    this.PolyLine = new google.maps.Polyline({
        path: getGoogleCoords(this.LocationList),
        geodesic: true,
        strokeColor: this.Color,
        strokeOpacity: 1.0,
        strokeWeight: 5
    });
}
var CMarkers = [];
var shouldAutoCenter = true;
var Location = function (latitude, longitude) {
    this.Latitude = latitude;
    this.Longitude = longitude;
    this.LatLng = new google.maps.LatLng(latitude, longitude);
}
function showMarkersOnMap(response) {

    $('#processingIndicator').hide();
    if (response != null) {
        // Listen to 'Escape' key press
        setKeypressListener();
        // Clear data
        clearMarkers();
        clearPaths();
        clearLocations();
        selectedMarkers = [];
        selectedRealMarkers = [];

        // Hide window
        $('#selectedMarkersWindow').hide();
        CMarkers = [];
        if (lastDrawing != null) {
            lastDrawing.setMap(null);
        }

        // Setting Map properties
        if(shouldAutoCenter){
            var mapProp = {
                center: new google.maps.LatLng(54.7293520508, -115.460487494),
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.HYBRID
            };
            map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

            var polyOptions = {
                fillColor: '#1E90FF',
                fillOpacity: 0.7,
                strokeWeight: 3,
                strokeColor: '#0076E4',
                zIndex: 1,
                editable: true
            };

            var drawingManager = new google.maps.drawing.DrawingManager({
                drawingMode: null,
                drawingControl: true,
                drawingControlOptions: {
                    position: google.maps.ControlPosition.TOP_CENTER,
                    drawingModes: [
                      //google.maps.drawing.OverlayType.MARKER,
                      google.maps.drawing.OverlayType.CIRCLE,
                      google.maps.drawing.OverlayType.POLYGON,
                      //google.maps.drawing.OverlayType.POLYLINE,
                      google.maps.drawing.OverlayType.RECTANGLE
                    ]
                },
                circleOptions: polyOptions,
                rectangleOptions: polyOptions,
                polygonOptions: polyOptions
            });

            drawingManager.setMap(map);
            google.maps.event.addListener(drawingManager, 'overlaycomplete', function (event) {
                //doSelection(drawingManager, event);
                selectPolylines(drawingManager, event);
            });
        }
        


        for (var i = 0; i < response.length; i++) {

            // Loop through data and set them up on map
            var cpath = new CPath(response[i].ID, response[i].Date, response[i].Path);
            allCPaths.push(cpath);

            var locations = cpath.LocationList;
            selectedLocations.push(locations);
            
            // Set up pop up windows
            var infoWindowOptions = { content: 'Empty' };
            var infoWindow = new google.maps.InfoWindow(infoWindowOptions);

            if (locations.length > 0) {
                // Draw things on map if locations exist
                //var googlePath = drawPath(getGoogleCoords(locations), map);
                if (cpath.LocationList.length > 1) {
                        cpath.PolyLine.setMap(map);
                        bindPolylineInfoWindow(cpath, map, infoWindow, response[i]);
                }else{
                
                    // Set up marker options according to the latitude and longitude
                    var markerOptions = {
                        position: new google.maps.LatLng(locations[0].Latitude, locations[0].Longitude),
                        icon: '/Content/Images/marker_green.png'
                    };
                    // Create a marker for starting point
                    var marker = new google.maps.Marker(markerOptions);
                    var cMarker = new CMarker(response[i].ID, locations[0].Latitude, locations[0].Longitude);


                    // Add marker to array
                    markers.push(marker);
                    CMarkers.push(cMarker);


                    // Set marker on map
                    marker.setMap(map);
                    bindInfoWindow(marker, map, infoWindow, response[i]);
                }
                
            }
        }

        if (shouldAutoCenter){
        // Auto center the map according to markers
            //AutoCenter(map, markers);
            autoCenterPolylines(map, allCPaths);
            shouldAutoCenter = false;
        }
    } else {
        alert("Cannot get data.");
    }

}
function clearMarkers() {
    if (markers != null && markers.length > 0) {
        for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(null);
        }
    }
    markers = [];
}
function clearPaths() {
    if (allCPaths != null) {
        for (i = 0; i < allCPaths.length; i++) {
            allCPaths[i].PolyLine.setMap(null);
        }

    }
    allCPaths = [];
}
function clearLocations() {
    selectedLocations = [];
}
function parsePath(path) {
    var locations = [];
    var temp = path.trim().split(" ");
    for (i = 0; i < temp.length; i++) {

        var temp2 = temp[i].split(",");
        var location = new Location(temp2[1], temp2[0]);
        locations.push(location);
    }

    return locations;
}
function getGoogleCoords(locations) {
    var coords = [];
    for (i = 0; i < locations.length; i++) {
        coords.push(locations[i].LatLng);
        
    }

    return coords;
}
var paths = [];
function drawPath(coords, map) {
    if(coords.length > 1){
        var path = new google.maps.Polyline({
            path: coords,
            geodesic: true,
            strokeColor: COLOR_UNSELECTED,
            strokeOpacity: 1.0,
            strokeWeight: 5
        });

        paths.push(path);
        path.setMap(map);

        return path;
    }

    return null;
}

var lastDrawing;
function doSelection(drawingManager, event) {
    // Clear
    selectedMarkers = [];
    selectedRealMarkers = [];
    if (lastDrawing != null) {
        lastDrawing.setMap(null);
    }
    if (lastInfoWindow != null) {
        // Close last openned window
        lastInfoWindow.close();
    }

    if (lastMarker != null) {
        lastMarker.setAnimation(null);
    }


    lastDrawing = event.overlay;
    lastDrawing.type = event.type;

    if (event.type == google.maps.drawing.OverlayType.POLYGON) {
        // Drawing is polygon
        for (var k = 0; k < CMarkers.length; k++) {
            if (google.maps.geometry.poly.containsLocation(CMarkers[k].LatLng, event.overlay)) {
                selectedMarkers.push(CMarkers[k]);
                selectedRealMarkers.push(markers[k]);
            }
        }

        lastDrawing.getPaths().forEach(function (path, index) {
            google.maps.event.addListener(path, 'insert_at', function () {
                // New point
                doOnSelectionChanged(drawingManager, event);
            });

            google.maps.event.addListener(path, 'remove_at', function () {
                // Point was removed
                doOnSelectionChanged(drawingManager, event);
            });

            google.maps.event.addListener(path, 'set_at', function () {
                // Point was moved
                doOnSelectionChanged(drawingManager, event);
            });

        });
        

    } else {
        var bounds = event.overlay.getBounds();
        for (var k = 0; k < CMarkers.length; k++) {
            if (bounds.contains(CMarkers[k].LatLng)) {
                selectedMarkers.push(CMarkers[k]);
                selectedRealMarkers.push(markers[k]);
            }
        }

        google.maps.event.addListener(lastDrawing, "bounds_changed", function () {
            doOnSelectionChanged(drawingManager, event);
        });

    }

    showSelectedMarkersInWindow();



    // Cancel drawing mode
    drawingManager.setDrawingMode(null);
    //console.log("# selected markers: " + selectedMarkers.length);
    //console.log("# selected real markers: " + selectedRealMarkers.length);
}

function doOnSelectionChanged(drawingManager, event) {
    // Clear
    selectedMarkers = [];
    selectedRealMarkers = [];

    if (event.type == google.maps.drawing.OverlayType.POLYGON) {
        for (var k = 0; k < CMarkers.length; k++) {
            if (google.maps.geometry.poly.containsLocation(CMarkers[k].LatLng, event.overlay)) {
                selectedMarkers.push(CMarkers[k]);
                selectedRealMarkers.push(markers[k]);
            }
        }
    } else {
        var bounds = event.overlay.getBounds();
        for (var k = 0; k < CMarkers.length; k++) {
            if (bounds.contains(CMarkers[k].LatLng)) {
                selectedMarkers.push(CMarkers[k]);
                selectedRealMarkers.push(markers[k]);
            }
        }
    }
    showSelectedMarkersInWindow();
    // Cancel drawing mode
    drawingManager.setDrawingMode(null);
    console.log("# selected markers: " + selectedMarkers.length);
}

// Show selected marker information in window
function showSelectedMarkersInWindow() {

    $('#selectedMarkersWindow').empty();
    
    $('#selectedMarkersWindow').append("<div id='ccrow' class='row'><h5 class='col-md-10'>Selected " + selectedMarkers.length + " markers</h5>" +
        '<a id="close_window" class="col-md-1" href="javascript:void(0)" onclick="onEscPressedClear()">[X]</a></div>');
    for (var i = 0; i < selectedMarkers.length; i++) {
        $('#selectedMarkersWindow').append("<div class='col-md-12'>"+
            "<label><input id='choose_marker_option' name='choose_marker_option' type='radio' value='" +
            i + "'>" + (i+1) + ". (" + selectedMarkers[i].Latitude + ", " + selectedMarkers[i].Longitude
            +")</label></div>");
    }

    $("input[name='choose_marker_option']").change(markerSelectionChanged);
    //$('#close_window').click(onEscPressedClear);
    $('#selectedMarkersWindow').show();
}

var previousSelectedMarker;
function markerSelectionChanged() {
    if (previousSelectedMarker != null) {
        previousSelectedMarker.setAnimation(null);
    }
    radioValue = $(this).val();

    // Animate selected marker
    selectedRealMarkers[radioValue].setAnimation(google.maps.Animation.BOUNCE);
    previousSelectedMarker = selectedRealMarkers[radioValue];

    currentID = selectedMarkers[radioValue].ID;
    // show details
    getOneLocation(selectedMarkers[radioValue].ID);
}

// Give each marker a info window
var lastMarker;
var lastInfoWindow;
var currentID;

function bindInfoWindow(marker, map, infowindow, json) {
    google.maps.event.addListener(marker, "click", function (e) {
        // Marker is clicked
        // Should get entire info from database now
        // Clear
        selectedMarkers = [];
        selectedRealMarkers = [];
        $('#selectedMarkersWindow').hide();
        if (lastDrawing != null) {
            lastDrawing.setMap(null);
        }
        var mar = new CMarker(json.ID, json.Latitude, json.Longitude);
        selectedMarkers.push(mar);

        if (lastInfoWindow != null) {
            // Close last openned window
            lastInfoWindow.close();
        }

        if (lastMarker != null) {
            lastMarker.setAnimation(null);
        }

        if (previousSelectedMarker != null) {
            previousSelectedMarker.setAnimation(null);
        }

        marker.setAnimation(google.maps.Animation.BOUNCE);
        lastMarker = marker;
        lastInfoWindow = infowindow;
        var date = convertMilliToDate(json.Date);
        infowindow.setContent("<table><tr><td>ID </td><td>" + json.ID + "</td></tr><tr><td>Date </td><td>" + date + "</td></tr></table>");
        getOneLocation(json.ID);

        // Temporaty hold the current latitude and longtitude
        currentID = json.ID;
        //currentLatLong["Latitude"] = json.Latitude;
        //currentLatLong["Longtitude"] = json.Longitude;
        //currentLatLong["Client"] = json.Client;

        infowindow.open(map, this);
    });
}


function getOneLocation(id) {
    var actionUrl = "/Data/OneRowD/" + id + "/";
    $.getJSON(actionUrl, displayInWindow);
}

// Show detailed infomation in a table
var currentDetailData;
function displayInWindow(response) {
    if (response != null) {

        // Set current detailed data
        currentDetailData = response;
        // Empty list first
        $("#coordList").empty();
        $("#imageList").empty();
        $("#downloadDiv").empty();
        $("#detailsDiv").empty();
        $("#downloadDiv").append('<a href="/RoadInspections/edit/' + response["RoadInspection"]["RoadInspectionID"] + '" class="btn btn-primary">Edit</a>');


        // Got data, Display it
            
        var i = 1;
        extractJSON(i, response["RoadInspection"]);


            
        // Display images
        console.log(response["Photos"]);
        var images = response["Photos"];
        displayImages(images);

    } else {
        alert("No Data");
    }
}

// Recursively parse JSON objects
function extractJSON(i, response) {
    for (var key in response) {
        var rst = i % 2;
        if (response.hasOwnProperty(key)) {

            var value = response[key];
            if (typeof value == 'object') {
                i++;
                extractJSON(i, value);
                    
            } else {

                if (value != null && value != "") {
                    var keyLower = key.toLowerCase();

                    if (keyLower.indexOf("date") > -1) {
                        value = convertMilliToDate(value);
                    }

                    if (keyLower === "siteid" || key.indexOf("ID") == -1) {

                        if (rst == 1) {
                            $("#coordList").append("<tr class='mTableStyle'><td>" + key + " </td><td>" + value + "</td></tr>");
                        } else {
                            $("#coordList").append("<tr><td>" + key + " </td><td>" + value + "</td></tr>");
                        }
                        i++;
                    }
                }
            }
        }
    }
}

function displayImages(theImages) {
    for (i = 0; i < theImages.length; i++) {
        var image = theImages[i];
        for (var key in image) {
            if (image.hasOwnProperty(key)) {
                if (key.indexOf("Path") > -1 && image.Path != null && image.Path.length > 0) {
                    $("#imageList").append('<span>' + image.Description + '</span><img src="' + image.Path + '" class="img-responsive"><br />');
                }
            }

        }
    }
}

function AutoCenter(map, markers) {
    //  Create a new viewpoint bound
    var bounds = new google.maps.LatLngBounds();
    //  Go through each...
    $.each(markers, function (index, marker) {
        bounds.extend(marker.position);
    });
    //  Fit these bounds to the map
    map.fitBounds(bounds);
}



// Sets all markers
function setMarkers(mMap, theMarkers) {
    for (var i = 0; i < theMarkers.length; i++) {
        theMarkers[i].setMap(mMap);
    }
}

// Show all markers
function showMarkers(theMarkers) {
    setMarkers(map, theMarkers);
}

// Hide all markers
function hideMarkers(theMarkers) {
    setMarkers(null, theMarkers);
}

function checkboxAction(option) {
    var theMarkers = [];
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].title == option) {
            theMarkers.push(markers[i]);
        }
    }

    switch (option) {
        case 'high':

            toggleMarkers("#marker_highRisk", theMarkers);
            break;

        case 'mod':
            toggleMarkers("#marker_modRisk", theMarkers);
            break;

        case 'low':
            toggleMarkers("#marker_lowRisk", theMarkers);
            break;

        case 'no':
            toggleMarkers("#marker_noRisk", theMarkers);
            break;

        default: alert("Unknown marker selection");
    }

}

function toggleMarkers(id, theMarkers) {
    if ($(id).is(':checked')) {
        showMarkers(theMarkers);
        AutoCenter(map, theMarkers);
    } else {
        hideMarkers(theMarkers);
    }
}

function setKeypressListener() {
    //var listener = new window.keypress.Listener();
    
    //listener.simple_combo("esc", function () {
    //    if (lastDrawing != null) {
    //        lastDrawing.setMap(null);
    //    }
    //});
    window.onkeyup = function (e) {
        var key = e.keyCode ? e.keyCode : e.which;
        if (key == 27) {
            onEscPressedClear();
        }
    }
}

function onEscPressedClear() {
    // Escape is pressed
    if (lastDrawing != null) {
        lastDrawing.setMap(null);
        selectedMarkers = [];
        selectedRealMarkers = [];
        selectedCPaths = [];
        setPathColorBack();
    }

    // Clear marker selection window
    $('#selectedMarkersWindow').empty();
    $('#selectedMarkersWindow').hide();
    if (previousSelectedMarker != null) {
        previousSelectedMarker.setAnimation(null);
    }
    return false;
}

