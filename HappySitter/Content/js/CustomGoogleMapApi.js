
// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">


var map;
var service;
var infowindow;

function initMap() {

    var customerLatLng = {
        lat: parseFloat($('#User_Latitude').val()),
        lng: parseFloat($('#User_Longitude').val())
    };
    
    //var sydney = new google.maps.LatLng(-33.867, 151.195);
    //var sydney = new google.maps.LatLng(customerLatLng.lat, customerLatLng.lng);
    infowindow = new google.maps.InfoWindow();

    map = new google.maps.Map(
        document.getElementById('map'), { center: customerLatLng, zoom: 11 });
    //map.setCenter(sydney);
    //var marker = new google.maps.Marker({
    //    position: customerLatLng,
    //    map: map,
    //    title: 'Me'
    //});

    var request = {
        //query: 'Museum of Contemporary Art Australia',
        //fields: ['name', 'geometry'],
    };

    service = new google.maps.places.PlacesService(map);

    service.findPlaceFromQuery(request, function (results, status) {
        if (status === google.maps.places.PlacesServiceStatus.OK) {
            for (var i = 0; i < results.length; i++) {
                createMarker(results[i]);
            }

            map.setCenter(results[0].geometry.location);
        }
    });
}

function createMarker(place) {
    var marker = new google.maps.Marker({
        map: map,
        position: place.geometry.location
    });

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(map, this);
    });
}


