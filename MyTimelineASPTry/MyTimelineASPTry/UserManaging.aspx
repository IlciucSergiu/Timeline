<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManaging.aspx.cs" Inherits="MyTimelineASPTry.UserManaging"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="textBoxSearchId" runat="server"></asp:TextBox>
        <asp:Button ID="buttonSearchId" runat="server" Text="SearchID" OnClick="buttonSearchId_Click" /><br /><br />
        

        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        
        <br />
        <asp:ListBox ID="listBoxOwns" runat="server" Height="131px" Width="132px" AutoPostBack="True" OnSelectedIndexChanged="listBoxOwns_SelectedIndexChanged" ViewStateMode="Enabled"></asp:ListBox><br /><br />
    
        <asp:Button ID="buttonEdit" runat="server" Text="Edit" CssClass="userManButton" Enabled="False" OnClick="buttonEdit_Click" Width="67px" />
        <asp:Button ID="buttonCreate" runat="server" Text="Create new" CssClass="userManButton" Width="81px" OnClick="buttonCreate_Click" Enabled="False" />
    
    </div>
    </form>
</body>
</html>
