<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailVerification.aspx.cs" Inherits="MyTimelineASPTry.EmailVerification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
         <div id="header">

            <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/WebFormTimeline.aspx" />
        </div>

    <div id="pageBody">
     <h2>This email is not verified yet! Check your email to get the key or link. </h2>

         <asp:TextBox ID="textBoxVerifcation" runat="server" Height="22px" Width="263px" placeholder ="activation key"></asp:TextBox>
       
        
        <asp:Button ID="buttonCheckKey" runat="server" Text="Check" OnClick="buttonCheckKey_Click" />
        
       
        <h3>You can also <a>resend email.</a></h3>
    </div>

    </form>
</body>
</html>
