<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginUser.aspx.cs" Inherits="MyTimelineASPTry.SignUser" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

    <div id="loginDiv">
        
        
        <asp:Label ID="Label1" runat="server" Text="User name" CssClass="loginLabel"></asp:Label>
        
         <asp:TextBox ID="textBoxSearchId" runat="server" CssClass="loginTextBox" Width="150px"></asp:TextBox> 
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password     " CssClass="loginLabel"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="loginTextBox" Width="152px"></asp:TextBox>
        <br /><br />
        <asp:Button ID="buttonSearchId" runat="server" Text="SearchID" OnClick="buttonSearchId_Click"  CssClass="loginButton"/>
    </div>
    </form>
</body>
</html>
