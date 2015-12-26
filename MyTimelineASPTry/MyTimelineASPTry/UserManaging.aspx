<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManaging.aspx.cs" Inherits="MyTimelineASPTry.UserManaging"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />
</head>
<body>
    
    <form id="form1" runat="server">
        <div id="header">
           
        <asp:ImageButton ID="ImageButton1" runat="server" Height="43px" Width="210px" AlternateText="Time Trail" OnClick="ImageButton1_Click"  CssClass="linkMain"/>
        </div>
    <div>
    
        <asp:TextBox ID="textBoxSearchId" runat="server" Visible="False"></asp:TextBox>
        <asp:Button ID="buttonSearchId" runat="server" Text="SearchID" OnClick="buttonSearchId_Click" Visible="False" /><br /><br />
        
        <div id="documentsManaging">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        
        <br />
        <asp:ListBox ID="listBoxOwns" runat="server" Height="131px" Width="132px" OnSelectedIndexChanged="listBoxOwns_SelectedIndexChanged"></asp:ListBox>
        <br /><br />
    
        <asp:Button ID="buttonEdit" runat="server" Text="Edit" CssClass="userManButton" OnClick="buttonEdit_Click" Width="67px" />
        &nbsp;
        <asp:Button ID="buttonCreate" runat="server" Text="Create new" CssClass="userManButton" Width="81px" OnClick="buttonCreate_Click" />
    </div>
        <hr />

        <div id="tagsManaging">
            <h3>Managing tags</h3>
            <h3>
                <asp:ListBox ID="listBoxTags" runat="server" Height="73px" Width="128px"></asp:ListBox>
            </h3>
            
        <!--<asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton> -->
            <div id="tagsManagingButton">
            <asp:Button ID="buttonEditTag" runat="server" Text="Edit tag" CssClass="tagButton" OnClick="buttonEditTag_Click" />
            
            <asp:Button ID="buttonCreateTag" runat="server" Text="Create tag" CssClass="tagButton" OnClick="buttonCreateTag_Click" />
                </div>
        </div>
    </div>
    </form>
</body>
</html>
