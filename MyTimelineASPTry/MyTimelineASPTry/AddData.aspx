﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddData.aspx.cs" Inherits="MyTimelineASPTry.AddData" Async="true" ViewStateMode="Enabled" %>

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
        
         <asp:ScriptManager ID="ScriptManagerMain"
            runat="server"
            EnablePageMethods="true" 
            ScriptMode="Release" 
            LoadScriptsBeforeUI="true">
    </asp:ScriptManager>

    <div id="header">
           
        <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" Width="224px" AlternateText="Time Trail" CssClass="linkMain" PostBackUrl="~/WebFormTimeline.aspx" />
           
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
                  Start date :
                <input type="text" class="datepicker" id="dateBirth" runat="server" placeholder="yyyy-mm-dd" />
                </p>
                &nbsp;&nbsp;&nbsp;
         <p >
             End date :
             <input type="text" class="datepicker" id="dateDeath" runat="server"  placeholder="yyyy-mm-dd"/>
         </p>
                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="checkBoxContemporary" runat="server" Text="Contemporary" class="checkContemporary" /><br />
                <br />
            </div>
          
            
            <br />

             <div id="addTags">

                <br />
                 <input id="hiddenId" type="hidden" runat="server"/>
               <br />
                 <asp:TextBox ID="textBoxTagName" runat="server" placeholder="Tag name" CssClass="textBoxAddTag"></asp:TextBox> 
               -
                <input id="inputImportanceTag" runat="server" max="100" min="0" style="width: 52px" type="number" />&nbsp;<asp:Button ID="buttonAddTag" runat="server" Text="Add" Width="49px" OnClientClick=" AddTagItem(); return false;"    />
                
                  &nbsp;&nbsp;
                 <input id="buttonRemoveTags" type="button" value="Remove" /> <asp:Label ID="labelInfo" runat="server" Text="" CssClass="verifyTag" ForeColor="Red"></asp:Label> <!-- <p id="verifyTag" ></p> -->
                 <br /> 
                  <asp:HiddenField ID="hiddenFieldTags" runat="server" />
                 <asp:ListBox ID="listBoxTags" runat="server" Height="67px" Width="161px" CssClass="listBoxTags"></asp:ListBox>
                <br />
            </div>
            <br />


            


            <asp:Label ID="labelDescription" runat="server" Text="Description"></asp:Label><br />
            <asp:TextBox ID="textBoxDescription" TextMode="MultiLine" runat="server" Height="121px" Width="353px"></asp:TextBox><br />

            <asp:Label ID="labelImage" runat="server" Text="Link to image"></asp:Label><br />
            <asp:TextBox ID="textBoxImage" runat="server" Width="345px" CssClass="textBoxImageLink"></asp:TextBox>
            <br />
            <asp:Image ID="imageDocument" runat="server" CssClass="documentImage profileImage" />
            <br />

            <asp:Label ID="labelLink" runat="server" Text="Link to aditional resources"></asp:Label><br />
            <asp:TextBox ID="textBoxLink" runat="server" Width="345px"></asp:TextBox>
            <br />
            <br />


            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" OnClick="buttonCancel_Click" CssClass="essentialButtons"/>
            <asp:Button ID="buttonSubmit" runat="server" Text="Save" OnClick="buttonSubmit_Click" Width="82px" CssClass="essentialButtons"/>


            <asp:Button ID="buttonModify" runat="server" Text="Modify" CssClass="essentialButtons" OnClick="buttonModify_Click" Width="90px"/>


            <br />



            <br />
            <script src="MySecondTry1/js/AddData.js"></script>

            <script>
         
                $(document).ready(function () {
                    //alert("is ready");
                    if ('<%=setDate%>' == "True") {
                        $("#dateBirth").datepicker("setDate", '<%=dateBirth.Value.ToString()%>');
                        $("#dateDeath").datepicker("setDate", '<%=dateDeath.Value.ToString()%>');
                        //alert('<%=dateBirth.Value%>' + "" + '<%=dateDeath.Value%>');
                        //alert('<%=setDate%>');
                    }
                    if ('<%=showEssential%>' == "True") {
                        $("#divAddEssentials").css("display", "block");
                        $("#divMainInfo").css("display", "none");
                    }
                    else {
                        $("#divAddEssentials").css("display", "none");
                        $("#divMainInfo").css("display", "block");

                    }


                });
                //try {
                function UpdHidTag(listString1) {
                   
                    $('#' + '<%=hiddenFieldTags.ClientID %>').val(listString1);
                   
                         return false;
                }

                function UpdHidLink(listString1) {
                    //alert(listString1);
                    $('#' + '<%=hiddenFieldLinks.ClientID %>').val(listString1);
                  alert("In hidden: " + $('#' + '<%=hiddenFieldLinks.ClientID %>').val());
                    return false;
                }
               // }
                //catch (err) {
                //    alert(err.message);
               // }
            </script>
             
        </div>








        <div id="divMainInfo" runat="server">
            <br />
            <asp:TextBox ID="textBoxId" runat="server" Width="128px" Visible="False"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Search ID" OnClick="Button1_Click" Visible="False" />
            <br />

            <div id="divEssentialInfo" class="imageInline">
                <asp:Image ID="imageProfile" runat="server" CssClass="imageProfile" />
            </div>
            <div class="imageInline" id="labelsInfo">
                <asp:Label ID="labelName" runat="server" Text="Name" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelDates" runat="server" Text="Dates" CssClass="essentialLabels" Width="300px"></asp:Label><br />
               

                <asp:LinkButton ID="linkButtonEdit" runat="server" CssClass="linkLabel" OnClick="linkButtonEdit_Click"  OnClientClick="setDate()">edit</asp:LinkButton>

            </div>
            <br /><br /><br /><br />
            
            <div id="divDocumentFeedback" >
                <h2 id="feedbackShow">Feedback <asp:Label ID="labelFeedbackNumber" runat="server" Text="(0)"></asp:Label></h2>
                <div id="feedbackContent" runat="server" class="hide">

                </div>
            </div>
        
        <div id="ckEditor">
            <CKEditor:CKEditorControl ID="CKEditorInformation" BasePath="/ckeditor/" runat="server" Height="350" Width="1000" ></CKEditor:CKEditorControl>
        </div>

            <asp:HiddenField ID="hiddenFieldCk" runat="server" />
            &nbsp;
           
        <p>
           <!-- <asp:LinkButton ID="LinkButton2" runat="server">Add additional resources</asp:LinkButton> -->
            <asp:Label ID="labelAddResources" runat="server" Text="Add additional resources"></asp:Label>
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server"> Add book </asp:LinkButton>

            
        </p>

            <p>Insert id of a youtube video or the entire link</p>
            <asp:TextBox ID="textBoxVideoId" runat="server" Width="296px" ></asp:TextBox>
            

                <p>Insert links of additional images</p>
            <asp:TextBox ID="textBoxLinksImages" runat="server" Width="296px" ></asp:TextBox>
            <p>
            
            Title : 
            <asp:TextBox ID="textBoxAddBooks" runat="server" Width="147px"></asp:TextBox>

               &nbsp; <asp:Button ID="buttonAddBook" runat="server" Text="Add book" OnClick="buttonAddBook_Click"  />

            
        </p>
        <p>

        
          <!--  <asp:LinkButton ID="linkButtonAddLink" runat="server" OnClientClick="showAddLink();return false" >Add links to external resources</asp:LinkButton> -->
        
            <asp:ListBox ID="listBoxBooks" runat="server"  Width="185px"></asp:ListBox>
        
        </p>
            <p>

        
            <asp:Label ID="labelAddLinks" runat="server" Text="Add links to external resources"></asp:Label>
        
        </p>
            <div id="addLinks" >
                <asp:TextBox ID="textBoxAddLink" runat="server" Width="256px" CssClass="textBoxAddLink"></asp:TextBox>
                <asp:Button ID="buttonAddLink" runat="server" Text="Add" OnClick="buttonAddLink_Click" Width="74px" OnClientClick="AddLinkItem(); return false;"/>&nbsp;&nbsp;
                <input id="buttonRemoveLinks" type="button" value="Remove" /></div>
            <p>

            <asp:ListBox ID="listBoxLinks" runat="server" Height="53px" Width="300px" CssClass="listBoxLinks"></asp:ListBox>
                <asp:HiddenField ID="hiddenFieldLinks" runat="server"  />
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
    <br />

</body>
</html>
