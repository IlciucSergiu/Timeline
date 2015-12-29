

function AddTagItem() {

    $(".verifyTag").text("");
    VerifyIfInList();
    if (VerifyTagItem() && VerifyIfInList()) {
        if ($("#inputImportanceTag").val() != "") {
            // alert($("#inputImportanceTag").val());
            var tagName = $(".textBoxAddTag").val().toString().toLowerCase();
            $(".textBoxAddTag").val(null);
            var tagImportance = $("#inputImportanceTag").val();
            $("#inputImportanceTag").val(null);
            try {
                var tagValue = tagName + "-" + tagImportance.toString();
                $('.listBoxTags').append("<option value=" + tagValue.toString() + ">" + tagName + " " + tagImportance + "</option>");

                // alert(tagName + " ---- " + tagImportance);

                var listString = "";
                $(".listBoxTags option").each(function () {

                    listString += $(this).val() + ";";

                });

                UpdHidTag(listString);
            }
            catch (err) {
                alert(err.message);
            }
            return false;
        }
        else {
            $(".verifyTag").text("Invalid information.");
        }
    }
    
}

function VerifyIfInList() {

    var response = true;
    $(".listBoxTags option").each(function () {

        var listString = $(this).val().split('-');
        if ($(".textBoxAddTag").val().toString().toLowerCase() == listString[0]) {
            $(".verifyTag").text("This tag already exists in list.");
            response = false;
        }


    });
    return response;
}

function VerifyTagItem() {

    var dataValue = { inputValue: $(".textBoxAddTag").val() };
    var response;
    $.ajax({
        type: "POST",
        url: "AddData.aspx/FindTagOptions",
        data: JSON.stringify(dataValue),
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',

        error: function (err) {

            console.log("A aparut o eroare: " + err.responseText);
            // return false;
        },
        success: function (result) {

            var availableTags = result.d.split("{;}");
            // alert(availableTags);

            if (availableTags != "") {
                //alert("asdf");
                if ($.inArray($(".textBoxAddTag").val(), availableTags) != -1) {
                    // alert($.inArray($(".textBoxAddTag").val(), availableTags));

                    response = true;
                }
                else {
                    $(".verifyTag").text("This tag does not exist!");
                }

            }
            else {
                $(".verifyTag").text("This tag does not exist!");
                response = false;
            }
          
        }
    });

    return response;

}

function ContemporaryChecked(isChecked) {
    alert("check");
    if (isChecked) {
        $("#dateDeath").attr("disabled", "disabled");
    }
    else {
        $("#dateDeath").attr("enabled", "enabled");
    }
    return false;
}
function AddLinkItem() {
    var linkText = $(".textBoxAddLink").val().toString();
    $(".textBoxAddLink").val(null);


    try {

        $('.listBoxLinks').append("<option value=" + linkText + ">" + linkText + "</option>");

        // alert(tagName + " ---- " + tagImportance);

        var listString = "";
        $(".listBoxLinks option").each(function () {

            listString += $(this).val() + ";";

        });

        //alert(listString);
        UpdHidLink(listString);
    }
    catch (err) {
        alert(err.message);
    }
    return false;
}


