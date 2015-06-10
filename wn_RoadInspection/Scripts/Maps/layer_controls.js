var layers = [];
var urls = [];
//var urls = ["http://woodlandsnorth.azurewebsites.net/Content/KML/Crossings.kml",
//    "http://woodlandsnorth.azurewebsites.net/Content/KML/Dispositions.kmz",
//    "http://woodlandsnorth.azurewebsites.net/Content/KML/Rd2.kmz",
//    "http://woodlandsnorth.azurewebsites.net/Content/KML/ext.kmz",
//    "http://woodlandsnorth.azurewebsites.net/Content/KML/GoogleEarth_SCARI_CNRL.kmz"];

function setLayers(id, url){

    var sid = "#layer_" + id;
    var ctaLayer = new google.maps.KmlLayer({
        url: url,
        title: sid
    });
    layers.push(ctaLayer);

}

function layerController(id) {
    var theLayer;
    for (var i = 0; i < layers.length; i++) {
        if (layers[i].title == id) {
            theLayer = layers[i];
        }
    }

    //switch (id) {
    //    case '#layer0':
    //        toggleLayers(id, theLayer);
    //        break;
    //    default: alert("Unknown kml file");
    //}
    toggleLayers(id, theLayer);

}

function toggleLayers(id, theLayer) {
    if ($(id).is(':checked')) {

        // Show layer
        theLayer.setMap(map);

    } else {
        // Hide layer
        theLayer.setMap(null);

    }
}