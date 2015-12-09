<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddData.aspx.cs" Inherits="MyTimelineASPTry.AddData" Async="true" ViewStateMode="Enabled" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyTimeline</title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="MySecondTry1/css/MyTimeline.css" type="text/css" />
    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/jquery-ui.js" type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
</head>
<body>
    <form runat="server" id="formMainForm">
        
    <div id="header">
           
        <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" Width="224px" AlternateText="The name" CssClass="linkMain" PostBackUrl="~/WebFormTimeline.aspx" />
           
        </div>
        
        <div id="divAddEssentials" runat="server">

          <!--  <p>
                &nbsp;&nbsp; First name:&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="firstName" runat="server"></asp:TextBox>
            </p> -->
            <p>
                Complete name:&nbsp;&nbsp;
            <asp:TextBox ID="textBoxCompleteName" runat="server" Width="244px" Height="21px"></asp:TextBox>
            </p>



            <div id="datesPosition">
                <p>
                    Date of birth:
                <input type="text" class="datepicker" id="dateBirth" runat="server" placeholder="yyyy-mm-dd" />
                </p>
                &nbsp;&nbsp;&nbsp;
         <p>
             Date of death
             <input type="text" class="datepicker" id="dateDeath" runat="server"  placeholder="yyyy-mm-dd"/>
         </p>
                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="checkBoxContemporary" runat="server" Text="Contemporary" OnCheckedChanged="checkBoxContemporary_CheckedChanged"  /><br />
                <br />
            </div>
            <asp:Label ID="labelImportance" runat="server" Text="Importance"></asp:Label>&nbsp;&nbsp;
        <input id="inputImportance" type="number" runat="server" max="100" min="0" style="width: 72px" /><br />
            <br />


            <div id="notEssential">
                <div id="gender">
                    <p>
                        *Gender:&nbsp;&nbsp;<asp:RadioButtonList ID="RadioButtonListGender" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </p>
                </div>
                <p>
                    &nbsp;*Profession :
                <asp:TextBox ID="textBoxProfession" runat="server"></asp:TextBox>
                    <p>
                        &nbsp;*Nationality :
                    <asp:TextBox ID="textBoxNationality" runat="server"></asp:TextBox>
                        <p>
                            &nbsp;*Religion :
                        <asp:TextBox ID="textBoxReligion" runat="server"></asp:TextBox>
                        </p>
            </div>


            <asp:Label ID="labelDescription" runat="server" Text="Description"></asp:Label><br />
            <asp:TextBox ID="textBoxDescription" TextMode="MultiLine" runat="server" Height="121px" Width="353px"></asp:TextBox><br />

            <asp:Label ID="labelImage" runat="server" Text="Link to image"></asp:Label><br />
            <asp:TextBox ID="textBoxImage" runat="server" Width="345px"></asp:TextBox><br />

            <asp:Label ID="labelLink" runat="server" Text="Link to aditional resources"></asp:Label><br />
            <asp:TextBox ID="textBoxLink" runat="server" Width="345px"></asp:TextBox>
            <br />
            <br />


            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" OnClick="buttonCancel_Click" CssClass="essentialButtons"/>
            <asp:Button ID="buttonSubmit" runat="server" Text="Save" OnClick="buttonSubmit_Click" Width="82px" CssClass="essentialButtons"/>


            <asp:Button ID="buttonModify" runat="server" Text="Modify" CssClass="essentialButtons" OnClick="buttonModify_Click" Width="90px"/>


            <br />



            <br />

            <script>
                $(function () {
                    $(".datepicker").datepicker({
                        showOn: "button",
                        buttonImage:"MySecondTry1/timeglider/img/calendar_16.png",
                        buttonImageOnly: true,
                        buttonText: "Select date",
                        changeYear: true,
                        changeMonth: true,
                        yearRange: "1:c"
                    });
                    $(".datepicker").datepicker("option", "dateFormat", "yy-mm-dd");

                });

                $(document).ready(function () {
                    if ('<%=setDate%>' == "True") {
                        $("#dateBirth").datepicker("setDate", '<%=dateBirth.Value.ToString()%>');
                        $("#dateDeath").datepicker("setDate", '<%=dateDeath.Value.ToString()%>');
                        //alert('<%=dateBirth.Value%>' + "" + '<%=dateDeath.Value%>');
                        //alert('<%=setDate%>');
                    }
                    if ('<%=showEssential%>' == "True")
                    {
                        $("#divAddEssentials").css("display", "block");
                        $("#divMainInfo").css("display", "none");
                    }
                    else {
                        $("#divAddEssentials").css("display", "none");
                        $("#divMainInfo").css("display", "block");
                        
                    }
                });
               
               // $(document).ready(function () {
                 //   $(document).dblclick(function () {
                        // alert(ckEditor.textContent);
                 //       alert($(ckEditor).toString());
                        //alert(ckEditor.id);
                //    });
               // });
                
            </script>
        </div>








        <div id="divMainInfo" runat="server">
            <br />
            <asp:TextBox ID="textBoxId" runat="server" Width="128px" Visible="False"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Search ID" OnClick="Button1_Click" Visible="False" />
            <br />

            <div id="divEssentialInfo" class="imageInline">
                <asp:Image ID="imageProfile" runat="server" Height="177px" Width="151px" />
            </div>
            <div class="imageInline" id="labelsInfo">
                <asp:Label ID="labelName" runat="server" Text="Name" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelDates" runat="server" Text="Dates" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelProfession" runat="server" Text="Profession" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelNationality" runat="server" Text="Nationality" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelReligion" runat="server" Text="Religion" CssClass="essentialLabels" Width="300px"></asp:Label><br />

                <asp:LinkButton ID="linkButtonEdit" runat="server" CssClass="linkLabel" OnClick="linkButtonEdit_Click"  OnClientClick="setDate()">edit</asp:LinkButton>

            </div>
            <br />

        
        <div id="ckEditor">
            <CKEditor:CKEditorControl ID="CKEditorInformation" BasePath="/ckeditor/" runat="server" Height="350" Width="1000" placeholder="Importance"></CKEditor:CKEditorControl>
        </div>

            <asp:HiddenField ID="hiddenFieldCk" runat="server" />
            &nbsp;</p>
            <div id="addTags">
                <asp:LinkButton ID="linkButtonAddTag" runat="server">Add tag</asp:LinkButton>
                <br />
               <br />
                 <asp:TextBox ID="textBoxTagName" runat="server" placeholder="Tag name"></asp:TextBox> 
               -
                <input id="inputImportanceTag" runat="server" max="100" min="0" style="width: 52px" type="number" />&nbsp;<asp:Button ID="buttonAddTag" runat="server" Text="Add" Width="49px" OnClick="buttonAddTag_Click" />
                <asp:Table ID="tableTags" runat="server" Height="52px" Width="80px" Visible="False">

                </asp:Table>
               <br />
                 <asp:ListBox ID="listBoxTags" runat="server" Height="67px" Width="161px"></asp:ListBox>
                <br />
            </div>
        <p>
           <!-- <asp:LinkButton ID="LinkButton2" runat="server">Add additional resources</asp:LinkButton> -->
            <asp:Label ID="labelAddResources" runat="server" Text="Add additional resources"></asp:Label>
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server"> Add book </asp:LinkButton>

            
        </p>
            <p>
            
            Title : 
            <asp:TextBox ID="textBoxAddBooks" runat="server" Width="147px"></asp:TextBox>

               &nbsp; <asp:Button ID="buttonAddBook" runat="server" Text="Add book" OnClick="buttonAddBook_Click" />

            
        </p>
        <p>

        
          <!--  <asp:LinkButton ID="linkButtonAddLink" runat="server" OnClientClick="showAddLink();return false" >Add links to external resources</asp:LinkButton> -->
        
            <asp:ListBox ID="listBoxBooks" runat="server"  Width="185px"></asp:ListBox>
        
        </p>
            <p>

        
            <asp:Label ID="labelAddLinks" runat="server" Text="Add links to external resources"></asp:Label>
        
        </p>
            <div id="addLinks" >
                <asp:TextBox ID="textBoxAddLink" runat="server" Width="256px"></asp:TextBox>
                <asp:Button ID="buttonAddLink" runat="server" Text="Add" OnClick="buttonAddLink_Click" Width="74px"/>
            </div>
            <p>

            <asp:ListBox ID="listBoxLinks" runat="server" Height="53px" Width="300px"></asp:ListBox>
            </p>
            <asp:Button ID="buttomSaveChanges" runat="server" Text="Save" OnClick="buttomSaveChanges_Click" Width="83px" />
        </div>
        
        <script>
            

           function showAddLink()
            {
               $("#addLinks").css("display", "block");
            }

        </script>
            
    </form>


</body>
</html>
