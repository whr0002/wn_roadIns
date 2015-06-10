
function createPDF(Response, doc) {
    //console.log(Response);
    // You'll need to make your image into a Data URL
    // Use http://dataurl.net/#dataurlmaker
    // You'll need to make your image into a Data URL
    // Use http://dataurl.net/#dataurlmaker
    var imageData = 'data:image/jpeg;base64,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';

    doc.setFontSize(11.5);
    doc.setFontType("bold");
    doc.setFontType("normal");
    var bx = 23;
    var by = 13;
    var bendx = 190.9;
    doc.line(bx, by, bendx, by);
    doc.addImage(imageData, "JPEG", bx + 2, by - 10, 52, 9);

    doc.setFontSize(8);
    doc.setFontType("bold");
    doc.text(bendx - 22, by - 5, "Evironment and");
    doc.text(bendx - 49, by - 1, "Sustainable Resource Development");
    doc.setFontSize(12.5);

    doc.text(bendx - 84, by + 9, "Watercourse Crossing Inspection Form")
    doc.text(bendx - 109, by + 18, "Roadway Watercourse Crossing Inspection Manual");
    doc.setFontType("normal");
    // First block

    var r = new Rect(27, 36, 161.9, 20);
    doc.rect(r.x, r.y, r.width, r.height);
    doc.line(r.x, r.y + 8, r.endx, r.y + 8);
    doc.line(r.x, r.y + 14, r.endx, r.y + 14);
    doc.line(r.x + 82, r.y, r.x + 82, r.y + 14)
    doc.line(r.x + 57, r.y + 14, r.x + 57, r.y + 20)
    doc.line(r.x + 98, r.y + 14, r.x + 98, r.y + 20)

    doc.setFontSize(8);
    doc.setFontType("bold");
    doc.text(r.x + 2, r.y + 4, "Water Crossing Name of ID");

    doc.setFontSize(7);
    doc.setFontType("normal");
    doc.text(r.x + 39, r.y + 4, " (ex. # spray painted on or around");
    doc.text(r.x + 2, r.y + 7, "culvert)");

    doc.setFontSize(8);
    doc.setFontType("bold");

    doc.text(r.x + 2, r.y + 12, "Watercourse Name: ");
    doc.text(r.x + 84, r.y + 12, "Disposition No. ");
    doc.text(r.x + 2, r.y + 18, "GPS Co-ordinates (UTM): ");
    doc.text(r.x + 59, r.y + 18, "Easting: ");
    doc.text(r.x + 100, r.y + 18, "Northing: ");


    // Second block
    var r2 = new Rect(r.x, r.endy + 5, r.width, r.height + 1);
    doc.rect(r2.x, r2.y, r2.width, r2.height);
    doc.line(r2.x, r2.y + 7, r2.endx, r2.y + 7);
    doc.line(r2.x, r2.y + 14, r2.endx, r2.y + 14);

    doc.text(r2.x + 2, r2.y + 4.5, "Stream Classification: ");
    generateBoxWithText(r2.x + 35, r2.y + 2.5, "Ephemeral", doc);
    generateBoxWithText(r2.x + 59, r2.y + 2.5, "Non-Fluvial", doc);

    doc.setFontSize(7);
    doc.setFontType("normal");
    doc.text(r2.x + 79, r2.y + 4.5, "(in non-fluvival, omit shaded section)");

    doc.setFontSize(8);
    doc.setFontType("bold");
    generateBoxWithText(r2.x + 2, r2.y + 9.5, "Fluvial &      either: ", doc);
    generateBoxWithText(r2.x + 34, r2.y + 9.5, "Intermittent, or", doc);
    generateBoxWithText(r2.x + 62, r2.y + 9.5, "Permanent - Small, or", doc);
    generateBoxWithText(r2.x + 99, r2.y + 9.5, "Permanent - Large", doc);
    doc.text(r2.x + 2, r2.y + 18.5, "Bankfull width:");
    doc.line(r2.x + 23, r2.y + 19, r2.x + 28, r2.y + 19);
    doc.text(r2.x + 28, r2.y + 18.5, ".");
    doc.line(r2.x + 29, r2.y + 19, r2.x + 32, r2.y + 19);
    doc.text(r2.x + 32, r2.y + 18.5, "m (");
    generateBoxWithText(r2.x + 37, r2.y + 16.5, "measured       or       ", doc);
    doc.setFontType("normal");
    generateBoxWithText(r2.x + 69, r2.y + 16.5, "estimated to nearest metre)", doc);
    doc.setFontType("bold");

    //Third block
    var r3 = new Rect(r.x, r2.endy + 5, r.width, r2.height);

    doc.rect(r3.x, r3.y, r3.width, r3.height);
    doc.line(r3.x + 27, r3.y, r3.x + 27, r3.endy);
    doc.line(r3.x + 27, r3.y + 7, r3.endx, r3.y + 7);
    doc.line(r3.x + 27, r3.y + 14, r3.endx, r3.y + 14);

    doc.text(r3.x + 2, r3.y + 4.5, "Crossing Type:");
    generateBoxWithText(r3.x + 29, r3.y + 2.5, "Bridge - Permanent", doc);
    generateBoxWithText(r3.x + 68, r3.y + 2.5, "Bridge - Temporary", doc);
    generateBoxWithText(r3.x + 107, r3.y + 2.5, "Culvert - Single", doc);

    generateBoxWithText(r3.x + 29, r3.y + 9.5, "Culvert - Multiple", doc);
    generateBoxWithText(r3.x + 60, r3.y + 9.5, "Culvert - Open Bottom", doc);

    generateBoxWithText(r3.x + 29, r3.y + 16.5, "Fill - Log", doc);
    generateBoxWithText(r3.x + 55, r3.y + 16.5, "Ford", doc);
    generateBoxWithText(r3.x + 75, r3.y + 16.5, "Suspended", doc);
    generateBoxWithText(r3.x + 105, r3.y + 16.5, "Reclaimed", doc);


    // Fourth block
    var r4 = new Rect(r.x, r3.endy + 5, r3.width, 28);
    doc.rect(r4.x, r4.y, r4.width, r4.height);
    doc.line(r4.x, r4.y + 7, r4.endx, r4.y + 7);
    doc.line(r4.x, r4.y + 21, r4.endx, r4.y + 21);
    doc.line(r4.x + 82, r4.y, r4.x + 82, r4.y + 7)
    doc.text(r4.x + 2, r4.y + 4.5, "Erosion at site?");
    generateBoxWithText(r4.x + 27, r4.y + 2.5, "Yes", doc);
    generateBoxWithText(r4.x + 42, r4.y + 2.5, "Potential", doc);
    generateBoxWithText(r4.x + 63, r4.y + 2.5, "No", doc);
    generateBoxWithText(120, r4.y + 2.5, "Inlet", doc);
    generateBoxWithText(135, r4.y + 2.5, "Outlet", doc);
    generateBoxWithText(150, r4.y + 2.5, "Both", doc);
    doc.text(r4.x + 2, r4.y + 11.5, "If Yes or Potential, identify source (check all that apply):");
    generateBoxWithText(r4.x + 2, r4.y + 13.5, "Ditch Gully", doc);
    generateBoxWithText(r4.x + 2, r4.y + 17.5, "Other", doc);
    generateBoxWithText(r4.x + 30, r4.y + 13.5, "Bank Slump", doc);
    generateBoxWithText(r4.x + 60, r4.y + 13.5, "Fill Slope", doc);
    generateBoxWithText(r4.x + 85, r4.y + 13.5, "Road Surface", doc);
    generateBoxWithText(r4.x + 115, r4.y + 13.5, "Bridge Deck", doc);
    doc.text(r4.x + 2, r4.y + 25.5, "Extent:");
    generateBoxWithText(r4.x + 20, r4.y + 23.5, "Low", doc);
    generateBoxWithText(r4.x + 40, r4.y + 23.5, "High-unsatisfactory", doc);
    doc.text(r4.x + 80, r4.y + 25.5, "Total Erosion Area (m\xB2)");
    doc.line(r4.x + 112, r4.y + 26, r4.x + 121, r4.y + 26);
    doc.text(r4.x, r4.endy + 4, "Culvert(s) diameter:");
    doc.line(r4.x + 28, r4.endy + 4.5, r4.x + 38, r4.endy + 4.5);
    doc.text(r4.x + 38, r4.endy + 4, "m");
    doc.line(r4.x + 48, r4.endy + 4.5, r4.x + 58, r4.endy + 4.5);
    doc.text(r4.x + 58, r4.endy + 4, "m");
    doc.line(r4.x + 68, r4.endy + 4.5, r4.x + 78, r4.endy + 4.5);
    doc.text(r4.x + 78, r4.endy + 4, "m");
    doc.line(r4.x + 88, r4.endy + 4.5, r4.x + 98, r4.endy + 4.5);
    doc.text(r4.x + 98, r4.endy + 4, "m    (primary)");

    // fifth block
    var r5 = new Rect(r.x, r4.endy + 6, r.width, 56);
    doc.rect(r5.x, r5.y, r5.width, r5.height);
    sevenLines(doc, r5);
    doc.text(r5.x + 2, r5.y + 4.5, "Greater than 10 % of diameter blocked by debris?");
    generateBoxWithText(r5.x + 72, r5.y + 2.5, "Yes", doc);
    generateBoxWithText(r5.x + 87, r5.y + 2.5, "No   (note cause in comments)", doc);

    doc.text(r5.x + 2, r5.y + 11.5, "Substrate in the culvert?");
    generateBoxWithText(r5.x + 40, r5.y + 9.5, "Yes", doc);
    generateBoxWithText(r5.x + 55, r5.y + 9.5, "No", doc);
    generateBoxWithText(r5.x + 70, r5.y + 9.5, "Unknown", doc);

    doc.text(r5.x + 2, r5.y + 18.5, "If yes, what type?");
    generateBoxWithText(r5.x + 30, r5.y + 16.5, "Sand", doc);
    generateBoxWithText(r5.x + 45, r5.y + 16.5, "Gravel", doc);
    generateBoxWithText(r5.x + 62, r5.y + 16.5, "Cobble", doc);
    generateBoxWithText(r5.x + 80, r5.y + 16.5, "Boulder", doc);
    generateBoxWithText(r5.x + 99, r5.y + 16.5, "Other", doc);

    doc.text(r5.x + 2, r5.y + 25.5, "For what length of culvert");
    generateBoxWithText(r5.x + 40, r5.y + 23.5, "25% or less", doc);
    generateBoxWithText(r5.x + 65, r5.y + 23.5, "50%", doc);
    generateBoxWithText(r5.x + 80, r5.y + 23.5, "75%", doc);
    generateBoxWithText(r5.x + 95, r5.y + 23.5, "100%", doc);

    doc.text(r5.x + 2, r5.y + 32.5, "What proportion has backwater?");
    generateBoxWithText(r5.x + 50, r5.y + 30.5, "0%", doc);
    generateBoxWithText(r5.x + 65, r5.y + 30.5, "25%", doc);
    generateBoxWithText(r5.x + 80, r5.y + 30.5, "50%", doc);
    generateBoxWithText(r5.x + 95, r5.y + 30.5, "75%", doc);
    generateBoxWithText(r5.x + 110, r5.y + 30.5, "100%", doc);

    doc.text(r5.x + 2, r5.y + 39.5, "Culvert slope:");
    generateBoxWithText(r5.x + 22, r5.y + 37.5, "Level and Uniform", doc);
    generateBoxWithText(r5.x + 55, r5.y + 37.5, "Slope > or Vertically Bent", doc);

    doc.text(r5.x + 2, r5.y + 46.5, "Outlet Gap:");
    doc.line(r5.x + 18, r5.y + 47, r5.x + 25.5, r5.y + 47);
    doc.text(r5.x + 25.5, r5.y + 46.5, "m");
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(r5.x + 28.5, r5.y + 46.5, "(for lowest, if more than one culvert)");
    doc.setFontType("bold");
    doc.setFontSize(8);
    generateBoxWithText(r5.x + 75, r5.y + 44.5, "Embedded", doc);

    doc.text(r5.x + 2, r5.y + 53.5, "+Pool Depth:");
    doc.line(r5.x + 20, r5.y + 54, r5.x + 30, r5.y + 54);
    doc.text(r5.x + 30, r5.y + 53.5, "m = Score:");
    doc.line(r5.x + 45, r5.y + 54, r5.x + 59, r5.y + 54);
    doc.text(r5.x + 64, r5.y + 53.5, "Scour pool apparent?");
    generateBoxWithText(r5.x + 95, r5.y + 51.5, "Yes", doc);
    generateBoxWithText(r5.x + 110, r5.y + 51.5, "No", doc);

    doc.text(r5.x, r5.endy + 7, "Fish Passage Assessment (use 10.1: Fish Passage Evaluation Criteria for Culvert Stream Crossings)")

    // Sixth block
    var r6 = new Rect(r.x, r5.endy + 9, r.width, 7);
    doc.rect(r6.x, r6.y, r6.width, r6.height);
    generateBoxWithText(r6.x + 2, r6.y + 2.5, "No Concerns", doc);
    generateBoxWithText(r6.x + 30, r6.y + 2.5, "Some Concerns", doc);
    generateBoxWithText(r6.x + 65, r6.y + 2.5, "Serious Concerns", doc);

    // Seventh block
    var r7 = new Rect(r.x, r6.endy + 5, r.width, 8);
    doc.rect(r7.x, r7.y, r7.width, r7.height);
    doc.line(r7.x + 83, r7.y, r7.x + 83, r7.y + 8);
    doc.text(r7.x + 2, r7.y + 4.5, "Inspector's Name:");
    doc.text(r7.x + 85, r7.y + 4.5, "Inspection Date:");
    // Comments
    doc.text(r7.x, r7.endy + 7, "Comments: (if photos taken of inlet and outlet, please record image numbers)");
    var bby = 270.4;
    doc.line(bx, bby, bendx - 2, bby);
    doc.setFontSize(6.5);
    doc.setFontType("normal");
    doc.text(bx + 2, bby + 3, "Mar 13, 2015");
    doc.text(bx + 57, bby + 3, "Watercourse Crossing Inspection Form");
    doc.text(bx + 61, bby + 6, "\xa9 2015 Government of Alberta");
    doc.text(bendx - 15, bby + 3, "Page 1 of 1");

    fillForm(Response, doc);

}

