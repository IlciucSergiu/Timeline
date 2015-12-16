

function AddTagItem() {
    var tagName = $(".textBoxAddTag").val().toString().toLowerCase();
   // tagName = tagname.toString().toLowerCase();
    var tagImportance = $("#inputImportanceTag").val();

    try {
        var tagValue = tagName +"-"+ tagImportance.toString();
        $('.listBoxTags').append("<option value="+ tagValue.toString() + ">" + tagName + " " + tagImportance + "</option>");
       // $('.listBoxTags').html += "<option value=" + tagValue.toString() + ">" + tagName + " " + tagImportance + "</option>";
        alert(tagName + " ---- " + tagImportance);

        var listString = "";
        $(".listBoxTags option").each(function () {
           // alert($(this).val());
            listString += $(this).val()+";";
            // Add $(this).val() to your list
        });
       // alert(listString);
        UpdHid(listString);
    }
    catch (err) {
        alert(err.message);
    }
    return false;
}



$(function () {

    $('#buttonRemoveTags').click(function () {
        $(".listBoxTags option:selected").remove();
        var listString = "";
        $(".listBoxTags option").each(function () {
            // alert($(this).val());
            listString += $(this).val() + ";";
            // Add $(this).val() to your list
        });
        // alert(listString);
        UpdHid(listString);
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
    });
});

//$(function () {

//    $(".tagLinks").click(function (e) {

//       // alert(this.innerHTML);
       
//        //PageMethods.LoadTimelineQuery(e.innerHTML, OnSuccessCallback, OnFailureCallback);
        

//        //function OnSuccessCallback(res) {
//        //    alert(res);
//        //}

//        //function OnFailureCallback() {
//        //    alert('Error');
//        //}
        
//        try {
//            var dataValue = { criteria: this.innerHTML };
//           // alert("got here");
//        $.ajax({
//            type: "POST",
//            url: "WebFormTimeline.aspx/SearchByCriteria",
//            data: JSON.stringify(dataValue),                
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'text',
//           // async: false,
//            error: function (err) {
//                alert("Errort: " + err.responseText);
//            },
//            success: function (result) {
//                alert("We returned: " + result);
//            }
//        });
//    }
//        catch(e)
//        {
//            alert("Error" + e.message);
//        }
//        });
//});



