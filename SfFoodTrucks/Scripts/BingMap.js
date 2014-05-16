var bingCreds = "Av1LPz3-lukZMdQTm3HzUzRuBaj_tdmZ6XcFQ7D7Cq8xh7JRzLKg3lxUp0cguHJ-";
var infoboxLayer = null;
var map = null;
var searchLocation = null;

function LoadMap() {
    Microsoft.Maps.loadModule('Microsoft.Maps.Themes.BingTheme', { callback: themesModuleLoaded });
}

function themesModuleLoaded() {
    map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
                      {
                          credentials: bingCreds,
                          mapTypeId: Microsoft.Maps.MapTypeId.road,
                          showMapTypeSelector:false,
                          zoom: 11
                      });

    var geoLocationProvider = new Microsoft.Maps.GeoLocationProvider(map);
    geoLocationProvider.getCurrentPosition();
}

function ClickGeocode(credentials) {
    map.entities.clear();
    map.getCredentials(MakeGeocodeRequest);
}

function MakeGeocodeRequest(credentials) {
    var geocodeRequest = "http://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURI(document.getElementById('txtQuery').value) + "&output=json&jsonp=GeocodeCallback&key=" + credentials;

    CallRestService(geocodeRequest);
}

function CallRestService(request) {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", request);
    document.body.appendChild(script);
}

function GeocodeCallback(result) {

    if (result &&
           result.resourceSets &&
           result.resourceSets.length > 0 &&
           result.resourceSets[0].resources &&
           result.resourceSets[0].resources.length > 0) {

        // Set the map view using the returned bounding box
        var bbox = result.resourceSets[0].resources[0].bbox;
        var viewBoundaries = Microsoft.Maps.LocationRect.fromLocations(new Microsoft.Maps.Location(bbox[0], bbox[1]), new Microsoft.Maps.Location(bbox[2], bbox[3]));
        map.setView({ bounds: viewBoundaries });

        // Add a pushpin at the found location
        searchLocation = new Microsoft.Maps.Location(result.resourceSets[0].resources[0].point.coordinates[0], result.resourceSets[0].resources[0].point.coordinates[1]);
        var pushpinOptions = { draggable: false };

        // Add a pin to the map
        var pin = new Microsoft.Maps.Pushpin(searchLocation, pushpinOptions);
        map.entities.push(pin);
        GetLoc(map);
    }

    function WriteResponse(data, map) {
        var pushpinOptions = { icon: '../Content/GreenPushpin.png' };
        var boundaries = new Array();
        infoboxLayer = new Microsoft.Maps.EntityCollection();
        boundaries.push(searchLocation);
        for (var i = 0; i < data.length; i++) {
            var newLoc = new Microsoft.Maps.Location(data[i].latitude, data[i].longitude);
            boundaries.push(newLoc);
            var pushpin = new Microsoft.Maps.Pushpin(newLoc, pushpinOptions);

            // Create the infobox for the pushpin
            var pinInfobox = new Microsoft.Maps.Infobox(newLoc,
                {
                    width: 150,
                    height: 100,
                    title: "<a href=\"" + data[i].schedule + "\">" + data[i].applicant.substr(0,30) + "</a>",
                    description: data[i].address,
                    visible: false,
                    offset: new Microsoft.Maps.Point(0, 15),
                });

            // Add handler for the pushpin click event.
            Microsoft.Maps.Events.addHandler(pushpin, 'click', displayInfobox);
            Microsoft.Maps.Events.addHandler(pushpin, 'mouseover', displayInfobox);

            map.entities.push(pushpin);
            infoboxLayer.push(pinInfobox);
        }

        map.entities.push(infoboxLayer);
        var viewBoundaries = Microsoft.Maps.LocationRect.fromLocations(boundaries);
        map.setView({ bounds: viewBoundaries });
    }

    function GetLoc(map) {
        jQuery.support.cors = true;
        $.ajax({
            url: document.location.protocol + "//" + document.location.host + "/v1/FoodTruck?latitude=" + searchLocation.latitude + "&longitude=" + searchLocation.longitude,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                WriteResponse(data, map);
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }

    function GetEntityByLocation(collection, propertyValue) {
        var len = collection.getLength(), entity;

        for (var i = 0; i < len; i++) {
            entity = collection.get(i);

            if (entity.getLocation() == propertyValue) {
                return entity;
            }
        }

        return null;
    }

    function displayInfobox(e) {
        if (e.targetType == "pushpin") {
            var loc = e.target.getLocation();
            if (infoboxLayer != null) {                
                var pinInfobox = GetEntityByLocation(infoboxLayer, loc);

                //A buffer limit to use to specify the infobox must be away from the edges of the map.
                var buffer = 25;

                var infoboxOffset = pinInfobox.getOffset();
                var infoboxAnchor = pinInfobox.getAnchor();
                var infoboxLocation = map.tryLocationToPixel(e.target.getLocation(), Microsoft.Maps.PixelReference.control);

                var dx = infoboxLocation.x + infoboxOffset.x - infoboxAnchor.x;
                var dy = infoboxLocation.y - 25 - infoboxAnchor.y;

                if (dy < buffer) {    //Infobox overlaps with top of map.
                    //Offset in opposite direction.
                    dy *= -1;

                    //add buffer from the top edge of the map.
                    dy += buffer;
                } else {
                    //If dy is greater than zero than it does not overlap.
                    dy = 0;
                }

                if (dx < buffer) {    //Check to see if overlapping with left side of map.
                    //Offset in opposite direction.
                    dx *= -1;

                    //add a buffer from the left edge of the map.
                    dx += buffer;
                } else {              //Check to see if overlapping with right side of map.
                    dx = map.getWidth() - infoboxLocation.x + infoboxAnchor.x - pinInfobox.getWidth();

                    //If dx is greater than zero then it does not overlap.
                    if (dx > buffer) {
                        dx = 0;
                    } else {
                        //add a buffer from the right edge of the map.
                        dx -= buffer;
                    }
                }

                //Adjust the map so infobox is in view
                if (dx != 0 || dy != 0) {
                    map.setView({ centerOffset: new Microsoft.Maps.Point(dx, dy), center: map.getCenter() });
                }
            }


            pinInfobox.setOptions({ visible: true });
            HideAllInfoboxButLocation(loc);
        }
    }


    function HideAllInfoboxButLocation(loc)
    {
        var len = infoboxLayer.getLength(), entity;

        for (var i = 0; i < len; i++) {
            entity = infoboxLayer.get(i);

            if (entity.getLocation() != loc) {
                entity.setOptions({ visible: false });;
            }
        }
    }

    function hideInfobox(e) {
        var loc = e.target.getLocation();
        if (infoboxLayer != null) {
            var infoBox = GetEntityByLocation(infoboxLayer, loc);
            infoBox.setOptions({ visible: false });
        }
    }
}