<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewCategory.aspx.cs" Inherits="MyTimelineASPTry.AddNewCategory" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

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

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/WebFormTimeline.aspx" />
        </div>
        
    <div id="pageBody">
      <br />
        <p>To see all categories added until now click <a href="CategoriesMap.aspx">here</a></p>
        <asp:TextBox ID="textBoxCategoryName" runat="server" placeholder="Category name" Height="24px" Width="178px" CssClass="textBoxCategoryName" ></asp:TextBox><p id="verifyCategoryName" class="inline"></p> 
         <br />
       <p>Short description</p>
        <asp:TextBox ID="textBoxCategoryShortDescription" TextMode="MultiLine" runat="server" Height="86px" Width="279px"></asp:TextBox>
       
        
        <p>This category needs to be appended to a parent tag</p>
        <asp:TextBox ID="textBoxParentName" runat="server" Width="187px" placeholder="parent" CssClass="textBoxAddParentCategory" ></asp:TextBox><p id="verifyCategory" class="inline"> </p>
         <br />
        
        <asp:HiddenField ID="hiddenFieldParentCategoryId"  runat="server" Value="geoman" />
         
        
        
        <p>How significant is this category to the parent category </p>
        <p><small>On a scale from 1 to 100</small></p>
       
        <p>
        
            <input id="textBoxRelativeImportance" type="number" runat="server" max="100" min="1" style="width: 72px" /><br />
        </p>
        

        <p>Add synonyms for this category, these will be usefull for searching.(separate with ';')</p>
         <asp:TextBox ID="textBoxSynonyms" runat="server" Width="363px"></asp:TextBox>
        
         <br />
        
        <hr />
       
        <br />
        <h3>Add more information about this category</h3>
       
     <div id="ckEditor">
            <CKEditor:CKEditorControl ID="CKEditorCategoryInformation" BasePath="/ckeditor/" runat="server" Height="350" Width="1000"></CKEditor:CKEditorControl>
        </div>
        <br />
        <br />
        <asp:Button ID="buttonCreateCategory" runat="server" OnClick="buttonCreateCategory_Click" Text="Create category" />
       
    
    </div>
        <br />
    </form>
    <script>
        function UpdHidId(inputValue) {
          
            $('#' + '<%=hiddenFieldParentCategoryId.ClientID %>').val(inputValue);
           
        }

    </script>

</body>
</html>
