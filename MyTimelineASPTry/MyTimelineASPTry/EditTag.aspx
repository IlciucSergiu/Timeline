<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTag.aspx.cs" Inherits="MyTimelineASPTry.EditTag" Async="true" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail" CssClass="linkMain" PostBackUrl="~/WebFormTimeline.aspx" />
        </div>
    <div id="pageBody">
    <br />
        <p>To see all tags added until now click <a href="TagsMap.aspx">here</a></p>
        <asp:TextBox ID="textBoxTagName" runat="server" placeholder="Tag name" Height="24px" Width="178px"></asp:TextBox>
         <br />
        <br />
        <p>This tag needs to be appended to a parent tag</p>
        <asp:TextBox ID="textBoxParentName" runat="server" Width="187px" placeholder="parent"></asp:TextBox>
         <br />
         <asp:HiddenField ID="hiddenFieldParentTagId" runat="server" />
        <p>How significant is this tag to the parent tag </p>
        <p><small>On a scale from 1 to 100</small></p>
        <p>
        
            <input id="textBoxRelativeImportance" type="number" runat="server" max="99" min="1" style="width: 72px" /><br />
        </p>
        
         <br />
        <p>Short description</p>
        <asp:TextBox ID="textBoxTagShortDescription" TextMode="MultiLine" runat="server" Height="86px" Width="279px"></asp:TextBox>
       
        <hr />
       
        <br />
        <h3>Add more information about this tag</h3>
       
     <div id="ckEditor">
            <CKEditor:CKEditorControl ID="CKEditorInformation" BasePath="/ckeditor/" runat="server" Height="350" Width="1000" placeholder="Importance"></CKEditor:CKEditorControl>
        </div>
        <br />
        <br />
        <asp:Button ID="buttonSaveTagChanges" runat="server" Text="Save changes" OnClick="buttonSaveTagChanges_Click" />
       
    
        &nbsp; &nbsp;<asp:Button ID="buttonDeleteTag" runat="server" Text="Delete" OnClick="buttonDeleteTag_Click" />
       
    
    </div>
    </form>
</body>
</html>
