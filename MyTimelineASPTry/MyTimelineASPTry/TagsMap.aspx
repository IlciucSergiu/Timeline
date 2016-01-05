<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TagsMap.aspx.cs" Inherits="MyTimelineASPTry.TagsMap"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
    <title></title>
</head>

<body>
    
    <form id="form1" runat="server">
        <div id="header">

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/WebFormTimeline.aspx" />
        </div>
    <div>
    <asp:treeview runat="server" ID="treeViewTagsMap" ></asp:treeview>
    </div>
    </form>
</body>
</html>
