<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormTimeline.aspx.cs" Inherits="MyTimelineASPTry.WebFormTimeline" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>MyTry</title>
    <link rel="stylesheet" href="MySecondTry1/css/jquery-ui-1.10.3.custom.css" type="text/css" charset="utf-8" />
    <link rel="stylesheet" href="MySecondTry1/timeglider/Timeglider.css" type="text/css" charset="utf-8" />
    <link rel="stylesheet" href="MySecondTry1/timeglider/timeglider.datepicker.css" type="text/css" charset="utf-8" />
    <link rel="stylesheet" type="text/css" href="MySecondTry1/css/MyTimeline.css" charset="utf-8" />
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />



</head>
<body>
    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/testSergiu.js" type="text/javascript" charset="utf-8"></script>
    <h1>MyTry</h1>
    <form runat="server">
        <asp:Button ID="buttonAddData" runat="server" Text="Login" OnClick="buttonAddData_Click" Width="88px" />
        <asp:Button ID="buttonLoadTimeline" runat="server" Text="Load timeline" OnClick="buttonLoadTimeline_Click" /><br />
        <br />
        <div id='placement' style="height: 400px"></div>



        
        <script>

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

            });

            $(function () {
                $('div').click(function (e) {
                   // $("div").off("click");
                    // setTimeout(function () { $("div").on("click"); }, 1000);

                   // $(e.target.className).off("click");

                    //$("div").css("pointer-events", "none");

                   // setTimeout(function () { $("div").css("pointer-events", "auto"); }, 1000);
                    
                   
                });
                var firstClick = false; var secondClick = false; var theId;
                $('div').mousedown(function (e) {
                    if (e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner" || e.target.className == "tg-event-hoverline")
                    {
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
                        
                        // alert(theId);
                        
                   // if ( e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner" || e.target.className == "tg-event-hoverline") {
                        
                       // var eventId = $(e.target).closest('div[id]').attr("id");
                      //  eventId = eventId.replace("_modal", "");
                      //  alert(eventId);
                      //  e.stopPropagation();
                    }
                    //  if (e.target.className == "timeglider-event-title" || "timeglider-event-spanner")
                    //   {   

                    //$('#<% =hiddenLabel.ClientID %>').text(e.target.parentElement.id);
                    //      var child = e.target.id;
                    //      var parent = e.target.parentElement;
                    //        var grandParent = parent.parentElement.id;


                    //       var parent2 = parent.id.replace("_modal", "");
                    //       alert(child+"///"+parent.id + "----" + parent2 + "----" + grandParent);
                    //     }
                });

                //var element = e.target;


                // if(element.hasClass("timeglider-timeline-event") || element.hasClass("timeglider-event-spanning"))
                //  aler("it has");
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

        <br />
        <asp:Button ID="buttonCreate" runat="server" OnClick="buttonCreate_Click" Text="Creaza JSON" />
        <br />
        <br />



        <asp:TextBox ID="textBoxId" runat="server"></asp:TextBox>
        <asp:Button ID="buttonSearchId" runat="server" Text="Search Id" OnClick="buttonSearchId_Click" />

        <asp:HiddenField ID="hiddenId" runat="server" />

        <asp:Label ID="hiddenLabel" runat="server" Text="mozart"></asp:Label>
        <div id="individualInfo">

            <div id="divEssentialInfo" class="imageInline">
                <asp:Image ID="imageProfile" runat="server" Height="209px" Width="183px" />
            </div>
            <div class="imageInline" id="labelsInfo">
                <asp:Label ID="labelName" runat="server" Text="Name" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelDates" runat="server" Text="Dates" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelProfession" runat="server" Text="Profession" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelNationality" runat="server" Text="Nationality" CssClass="essentialLabels" Width="300px"></asp:Label><br />
                <asp:Label ID="labelReligion" runat="server" Text="Religion" CssClass="essentialLabels" Width="300px"></asp:Label><br />



            </div>
            <br />
            <div id="afterImage">
                <div id="htmlInfo" runat="server">
                    html info
                </div>

                <div id="additionalResources" runat="server">
                </div>

                <div id="additionalLinks" runat="server">
                </div>
            </div>

        </div>


    </form>


</body>

</html>
