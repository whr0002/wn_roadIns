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
var map;
var CMarker = function (id, lat, lng) {
    this.ID = id;
    this.Latitude = lat;
    this.Longitude = lng;
    this.LatLng = new google.maps.LatLng(this.Latitude, this.Longitude);
}
var CMarkers = [];
var shouldAutoCenter = true;
function showMarkersOnMap(response) {
    $('#processingIndicator').hide();
    if (response != null) {
        // Listen to 'Escape' key press
        setKeypressListener();
        // Clear data
        clearMarkers();       
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
                doSelection(drawingManager, event);
            });
        }
        


        for (var i = 0; i < response.length; i++) {
            // Loop through data and set them up on map
            //$('#coordList').append("<li>" + response[i].Latitude + " " + response[i].Longitude + "</li>");

            // Set up marker options according to the latitude and longitude
            var markerOptions = {
                position: new google.maps.LatLng(response[i].Latitude, response[i].Longitude),
                icon: '/Content/Images/marker_green.png'
            };

            // Give a color to markers according to their risk level
            //if (response[i].Risk.toLowerCase() == "no") {
            //    markerOptions.icon = '/Content/Images/marker_grey.png';
            //    markerOptions.title = "no";
            //} else if (response[i].Risk.toLowerCase() == "low") {
            //    markerOptions.icon = '/Content/Images/marker_green.png';
            //    markerOptions.title = "low";
            //} else if (response[i].Risk.toLowerCase() == "mod") {
            //    markerOptions.icon = '/Content/Images/marker_orange.png';
            //    markerOptions.title = "mod";
            //} else if (response[i].Risk.toLowerCase() == "high") {
            //    markerOptions.title = "high";
            //} else {
            //    markerOptions.icon = '/Content/Images/marker_blue.png';
            //    markerOptions.title = "Unknown";
            //}
            // Create a marker
            var marker = new google.maps.Marker(markerOptions);
            var cMarker = new CMarker(response[i].ID, response[i].Latitude, response[i].Longitude);

            // Add marker to array
            markers.push(marker);
            CMarkers.push(cMarker);

            //var mapLabel = new MapLabel({
            //    text: i,
            //    position: markerOptions.position,
            //    map: map,
            //    fontSize: 16,
            //    align: 'center'
            //});

            ////mapLabel.set('position', markerOptions.position);
            //marker.bindTo('map', mapLabel);
            //marker.bindTo('position', mapLabel);
            //mapLabel.setMap(null);

            // Set marker on map
            marker.setMap(map);

            // Set up pop up windows
            var info = response[i].Risk;
            var infoWindowOptions = { content: 'Empty' };
            var infoWindow = new google.maps.InfoWindow(infoWindowOptions);
            bindInfoWindow(marker, map, infoWindow, response[i]);


        }

        if (shouldAutoCenter){
        // Auto center the map according to markers
            AutoCenter(map, markers);
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
            infowindow.setContent("<table><tr><td>Client</td><td>"+json.Client+"</td></tr><tr><td>Risk</td><td>" + json.Risk + "</td></tr><tr><td>Latitude</td><td>"
                + json.Latitude + "</td></tr><tr><td>Longitude</td><td>" + json.Longitude + "</td></tr></table>");
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
            $("#downloadDiv").append('<a href="/desktopreviews/edit/' + response["Part1"]["DesktopReviewID"] + '" class="btn btn-primary">Edit</a>');


            // Got data, Display it
            var images = new Object();
            var i = 1;
            extractJSON(i, response);


            // Display images
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
        for (var key in theImages) {
            if(theImages.hasOwnProperty(key)){
                if(theImages[key].length > 0){
                    $("#imageList").append('<span>'+ key +'</span><img src="/Content/Photos/' + theImages[key] + '" class="img-responsive"><br />');
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
        }

        // Clear marker selection window
        $('#selectedMarkersWindow').empty();
        $('#selectedMarkersWindow').hide();
        if (previousSelectedMarker != null) {
            previousSelectedMarker.setAnimation(null);
        }
        return false;
    }
    google.maps.event.addDomListener(window, 'load', getCoords);
