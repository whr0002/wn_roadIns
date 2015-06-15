const COLOR_SELECTED = "#97e41b";
const COLOR_UNSELECTED = "#FFFF00";
var lastPolyline = null;
var selectedCPaths = [];


function selectPolylines(drawingManager, event) {
    selectedCPaths = [];
    if (lastDrawing != null) {
        lastDrawing.setMap(null);
    }

    if (lastPolyline != null) {
        setPathColorBack();
        //lastPolyline.setOptions({ strokeColor: COLOR_UNSELECTED });
    }

    lastDrawing = event.overlay;
    lastDrawing.type = event.type;

    if (event.type == google.maps.drawing.OverlayType.POLYGON) {
        // Drawing is polygon

        addToSelectedPolylines(null, event.overlay);


        lastDrawing.getPaths().forEach(function (path, index) {
            google.maps.event.addListener(path, 'insert_at', function () {
                // New point
                selectedPolylineChanged(drawingManager, event);
            });

            google.maps.event.addListener(path, 'remove_at', function () {
                // Point was removed
                selectedPolylineChanged(drawingManager, event);
            });

            google.maps.event.addListener(path, 'set_at', function () {
                // Point was moved
                selectedPolylineChanged(drawingManager, event);
            });

        });


    } else {

        var bounds = event.overlay.getBounds();

        addToSelectedPolylines(bounds, null);

        google.maps.event.addListener(lastDrawing, "bounds_changed", function () {
            selectedPolylineChanged(drawingManager, event);
        });

    }

    console.log("# Path: " + selectedCPaths.length);
    // Cancel drawing mode
    drawingManager.setDrawingMode(null);

}

function selectedPolylineChanged(drawingManager, event) {
    // Clear
    selectedCPaths = [];
    setPathColorBack();

    if (event.type == google.maps.drawing.OverlayType.POLYGON) {

        addToSelectedPolylines(null, event.overlay);

    } else {
        var bounds = event.overlay.getBounds();
        addToSelectedPolylines(bounds, null);
    }

    console.log("# Path: " + selectedCPaths.length);
    // Cancel drawing mode
    drawingManager.setDrawingMode(null);
}



function addToSelectedPolylines(bounds, overlay) {

    for (var i = 0; i < allCPaths.length; i++) {

        var coords = allCPaths[i].LocationList;

        for (var k = 0; k < coords.length; k++) {

            // Loop through every coords of a polyline, check if it is in selected area
            if (bounds != null) {
                if (bounds.contains(coords[k].LatLng)){
                    // it is in, push it in selected list and change color
                    selectedCPaths.push(allCPaths[i]);

                    allCPaths[i].PolyLine.setOptions({ strokeColor: COLOR_SELECTED });

                    break;
                }
            } else if (overlay != null) {
                // Loop through every coords of a polyline, check if it is in selected area
                if (google.maps.geometry.poly.containsLocation(coords[k].LatLng, overlay)) {
                    // it is in, push it in selected list and change color
                    selectedCPaths.push(allCPaths[i]);

                    allCPaths[i].PolyLine.setOptions({ strokeColor: COLOR_SELECTED });
                    break;

                }
            }
        }
    }

    generateIds();


}

function generateIds() {
    $("#showonnewtab").empty();
    $("#showonnewtab").append(
'<input type="hidden" name="ids" value="' + getPathPositions() + '"/><input type="submit" value="Show on new tab" class="btn btn-primary">');
}

function bindPolylineInfoWindow(cpath, map, infowindow, json) {
    google.maps.event.addListener(cpath.PolyLine, "click", function (e) {
        // Get detail data
        getOneLocation(cpath.ID);

        // Polyline is clicked, Clear
        selectedCPaths = [];
        selectedCPaths.push(cpath);

        var polyline = cpath.PolyLine;

        if (lastPolyline != null) {
            setPathColorBack();
            //lastPolyline.setOptions({ strokeColor: cpath.Color });
        }

        lastPolyline = polyline;
        polyline.setOptions({ strokeColor: COLOR_SELECTED });

        
        $('#selectedMarkersWindow').hide();
        if (lastDrawing != null) {
            lastDrawing.setMap(null);
        }

        if (lastInfoWindow != null) {
            // Close last openned window
            lastInfoWindow.close();
        }


        lastInfoWindow = infowindow;
        var date = convertMilliToDate(json.Date);
        infowindow.setContent("<table><tr><td>ID </td><td>" + json.ID + "</td></tr><tr><td>Date </td><td>" + date + "</td></tr></table>");

        infowindow.setPosition(getMidLatLng(cpath));
        infowindow.open(map, this);
    });
}


function getMidLatLng(CPath) {
    var list = CPath.LocationList;
    if (list.length > 0) {
        
        return list[Math.floor(list.length / 2)].LatLng;
    }

}


function autoCenterPolylines(map, plines) {
    var bounds = new google.maps.LatLngBounds();

    $.each(plines, function (index, line) {
        if (line.LocationList.length > 0)
            bounds.extend(line.LocationList[0].LatLng);
    });

    map.fitBounds(bounds);
}

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

function setPathColorBack() {
    for (var i = 0; i < allCPaths.length; i++) {
        allCPaths[i].PolyLine.setOptions({ strokeColor: allCPaths[i].Color });
    }
}

function getPathPositions() {


        var json = "";
        for (i = 0; i < selectedCPaths.length; i++) {
            //var lats, longs;

            //lats = selectedMarkers[i].Latitude;
            //longs = selectedMarkers[i].Longitude;
            if (i == 0) {
                json += selectedCPaths[i].ID;
                //json += '{"Latitude":"' + lats + '" , "Longitude": "' + longs + '"}';
            } else {
                json += ',' + selectedCPaths[i].ID;
                //json += ',{"Latitude":"' + lats + '" , "Longitude": "' + longs + '"}';
            }

        }

        console.log(json);
        
        return json;
        //$.ajax({
        //    type: 'POST',
        //    url: '/Data/PostPositions',
        //    traditional: true,
        //    success: onGetDataSuccess,
        //    data: { data: json },
        //});

        //$("#pdfBatch").attr("href", "/Data/showonnewtab/" + json);
    
}

