<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="location.aspx.cs" Inherits="WebAppAegisCRM.location" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <title></title>
    <script type="text/javascript">
        $(function () {
            navigator.geolocation.getCurrentPosition(function (position) {
                setLocation(position);
            });
        });

        function setLocation(position)
        {
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
    </div>
    </form>
</body>
</html>
