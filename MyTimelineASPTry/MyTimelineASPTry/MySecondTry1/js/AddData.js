

function AddTagItem() {

    $(".verifyTag").text("");
    // VerifyIfInList();
    if (VerifyTagItem() && VerifyIfInList()) {
        if ($("#inputImportanceTag").val() != "") {
            // alert($("#inputImportanceTag").val());
            var tagName = $(".textBoxAddTag").val().toString().toLowerCase();
            $(".textBoxAddTag").val(null);
            var tagImportance = $("#inputImportanceTag").val();

            //alert("here");
            var documentId = $("#hiddenId").val();
            //alert(documentId);

            InsertInTagCollection(tagName, documentId, tagImportance);

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

function InsertInTagCollection(tagName, documentId, relativeImportance) {

    //alert(userId1);

    try {
        var dataValue = { documentId: documentId, tagName: tagName, relativeImportance: relativeImportance };
        // alert(JSON.stringify(dataValue));
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "AddData.aspx/InsertInTagCollection",
            data: JSON.stringify(dataValue),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            // async: false,
            error: function (err) {
                alert("Errort: " + err.responseText);
            },
            success: function (result) {
                alert("We returned: " + result.d);


            }
        });
    }
    catch (e) {
        alert("Error" + e.message);
    }
}


function RemoveInTagCollection(tagName, documentId) {

    //alert(userId1);

    try {
        var dataValue = { documentId: documentId, tagName: tagName };
        // alert(JSON.stringify(dataValue));
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "AddData.aspx/RemoveInTagCollection",
            data: JSON.stringify(dataValue),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            // async: false,
            error: function (err) {
                alert("Errort: " + err.responseText);
            },
            success: function (result) {
                alert("We returned: " + result.d);


            }
        });
    }
    catch (e) {
        alert("Error" + e.message);
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

function CheckInListBox(value, listBox) {

    var response = true;
    $(listBox + " option").each(function () {

       // alert();
       
        if (value.toLowerCase() == $(this).val().toLowerCase())
            response = false;

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

    //alert("sadtag");

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
        if ($(".listBoxTags option:selected").text() != "") {
            var documentId = $("#hiddenId").val();
            //alert(documentId);
            var tagName = $(".listBoxTags option:selected").text().split(' ')[0];
            //alert(tagName + "   " + documentId);

            RemoveInTagCollection(tagName, documentId);

            $(".listBoxTags option:selected").remove();
            var listString = "";
            $(".listBoxTags option").each(function () {
                // alert($(this).val());
                listString += $(this).val() + ";";
                // Add $(this).val() to your list
            });
            // alert(listString);
            UpdHidTag(listString);
        }
        else {
            $(".verifyTag").text("No tag was selected.");
        }
    }
    );

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

    $(".textBoxImageLink").on('blur', function () {
        alert($(".textBoxImageLink").val());
        $(".documentImage").attr("src", $(".textBoxImageLink").val())
    });

    $("#feedbackShow").click(function () {

        if ($("#feedbackContent").css("display") == "none")
            $("#feedbackContent").css("display", "block");
        else
            $("#feedbackContent").css("display", "none");

    });

    $("#improvePage").click(function () {

        // 
        if ($("#feedbackMessage").css("display") == "none") {
            //alert("adgadsg");
            $("#feedbackMessage").css("display", "block");
        }
        else {
            $("#feedbackMessage").css("display", "none");
        }

        return false;

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


    // $('#pageBody').click(function () { alert("sdasdg") });

    $(".textBoxAddParentTag").on('keydown keypress focus', function () {
        //alert("here");
        if ($(".textBoxAddParentTag").val() != "")
            try {
                var dataValue = { inputValue: $(".textBoxAddParentTag").val() };

                $.ajax({
                    type: "POST",
                    url: "AddNewTag.aspx/FindTagParentOptions",
                    data: JSON.stringify(dataValue),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',

                    error: function (err) {

                        alert("Errort: " + err.responseText);
                    },
                    success: function (result) {
                        var arrayTagNames = [];
                        var arrayTagId = [];
                        var availableTags = result.d.split("{;}").forEach(function (item) {
                            //array2.push(item);
                            var array3 = item.split("{0}");
                            // alert(array3[0]);
                            arrayTagNames.push(array3[0]);
                            arrayTagId.push(array3[1]);
                        });
                        // alert(array2);
                        // $('#debug1').text(arrayTagNames);
                        // $('#debug2').text(arrayTagId);

                        $(".textBoxAddParentTag").autocomplete({
                            source: arrayTagNames,
                            select: function (event, ui) {
                                var parentId = arrayTagId[$.inArray(ui.item.label, arrayTagNames)];
                                $('#debug2').text(parentId);
                                UpdHidId(parentId);
                                //alert(ui.item.label);
                                // $('hiddenFieldParentTagId').val(parentId);
                                //  alert($('hiddenFieldParentTagId').val);
                                // $(".textBoxAddParentTag").val(ui.item.label);

                            }
                        });
                    }
                });
            }
            catch (e) {
                alert("Error" + e.message);
            }
    });

    $(".textBoxAddParentTag").focusout(function () {
        // alert($(".textBoxAddParentTag").val());
        if (!VerifyTagExistence($(".textBoxAddParentTag").val())) {
            // $("#verifyTag").css("display", "block");
            $("#verifyTag").css("color", "red");
            $("#verifyTag").text("This tag does not exist.");
        }
        else {
            $("#verifyTag").text("");
        }
    });

    $(".textBoxTagName").focusout(function () {
        // alert($(".textBoxAddParentTag").val());
        if (VerifyTagExistence($(".textBoxTagName").val())) {
            // $("#verifyTagName").css("display", "block");
            $("#verifyTagName").css("color", "red");
            $("#verifyTagName").text("This tag already exists.");
        }
        else {
            // $("#verifyTagName").css("display", "none");
            $("#verifyTagName").text("");
        }
    });

    $("#additionalResources a").click(function (e) {
        // alert($(this).text());
        tabName = $(this).text();
        $('#additionalResources div.tab').each(function (i, obj) {
            // alert($(this).attr('class'));
            if ($(this).hasClass(tabName))
                $(this).css("display", "block");
            else
                $(this).css("display", "none");
        });
        return false;
    });

    changeImage(1);

    countTimes = 1;
    $('.documentSlideshow img').click(function () {

        imagesLengt = 0;
        $('.documentSlideshow img').each(function (e) {
            imagesLengt++;
        });

        if (countTimes == imagesLengt)
            countTimes = 1;
        else
            countTimes = countTimes + 1;

        changeImage(countTimes);
    });


    $("#buttonSearchBook").click(function () {

        var query = $(".textBoxBook").val();
        // alert(query);
        $("#booksOptions").html("");
        $("#buttonRemoveBook").css("display", "none");
        $("#buttonAddThisBook").css("display", "block");
         $("#bookSelectedBook").css("display", "none");
       

        $.get("https://www.googleapis.com/books/v1/volumes?q=" + query, function (data) {


            var book = data.items[0];

            var title = (book["volumeInfo"]["title"]);

            //alert(title);
            // alert(JSON.stringify(data) + "asdgasd");

            //$("#bookInfo").text(JSON.stringify(data.items));

            $.each(data.items, function () {
                book = this;
                //alert(book["volumeInfo"]["title"]);

                $("#booksOptions").append('<div id="' + book["volumeInfo"]["industryIdentifiers"][0]["identifier"] + '" class="divBookOption"><img class="bookOption" src="' + book["volumeInfo"]["imageLinks"]["smallThumbnail"] + '" class="divBookOption"  /></div>');

            });

            $('.divBookOption').on("click", function () {

                BookImageClick(this);
            });

        });

    });


    $("#listBoxBooks").click(function (e) {
        ListBookSelected(this);
    });
    
});

function BookImageClick(e) {



    var isbn = $(e).attr('id');
     //alert(isbn);
    $("#hiddenIsbn").val(isbn);

    $.ajax({
        url: "https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn.toString(),
        type: 'GET',
       // async: false,
        success: function (data) {

           // alert("ajax");
            // alert(JSON.stringify(data));
            if(data["totalItems"] != "0")
                {
            $("#booksOptions").html("");

            var book = data.items[0];

            //var title = (book["volumeInfo"]["title"]);

            // alert(title);

            $("#selectedBookImage").attr("src", book["volumeInfo"]["imageLinks"]["smallThumbnail"]);

            $("#selectedBookTitle").text(book["volumeInfo"]["title"]);
            $("#selectedBookAuthors").text(book["volumeInfo"]["authors"]);
            $("#selectedBookDescription").text(book["volumeInfo"]["description"]);
            $("#selectedBookPages").text("Pages: " + book["volumeInfo"]["pageCount"]);
            $("#bookSelectedBook").css("display", "block");
            }
            else {
                alert("this book is not availible");
            }
        },
        error: function (data) {
            alert("woops"); //or whatever
        }

    });

    //return false;


    //$.get("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn, function (data) {

    //    alert("get");

    //    $("#booksOptions").html("");

    //    var book = data.items[0];

    //    //var title = (book["volumeInfo"]["title"]);

    //    // alert(title);

    //    $("#selectedBookImage").attr("src", book["volumeInfo"]["imageLinks"]["smallThumbnail"]);

    //    $("#selectedBookTitle").text(book["volumeInfo"]["title"]);
    //    $("#selectedBookAuthors").text(book["volumeInfo"]["authors"]);
    //    $("#selectedBookDescription").text(book["volumeInfo"]["description"]);
    //    $("#selectedBookPages").text("Pages: " + book["volumeInfo"]["pageCount"]);
    //    $("#bookSelectedBook").css("display", "block");
    //});



}

function ListBookSelected(e) {



    
    var title = $(e).find(':selected').text();
    var isbn = $(e).find(':selected').val();
   // alert(isbn);
    $("#hiddenIsbn").val(isbn);

   

    $.get("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn, function (data) {


        

        $("#booksOptions").html("");

        var book = data.items[0];

       

        $("#selectedBookImage").attr("src", book["volumeInfo"]["imageLinks"]["smallThumbnail"]);

        $("#selectedBookTitle").text(book["volumeInfo"]["title"]);
        $("#selectedBookAuthors").text(book["volumeInfo"]["authors"]);
        $("#selectedBookDescription").text(book["volumeInfo"]["description"]);
        $("#selectedBookPages").text("Pages: " + book["volumeInfo"]["pageCount"]);
       
        $("#buttonRemoveBook").css("display", "block");
        $("#buttonAddThisBook").css("display", "none");
        $("#bookSelectedBook").css("display", "block");
    });



}

function AddThisBook() {

    var documentId = $("#hiddenId").val();
    var isbn = $("#hiddenIsbn").val();
    var imageUrl = $("#selectedBookImage").attr("src");
    var title = $("#selectedBookTitle").text();


    // $("#bookSelectedBook").css("display", "block");
    if(CheckInListBox(title, ".listBoxBooks"))
    {
       // alert("good");
    
    var dataValue = { title: title, isbn: isbn, imageUrl: imageUrl, documentId: documentId };
    // alert(JSON.stringify(dataValue));
    //alert(document.getElementById('hiddenId').value);
    $.ajax({
        type: "POST",
        url: "AddData.aspx/AddSelectedBook",
        data: JSON.stringify(dataValue),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        // async: false,
        error: function (err) {
            alert("Errort: " + err.responseText);
        },
        success: function (result) {
            // alert("We returned: " + result.d);
            if (result.d == "Inserted") {

                $('.listBoxBooks').append("<option value=" + isbn + ">" + $("#selectedBookTitle").text() + "</option>");

            }

        }
    });

    }
    else {
        alert("already here");
    }
}


function RemoveThisBook() {

    var documentId = $("#hiddenId").val();
    var isbn = $("#hiddenIsbn").val();
    var selectedIsbn = $('#listBoxBooks :selected').val();
    var title = $('#listBoxBooks :selected').text();

    //alert(selectedIsbn + "----" + isbn);
    //var imageUrl = $("#selectedBookImage").attr("src");
   // var title = $("#selectedBookTitle").text();


    // $("#bookSelectedBook").css("display", "block");
   

        var dataValue = { title: title, isbn: isbn, documentId: documentId };
        // alert(JSON.stringify(dataValue));
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "AddData.aspx/RemoveSelectedBook",
            data: JSON.stringify(dataValue),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            // async: false,
            error: function (err) {
                alert("Errort: " + err.responseText);
            },
            success: function (result) {
                alert("We returned: " + result.d);
                if (result.d == "Deleted") {

                    $('#listBoxBooks :selected').remove();
                }

            }
        });

        $("#selectedBookImage").attr("src", "#");

        $("#selectedBookTitle").text("");
        $("#selectedBookAuthors").text("");
        $("#selectedBookDescription").text("");
        $("#selectedBookPages").text("");
        $("#bookSelectedBook").css("display", "none");
}



