function fillForm(res, doc) {

    var r = new Rect(27, 36, 161.9, 20);
    var r2 = new Rect(r.x, r.endy + 5, r.width, r.height + 1);
    var r3 = new Rect(r.x, r2.endy + 5, r.width, r2.height);
    var r4 = new Rect(r.x, r3.endy + 5, r3.width, 28);
    var r5 = new Rect(r.x, r4.endy + 6, r.width, 56);
    var r6 = new Rect(r.x, r5.endy + 9, r.width, 7);
    var r7 = new Rect(r.x, r6.endy + 5, r.width, 8);

    for (var key in res) {
        doc.setFontSize(8);
        if (res.hasOwnProperty(key)) {
            //alert(key + " -> " + response[key]);
            var value = res[key];
            if (value != null) {
                //console.log(key + ": " + value);
                var lk = String(key.toLowerCase());
                
                var mValue = String(value);
                var lv = mValue.toLowerCase();
                if (lk === "water crossing name or id") {
                    waterCrossingID = mValue.trim();
                    doc.text(r.x + 84, r.y+4.5, mValue);
                }else if (lk === "water crossing name") {
                    doc.text(r.x + 30, r.y + 12, mValue);
                }else if (lk === "disposition no.") {
                    doc.text(r.x+109, r.y+12, mValue);
                } else if (lk === "easting") {
                    doc.text(r.x+59+13, r.y+18, mValue);
                } else if (lk === "northing") {
                    doc.text(r.x+100+15,r.y+18, mValue);
                } else if (lk === "stream classification") {
                    if (lv === "ephemeral") {
                        drawRect(r2.x + 35, r2.y + 2.5,doc);
                    } else if (lv.indexOf("non") > -1) {
                        doc.rect(93, 78.5, 2, 2, 'F');
                    } else if (lv === "fluvial - intermittent") {
                        drawRect(r2.x + 2, r2.y + 9.5,doc);
                        drawRect(r2.x + 34, r2.y + 9.5,doc);

                    } else if (lv === "fluvial - small permanent") {
                        drawRect(r2.x + 2, r2.y + 9.5, doc);
                        drawRect(r2.x + 62, r2.y + 9.5, doc);

                    } else if (lv === "fluvial - large permanent") {
                        drawRect(r2.x + 2, r2.y + 9.5, doc);
                        drawRect(r2.x + 99, r2.y + 9.5, doc);
                    }
                }else if (lk === "bankfull width") {
                    var index = mValue.indexOf(".");
                    if(index > -1){
                        var beforeDecimal = mValue.substring(0, index);
                        var afterDecimal = mValue.substring(index + 1, mValue.length);
                        
                        doc.text(r2.x + 23, r2.y + 18.5, beforeDecimal);
                        doc.text(r2.x + 29, r2.y + 18.5, afterDecimal);
                    } else {
                        doc.text(r2.x + 23, r2.y + 18.5, mValue);
                    }
                } else if (lk === "bankfull width measured?") {
                    if (lv === "estimated") {
                        drawRect(r2.x + 69, r2.y + 16.5, doc);
                    } else if (lv === "measured") {
                        drawRect(r2.x + 37, r2.y + 16.5, doc);
                    }
                } else if (lk === "crossing type") {
                    if (lv === "bridge - permanent") {
                        drawRect(r3.x + 29, r3.y + 2.5, doc);

                    } else if (lv === "bridge - temporary") {
                        drawRect(r3.x+68, r3.y+2.5, doc);

                    } else if (lv === "culvert - single") {
                        drawRect(r3.x+107, r3.y+2.5, doc);

                    } else if (lv === "culvert - multiple") {
                        drawRect(r3.x+29, r3.y+9.5, doc);

                    } else if (lv === "culvert - open bottom") {
                        drawRect(r3.x+60, r3.y+9.5, doc);

                    } else if (lv === "log - fill") {
                        drawRect(r3.x+29, r3.y+16.5, doc);

                    } else if (lv === "snow - fill") {
                        drawRect(r3.x+55, r3.y+16.5, doc);

                    } else if (lv === "suspended") {
                        drawRect(r3.x+75, r3.y+16.5, doc);

                    } else if (lv === "reclaimed") {
                        drawRect(r3.x + 105, r3.y + 16.5, doc);

                    }
                } else if (lk === "erosion at site?") {
                    if (lv === "yes") {
                        drawRect(r4.x+27, r4.y+2.5,doc);

                    } else if (lv === "no") {
                        drawRect(r4.x+63, r4.y+2.5,doc);

                    } else if (lv === "pot" || lv === "potential") {
                        drawRect(r4.x+42, r4.y+2.5,doc);

                    }
                } else if (lk === "location of erosion") {
                    if (lv === "inlet") {
                        drawRect(120, r4.y+2.5,doc);
                    } else if (lv === "outlet") {
                        drawRect(135, r4.y+2.5,doc);
                    } else if (lv === "both") {
                        drawRect(150, r4.y+2.5,doc);
                    }
                } else if (lk === "source of erosion") {
                    if (lv === "ditch") {
                        drawRect(r4.x+2, r4.y+13.5,doc);
                    } else if (lv === "bank slump") {
                        drawRect(r4.x+30, r4.y+13.5,doc);
                    } else if (lv === "fill slope") {
                        drawRect(r4.x+60, r4.y+13.5,doc);
                    } else if (lv === "road surface") {
                        drawRect(r4.x+85, r4.y+13.5,doc);
                    } else if (lv === "bridge deck") {
                        drawRect(r4.x+115, r4.y+13.5,doc);
                    } else if (lv === "other") {
                        drawRect(r4.x+2, r4.y+17.5,doc);
                    }
                } else if (lk === "degree of erosion") {
                    if (lv === "low") {
                        drawRect(r4.x+20, r4.y+23.5,doc);
                    } else if (lv === "moderate" || lv.indexOf("high") > -1) {
                        drawRect(r4.x+40, r4.y+23.5,doc);
                    }
                } else if (lk === "area of erosion") {
                    doc.text(r4.x + 112, r4.y + 25.5, mValue);
                } else if (lk === "culvert diameter 1") {
                    doc.text(r4.x + 28, r4.endy + 4, mValue);
                } else if (lk === "culvert diameter 2") {
                    doc.text(r4.x + 48, r4.endy + 4, mValue);
                } else if (lk === "culvert diameter 3") {
                    doc.text(r4.x + 68, r4.endy + 4, mValue);
                } else if (lk === "culvert diameter 4") {
                    doc.text(r4.x + 88, r4.endy + 4, mValue);
                } else if (lk === "blockage") {
                    if (lv === "yes") {
                        drawRect(r5.x + 72, r5.y + 2.5, doc);

                    } else if (lv === "no") {                
                        drawRect(r5.x + 87, r5.y + 2.5, doc);
                    }
                } else if (lk === "culvert substrate") {
                    if (lv === "yes") {
                        drawRect(r5.x+40,r5.y+9.5, doc);
                    } else if (lv === "no") {
                        drawRect(r5.x+55,r5.y+9.5, doc);
                    } else {
                        drawRect(r5.x+70,r5.y+9.5, doc);
                    }
                } else if (lk === "culvert substrate type") {
                    if (lv === "sand") {
                        drawRect(r5.x+30,r5.y+16.5, doc);
                    } else if (lv === "gravel") {
                        drawRect(r5.x+45,r5.y+16.5, doc);
                    }else if (lv === "cobble") {
                        drawRect(r5.x+62,r5.y+16.5, doc);
                    }else if (lv === "boulder") {
                        drawRect(r5.x+80,r5.y+16.5, doc);
                    }else if (lv === "other") {
                        drawRect(r5.x+99,r5.y+16.5, doc);
                    }
                } else if (lk === "for what length of culvert?") {
                    if (lv === "0 - 25") {
                        drawRect(r5.x+40,r5.y+23.5, doc);
                    } else if (lv === "26 - 50") {
                        drawRect(r5.x+65,r5.y+23.5, doc);
                    } else if (lv === "51 - 75") {
                        drawRect(r5.x+80,r5.y+23.5, doc);
                    } else if (lv === "76 - 100") {
                        drawRect(r5.x+95,r5.y+23.5, doc);
                    }
                } else if (lk === "what proportion of back water?") {
                    if (lv === "0") {
                        drawRect(r5.x+50,r5.y+30.5, doc);
                    } else if(lv === "1 - 25"){
                        drawRect(r5.x+65,r5.y+30.5, doc);
                    } else if (lv === "26 - 50") {
                        drawRect(r5.x+80,r5.y+30.5, doc);
                    } else if (lv === "51 - 75") {
                        drawRect(r5.x+95,r5.y+30.5, doc);
                    } else if (lv === "76 - 100") {
                        drawRect(r5.x+110,r5.y+30.5, doc);
                    }
                } else if (lk === "culvert slope") {
                    if (lv === "level, uniform") {
                        drawRect(r5.x+22,r5.y+37.5, doc);
                    } else {
                        drawRect(r5.x + 55, r5.y + 37.5, doc);
                    }
                } else if (lk === "culvert outlet gap") {
                    doc.text(r5.x + 18, r5.y + 46.5, mValue);
                } else if (lk === "culvert outlet type") {
                    if (lv === "embedded") {
                        drawRect(r5.x + 75, r5.y + 44.5, doc);
                    }
                } else if (lk === "culvert pool depth") {
                    doc.text(r5.x + 20, r5.y + 53.5, mValue);
                } else if (lk === "outlet score") {
                    doc.text(r5.x + 45, r5.y + 53.5, mValue);
                } else if (lk === "scour pool present") {
                    if (lv === "yes") {
                        drawRect(r5.x+95,r5.y+51.5,doc);

                    } else if (lv === "no") {
                        drawRect(r5.x+110,r5.y+51.5,doc);

                    }
                    
                } else if (lk === "fish passage concerns") {
                    console.log(lv);
                    if (lv === "yes" || lv === "concerns" || lv === "some concerns") {
                        drawRect(r6.x+30,r6.y+2.5,doc);

                    } else if (lv === "no" || lv === "no concerns") {
                        drawRect(r6.x+2,r6.y+2.5,doc);
                    } else if(lv === "serious" || lv === "serious concerns"){
                        drawRect(r6.x + 65, r6.y + 2.5, doc);
                    }
                } else if (lk === "crew") {
                    doc.text(r7.x + 29, r7.y + 4.5, mValue);
                } else if (lk === "inspection date") {
                    var d = mValue.substring(0, mValue.indexOf("T"));
                    doc.text(r7.x + 85 + 25, r7.y + 4.5, d);
                } else if (lk === "remarks") {
                    doc.text(r7.x, r7.endy + 10, mValue);
                } else if (lk.indexOf("photo") > -1) {
                    doc.setFontSize(6.5);
                    var last = lv.lastIndexOf("\\");
                    if (last > -1) {
                        var photoName = mValue.substring(last + 1, mValue.length);
                        var x = r7.endy + 21;
                        if (lk === "photo inlet upstream") {
                            doc.text(r7.x, x, "Inlet Upstream: " + photoName);
                        } else if (lk === "photo inlet downstream") {
                            doc.text(r7.x, x + 3, "Inlet Downstream: " + photoName);
                        } else if (lk === "photo outlet upstream") {
                            doc.text(r7.x, x + 6, "Outlet Upstream: " + photoName);
                        } else if (lk === "photo outlet downstream") {
                            doc.text(r7.x, x + 9, "Outlet Downstream: " + photoName);
                        } else if (lk === "photo other 1") {
                            doc.text(r7.x, x + 12, "Other: " + photoName);
                        } else if (lk === "photo other 2") {
                            doc.text(r7.x, x + 15, "      " + photoName);
                        }
                    } else {
                        var photoName = mValue;
                        var x = r7.endy + 21;
                        if (lk === "photo inlet upstream") {
                            doc.text(r7.x, x, "Inlet Upstream: " + photoName);
                        } else if (lk === "photo inlet downstream") {
                            doc.text(r7.x, x + 3, "Inlet Downstream: " + photoName);
                        } else if (lk === "photo outlet upstream") {
                            doc.text(r7.x, x + 6, "Outlet Upstream: " + photoName);
                        } else if (lk === "photo outlet downstream") {
                            doc.text(r7.x, x + 9, "Outlet Downstream: " + photoName);
                        } else if (lk === "photo other 1") {
                            doc.text(r7.x, x + 12, "Other: " + photoName);
                        } else if (lk === "photo other 2") {
                            doc.text(r7.x, x + 15, "      " + photoName);
                        }
                    }
                }


                

                
            }
        }

    }
}

function drawRect(x, y, doc) {
    doc.rect(x, y, 2.5, 2.5, 'F');

}