<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoriesMap.aspx.cs" Inherits="MyTimelineASPTry.CategoriesMap"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
</head>
<body>
     <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/AddData.js"></script>



    <form id="form1" runat="server">
        <div id="header">

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/Home.aspx" />
        </div>
       
    <div>
    <asp:treeview runat="server" ID="treeViewCategoriesMap" PopulateNodesFromClient="True"></asp:treeview>
    </div>
    </form>
</body>
</html>
