<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoriesMap.aspx.cs" Inherits="MyTimelineASPTry.CategoriesMap" %>

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

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail" CssClass="linkMain" PostBackUrl="~/Home.aspx" />
        </div>
        <div id="pageBody">
            <div id="searchCategory">
                <br />
                <asp:TextBox ID="textBoxSearchCategory" runat="server" Height="22px" Width="210px" placeholder="Search a category by name"></asp:TextBox>
              
                <asp:Button ID="buttonSearchCategory" runat="server" Text="Search" Width="72px" OnClick="buttonSearchCategory_Click" />
            
            <br /><br />
            </div>

             
            <asp:Label ID="labelCategoryName" runat="server" Text="Category Name" Font-Size="Large"></asp:Label>
                <a id="linkCategoryInfo" runat="server" >info</a>
               <br /><br />

             <a id="linkParentCategory" runat="server" >Parent</a>
            <br />
            <div>
                <asp:TreeView runat="server" ID="treeViewCategoriesMap" PopulateNodesFromClient="True"></asp:TreeView>
            </div>
        </div>
    </form>
</body>
</html>
