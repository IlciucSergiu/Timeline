<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewTag.aspx.cs" Inherits="MyTimelineASPTry.AddNewTag" %>
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

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/Home.aspx" />
        </div>
        
    <div id="pageBody">
      <br />
        <p>To see all tags added until now click <a href="TagsMap.aspx">here</a></p>
        <asp:TextBox ID="textBoxTagName" runat="server" placeholder="Tag name" Height="24px" Width="178px" CssClass="textBoxTagName" ></asp:TextBox><p id="verifyTagName" class="inline"></p> 
         <br />
       <p>Short description</p>
        <asp:TextBox ID="textBoxTagShortDescription" TextMode="MultiLine" runat="server" Height="86px" Width="279px"></asp:TextBox>
       
        
        <p>This tag needs to be appended to a parent tag</p>
        <asp:TextBox ID="textBoxParentName" runat="server" Width="187px" placeholder="parent" CssClass="textBoxAddParentTag" ></asp:TextBox><p id="verifyTag" class="inline"> </p>
         <br />
        
        <asp:HiddenField ID="hiddenFieldParentTagId"  runat="server" Value="geoman" />
         
        
        
        <p>How significant is this tag to the parent tag </p>
        <p><small>On a scale from 1 to 100</small></p>
       
        <p>
        
            <input id="textBoxRelativeImportance" type="number" runat="server" max="100" min="1" style="width: 72px" /><br />
        </p>
        

        <p>Add synonyms for this tag, these will be usefull for searching.(separate with ';')</p>
         <asp:TextBox ID="textBoxSynonyms" runat="server" Width="363px"></asp:TextBox>
        
         <br />
        
        <hr />
       
        <br />
        <h3>Add more information about this tag</h3>
       
     <div id="ckEditor">
            <CKEditor:CKEditorControl ID="CKEditorInformation" BasePath="/ckeditor/" runat="server" Height="350" Width="1000" placeholder="Importance"></CKEditor:CKEditorControl>
        </div>
        <br />
        <br />
        <asp:Button ID="buttonCreateTag" runat="server" OnClick="buttonCreateTag_Click" Text="Create tag" />
       
    
    </div>
        <br />
    </form>

    <script>
        function UpdHidId(inputValue) {
          
            $('#' + '<%=hiddenFieldParentTagId.ClientID %>').val(inputValue);
           
        }

    </script>
</body>
</html>
