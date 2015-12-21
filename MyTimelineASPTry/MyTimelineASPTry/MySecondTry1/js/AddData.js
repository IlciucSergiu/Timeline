

function AddTagItem() {
    var tagName = $(".textBoxAddTag").val().toString().toLowerCase();
    $(".textBoxAddTag").val(null);
    var tagImportance = $("#inputImportanceTag").val();
    $("#inputImportanceTag").val(null);
    try {
        var tagValue = tagName +"-"+ tagImportance.toString();
        $('.listBoxTags').append("<option value="+ tagValue.toString() + ">" + tagName + " " + tagImportance + "</option>");
      
       // alert(tagName + " ---- " + tagImportance);

        var listString = "";
        $(".listBoxTags option").each(function () {
          
            listString += $(this).val()+";";
            
        });
       
        UpdHidTag(listString);
    }
    catch (err) {
        alert(err.message);
    }
    return false;
}

function ContemporaryChecked(isChecked)
{
    alert("check");
    if(isChecked)
    {
        $("#dateDeath").attr("disabled", "disabled");
    }
    else
    {
        $("#dateDeath").attr("enabled", "enabled");
    }
    return false;
}
function AddLinkItem() {
    var linkText = $(".textBoxAddLink").val().toString();
    $(".textBoxAddLink").val(null);
    

    try {
        
        $('.listBoxLinks').append("<option value=" + linkText + ">" +linkText + "</option>");

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

});
$(function () {

    $(".tagLinks").click(function (e) {
       // alert("bun");
          UpdHidCriteria(this.innerHTML);

        //try {
        //            var dataValue = { criteria : this.innerHTML};
        //            // alert("got here");
        //            $.ajax({
        //                type: "POST",
        //                url: "WebFormTimeline.aspx/SearchByCriteria",
        //                data: JSON.stringify(dataValue),
        //                contentType: 'application/json; charset=utf-8',
        //                dataType: 'json',
                        
        //                error: function (err) {
        //                    alert("Errort: " + err.responseText);
        //                },
        //                success: function (result) {
        //                    var jsonString = result.d;
        //                   // var jsonObject = jQuery.parseJSON("[" + jsonData + "]");
                
        //                    alert("We returned: " + jsonString);

        //                    if (jsonString != "") {
        //                        //alert(jsonString);
        //                        var jsonData = JSON.parse(jsonString);

        //                        var tg1 = $("#placement").timeline({

        //                            "data_source": jsonData, //"MySecondTry1/json/sergiu3.json",
        //                            "min_zoom": 15,
        //                            "max_zoom": 60
        //                        }
        //                        );
        //                    }
        //                }
        //            });
        //        }
        //        catch (e) {
        //            alert("Error" + e.message);
        //        }
    });
});

$(function () {

    $(".linkContact").click(function (e) {

        alert(this.innerHTML);
       
        //PageMethods.LoadTimelineQuery(e.innerHTML, OnSuccessCallback, OnFailureCallback);
        

        //function OnSuccessCallback(res) {
        //    alert(res);
        //}

        //function OnFailureCallback() {
        //    alert('Error');
        //}
        
        try {
            var dataValue = { criteria: "clasicism" /*this.innerHTML*/ };
           // alert("got here");
        $.ajax({
            type: "POST",
            url: "WebFormTimeline.aspx/SearchByCriteria",
            data: JSON.stringify(dataValue),                
            contentType: 'application/json; charset=utf-8',
            dataType: 'text',
           // async: false,
            error: function (err) {
                alert("Errort: " + err.responseText);
            },
            success: function (result) {
                alert("We returned: " + result);
            }
        });
    }
        catch(e)
        {
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