$(function () {


    $(".textBoxSearchQuery").keydown(function (event) {
        if (e.keyCode == 13) {
            alert(e.keyCode);
            $(".searchButton").click();

        }

    });


    $(".checkContemporary").click(function () {
        // alert(document.getElementByClass('checkBoxContemporary').checked).toString();
        // alert(document.getElementById('checkBoxContemporary').checked).toString();
        if (document.getElementById('checkBoxContemporary').checked) {
            $("#dateDeath").prop('disabled', true);
        }
        else {
            $("#dateDeath").prop('disabled', false);
        }
        //return false;
    });


    $('#buttonRemoveTags').click(function () {
        $(".listBoxTags option:selected").remove();
        var listString = "";
        $(".listBoxTags option").each(function () {
            // alert($(this).val());
            listString += $(this).val() + ";";
            // Add $(this).val() to your list
        });
        // alert(listString);
        UpdHidTag(listString);
    });

    $('#buttonRemoveLinks').click(function () {
        $(".listBoxLinks option:selected").remove();
        var listString = "";
        $(".listBoxLinks option").each(function () {
            // alert($(this).val());
            listString += $(this).val() + ";";
            // Add $(this).val() to your list
        });
        //alert(listString);
        UpdHidLink(listString);
    });

    $(".datepicker").datepicker({
        showOn: "button",
        buttonImage: "MySecondTry1/timeglider/img/calendar_16.png",
        buttonImageOnly: true,
        buttonText: "Select date",
        changeYear: true,
        changeMonth: true,
        yearRange: "1:c"
    });
    $(".datepicker").datepicker("option", "dateFormat", "yy-mm-dd");

    $(".textBoxAddTag").on('keydown keypress focus', function () {
        try {
            var dataValue = { inputValue: $(".textBoxAddTag").val() };

            $.ajax({
                type: "POST",
                url: "AddData.aspx/FindTagOptions",
                data: JSON.stringify(dataValue),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',

                error: function (err) {

                    alert("Errort: " + err.responseText);
                },
                success: function (result) {

                    var availableTags = result.d.split("{;}");
                    $(".textBoxAddTag").autocomplete({
                        source: availableTags
                    });
                }
            });
        }
        catch (e) {
            alert("Error" + e.message);
        }
    });


    $(".textBoxSearchQuery").on('keydown keypress focus', function () {
        try {
            var dataValue = { inputValue: $(".textBoxSearchQuery").val() };

            $.ajax({
                type: "POST",
                url: "AddData.aspx/FindTagOptions",
                data: JSON.stringify(dataValue),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',

                error: function (err) {

                    alert("Errort: " + err.responseText);
                },
                success: function (result) {

                    var availableTags = result.d.split("{;}");
                    $(".textBoxSearchQuery").autocomplete({
                        source: availableTags
                    });
                }
            });
        }
        catch (e) {
            alert("Error" + e.message);
        }
    });


    
});



function VoteUp(userId1) {

    //alert(userId1);

    try {
        var dataValue = { documentId: document.getElementById('hiddenId').value, userId: userId1  /*this.innerHTML*/ };

        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "WebFormTimeline.aspx/UpVoteDocument",
            data: JSON.stringify(dataValue),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            // async: false,
            error: function (err) {
                alert("Errort: " + err.responseText);
            },
            success: function (result) {
                //alert("We returned: " + result.d);
                if (result.d == "worked") {
                    $(".labelVote").css("display", "block");
                    $(".labelVote").css("color", "green");
                    $(".labelVote").text("you have already voted");
                }
                if (result.d == "already") {

                    $(".labelVote").css("display", "block");
                    $(".labelVote").css("color", "red");
                    $(".labelVote").text("vote registred");
                }
            }
        });
    }
    catch (e) {
        alert("Error" + e.message);
    }
}

$(function () {

    $(".tagLinks").click(function (e) {

        UpdHidCriteria(this.innerHTML);

    });
    $(".linkContact").click(function (e) {

        alert(this.innerHTML);



        try {
            var dataValue = { criteria: "clasicism" };

            $.ajax({
                type: "POST",
                url: "WebFormTimeline.aspx/SearchByCriteria",
                data: JSON.stringify(dataValue),
                contentType: 'application/json; charset=utf-8',
                dataType: 'text',

                error: function (err) {
                    alert("Errort: " + err.responseText);
                },
                success: function (result) {
                    alert("We returned: " + result);
                }
            });
        }
        catch (e) {
            alert("Error" + e.message);
        }
    });



});

// De pastrat
//function SearchPersonalInfo(theId) {

//    alert(theId);
//    try {
//        var dataValue = { personId: theId};
//        // alert("got here");
//        $.ajax({
//            type: "POST",
//            url: "WebFormTimeline.aspx/SearchPersonalInfo",
//            data: JSON.stringify(dataValue),
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'json',
//            // async: false,
//            error: function (err) {
//                alert("Errort: " + err.responseText);
//            },
//            success: function (result) {
//                var jsonData = result.d;
//                var jsonObject = jQuery.parseJSON("[" + jsonData + "]");

//                alert("We returned: " + jsonObject.id);
//            }
//        });
//    }
//    catch (e) {
//        alert("Error" + e.message);
//    }
//}