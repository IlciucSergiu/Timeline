<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewDocument.aspx.cs" Inherits="MyTimelineASPTry.AddNewDocument" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="MySecondTry1/css/MyTimeline.css" type="text/css" />
    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/jquery-ui.js" type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="MySecondTry1/js/AddData.js"></script>
</head>
<body>


    <form id="form1" runat="server">
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

                <h2>Here you add the main informations about the subject. The rest you will add later. </h2>
                <p>
                    Complete name:&nbsp;&nbsp;
            <asp:TextBox ID="textBoxCompleteName" runat="server" Width="244px" Height="21px" CssClass="textBoxCompleteName"></asp:TextBox>
                </p>
                <p id="uniqueIdCheck"></p>



                <div id="datesPosition">
                    <p>
                        Start date : 
                        <select id="startDateEra" runat="server">
                            <option value="AD"></option>
                            <option value="BC"></option>
                        </select>
                        <input type="text" class="datepicker" id="startDatePicker" runat="server" placeholder="yyyy-mm-dd" />
                    </p>
                    &nbsp;&nbsp;&nbsp;
         <p>
             End date :
             <select id="endDateEra" runat="server">
                            <option value="AD"></option>
                            <option value="BC"></option>
                        </select>
             <input type="text" class="datepicker" id="endDatePicker" runat="server" placeholder="yyyy-mm-dd" />
         </p>
                    &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="checkBoxContemporary" runat="server" Text="continuing" class="checkContemporary" /><br />
                    <br />
                </div>


                <br />

                <!-- <div id="addTags">

                    <br />
                    <input id="hiddenId" type="hidden" runat="server" />
                    <p>To see all tags check <a href="TagsMap.aspx">tag map</a></p>
                    <asp:TextBox ID="textBoxTagName" runat="server" placeholder="Tag name" CssClass="textBoxAddTag"></asp:TextBox>
                    -
                <input id="inputImportanceTag" runat="server" max="100" min="0" style="width: 52px" type="number" />&nbsp;<asp:Button ID="buttonAddTag" runat="server" Text="Add" Width="49px" OnClientClick=" AddTagItem(); return false;" />

                    &nbsp;&nbsp;
                 <input id="buttonRemoveTags" type="button" value="Remove" />
                    <asp:Label ID="labelInfo" runat="server" Text="" CssClass="verifyTag" ForeColor="Red"></asp:Label>
                     <p id="verifyTag" ></p> 
                    <br />
                    <asp:HiddenField ID="hiddenFieldTags" runat="server" />
                    <asp:ListBox ID="listBoxTags" runat="server" Height="67px" Width="161px" CssClass="listBoxTags"></asp:ListBox>
                    <br />
                </div>
                <br />
                -->




                <asp:Label ID="labelDescription" runat="server" Text="Short description"></asp:Label><br />
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

                 <asp:Button ID="buttonSubmit" runat="server" Text="Save" OnClick="buttonSubmit_Click" Width="82px" CssClass="essentialButtons" />

                <asp:Button ID="buttonCancel" runat="server" Text="Cancel" OnClick="buttonCancel_Click" CssClass="essentialButtons" />
               
            </div>
        </div>
    </form>
</body>
</html>
