<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManaging.aspx.cs" Inherits="MyTimelineASPTry.UserManaging"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="textBoxSearchId" runat="server"></asp:TextBox>
        <asp:Button ID="buttonSearchId" runat="server" Text="SearchID" OnClick="buttonSearchId_Click" /><br /><br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        
        <br />
        <asp:ListBox ID="listBoxOwns" runat="server"></asp:ListBox>
    
    </div>
    </form>
</body>
</html>