function sevenLines(theDoc, r) {
    var j = 7;
    for (var i = 0; i < 7; i++) {
        var v = r.y + j * (i + 1);
        theDoc.line(r.x, v, r.endx, v);
    }

}

function generateBoxWithText(x, y, text, theDoc) {
    theDoc.rect(x, y, 2.5, 2.5);
    theDoc.text(x + 4, y + 2.25, text);
}

function Rect(x, y, width, height) {
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.endx = x + width;
    this.endy = y + height;
}



function getFormData() {

    if (currentLatLong["Latitude"] != null) {
        actionUrl = "/Data/onerow/" + currentLatLong["Latitude"] + "/" + currentLatLong["Longtitude"]+"/";
        console.log(actionUrl);
        $.getJSON(actionUrl, createPDF);
    }
    
}

function postPositions() {
    if(selectedMarkers.length > 0){
        var json = "[";
        for (i = 0; i < selectedMarkers.length; i++) {
            //var lats, longs;

            //lats = selectedMarkers[i].Latitude;
            //longs = selectedMarkers[i].Longitude;
            if (i == 0) {
                json += '{"ID":"' + selectedMarkers[i].ID + '"}';
                //json += '{"Latitude":"' + lats + '" , "Longitude": "' + longs + '"}';
            } else {
                json += ',{"ID":"' + selectedMarkers[i].ID + '"}';
                //json += ',{"Latitude":"' + lats + '" , "Longitude": "' + longs + '"}';
            }

        }

        json += "]";
        //console.log(json);
        $('#processingIndicator').show();
        $.ajax({
            type: 'POST',
            url: '/Data/PostPositions',
            traditional: true,
            success: onGetDataSuccess,
            data: {data: json},
        });
    } else {
        alert("Please select at least one marker");
    }

}


