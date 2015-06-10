function toolbarControls() {
    $('#toolPanel').toggleClass("mNone");
    $('#googleMap').toggleClass("mFullWidth");
    google.maps.event.trigger(map, 'resize');
}