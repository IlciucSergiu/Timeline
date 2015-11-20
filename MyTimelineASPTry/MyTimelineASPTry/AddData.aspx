﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddData.aspx.cs" Inherits="MyTimelineASPTry.AddData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyTimeline</title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="MySecondTry1/css/MyTimeline.css" type="text/css"/>
  <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script> 
  <script src="MySecondTry1/js/jquery-ui.js" type="text/javascript" charset="utf-8"></script>
  <link rel="stylesheet" href="/resources/demos/style.css" />
</head>
<body>

    <form id="form1" runat="server" >
    <div>
    
    </div>
        <p>&nbsp;&nbsp; First name:&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="firstName" runat="server"></asp:TextBox></p>
        <p>Last name(s):&nbsp;&nbsp; <asp:TextBox ID="lastName" runat="server"></asp:TextBox></p>
        
       
        
        <div id="datesPosition">
         <p>Date of birth: <input type="text" class="datepicker" id="dateBirth" runat="server"/></p>&nbsp;&nbsp;&nbsp;
         <p>   Date of death <input type="text" class="datepicker" id="dateDeath" runat="server"/></p>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" Text="Contemporary" /><br /><br />
         </div>
         <asp:Label ID="labelImportance" runat="server" Text="Importance"></asp:Label>&nbsp;&nbsp;
        <input id="inputImportance" type="number" runat ="server" max="100" min="0" placeholder="importance" style="width: 72px" /><br /><br />
        

        <div id="notEssential">
        <div id="gender">
        <p>Gender:&nbsp;&nbsp;</p>
           <asp:RadioButtonList ID="RadioButtonListGender"  runat="server">
               <asp:ListItem>Male</asp:ListItem> 
                <asp:ListItem>Female</asp:ListItem>
            </asp:RadioButtonList>
            </div>
            <p>&nbsp;Profession : <asp:TextBox ID="textBoxProfession" runat="server"></asp:TextBox>
            <p>&nbsp;Nationality : <asp:TextBox ID="textBoxNationality" runat="server"></asp:TextBox>
         <p>&nbsp;Religion : <asp:TextBox ID="textBoxReligion" runat="server"></asp:TextBox>
        </p>
            </div>


        <asp:Label ID="labelDescription" runat="server" Text="Description"></asp:Label><br />
        <asp:TextBox ID="textBoxDescription" TextMode="MultiLine" runat="server" Height="121px" Width="353px"></asp:TextBox><br />
       
        <asp:Label ID="labelImage" runat="server" Text="Link to image"></asp:Label><br />
        <asp:TextBox ID="textBoxImage" runat="server" Width="345px"></asp:TextBox><br/>

        <asp:Label ID="labelLink" runat="server" Text="Link to aditional resources"></asp:Label><br />
        <asp:TextBox ID="textBoxLink" runat="server" Width="345px"></asp:TextBox><br />  
        
        
        <br />
       
         
  
        <asp:Button id="buttonSubmit" runat="server" Text="Submit" OnClick="buttonSubmit_Click" />
       
         
        <br />

        <script>
            $(function () {
                $(".datepicker").datepicker({
                    changeYear: true,
                    changeMonth: true,
                    yearRange: "1:c"
                });
                $(".datepicker").datepicker("option", "dateFormat", "yy-mm-dd");
                
            });
  </script>
       
         
  
    </form>
</body>
</html>