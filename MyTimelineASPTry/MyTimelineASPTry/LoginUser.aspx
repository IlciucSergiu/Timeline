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
        
        
        <asp:Label ID="Label1" runat="server" Text="User name" CssClass="labelLoginUsername"></asp:Label>
        
         <asp:TextBox ID="textBoxSearchId" runat="server" CssClass="loginTextBox" Width="150px"></asp:TextBox> 
        <br />
        <asp:Label ID="labelEmailVerification" runat="server" CssClass="loginLabel" ForeColor="Red" Text="Email verif" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password     " CssClass="loginLabel"></asp:Label>
        <asp:TextBox ID="textBoxPassword" runat="server" CssClass="loginTextBox" Width="152px" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="labelPasswordVerification" runat="server" CssClass="loginLabel" ForeColor="Red" Text="Password verif" Visible="False"></asp:Label>
        <br /><br />
        <asp:LinkButton ID="linkButtonSignUp" runat="server" CssClass="signUpLink" PostBackUrl="~/SignUpUser.aspx">Create account</asp:LinkButton>
        <asp:Button ID="buttonSearchId" runat="server" Text="SearchID" OnClick="buttonSearchId_Click"  CssClass="loginButton"/>
    </div>
    </form>
</body>
</html>
