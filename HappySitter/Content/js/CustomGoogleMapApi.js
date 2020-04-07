
// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">


function createMarkerPage(jsonSitter) {

    var sitterId = jsonSitter.id;
    var userName = jsonSitter.UserName;
    var rateScore = jsonSitter.RateScore;

    var phoneNumber = jsonSitter.PhoneNumber;
    var costPerHour = jsonSitter.CostPerHour;
    var totalServiceHours = jsonSitter.TotalServiceHours;
    var costForServiceHours = jsonSitter.CostForServiceHours;
    var platformFee = jsonSitter.PlatformFee;
    var totalCost = jsonSitter.TotalCost;
    var hst = jsonSitter.Hst;

    var htmlContents;

    if (userName != "MyLocation") {
        htmlContents = "<div>" +
            "Name: <strong>@UserName</strong><br>" +
            //"RateScore: @RateScore / 10<br>" + 
            "Cost Per Hour: $@CostPerHour<br><br>" +
            "Total Service Hours: <strong>@TotalServiceHours Hours</strong><br>" +
            "Cost For ServiceHours: $@CostForServiceHours<br>" +
            "Platform Fee: $ @PlatformFee<br><br>" +
            "Total Cost(exclude for Hst): <strong>$@TotalCost</strong><br>" +
            "Hst: $@Hst<br><br>" +
            "<button id='openConfirmModal' onclick='openModalforConfirmation()' class='btn btn-info btnPopupConfirmModal' data-toggle='modal' data-target='#myModal' data-ajax='false' " +
            "data-sitterid='@SitterId' data-costperhour='@CostPerHour' " +
            "data-costforservicehours='@CostForServiceHours' data-platformfee='@PlatformFee' " +
            "data-totalcost='@TotalCost' data-hst='@Hst' data-totalservicehours='@TotalServiceHours'>" +
            "Book this Sitter</button>" +
            "</div>";

        htmlContents = htmlContents.replace(/@UserName/gi, userName);
        htmlContents = htmlContents.replace(/@RateScore/gi, rateScore);
        htmlContents = htmlContents.replace(/@CostPerHour/gi, costPerHour);
        htmlContents = htmlContents.replace(/@TotalServiceHours/gi, totalServiceHours);
        htmlContents = htmlContents.replace(/@CostForServiceHours/gi, costForServiceHours);
        htmlContents = htmlContents.replace(/@PlatformFee/gi, platformFee);
        htmlContents = htmlContents.replace(/@TotalCost/gi, totalCost);
        htmlContents = htmlContents.replace(/@Hst/gi, hst);
        htmlContents = htmlContents.replace(/@SitterId/gi, sitterId);

    } else {
        htmlContents = "<div>" +
            "<strong>@UserName</strong><br>" +
            "</div>";

        htmlContents = htmlContents.replace(/@UserName/gi, userName);
    }
    
    //htmlContents =
    //    htmlContents + "<script> $('#openConfirmModal').click(function (e) {alert('Tttttta');});</script>";

        console.log();
        return htmlContents;
    }
    
function initMap() {

    var customerLatLng = {
        lat: parseFloat($('#User_Latitude').val()),
        lng: parseFloat($('#User_Longitude').val())
    };

    var map = new google.maps.Map(document.getElementById('map'),
        {
            zoom: 11,
            center: new google.maps.LatLng(customerLatLng),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

    var infowindow = new google.maps.InfoWindow();

    var marker, i;

    for (i = 0; i < jsonSitters.length; i++) {

        if (i == (jsonSitters.length - 1)) { //My Location
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(jsonSitters[i].Latitude, jsonSitters[i].Longitude),
                map: map,
                icon: "/Content/icon/homegardenbusiness.png"
            });

        } else {
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(jsonSitters[i].Latitude, jsonSitters[i].Longitude),
                map: map
            });
        }
        

        google.maps.event.addListener(marker,
            'click',
            (function (marker, i) {
                return function () {
                    //infowindow.setContent(jsonSitters[i].UserName);
                    infowindow.setContent(createMarkerPage(jsonSitters[i]));
                    infowindow.open(map, marker);
                    
                }
            })(marker, i));

    }
}


