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