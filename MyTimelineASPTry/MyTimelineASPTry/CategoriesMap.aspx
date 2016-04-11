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
      <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
   <script src="MySecondTry1/js/jquery-ui.js" type="text/javascript" charset="utf-8"></script>

    <form id="form1" runat="server">
       <div id="header">

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/Home.aspx" TabIndex="2" />
        </div>

        <div id="pageBody">
            <div id="searchCategory">
                <br />
                <asp:TextBox ID="textBoxSearchCategory" CssClass="textBoxSearchCategory" onkeypress="return SearchCategory(event); " runat="server" Height="22px" Width="210px" placeholder="Search a category by name"></asp:TextBox>
              <asp:HiddenField ID="hiddenFieldParentCategoryId" runat="server" />
        <script>
        function UpdHidId(inputValue) {
          
            $('#' + '<%=hiddenFieldParentCategoryId.ClientID %>').val(inputValue);
           
        }

    </script>
                <asp:Button ID="buttonSearchCategory" CssClass="buttonSearchCategory" runat="server" Text="Search" Width="72px" OnClick="buttonSearchCategory_Click" TabIndex="1" />
            
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
