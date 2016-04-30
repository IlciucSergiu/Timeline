<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginUser.aspx.cs" Inherits="MyTimelineASPTry.SignUser" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
</head>
<body>
      <script src="MySecondTry1/js/AddData.js"></script>
    <form id="form1" runat="server" defaultbutton="buttonSearchId">
       
        <div id="header">
           
      <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" Width="224px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/Home.aspx" TabIndex="2" /> 
           
        </div>
    <div id="loginDiv">
        
        
        <asp:Label ID="Label1" runat="server" Text="User name" CssClass="labelLoginUsername"></asp:Label>
        
         <asp:TextBox ID="textBoxSearchId" runat="server" CssClass="loginTextBox textBoxUserId" Width="150px"></asp:TextBox> 
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password     " CssClass="loginLabel"></asp:Label>
        <asp:TextBox ID="textBoxPassword" runat="server" CssClass="loginTextBox textBoxPassword" Width="152px" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="labelEmailVerification" runat="server" CssClass="loginLabel" ForeColor="Red" Text="Email verif" Visible="False"></asp:Label>
        <br />
        <asp:LinkButton ID="linkButtonSignUp" runat="server" CssClass="signUpLink" PostBackUrl="~/SignUpUser.aspx">Create account</asp:LinkButton>
        <asp:Button ID="buttonSearchId" runat="server" Text="Login" OnClick="buttonSearchId_Click"  CssClass="loginButton" ForeColor="Black" Width="77px"/>
   
       
         </div>
    </form>
</body>
</html>
