<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpUser.aspx.cs" Inherits="MyTimelineASPTry.SignUpUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
</head>
<body>
    <form id="form1"  autocomplete="off" method="post" runat="server">
        <div id="header">
           
        <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" Width="224px" AlternateText="Time Trail" CssClass="linkMain" PostBackUrl="~/Home.aspx" />
           
        </div>
        <script>
            $(function () {
              $("#bigTitle").click(function(){
     alert('Handler for .click() called.')
   });
                });

        </script>
        <div id="signUpForm">


            <asp:TextBox ID="textBoxFirstName" runat="server" Height="23px" Width="158px" placeholder="First name" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;
        <asp:TextBox ID="textBoxLastName" runat="server" Height="23px" Width="158px" placeholder="Last name" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox><br />
            <br />
            <asp:TextBox ID="textBoxPassword" runat="server" Height="23px" Width="167px" placeholder="Password" AutoCompleteType="Disabled" autocomplete="off" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="textBoxPasswordVerify" runat="server" Height="23px" Width="167px" placeholder="Password again" TextMode="Password" autocomplete="off"></asp:TextBox><asp:Label ID="labelPasswordValidation" runat="server" ForeColor="Red" Text="Password" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="textBoxEmail" runat="server" Height="23px" Width="258px" placeholder="Email"></asp:TextBox>
            &nbsp;<br /> 
            <asp:Label ID="labelEmailValidation" runat="server" ForeColor="Red" Text="Email" Visible="False"></asp:Label>
            <br />
            <asp:RadioButtonList ID="radioButtonListGender" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="labelInvalid" runat="server" ForeColor="Red" Text="Validity" Visible="False"></asp:Label>
            <br />

            <asp:LinkButton ID="linkButtonLogin" runat="server" CssClass="loginLink" PostBackUrl="~/LoginUser.aspx">Login</asp:LinkButton>

            <asp:Button ID="buttonCreateButton" runat="server" Text="Create account" Height="33px" Width="110px" OnClick="Button1_Click" CssClass="buttonCreateAccount" />
        </div>
    </form>
</body>
</html>