function sevenLines(theDoc, r) {
    var j = 7;
    for (var i = 0; i < 7; i++) {
        var v = r.y + j * (i + 1);
        theDoc.line(r.x, v, r.endx, v);
    }

}

function generateBoxWithText(x, y, text, theDoc) {
    theDoc.rect(x, y, 2.5, 2.5);
    theDoc.text(x + 4, y + 2.25, text);
}

function Rect(x, y, width, height) {
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.endx = x + width;
    this.endy = y + height;
}

var waterCrossingID = "form";
function onGetDataSuccess(response) {
    //console.log(getPDFOption());
    
    var data = JSON.parse(response);
    if(getPDFOption() === "single"){
        var doc = new jsPDF('p', 'mm', [279.4, 215.9]);
        

        for (var i = 0; i < data.length; i++) {

            createPDF(data[i], doc);

            if (i != (data.length - 1)) {
                doc.addPage();
            }
        }

        $('#processingIndicator').hide();
        doc.save("inspection form.pdf");
    } else {
        //console.log("Multiple");
        for (var i = 0; i < data.length; i++) {
            var doc = new jsPDF('p', 'mm', [279.4, 215.9]);
            createPDF(data[i], doc);


            doc.save(waterCrossingID+".pdf");
            
        }
        $('#processingIndicator').hide();
        
    }
}

function getPDFOption() {
    return $('input[name=pdf_download_option]:checked').val();
}