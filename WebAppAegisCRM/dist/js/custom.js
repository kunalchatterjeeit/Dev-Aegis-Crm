function myFunction() {
    var input, filter, table, tr, td, i;
    input = document.getElementById("txtItem");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        td2 = tr[i].getElementsByTagName("td")[1];
        if (td) {
            if ((td.innerHTML.toUpperCase().indexOf(filter) > -1) || (td2.innerHTML.toUpperCase().indexOf(filter) > -1)) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
function showSignaturePad() {
    document.getElementById("signature-pad").style.display = 'block';
    resizeCanvas();
    //scroll to sign pad
    $('html, body').animate({
        scrollTop: $("#signature-pad").offset().top
    }, 2000);
}
function GetAutocompleteInventories() {
    myFunction();
    $("#txtItem").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../WebServices/InternalServices.asmx/LoadAutoCompleteItems",
                data: "{'searchContent':'" + document.getElementById('txtItem').value + "'}",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (result) {
                    alert(result.responseText);
                }
            });
        }
    });
}

$(document).ready(function () {
    var numItems = $('li.fancyTab').length;
    if (numItems == 12) {
        $("li.fancyTab").width('8.3%');
    }
    if (numItems == 11) {
        $("li.fancyTab").width('9%');
    }
    if (numItems == 10) {
        $("li.fancyTab").width('10%');
    }
    if (numItems == 9) {
        $("li.fancyTab").width('11.1%');
    }
    if (numItems == 8) {
        $("li.fancyTab").width('12.5%');
    }
    if (numItems == 7) {
        $("li.fancyTab").width('14.2%');
    }
    if (numItems == 6) {
        $("li.fancyTab").width('16.666666666666667%');
    }
    if (numItems == 5) {
        $("li.fancyTab").width('20%');
    }
    if (numItems == 4) {
        $("li.fancyTab").width('25%');
    }
    if (numItems == 3) {
        $("li.fancyTab").width('33.3%');
    }
    if (numItems == 2) {
        $("li.fancyTab").width('50%');
    }
});

$(window).load(function () {
    $('.fancyTabs').each(function () {
        var highestBox = 0;
        $('.fancyTab a', this).each(function () {

            if ($(this).height() > highestBox)
                highestBox = $(this).height();
        });
        $('.fancyTab a', this).height(highestBox / 1.3);
    });
});