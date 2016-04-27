
function getParameterByName(name) {
    var regexS = "[\\?&]" + name + "=([^&#]*)",
  regex = new RegExp(regexS),
  results = regex.exec(window.location.search);
    if (results == null) {
        return "";
    } else {
        return decodeURIComponent(results[1].replace(/\+/g, " "));
    }
}


function DocumentExists(documentId) {
    var response = false;
    try {
        var dataValue = { documentId: documentId };
        // alert(JSON.stringify(dataValue));
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "WebMethods.aspx/DocumentExists",
            data: JSON.stringify(dataValue),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
             async: false,
            error: function (err) {
                alert("Errort: " + err.responseText);
            },
            success: function (result) {
                // alert(result.d);
                if (result.d == "True")
                    response = true;
                else
                    response = false;
            }
        });
    }
    catch (e) {
        alert("Error" + e.message);
    }

    return response;
}

function AddTagItem() {

    $(".verifyTag").text("");
    // VerifyIfInList();
    if (VerifyTagItem() && VerifyIfInList()) {
        if ($("#inputImportanceTag").val() != "") {
            // alert($("#inputImportanceTag").val());
            var tagName = $(".textBoxAddTag").val().toString();
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
            url: "WebMethods.aspx/InsertInTagCollection",
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
            url: "WebMethods.aspx/RemoveInTagCollection",
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


function AddCategoryItem() {

    // alert("here");

    $(".verifyCategory").text("");
    // VerifyIfInList();
    if (VerifyCategoryItem() && VerifyCategoryInList()) {
        if ($("#inputImportanceCategory").val() != "") {
            //  alert($("#inputImportanceCategory").val());
            var categoryName = $(".textBoxAddCategory").val().toString();
            $(".textBoxAddCategory").val(null);
            var categoryImportance = $("#inputImportanceCategory").val();


            var documentId = $("#hiddenId").val();
            // alert(documentId);

            InsertInCategoryCollection(categoryName, documentId, categoryImportance);

            $("#inputImportanceCategory").val(null);
            try {
                var categoryValue = categoryName + "-" + categoryImportance.toString();
                $('.listBoxCategories').append("<option value=" + categoryValue.toString() + ">" + categoryName + " - " + categoryImportance + "</option>");


            }
            catch (err) {
                alert(err.message);
            }
            return false;
        }
        else {
            $(".verifyCategory").text("Invalid information.");
        }
    }

}

function InsertInCategoryCollection(categoryName, documentId, relativeImportance) {

    //alert(userId1);

    try {
        var dataValue = { documentId: documentId, categoryName: categoryName, relativeImportance: relativeImportance };
        // alert(JSON.stringify(dataValue));
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "WebMethods.aspx/InsertInCategoryCollection",
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

function RemoveCategoryItem() {
    if ($(".listBoxCategories option:selected").text() != "") {
        var documentId = $("#hiddenId").val();
        //alert(documentId);
        var categoryName = $(".listBoxCategories option:selected").text().split(' - ')[0];
        alert(categoryName + "   " + documentId);

        RemoveInCategoryCollection(categoryName, documentId);

        $(".listBoxCategories option:selected").remove();

    }
    else {
        $(".verifyCategory").text("No tag was selected.");
    }
}


function RemoveInCategoryCollection(categoryName, documentId) {

    //alert(userId1);

    try {
        var dataValue = { documentId: documentId, categoryName: categoryName };
        // alert(JSON.stringify(dataValue));
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "WebMethods.aspx/RemoveInCategoryCollection",
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
        if ($(".textBoxAddTag").val().toString().toLowerCase() == listString[0].toLowerCase()) {
            $(".verifyTag").text("This tag already exists in list.");
            response = false;
        }


    });
    return response;
}

function VerifyCategoryInList() {

    var response = true;
    $(".listBoxCategories option").each(function () {

        var listString = $(this).val().split('-');
        if ($(".textBoxAddCategory").val().toString().toLowerCase() == listString[0].toLowerCase()) {
            $(".verifyCategory").text("This tag already exists in list.");
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
        url: "WebMethods.aspx/FindTagOptions",
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


function VerifyCategoryItem() {

    // alert($(".textBoxAddCategory").val());
    var dataValue = { inputValue: $(".textBoxAddCategory").val() };
    var response;
    $.ajax({
        type: "POST",
        url: "WebMethods.aspx/FindCategoryOptions",
        data: JSON.stringify(dataValue),
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',

        error: function (err) {

            console.log("A aparut o eroare: " + err.responseText);
            // return false;
        },
        success: function (result) {

            var availableCategories = result.d.split("{;}");
            //alert(availableCategories);

            if (availableCategories != "") {
                //alert("asdf");
                if ($.inArray($(".textBoxAddCategory").val(), availableCategories) != -1) {
                    // alert($.inArray($(".textBoxAddTag").val(), availableCategories));

                    response = true;
                }
                else {
                    $(".verifyCategory").text("This Category does not exist!");
                }

            }
            else {
                $(".verifyCategory").text("This category does not exist!");
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

    try {

   
    $('.userManagingTab').click(function (e) {

        var tabName = $(this).text().toLowerCase();
        // alert($(this).text());
        $('.tabsContainer').each(function (i, obj) {
            // alert($(this).attr('class'));
            if ($(this).hasClass(tabName))
                $(this).css("display", "block");
            else
                $(this).css("display", "none");
        });


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



    $(".textBoxAddTag").on('keyup focus', function () {
        try {
            var dataValue = { inputValue: $(".textBoxAddTag").val() };

            $.ajax({
                type: "POST",
                url: "WebMethods.aspx/FindTagOptions",
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
        // alert($(".textBoxImageLink").val());
        $(".documentImage").attr("src", $(".textBoxImageLink").val())
    });

    $("#feedbackShow").click(function () {

        if ($("#feedbackContent").css("display") == "none")
            $("#feedbackContent").css("display", "block");
        else
            $("#feedbackContent").css("display", "none");

    });

    $(".improvePage").click(function () {

        // 
        if ($("#feedbackMessage").css("display") == "none") {
            //alert("adgadsg");
            $("#feedbackMessage").css("display", "block");
        }
        else {
            $("#feedbackMessage").css("display", "none");
        }

        //return false;

    });

    $(".textBoxSearchQuery").on('keyup focus', function () {
        try {
            var dataValue = { inputValue: $(".textBoxSearchQuery").val() };

            $.ajax({
                type: "POST",
                url: "WebMethods.aspx/FindTagOptions",
                data: JSON.stringify(dataValue),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',

                error: function (err) {

                    // alert("Errort: " + err.responseText);
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




    $(".textBoxAddParentTag").on('keyup focus', function () {
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

                            var array3 = item.split("{0}");

                            arrayTagNames.push(array3[0]);
                            arrayTagId.push(array3[1]);
                        });


                        $(".textBoxAddParentTag").autocomplete({
                            source: arrayTagNames,
                            select: function (event, ui) {
                                var parentId = arrayTagId[$.inArray(ui.item.label, arrayTagNames)];

                                // UpdHidId(parentId);
                            }
                        });
                    }
                });
            }
            catch (e) {
                alert("Error" + e.message);
            }
    });

    $(".textBoxAddParentCategory").on('keyup focus', function () {

        if ($(".textBoxAddParentCategory").val() != "")
            try {
                var dataValue = { inputValue: $(".textBoxAddParentCategory").val() };

                $.ajax({
                    type: "POST",
                    url: "AddNewCategory.aspx/FindCategoryParentOptions",
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

                            var array3 = item.split("{0}");
                            arrayTagNames.push(array3[0]);
                            arrayTagId.push(array3[1]);
                        });


                        $(".textBoxAddParentCategory").autocomplete({
                            source: arrayTagNames,
                            select: function (event, ui) {
                                var parentId = arrayTagId[$.inArray(ui.item.label, arrayTagNames)];

                                // UpdHidId(parentId);
                            }
                        });
                    }
                });
            }
            catch (e) {
                alert("Error" + e.message);
            }
    });

    $(".textBoxEditParentCategory").on('keyup focus', function () {
        //alert("here");
        if ($(".textBoxEditParentCategory").val() != "")
            try {
                var dataValue = { inputValue: $(".textBoxEditParentCategory").val() };

                $.ajax({
                    type: "POST",
                    url: "AddNewCategory.aspx/FindCategoryParentOptions",
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

                            var array3 = item.split("{0}");

                            arrayTagNames.push(array3[0]);
                            arrayTagId.push(array3[1]);
                        });


                        $(".textBoxEditParentCategory").autocomplete({
                            source: arrayTagNames,
                            select: function (event, ui) {
                                var parentId = arrayTagId[$.inArray(ui.item.label, arrayTagNames)];

                                //  UpdHidId(parentId);
                            }
                        });
                    }
                });
            }
            catch (e) {
                alert("Error" + e.message);
            }
    });

    $(".textBoxAddCategory").on('keyup focus', function () {

        if ($(".textBoxAddCategory").val() != "")
            try {
                var dataValue = { inputValue: $(".textBoxAddCategory").val() };

                $.ajax({
                    type: "POST",
                    url: "AddNewCategory.aspx/FindCategoryParentOptions",
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

                            var array3 = item.split("{0}");

                            arrayTagNames.push(array3[0]);
                            arrayTagId.push(array3[1]);
                        });


                        $(".textBoxAddCategory").autocomplete({
                            source: arrayTagNames,
                            select: function (event, ui) {
                                var parentId = arrayTagId[$.inArray(ui.item.label, arrayTagNames)];
                            }
                        });
                    }
                });
            }
            catch (e) {
                alert("Error" + e.message);
            }
    });

    $(".textBoxEditParentTag").on('keydown keypress focus', function () {
        //alert("here");
        if ($(".textBoxEditParentTag").val() != "")
            try {
                var dataValue = { inputValue: $(".textBoxEditParentTag").val() };

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

                            var array3 = item.split("{0}");

                            arrayTagNames.push(array3[0]);
                            arrayTagId.push(array3[1]);
                        });


                        $(".textBoxEditParentTag").autocomplete({
                            source: arrayTagNames,
                            select: function (event, ui) {
                                var parentId = arrayTagId[$.inArray(ui.item.label, arrayTagNames)];

                                //  UpdHidId(parentId);


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

    $(".textBoxEditParentTag").focusout(function () {
        // alert($(".textBoxAddParentTag").val());
        if (!VerifyTagExistence($(".textBoxEditParentTag").val())) {
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

    $(".textBoxCategoryName").focusout(function () {

        if (VerifyCategoryExistence($(".textBoxCategoryName").val())) {

            $("#verifyCategoryName").css("color", "red");
            $("#verifyCategoryName").text("This category already exists.");
        }
        else {

            $("#verifyCategoryName").css("color", "green");
            $("#verifyCategoryName").text("v");

        }
    });

    $("#additionalResources a.tab").click(function (e) {
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
            $("#booksOptions").css("display", "flex");

        });



    });


    $("#listBoxBooks").click(function (e) {
        ListBookSelected(this);
    });






    $('#selectedBookSelfLink').on("click", function () {

        var url = $("#selectedBookSelfLink").attr("href");
        window.open(url);
        return false;
    });


    $('.imageCollection').on("click", function () {
        ImageLinkClick(this);
    });

    $("#textBoxSearchQuery").on('keydown keypress', function (e) {
        if (e.keyCode == 13) {
            // alert("pressed");
            //document.getElementById().focus();
            $(".searchButton").click();
            return false;
        }
    });

    $(".textBoxUserId").on('keydown keypress', function (e) {
        alert("pressed");
        if (e.keyCode == 13) {
             alert("pressed");
            //document.getElementById().focus();
            $(".loginButton").click();
            return false;
        }
    });
    //alert("pressed");
    $(".textBoxPassword").on('keydown keypress', function (e) {
        alert("pressed");
        if (e.keyCode == 13) {
           
            $(".loginButton").click();
            return false;
        }
    });

    $(".textBoxCompleteName").on('keyup focus', function () {
        try {
            var dataValue = { documentId: $(".textBoxCompleteName").val() };

            $.ajax({
                type: "POST",
                url: "WebMethods.aspx/UniqueId",
                data: JSON.stringify(dataValue),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                // async:false,
                error: function (err) {

                    alert("Errort: " + err.responseText);
                },
                success: function (result) {
                    // alert(result.d);
                    $("#uniqueIdCheck").text($(".textBoxCompleteName").val() + "  " + result.d);
                    if (result.d.toString() == "true") {
                        $("#uniqueIdCheck").text("");
                    }
                    else {
                        $("#uniqueIdCheck").text("This name already exists. Make shure you don't refer to the same document.");
                        $("#uniqueIdCheck").css("color", "red");
                    }
                }
            });
        }
        catch (e) {
            alert("Error" + e.message);
        }
    });

    } catch (e) {
        alert(e.message);
    }
});

function setImagesEvent() {
    changeImage(1);

    countTimes = 1;

    $('.documentSlideshow img').click(function () {

        // alert("Changing 1");
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

    $('#imagesCollection img').click(function () {

        // alert("Changing 2");
        imageUrl = $(this).attr("src");
        imagesLengt = 0;

        $('#imagesCollection img').each(function (e) {
            imagesLengt++;

            if ($(this).attr("src") == imageUrl)
                countTimes = imagesLengt;
        });

        changeImage(countTimes);
    });
}

function SearchCategory(e) {
    //alert("pressed");
    if (e.keyCode == 13) {
        // alert("pressed");
        //document.getElementById().focus();
        $(".buttonSearchCategory").click();

        return false;
    }


    if ($(".textBoxSearchCategory").val() != "")
        // alert("here");
        try {
            var dataValue = { inputValue: $(".textBoxSearchCategory").val() };

            $.ajax({
                type: "POST",
                url: "AddNewCategory.aspx/FindCategoryParentOptions",
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
                    // alert(arrayTagNames);
                    // $('#debug1').text(arrayTagNames);
                    // $('#debug2').text(arrayTagId);

                    $(".textBoxSearchCategory").autocomplete({
                        source: arrayTagNames,
                        select: function (event, ui) {
                            var parentId = arrayTagId[$.inArray(ui.item.label, arrayTagNames)];
                            //$('#debug2').text(parentId);
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
}

function ShowImageLink(e) {

    $("#changeImageSource").css("display", "block");
};


function SearchCategoryInContainer(e) {

    var unicode = e.charCode;
    // typing = document.getElementById('textbox').value + String.fromCharCode(unicode);

    var input = $("#searchCategoryInContainer").val();// + String.fromCharCode(unicode);;
    if (input == "")
        $("#categoriesContainer div.categoriesContainerElement").each(function (e) {

            $(this).css("display", "block");
            // return false;
        });
    else
        $("#categoriesContainer div.categoriesContainerElement").each(function (e) {

            if ($(this).attr("class").toLowerCase().indexOf(input.toLowerCase()) > -1) {
                $(this).css("display", "block");

            }
            else {
                $(this).css("display", "none");
            }
        });

}

function SearchTagInContainer(e) {


    var input = $("#searchTagInContainer").val();
    if (input == "")
        $("#tagsContainer div.tagsContainerElement").each(function (e) {

            $(this).css("display", "block");
            // return false;
        });
    else
        $("#tagsContainer div.tagsContainerElement").each(function (e) {

            if ($(this).attr("class").toLowerCase().indexOf(input.toLowerCase()) > -1) {
                $(this).css("display", "block");

            }
            else {
                $(this).css("display", "none");
            }
        });

}

function SearchDocumentInContainer(e) {


    var input = $("#searchDocumentInContainer").val();
    if (input == "")
        $("#documentsContainer div.documentsContainerElement").each(function (e) {

            $(this).css("display", "block");
            // return false;
        });
    else
        $("#documentsContainer div.documentsContainerElement").each(function (e) {

            if ($(this).attr("class").toLowerCase().indexOf(input.toLowerCase()) > -1) {
                $(this).css("display", "block");

            }
            else {
                $(this).css("display", "none");
            }
        });

}

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
            //alert(JSON.stringify(data));
            if (data["totalItems"] != "0") {
                // $("#bookInfo").html(JSON.stringify(data));

                var book = data.items[0];

                //var title = (book["volumeInfo"]["title"]);

                // alert(title);

                $("#selectedBookImage").attr("src", book["volumeInfo"]["imageLinks"]["smallThumbnail"]);

                $("#selectedBookTitle").text(book["volumeInfo"]["title"]);
                $("#selectedBookAuthors").text(book["volumeInfo"]["authors"]);
                $("#selectedBookDescription").text(book["volumeInfo"]["description"]);
                $("#selectedBookPages").text("Pages: " + book["volumeInfo"]["pageCount"]);
                $("#selectedBookSelfLink").attr("href", book["accessInfo"]["webReaderLink"]);
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



}


function ImageLinkClick(e) {
    $('.imageCollection').each(function () {
        $(this).css({

            "border-width": "0px",

        });
    });
    $(e).css({
        "border-color": "#789",
        "border-left-width": "5px",
        "border-right-width": "5px",
        "border-bottom-width": "1px",
        "border-top-width": "1px",
        //"border-width": "5px",
        "border-style": "solid"
    });

    //src dupa care il sterg
    $("#hiddenSrcDelete").val($(e).attr("src"));

}





function ListBookSelected(e) {




    var title = $(e).find(':selected').text();
    var isbn = $(e).find(':selected').val();
    // alert(isbn);
    $("#hiddenIsbn").val(isbn);



    $.get("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn, function (data) {




        $("#booksOptions").html("");
        $("#booksOptions").css("display", "none");

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
    var authors = $("#selectedBookAuthors").text();


    // $("#bookSelectedBook").css("display", "block");
    if (CheckInListBox(isbn, ".listBoxBooks")) {
        // alert("good");

        var dataValue = { title: title, authors: authors, isbn: isbn, imageUrl: imageUrl, documentId: documentId };
        // alert(JSON.stringify(dataValue));
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "WebMethods.aspx/AddSelectedBook",
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

                    $('.listBoxBooks').append("<option value=" + isbn + ">" + $("#selectedBookTitle").text() + " - " + authors + "</option>");

                }

            }
        });

    }
    else {
        alert("already here");
    }
}

function AddAdditionalImage() {

    var documentId = $("#hiddenId").val();
    var imageUrl = $("#textBoxLinksImages").val();

    //if (!IsValidImageUrl2(imageUrl)) {
    //    $("#imageValidator").text("This url is not valid!");
    //    return false;
    //}

    $("#textBoxLinksImages").val("");
    //alert(imageUrl);
    var valid = true;
    $('#addedImages img').each(function (e) {
        if ($(this).attr("src") == imageUrl) {
            valid = false;
            //alert("already");
            return false;
        }
    });

    if (!valid) {
        $("#imageValidator").text("This image is already in collection!");
        return false;
    }





    var dataValue = { imageUrl: imageUrl, documentId: documentId };
    //alert(JSON.stringify(dataValue));
    //alert(document.getElementById('hiddenId').value);
    $.ajax({
        type: "POST",
        url: "WebMethods.aspx/AddAdditionalImage",
        data: JSON.stringify(dataValue),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        // async: false,
        error: function (err) {
            alert("Error: " + err.responseText);
        },
        success: function (result) {
            // alert("We returned: " + result.d);
            if (result.d == "Inserted") {

                $('#addedImages').append('<img class="imageCollection" src="' + imageUrl + '" />');

                $('.imageCollection').on("click", function () {
                    ImageLinkClick(this);
                });

            }

        }
    });


}


function DeleteAdditionalImage() {

    var documentId = $("#hiddenId").val();
    var imageUrl = $("#hiddenSrcDelete").val();

    var dataValue = { imageUrl: imageUrl, documentId: documentId };
    // alert(JSON.stringify(dataValue));
    //alert(document.getElementById('hiddenId').value);
    $.ajax({
        type: "POST",
        url: "WebMethods.aspx/DeleteAdditionalImage",
        data: JSON.stringify(dataValue),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        // async: false,
        error: function (err) {
            alert("Error: " + err.responseText);
        },
        success: function (result) {
            alert("We returned: " + result.d);
            if (result.d == "Deleted") {

                $('#addedImages img').each(function () {

                    if ($(this).attr("src") == imageUrl)
                        $(this).remove();
                });




            }

        }
    });


}

function myCallback(url, answer) {
    alert(url + ': ' + answer);
}

function IsValidImageUrl(url, callback) {
    $("<img>", {
        src: url,
        error: function () { callback(url, false); },
        load: function () { callback(url, true); }
    });
}

function IsValidImageUrl2(url) {
    var image = new Image();
    image.src = url;
    if (image.width == 0) {
        alert("no image");
        return false;
    }
    else return true;
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
        url: "WebMethods.aspx/RemoveSelectedBook",
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
            url: "WebMethods.aspx/UpVoteDocument",
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

function VoteDown(userId1) {

    //alert(userId1);

    try {
        var dataValue = { documentId: document.getElementById('hiddenId').value, userId: userId1  /*this.innerHTML*/ };
        // alert("asdg");
        //alert(document.getElementById('hiddenId').value);
        $.ajax({
            type: "POST",
            url: "WebMethods.aspx/DownVoteDocument",
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

    // alert("Changing");
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

function VerifyCategoryExistence(categoryName) {

    var dataValue = { inputValue: categoryName };
    var response;
    $.ajax({
        type: "POST",
        url: "AddNewCategory.aspx/VerifyCategoryExistence",
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

function ConfirmDelete() {
    //alert("asdf");
    if (confirm("Are you shure you want to delete this document?"))
    { }
    else return false;
}



function GetIndividualInfo(documentId) {

    if(DocumentExists(documentId)){
    var dataValue = { documentId: documentId };
    var response;
    $.ajax({
        type: "POST",
        url: "WebMethods.aspx/GetIndividualInfo",
        data: JSON.stringify(dataValue),
        //async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',

        error: function (err) {
            alert(err.responseText);
            console.log("A aparut o eroare: " + err.responseText);
            // return false;
        },
        success: function (result) {

            //alert(result.d);

            SetIndividualInfo(result.d);
}
    });


    }
}



function SetIndividualInfo(data) {
    // alert(data.toString());
    try {
        data = JSON.parse(data);
    } catch (e) {
        alert(e);
    }

    ResetFields();

    try {
        $(".labelName").text(data["name"].replace(',', ' ').replace('[', ' ').replace(']', ' '));
        $(".profileImage").attr("src", data["image"]);
        $(".labelDates").text(data["dates"]);




        //if (data["tags"] != null)
        //    $.each(data["tags"], function (key, tag) {
        //        $(".divTags").append("<a class=\"tagLinks\" runat=\"server\" >" + tag["tagName"] + "</a>  ");

        //    });


        if (data["categories"] != null)
            $.each(data["categories"], function (key, tag) {

                $(".divCategories").append("<a class=\"categoryLinks\" runat=\"server\" >" + tag["categoryName"] + "</a>  ");

            });
        SetCategoryListener();

        if (data["links"] != null)
            $.each(data["links"], function (key, link) {

                $("#additionalLinks").append("<br /><a href=\"" + link + "\">" + link + "<a/>");

            });





        $("#htmlInfo").html(data["htmlInformation"]);

        $(".labelViews").text("viewed " + data["timesViewed"] + " times");


        if (data["videos"] != null) {

            addPlayer(data["videos"].toString());

            $("#divNoVideo").css("display", "none");
        }
        else {
            $("#divNoVideo").css("display", "block");
        }



        if (data["images"] != null) {
            $.each(data["images"], function (key, image) {

                $("#documentSlideshow").append("<img  class=\"slideImage\" src=\"" + image + "\"/>");
                $("#imagesCollection").append("<img   class=\"imageCollection\" src=\"" + image + "\"/>");


            });
            setImagesEvent();
            $("#divNoImage").css("display", "none");
        }
        else {
            $("#divNoImage").css("display", "block");
        }


        if (data["books"] != null) {
            $.each(data["books"], function (key, book) {

                $("#booksContainer").append("<img  id=\"" + book["isbn"] + "\"  class=\"documentBook\" src=\"" + book["imageUrl"] + "\"/>");

            });

            $('.documentBook').on("click", function () {

                BookImageClick(this);
            });
            $("#divNoBook").css("display", "none");
        }
        else {
            $("#divNoBook").css("display", "block");
        }
    }
    catch (e) {
        alert(e.message);
    }

    $("#individualInfo").css("display", "block");
    $("#individualInfo").hide();
    //$("#individualInfo").slideDown();
    $("#individualInfo").fadeIn();
}

function ResetFields() {

    $(".divTags").html("");
    $(".divCategories").html("");
    $("#additionalLinks").html("");
    $("#imagesCollection").html("");
    $("#documentSlideshow").html("");
    $("#videosContainer").html("");
    $("#booksContainer").html("");
    $(".labelVote").html("");
    $("#htmlInfo").html("");

}

function SetCategoryListener() {
   
    $(".categoryLinks").click(function (e) {
        //alert("asdfd "+$(this).text());
        //alert(window.location.origin);
        window.location =window.location.origin+ "/Home.aspx?q=category:" + $(this).text();
    });
}

$(function () {

   

    $(".tagLinks").click(function (e) {

        UpdHidCriteria(this.innerHTML);

    });
    $(".linkContact").click(function (e) {

        try {
            var dataValue = { criteria: "persons" };

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

    $("#videoAdd").click(function () {
        addPlayer();
        return;
    });

});



// Loading youtube video !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


// 2. This code loads the IFrame Player API code asynchronously.
var tag = document.createElement('script');

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);


// 3. This function creates an <iframe> (and YouTube player)

var player;

//function onYouTubeIframeAPIReady() {
//    player = new YT.Player('player', {
//        height: '390',
//        width: '640',

//        videoId: 'VBEmqvVPOX4',
//       playerVars: { 'autoplay': 0, 'playlist': listVideos},
//        events: {
//            //'onReady': onPlayerReady,
//            // 'onStateChange': onPlayerStateChange
//        }
//    });


//}



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


function addPlayer(listVideos) {
    // alert(listVideos);

    $("#videosContainer").html("");
    $("#videosContainer").html("<div  id=\"player\"></div> ");
    player = new YT.Player('player', {
        height: '390',
        width: '640',

        //  videoId: '',
        playerVars: { 'autoplay': 0, 'playlist': listVideos },
        events: {
            //'onReady': onPlayerReady,
            // 'onStateChange': onPlayerStateChange
        }
    });

}