function LoadMap() {
    Microsoft.Maps.loadModule('Microsoft.Maps.Themes.BingTheme', { callback: themesModuleLoaded });
}



function themesModuleLoaded() {
    var map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
                      {
                          credentials: "Av1LPz3-lukZMdQTm3HzUzRuBaj_tdmZ6XcFQ7D7Cq8xh7JRzLKg3lxUp0cguHJ-",
                          //center: new Microsoft.Maps.Location(37.75733, -122.44628),
                          mapTypeId: Microsoft.Maps.MapTypeId.road,
                          zoom: 11
                      });

    
    //map.entities.clear();
    var geoLocationProvider = new Microsoft.Maps.GeoLocationProvider(map);
    geoLocationProvider.getCurrentPosition();

    // Retrieve the location of the map center 
    //var center = map.getCenter();

    //var pushpinOptions = { draggable: true };

    // Add a pin to the center of the map
    //var pin = new Microsoft.Maps.Pushpin(currPos, pushpinOptions);
    //Microsoft.Maps.Events.addHandler(pin, 'drag', ondragDetails);
    //map.entities.push(pin);
    

    // Create the infobox for the pushpin
    //pinInfobox = new Microsoft.Maps.Infobox(pin.getLocation(),
    //    {
    //        title: 'My Pushpin',
    //        description: 'This pushpin is located at (0,0).',
    //        visible: false,
    //        offset: new Microsoft.Maps.Point(0, 15),
    //    });

    // Add handler for the pushpin click event.
    //Microsoft.Maps.Events.addHandler(pin, 'click', displayInfobox);

    // Hide the infobox when the map is moved.
    //Microsoft.Maps.Events.addHandler(map, 'mouseout', hideInfobox);


    // Add the pushpin and infobox to the map
    //map.entities.push(pinInfobox);
}

function ondragDetails(e) {
    //pinInfobox.setOptions({ visible: true });
    //hideInfobox(e);
}

function displayInfobox(e) {
    pinInfobox.setOptions({ visible: true });
}

function hideInfobox(e) {
    pinInfobox.setOptions({ visible: false });
}

function ClickGeocode(credentials) {
    var map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
                     {
                         credentials: "Av1LPz3-lukZMdQTm3HzUzRuBaj_tdmZ6XcFQ7D7Cq8xh7JRzLKg3lxUp0cguHJ-"
                     });

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
    //alert("Found location: " + result.resourceSets[0].resources[0].name);

    var map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
                     {
                         credentials: "Av1LPz3-lukZMdQTm3HzUzRuBaj_tdmZ6XcFQ7D7Cq8xh7JRzLKg3lxUp0cguHJ-"
                     });
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
        var location = new Microsoft.Maps.Location(result.resourceSets[0].resources[0].point.coordinates[0], result.resourceSets[0].resources[0].point.coordinates[1]);
        var pushpinOptions = { draggable: true };

        // Add a pin to the map
        var pin = new Microsoft.Maps.Pushpin(location, pushpinOptions);
        Microsoft.Maps.Events.addHandler(pin, 'drag', ondragDetails);
        map.entities.push(pin);

        var limit = 50;
        GetLoc();
    }

    function WriteResponse(data)
    {
        var map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
                     {
                         credentials: "Av1LPz3-lukZMdQTm3HzUzRuBaj_tdmZ6XcFQ7D7Cq8xh7JRzLKg3lxUp0cguHJ-",
                         mapTypeId: Microsoft.Maps.MapTypeId.road,
                         zoom: 11
                     });

        var pushpinOptions = { icon: '../Content/GreenPushpin.png' };

        var pushpin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(data[0].XCoord, data[0].YCoord), pushpinOptions);
        map.entities.push(pushpin);
    }

    function GetLoc() {
        jQuery.support.cors = true;
        $.ajax({
            url: 'http://localhost:24869/v1/FoodTruck',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                WriteResponse(data);
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }
}