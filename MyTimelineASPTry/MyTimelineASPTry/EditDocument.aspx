<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDocument.aspx.cs" Inherits="MyTimelineASPTry.AddData" Async="true" ViewStateMode="Enabled" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TimeTrail</title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="MySecondTry1/css/MyTimeline.css" type="text/css" />
    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/jquery-ui.js" type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="MySecondTry1/js/AddData.js"></script>


</head>
<body>
    <form runat="server" id="formMainForm">

        <asp:ScriptManager ID="ScriptManagerMain"
            runat="server"
            EnablePageMethods="true"
            ScriptMode="Release"
            LoadScriptsBeforeUI="true">
        </asp:ScriptManager>

        <div id="header">

            <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" Width="224px" AlternateText="Time Trail" CssClass="linkMain" PostBackUrl="~/Home.aspx" />

        </div>

        <div id="pageBody">
            <div id="divAddEssentials" runat="server">


                <p>
                    Complete name:&nbsp;&nbsp;
            <asp:TextBox ID="textBoxCompleteName" runat="server" Width="244px" Height="21px"></asp:TextBox>
                </p>



                <div id="datesPosition">
                    <p>
                        Start date : 
                        <select id="startDateEra" runat="server">
                            <option value="AD"></option>
                            <option value="BC"></option>
                        </select>
                        <input type="text" id="startDatePicker" runat="server" placeholder="yyyy-mm-dd" />
                    </p>
                    &nbsp;&nbsp;&nbsp;
         <p>
             End date :
             <select id="endDateEra" runat="server">
                 <option value="AD"></option>
                 <option value="BC"></option>
             </select>
             <input type="text" id="endDatePicker" runat="server" placeholder="yyyy-mm-dd" />
         </p>
                    &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="checkBoxContemporary" runat="server" Text="Contemporary" class="checkContemporary" /><br />
                    <br />
                </div>

                <asp:Label ID="labelDescription" runat="server" Text="Description"></asp:Label><br />
                <asp:TextBox ID="textBoxDescription" TextMode="MultiLine" runat="server" Height="121px" Width="353px"></asp:TextBox><br />

                <asp:Label ID="labelImage" runat="server" Text="Link to image"></asp:Label><br />
                <asp:TextBox ID="textBoxImage" runat="server" Width="345px" CssClass="textBoxImageLink"></asp:TextBox>
                <br />
                <asp:Image ID="imageDocument" runat="server" CssClass="documentImage profileImage" />
                <br />

                <asp:Label ID="labelLink" runat="server" Text="Link to main source &quot;usualy Wikipedia&quot; "></asp:Label><br />
                <asp:TextBox ID="textBoxLink" runat="server" Width="345px"></asp:TextBox>
                <br />
                <br />



                <script>

                    <%--  $(document).ready(function () {
                        //alert("is ready");
                        if ('<%=setDate%>' == "True") {
                            $("#startDatePicker").datepicker("setDate", '<%=startDatePicker.Value.ToString()%>');
                            $("#endDatePicker").datepicker("setDate", '<%=endDatePicker.Value.ToString()%>');
                           
                        }
                   


                    });--%>




                    //try {
                   <%-- function UpdHidTag(listString1) {

                        $('#' + '<%=hiddenFieldTags.ClientID %>').val(listString1);

                        return false;
                    }--%>

                    function UpdHidLink(listString1) {
                        //alert(listString1);
                        $('#' + '<%=hiddenFieldLinks.ClientID %>').val(listString1);
                        alert("In hidden: " + $('#' + '<%=hiddenFieldLinks.ClientID %>').val());
                        return false;
                    }

                </script>

            </div>

            <div id="divMainInfo" runat="server">
                <br />
                <br />
            </div>

            <div id="divDocumentFeedback">
                <h2 id="feedbackShow">Feedback
                    <asp:Label ID="labelFeedbackNumber" runat="server" Text="(0)"></asp:Label></h2>
                <div id="feedbackContent" runat="server" class="hide">
                </div>
            </div>

            <div id="ckEditor">
                <CKEditor:CKEditorControl ID="CKEditorInformation" BasePath="/ckeditor/" runat="server" Height="350" Width="1000"></CKEditor:CKEditorControl>
            </div>

            <asp:HiddenField ID="hiddenFieldCk" runat="server" />
            &nbsp;
           
        
            
           <h1>Add aditional resources</h1>



            <hr />
            <h2>Add video's</h2>
            <p>Insert id of a youtube video or the entire link</p>
            <asp:TextBox ID="textBoxVideoId" runat="server" Width="296px"></asp:TextBox>

            <hr />
            <h2>Add images</h2>
            <p>Insert links to additional images</p>
            <asp:TextBox ID="textBoxLinksImages" runat="server" Width="296px"></asp:TextBox>
            <input id="hiddenSrcDelete" type="hidden" />
            <input id="buttonAddImage" type="button" value="Add image" onclick="AddAdditionalImage()" />
            <input id="buttonDeleteImage" type="button" value="Delete" onclick="DeleteAdditionalImage()" /><p id="imageValidator"></p>
            <br />
            <div id="addedImages" class="flexImages" runat="server">
            </div>
            <br />

            <hr />
            <h2>Add books</h2>
            <asp:TextBox ID="textBoxAddBooks" runat="server" Width="199px" CssClass="textBoxBook" Height="23px"></asp:TextBox>
            <input id="buttonSearchBook" type="button" value="Search book" />&nbsp;<br />
            <br />


            <div class="addBook">
                <div id="booksOptions"></div>
                <br />
                <asp:ListBox ID="listBoxBooks" runat="server" Width="264px" Height="129px" CssClass="listBoxBooks" ViewStateMode="Enabled"></asp:ListBox>

                <div id="divAddBook" class="addBook">
                    |<div id="bookSelectedBook" class="hide">

                        <img id="selectedBookImage" src="#" class="selectedBookInfo" />
                        <input id="hiddenIsbn" type="hidden" />
                        <div id="selectedBookInfo" class="selectedBookInfo">
                            <p id="selectedBookTitle"></p>
                            <p id="selectedBookAuthors"></p>
                            <p id="selectedBookDescription"></p>
                            <p id="selectedBookPages"></p>
                            <input id="buttonAddThisBook" type="button" value="Add this book" onclick="AddThisBook()" />
                            <input id="buttonRemoveBook" type="button" value="Remove book" onclick="RemoveThisBook()" /><br />
                        </div>
                    </div>
                    <p id="bookInfo"></p>
                </div>
            </div>


            <hr />
            <h2>Add links</h2>
            <div id="addLinks">
                <asp:TextBox ID="textBoxAddLink" runat="server" Width="256px" CssClass="textBoxAddLink"></asp:TextBox>
                <asp:Button ID="buttonAddLink" runat="server" Text="Add" OnClick="buttonAddLink_Click" Width="74px" OnClientClick="AddLinkItem(); return false;" />&nbsp;&nbsp;
                <input id="buttonRemoveLinks" type="button" value="Remove" />
            </div>
            <p>

                <asp:ListBox ID="listBoxLinks" runat="server" Height="53px" Width="300px" CssClass="listBoxLinks"></asp:ListBox>
                <asp:HiddenField ID="hiddenFieldLinks" runat="server" />
            </p>


            <hr />
            <h2>Add tags</h2>
            <div id="addTags">

                <p>To see all tags check <a href="TagsMap.aspx">tag map</a></p>
                <asp:TextBox ID="textBoxTagName" runat="server" placeholder="Tag name" CssClass="textBoxAddTag"></asp:TextBox>
                -
                <input id="inputImportanceTag" runat="server" max="100" min="0" style="width: 52px" type="number" />&nbsp;
                <asp:Button ID="buttonAddTag" runat="server" Text="Add" Width="49px" OnClientClick=" AddTagItem(); return false;" />

                &nbsp;&nbsp;
                 <input id="buttonRemoveTags" type="button" value="Remove" />
                <asp:Label ID="labelInfo" runat="server" Text="" CssClass="verifyTag" ForeColor="Red"></asp:Label>

                <br />

                <asp:ListBox ID="listBoxTags" runat="server" Height="67px" Width="161px" CssClass="listBoxTags"></asp:ListBox>
                <br />
            </div>

            <br />
            <br />

            <hr />
            <h2>Add categories</h2>
            <div id="addCategories">
                <input id="hidden1" type="hidden" runat="server" />
                <p>To see all categories check <a href="CategoriesMap.aspx">category map</a></p>
                <asp:TextBox ID="textBoxCategoryName" runat="server" placeholder="Category name" CssClass="textBoxAddCategory"></asp:TextBox>

                <input id="inputImportanceCategory" runat="server" max="100" min="0" style="width: 52px" type="number" />&nbsp;
                <asp:Button ID="buttonAddCategory" runat="server" Text="Add" Width="49px" OnClientClick=" AddCategoryItem(); return false;" />

                &nbsp;&nbsp;
                 <input id="buttonRemoveCategories" type="button" value="Remove" onclick=" RemoveCategoryItem(); return false;" />
                <asp:Label ID="labelCategoryInfo" runat="server" Text="" CssClass="verifyCategory" ForeColor="Red"></asp:Label>

                <br />

                <asp:ListBox ID="listBoxCategories" runat="server" Height="67px" Width="161px" CssClass="listBoxCategories"></asp:ListBox>
                <br />
            </div>

            <br />
            <br />
            <input id="hiddenId" type="hidden" runat="server" />
            
            <asp:Button ID="buttomSaveChanges" runat="server" Text="Save" OnClick="buttomSaveChanges_Click" Width="83px" />
            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" OnClick="buttonCancel_Click" CssClass="essentialButtons" />



            <script>


                function showAddLink() {
                    $("#addLinks").css("display", "block");
                }

            </script>
        </div>

    </form>
    <br />

</body>
</html>
