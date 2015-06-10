window.onload = function () {
    $('#SearchValueDiv').hide();
    $('#SearchSubmit').hide();
    $('#DatepickersDiv').hide();
    $('#CrossingIDDiv').hide();

    // Search Type change event
    $('#SearchType').change(function () {
        // Clear secondary selection first
        $('#SearchValue').empty();
        $('#SearchSubmit').hide();
        $('#SearchValueDiv').hide();
        $('#DatepickersDiv').hide();
        $('#CrossingIDDiv').hide();

        var st = $('#SearchType').val();
        if(st === "Date"){
            // Search Date, show two datepicker
            $('#DatepickersDiv').show();

        } else if (st === "Crossing ID") {
            // Search Crossing ID, show input fields
            $('#CrossingIDDiv').show();
            $('#SearchSubmit').show();

        }else {
            // Search types other than Date
            var url = "/Data/SearchType/" + encodeURIComponent($('#SearchType').val());

            $.getJSON(url, function (data) {
                // Fill in selection data
                var options = "<option>Select ...</option>";

                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i] + '">' + data[i] + "</option>";
                }

                // Show secondary selection
                $('#SearchValue').append(options);
                $('#SearchValueDiv').show();
            });
        }

        

    });

    $('#SearchValue').change(function () {
        $('#SearchSubmit').show();
    });

    $('#to').change(function () {
        $('#SearchSubmit').show();
    });





};


function sendQuery() {
    
    $('#processingIndicator').show();
    var st = $('#SearchType').val();
    if (st === "Date") {
        var from = $('#from').val();
        var to = $('#to').val();

        var url = "data/searchdate/" + encodeURIComponent(st)
            + "/" + encodeURIComponent(from)
            + "/" + encodeURIComponent(to)+"/";
        $.getJSON(url, showMarkersOnMap);
    } else if (st === "Crossing ID") {
        var data = $('#crossing_input_0').val()
            + "-" + $('#crossing_input_1').val()
            + "-" + $('#crossing_input_2').val()
            + "-" + $('#crossing_input_3').val()
            + " " + $('#crossing_input_4').val();
        console.log(data);
        var url = "data/searchCrossingID?q=" + data;
        $.getJSON(url, onQuerySuccess);
        

    }else{
        var sv = $('#SearchValue').val();
        var url = "/data/search/" + encodeURIComponent(st) + "/"+encodeURIComponent(sv)+"/";
    
        $.getJSON(url, showMarkersOnMap);
    } 
}



function toggleAdvSearch() {
    if ($('#AdvSearchSection').is(":hidden")) {
        $('#AdvSearchSection').slideDown();
        $('#SingleSearchSection').hide();
    } else {
        $('#AdvSearchSection').slideUp();
        $('#SingleSearchSection').show();
    }
}

function reset() {
    getCoords();
}

var numOfSearchTypes = 0;
function AdvancedSearch() {
    var searchObjects = [];
    for (var i = 0; i < numOfSearchTypes; i++) {
        var key = $('#advSearchKey' + i).text().trim();
        var value1 = $('#advSearchValue' + i).val();
        //console.log(value1);
        var so;
        if (key != null) {
            if(key != "Date"){
                // For fields except Date, build SearchObject
                if (value1 != null && value1.indexOf("Select") == -1) {
                    // Value does not contain "Select"
                    so = new SearchObject(key, value1);
                    searchObjects.push(so);
                }
            } else {
                // Get date values
                value1 = $('#advFrom').val();
                var value2 = $('#advTo').val();

                if (value1 != "" || value2 != "") {
                    so = new SearchObject(key, value1, value2);
                    searchObjects.push(so);
                }

            }
        }
    }

    if (searchObjects != null && searchObjects.length > 0) {
        // Send data to server
        var json = JSON.stringify(searchObjects);
        //console.log(json);
        //$.getJSON("/Data/AdvSearch", json, showMarkersOnMap);
        //console.log("Pre-post: ", json);
        $.ajax({
            type: 'POST',
            url: '/Data/AdvSearch',
            success: showMarkersOnMap,
            data: {data : json}
        });
    }

}

function onQuerySuccess(response) {
    console.log(response);
}

function SearchObject(key, value1, value2) {
    this.key = key;
    this.value1 = value1;
    this.value2 = value2;
}