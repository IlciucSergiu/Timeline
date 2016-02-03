<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManaging.aspx.cs" Inherits="MyTimelineASPTry.UserManaging" Async="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />


</head>
<body>
    <script src="MySecondTry1/js/AddData.js"></script>
    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <form id="form1" runat="server">

        <script>
            $(function () {
                $('.userManagingTab').click(function (e) {

                    var tabName = $(this).text();
                    //alert($(this).text());
                    $('.tabsContainer').each(function (i, obj) {
                        // alert($(this).attr('class'));
                        if ($(this).hasClass(tabName))
                            $(this).css("display", "block");
                        else
                            $(this).css("display", "none");
                    });


                });


                if (getParameterByName('tab') == "tags") {
                    $('.tabsContainer').each(function (i, obj) {
                        // alert($(this).attr('class'));
                        if ($(this).hasClass("Tags"))
                            $(this).css("display", "block");
                        else
                            $(this).css("display", "none");
                    });
                }


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





            });
        </script>
        <div id="header">

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail" OnClick="ImageButton1_Click" CssClass="linkMain" PostBackUrl="~/Home.aspx" />
        </div>

        <div id="pageBody">

            <asp:Button ID="buttonSearchId" runat="server" Text="SearchID" Visible="False" CssClass="hide" /><br />
            <br />


            <div id="profile" class="inlineProfile">
                <asp:Label ID="LabelUsername" runat="server" Text="Username" Font-Size="X-Large" CssClass="usernameLabel"></asp:Label><br />
                <br />

                <img id="profileImage" class="profileImage userManagingTab" runat="server" src="#" />
                <br />
                &nbsp;&nbsp;&nbsp;<asp:Label ID="labelReputation" runat="server" Text="Reputation"></asp:Label>

            </div>

            <div id="nearProfile" class="inlineProfile">
                <div id="tabs">
                    <a class="userManagingTab">Profile</a>
                    <a class="userManagingTab">Documents</a>
                    <a class="userManagingTab">Tags</a>
                    <a class="userManagingTab">Categories</a>
                    <a class="userManagingTab">Other</a>
                </div>

                <div id="documentsManaging" class="tabsContainer Documents">
                    <h2>All your documents &nbsp; &nbsp;<asp:Label ID="labelNumeberOfDocuments" runat="server" Text="()"></asp:Label>
                        <asp:Button ID="buttonCreate" runat="server" Text="Create new" CssClass="userManButton " OnClick="buttonCreate_Click" /></h2>


                    <div id="documentsContainer" runat="server" class="elementsContainer">
                    </div>

                </div>

                <div id="tagsManaging" class="tabsContainer Tags hide">

                    <h2>All your tags &nbsp; &nbsp;<asp:Label ID="labelNumberOfTags" runat="server" Text="()"></asp:Label>
                        <asp:Button ID="buttonCreateTag" runat="server" Text="Create tag" CssClass="userManButton" OnClick="buttonCreateTag_Click" /></h2>
                    <div id="tagsContainer" runat="server" class="elementsContainer">
                    </div>

                </div>


                <div id="categoriesManaging" class="tabsContainer Categories hide">

                    <h2>All your categories &nbsp; &nbsp;<asp:Label ID="labelNumberOfCategories" runat="server" Text="()"></asp:Label>
                        <asp:Button ID="buttonCreateCategory" runat="server" Text="Create category" CssClass="userManButton" OnClick="buttonCreateCategory_Click" /></h2>
                    <div id="categoriesContainer" runat="server" class="elementsContainer">
                    </div>

                </div>

                <div id="profileManaging" class="tabsContainer Profile hide">

                    <h2>Edit your profile </h2>

                    <div id="profileContent" runat="server">
                        <img id="profileImageEdit" class="profileImageEdit" runat="server" src="#" /><br />
                        <a id="changeProfileImage" onclick="ShowImageLink();">Change image</a>

                        <div id="changeImageSource" class="hide">
                            <p>Change the image source </p>
                            <asp:TextBox ID="textBoxProfileImage" runat="server" Width="220px"></asp:TextBox>
                        </div>


                        <br />
                        <h2>About me</h2>
                        <div id="ckEditor">
                            <CKEditor:CKEditorControl ID="CKEditorProfileInfo" BasePath="/ckeditor/" runat="server" Height="350" Width="1000"></CKEditor:CKEditorControl>
                        </div>
                        <br />
                        <asp:Button ID="buttonSaveChanges" runat="server" Text="Save changes" OnClick="buttonSaveChanges_Click" />
                    </div>

                </div>

            </div>

        </div>

        <br />
        <br />
        <br />
        
    </form>
</body>
</html>
