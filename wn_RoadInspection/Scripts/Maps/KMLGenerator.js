function generateKML() {
    mWindow = window.open("data:text/xml," + encodeURIComponent(buildRoadXML()),
        "_blank", "width=800,height=800");

    mWindow.focus();
}

// XML writer with attributes and smart attribute quote escaping 
function element(name,content,attributes){
    var att_str = ''
    if (attributes) { // tests false if this arg is missing!
        att_str = formatAttributes(attributes)
    }
    var xml
    if (!content){
        xml='<' + name + att_str + '/>'
    }
    else {
        xml='<' + name + att_str + '>' + content + '</'+name+'>'
    }
    return xml
}
var APOS = "'"; QUOTE = '"'
var ESCAPED_QUOTE = {  }
ESCAPED_QUOTE[QUOTE] = '&quot;'
ESCAPED_QUOTE[APOS] = '&apos;'
   
/*
   Format a dictionary of attributes into a string suitable
   for inserting into the start tag of an element.  Be smart
   about escaping embedded quotes in the attribute values.
*/
function formatAttributes(attributes) {
    var att_value
    var apos_pos, quot_pos
    var use_quote, escape, quote_to_escape
    var att_str
    var re
    var result = ''
   
    for (var att in attributes) {
        att_value = attributes[att]
        
        // Find first quote marks if any
        apos_pos = att_value.indexOf(APOS)
        quot_pos = att_value.indexOf(QUOTE)
       
        // Determine which quote type to use around 
        // the attribute value
        if (apos_pos ===  -1 && quot_pos === -1) {
            att_str = ' ' + att + "='" + att_value +  "'"
            result += att_str
            continue
        }
        
        // Prefer the single quote unless forced to use double
        if (quot_pos != -1 && quot_pos < apos_pos) {
            use_quote = APOS
        }
        else {
            use_quote = QUOTE
        }
   
        // Figure out which kind of quote to escape
        // Use nice dictionary instead of yucky if-else nests
        escape = ESCAPED_QUOTE[use_quote]
        
        // Escape only the right kind of quote
        re = new RegExp(use_quote,'g')
        att_str = ' ' + att + '=' + use_quote + 
            att_value.replace(re, escape) + use_quote
        result += att_str
    }
    return result
}
function test() {   
    var atts = {att1:"a1", 
        att2:"This is in \"double quotes\" and this is " +
         "in 'single quotes'",
        att3:"This is in 'single quotes' and this is in " +
         "\"double quotes\""}
    
    // Basic XML example
    alert(element('elem','This is a test'))
   
    // Nested elements
    var xml = element('p', 'This is ' + 
    element('strong','Bold Text') + 'inline')
    alert(xml)
   
    // Attributes with all kinds of embedded quotes
    alert(element('elem','This is a test', atts))
   
    // Empty element version
    alert(element('elem','', atts))    
}


function buildRoadXML() {
    var result = [];
    result.push("<?xml version='1.0' encoding='UTF-8'?>");
    result.push(element('kml',
        element('Document', 
            buildPlacemarks()
        ),
            { xmlns: "http://www.opengis.net/kml/2.2" }));

    return result.join(' ');
}

function buildPlacemarks() {
    console.log("Selected Locations: " + selectedCPaths.length);
    var placemarks = [];
    for (var i = 0; i < selectedCPaths.length; i++) {

        if(selectedCPaths[i].LocationList.length > 1){
            placemarks.push(element('Placemark',
                element('styleUrl', "#line-style-1")+
                element('name', "ID: "+selectedCPaths[i].ID)+
                element('description', "This path is created on " + selectedCPaths[i].Date) +
                element('LineString',
                    element('coordinates', selectedCPaths[i].Path))
                , { ID: "" + selectedCPaths[i].ID }));
        } else if (selectedCPaths[i].LocationList.length === 1) {
            placemarks.push(element('Placemark',
                element('styleUrl', "#line-style-1") +
                element('name', "ID: " + selectedCPaths[i].ID) +
                element('description', "This path is created on " + selectedCPaths[i].Date) +
                element('Point',
                    element('coordinates', selectedCPaths[i].Path))
                , { ID: "" + selectedCPaths[i].ID }));
        }
    }

    placemarks.push(element('Style', element('LineStyle',
        element('color', "FF00ffff") + element('width', 5))
        , { id: 'line-style-1' }));

    return placemarks.join(' ');
}

function locationsToString(locationList) {
    var result = "";
    for (var i = 0; i < locationList.length; i++) {
        result += locationList[i].Longitude + "," + locationList[i].Latitude + "," + 0 + " ";
    }

    return result;
}