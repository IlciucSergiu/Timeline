<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryInfo.aspx.cs" Inherits="MyTimelineASPTry.CategoryInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="MySecondTry1/js/jquery-ui.js" type="text/javascript" charset="utf-8"></script>

</head>
<body>
    <script src="MySecondTry1/js/AddData.js"></script>
    <form id="form1" runat="server">
        <div id="header">
            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail" CssClass="linkMain" PostBackUrl="~/Home.aspx" />
        </div>

        <div id="pageBodyTagInfo">
            <h2 id="h2TagInfo">About : <asp:Label ID="labelCategory" runat="server" Text="Category"></asp:Label></h2> 

            <div id="containerCategoryDescription" runat="server"></div>
            <div id="containerCategoryInfo" runat="server"></div>
        </div>
    </form>
</body>
</html>
