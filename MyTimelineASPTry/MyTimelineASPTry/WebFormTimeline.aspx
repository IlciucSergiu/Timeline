<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormTimeline.aspx.cs" Inherits="MyTimelineASPTry.WebFormTimeline" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>MyTry</title>
    <link rel="stylesheet" href="MySecondTry1/css/jquery-ui-1.10.3.custom.css" type="text/css"  />
    <link rel="stylesheet" href="MySecondTry1/timeglider/Timeglider.css" type="text/css"  />
    <link rel="stylesheet" href="MySecondTry1/timeglider/timeglider.datepicker.css" type="text/css"  />
    <link rel="stylesheet" type="text/css" href="MySecondTry1/css/MyTimeline.css"  />
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />



   


</head>
<body id="background">
    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/testSergiu.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/AddData.js"></script>

    <form runat="server" id="mainForm">
        <div id="header">
           <!-- <h1 class="inHeader" id="bigTitle" >MyTry</h1> -->
                       
        <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" Width="224px" AlternateText="Time Trail"  CssClass="linkMain" PostBackUrl="~/WebFormTimeline.aspx" />
           
        
            <asp:TextBox ID="textBoxSearchQuery" runat="server" CssClass="inHeader textBoxSearchQuery" Height="22px" Width="300px" BorderStyle="None" placeholder="search for events, persons or tags" ></asp:TextBox>
            <asp:Button ID="buttonSearchQuery" runat="server" Text="Search" CssClass="inHeader searchButton" OnClick="buttonSearchQuery_Click"/>
            <asp:Button ID="buttonLogin" runat="server" Text="Login" OnClick="buttonLogin_Click" Width="88px" CssClass="inHeader" UseSubmitBehavior="False" />
            <asp:Button ID="buttonWorkspace" runat="server" Text="Workspace" CssClass="inHeader" PostBackUrl="~/UserManaging.aspx" Visible="False" UseSubmitBehavior="False" />
            <asp:Button ID="buttonLoadTimeline" runat="server" Text="Load timeline" OnClick="buttonLoadTimeline_Click" CssClass="inHeader hideButton" />
             <asp:LinkButton ID="linkButtonLogout" runat="server" CssClass="linkLogout" OnClick="linkButtonLogout_Click" Visible="False">Logout</asp:LinkButton >
            
            
            
            
           
            
            
            
            
            
        </div>
       
        
        
        <div id='placement' style="height: 400px"></div>

            


        <script>

            function UpdHidCriteria(criteria) {
               // alert(criteria);
                $('#' + '<%=hiddenFieldCriteria.ClientID %>').val(criteria);
                document.getElementById('<%=buttonSearchQuery.ClientID %>').click();
                return false;
            }

            var jsonString = '<%=jsonData%>';

            $(document).ready(function () {


                if (jsonString != "") {
                    //alert(jsonString);
                    var jsonData = JSON.parse(jsonString);

                    var tg1 = $("#placement").timeline({

                        "data_source": jsonData, //"MySecondTry1/json/sergiu3.json",
                        "min_zoom": 15,
                        "max_zoom": 60
                    }
                );
                }
                 if ('<%=showIndividual%>' == "True") 
                $("#individualInfo").css("display", "block");
            });

            $(function () {
                $("#linkVoteUp").click(function (e) {

                    if('<%= Session["userId"] %>' != "")
                        VoteUp('<%= Session["userId"] %>');

                });
                var firstClick = false; var secondClick = false; var theId;
                $('div').mousedown(function (e) {
                    if (e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner" || e.target.className == "tg-event-hoverline") {
                        firstClick = true;
                        var eventId = $(e.target).closest('div[id]').attr("id");
                        eventId = eventId.replace("_modal", "");
                        // alert(eventId);
                        theId = eventId;
                        e.stopPropagation();
                        $("div").css("pointer-events", "none");
                        secondClick = false;
                        
                        setTimeout(function () {

                            $("div").css("pointer-events", "auto");
                            if (secondClick == false) {
                                // alert("should of");
                                $(e.target).trigger("click");
                            }
                            firstClick = false;
                        }, 501);
                    }
                });
                $(document).dblclick(function (e) {

                    if (firstClick) {
                        secondClick = true;
                        document.getElementById('hiddenId').value = theId;
                       // SearchPersonalInfo(theId);

                        // alert(divId);
                        
                       document.getElementById("buttonSearchId").click();
                        $("#individualInfo").css("display", "block");
                        // alert(theId);

                        // if ( e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner" || e.target.className == "tg-event-hoverline") {

                        // var eventId = $(e.target).closest('div[id]').attr("id");
                        //  eventId = eventId.replace("_modal", "");
                        //  alert(eventId);
                        //  e.stopPropagation();
                    }
                    
                });

                
            });

        </script>


        <script src="MySecondTry1/js/jquery-ui.js" type="text/javascript" charset="utf-8"></script>

        <script src="MySecondTry1/js/underscore-min.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/backbone-min.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/json2.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/jquery.tmpl.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/ba-tinyPubSub.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/jquery.mousewheel.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/jquery.ui.ipad.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/globalize.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/js/ba-debug.min.js" type="text/javascript" charset="utf-8"></script>

        <script src="MySecondTry1/timeglider/TG_Date.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/timeglider/TG_Org.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/timeglider/TG_Timeline.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/timeglider/TG_TimelineView.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/timeglider/TG_Mediator.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/timeglider/timeglider.timeline.widget.js" type="text/javascript" charset="utf-8"></script>
        <script src="MySecondTry1/timeglider/timeglider.datepicker.js" type="text/javascript" charset="utf-8"></script>

        <asp:Label ID="labelTime" runat="server" Text="Time" Visible="False"></asp:Label>

        <br />
        <asp:Button ID="buttonCreate" runat="server" OnClick="buttonCreate_Click" Text="Creaza JSON" Visible="False" CssClass="hideButton" />
        <asp:Button ID="buttonSearchId" runat="server" Text="Search Id" OnClick="buttonSearchId_Click" CssClass="hideButton" />

        <asp:HiddenField ID="hiddenId" runat="server" />


        <asp:ScriptManager ID="ScriptManagerMain"
            runat="server"
            EnablePageMethods="true" 
            ScriptMode="Release" 
            LoadScriptsBeforeUI="true">
    </asp:ScriptManager>


        <asp:HiddenField ID="hiddenFieldCriteria" runat="server" />


        <div id="individualInfo">

            <div id="divEssentialInfo" class="imageInline">
                <asp:Image ID="imageProfile" runat="server" Height="209px" Width="183px" />
            </div>
            <div class="imageInline" id="labelsInfo">
                <asp:Label ID="labelName" runat="server" CssClass="essentialLabels" Width="300px" Font-Size="X-Large"></asp:Label><br />
                <asp:Label ID="labelDates" runat="server" Text="" CssClass="essentialLabels" Width="300px"></asp:Label><br />
               
                <br />
                <a id="linkVoteUp" href="#" class="linkVoteUp">vote up</a>
               &nbsp;&nbsp; <a href="#" class="linkVoteDown">vote down</a><br/>
                <asp:Label ID="labelVote" runat="server" Text="voting status" CssClass="hide labelVote"></asp:Label>
                <br />
                <div id="divTags" runat="server" class="essentialLabels">

                </div>



            </div>
            <br />
            <div id="afterImage">
                <div id="htmlInfo" runat="server" >
                </div>

                <h3>Additional resources</h3>
                <hr />
                <div id="additionalResources" runat="server">
                    
                </div>

                <h3>Related pages</h3>
                <hr />
                <div id="additionalLinks" runat="server">
                    
                </div>
                <asp:Label ID="labelNumberOfViews" runat="server" Text="Views"></asp:Label>
            </div>

        </div>

        <div id="footer">
            <br />
            <asp:LinkButton ID="linkButtonContact" runat="server" CssClass="inFooter linkContact" OnClientClick="return false;">Contact</asp:LinkButton>
            <asp:LinkButton ID="linkButtonCopyright" runat="server" CssClass="inFooter">Copyright</asp:LinkButton>
            <asp:LinkButton ID="linkButtonTermsAndConditions" runat="server" CssClass="inFooter">Terms and conditions</asp:LinkButton>
        </div>
    </form>


</body>

</html>
