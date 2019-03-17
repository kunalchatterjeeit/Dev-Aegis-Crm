<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="location.aspx.cs" Inherits="WebAppAegisCRM.location" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" /> 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCbfTBb6nBwY7HAxzgqGODA2e8LMo0ddnU&callback=initMap"></script>
    <%--<script src="https://www.google.com/maps/embed/v1/view?key=AIzaSyC7UPU9JDzwcA4JwxuwSoPm3OUL05Rcm7k&center=-25.108967999999997,55.198316299999995&zoom=18&maptype=satellite"></script>--%>
    <title></title>
    <script type="text/javascript">
        $(function () {
            navigator.geolocation.getCurrentPosition(function (position) {
                setLocation(position);
            });
        });

        function setLocation(position) {
            $("#location").html(
                'latitude: ' + position.coords.latitude
                + ' longitude: ' + position.coords.longitude
                + ' altitude: ' + position.coords.altitude
                + ' accuracy: ' + position.coords.accuracy
                + ' altitudeAccuracy: ' + position.coords.altitudeAccuracy
                + ' heading: ' + position.coords.heading
                + ' speed: ' + position.coords.speed)
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span id="location"></span>
            <a id="loc" href="MainLogout.aspx"></a>
            <div id="map" style="width: 500px; height: 400px;"></div>
            <script type="text/javascript">
                lat = 25.108967999999997;//from other function
                long = 55.198316299999995;//from other function
                coords = new google.maps.LatLng(lat, long);
                var mapOptions = {
                    zoom: 17,
                    center: coords,
                    mapTypeId: google.maps.MapTypeId.HYBRID
                };
                map = new google.maps.Map(
                    document.getElementById("map"), mapOptions
                    );
            </script>
        </div>
    </form>
</body>
</html>