function VoteUp(userId1) {

    //alert(userId1);

    try {
        var dataValue = { documentId: document.getElementById('hiddenId').value, userId: userId1  /*this.innerHTML*/ };
        // alert("asdg");
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
                    $(".labelVote").text("vote registered");
                }
                if (result.d == "already") {

                    $(".labelVote").css("display", "block");
                    $(".labelVote").css("color", "red");
                    $(".labelVote").text("you have already voted");
                }
            }
        });
    }
    catch (e) {
        alert("Error" + e.message);
    }
}

function changeImage(numberDisplay) {


    var count = 0;
    $('.documentSlideshow img').each(function (e) {
        count++;
        if (count == numberDisplay) {
            $(this).css("display", "block");
            // return false;
        }
        else {
            $(this).css("display", "none");

        }

    });
}

function VerifyTagExistence(tagName) {

    var dataValue = { inputValue: tagName };
    var response;
    $.ajax({
        type: "POST",
        url: "AddNewTag.aspx/VerifyTagExistence",
        data: JSON.stringify(dataValue),
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',

        error: function (err) {

            console.log("A aparut o eroare: " + err.responseText);
            // return false;
        },
        success: function (result) {

            response = result.d;


        }
    });

    return response;

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
function LoadVideo(videoId1) {
    // 2. This code loads the IFrame Player API code asynchronously.
    var tag = document.createElement('script');

    tag.src = "https://www.youtube.com/iframe_api";
    var firstScriptTag = document.getElementsByTagName('script')[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

    // 3. This function creates an <iframe> (and YouTube player)
    //    after the API code downloads.
    alert(videoId1);
    var player;
    function onYouTubeIframeAPIReady() {
        player = new YT.Player('player', {
            height: '390',
            width: '640',
            videoId: videoId1,
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
    }

    // 4. The API will call this function when the video player is ready.
    function onPlayerReady(event) {
        event.target.playVideo();
    }

    // 5. The API calls this function when the player's state changes.
    //    The function indicates that when playing a video (state=1),
    //    the player should play for six seconds and then stop.
    var done = false;
    function onPlayerStateChange(event) {
        if (event.data == YT.PlayerState.PLAYING && !done) {
            setTimeout(stopVideo, 6000);
            done = true;
        }
    }
    function stopVideo() {
        player.stopVideo();
    }
}