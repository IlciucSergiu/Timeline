<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MyTimelineASPTry.WebFormTimeline" Async="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>TimeTrail</title>
    <link rel="stylesheet" href="MySecondTry1/css/jquery-ui-1.10.3.custom.css" type="text/css" />
    <link rel="stylesheet" href="MySecondTry1/timeglider/Timeglider.css" type="text/css" />
    <link rel="stylesheet" href="MySecondTry1/timeglider/timeglider.datepicker.css" type="text/css" />
    <link href="MySecondTry1/css/MyTimeline.css" rel="stylesheet" />






</head>

<body id="background">

    <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="MySecondTry1/js/AddData.js"></script>
    <form runat="server" id="mainForm">
        <div id="header">


            <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" Width="224px" AlternateText="Time Trail" CssClass="linkMain" PostBackUrl="~/Home.aspx" />


            <asp:TextBox ID="textBoxSearchQuery" runat="server" CssClass="inHeader textBoxSearchQuery" Height="22px" Width="300px" BorderStyle="None" placeholder="search for events, persons or tags"></asp:TextBox>
            <asp:Button ID="buttonSearchQuery" runat="server" Text="Search" CssClass="inHeader searchButton" OnClick="buttonSearchQuery_Click" />
            <asp:Button ID="buttonLogin" runat="server" Text="Login" OnClick="buttonLogin_Click" Width="88px" CssClass="inHeader" UseSubmitBehavior="False" />
            <asp:Button ID="buttonWorkspace" runat="server" Text="Workspace" CssClass="inHeader" PostBackUrl="~/UserManaging.aspx" Visible="False" UseSubmitBehavior="False" />
            <asp:Button ID="buttonLoadTimeline" runat="server" Text="Load timeline" OnClick="buttonLoadTimeline_Click" CssClass="inHeader hideButton" />
            <asp:LinkButton ID="linkButtonLogout" runat="server" CssClass="linkLogout" OnClick="linkButtonLogout_Click" Visible="False">Logout</asp:LinkButton>
            <asp:Button ID="buttonSearchTag" runat="server" Text="Search Tag" OnClick="buttonSearchTag_Click" Style="display: none" />

        </div>



        <div id='placement' style="height: 400px"></div>






        <script>

            // Can also be used with $(document).ready()


            $(document).ready(function () {


                jsonString = '<%=jsonData%>';

                if (jsonString != "") {
                    // alert(jsonString);




                    tg1 = $("#placement").timeline({

                        "data_source": JSON.parse(jsonString),
                        "min_zoom": 15,
                        "max_zoom": 60
                    }
                        );



                }

               

                if ('<%=showIndividual%>' == "True")
                    $("#individualInfo").css("display", "block");

            });

            function UpdHidCriteria(criteria) {
                // alert(criteria);
                $('#' + '<%=hiddenFieldCriteria.ClientID %>').val(criteria);
                document.getElementById('<%=buttonSearchTag.ClientID %>').click();
                return false;
            }

            var tg1;



            $(function () {
               
                $("#linkVoteUp").click(function (e) {
                    //the user id is visible but only if logged in
                    if ('<%= Session["userId"] %>' != "")
                         VoteUp('<%= Session["userId"] %>');
        else {
            $(".labelVote").css("display", "block");
            $(".labelVote").css("color", "red");
            $(".labelVote").text("you need an acount to vote");
        }


                 });

                $("#linkVoteDown").click(function (e) {
                    //the user id is visible but only if logged in
                    if ('<%= Session["userId"] %>' != "")
            VoteDown('<%= Session["userId"] %>');
        else {
            $(".labelVote").css("display", "block");
            $(".labelVote").css("color", "red");
            $(".labelVote").text("you need an acount to vote");
        }
    });

                var firstClick = false; var secondClick = false; var theId;




                //$('div').mouseover(function (e) { 
                //    if (e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner" || e.target.className == "tg-event-hoverline") {
                //        //alert("jgdj");

                //        $(e.target).trigger("click");
                //        e.stopPropagation();
                //    }
                //});

                //$('div').mouseleave(function (e) {
                //    if (e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner" || e.target.className == "tg-event-hoverline") {
                //        alert("jgdj5435");
                //        //$(e.target).trigger("click");
                //    }
                //});
               

                var isOver=false;

                $('div').mouseover(function (e) {
                    if (e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner") {
                        isOver = true;
                       // console.log(isOver);
                        var eventId = $(e.target).closest('div[id]').attr("id");
                        eventId = eventId.replace("_modal", "");
                        // alert(eventId);
                        theId = eventId;
                        e.stopPropagation();
                      
                        // $(e.target).trigger("click");

                        setTimeout(function () {

                            $("div").css("pointer-events", "auto");
                            if (isOver == true && eventId==theId) {
                               
                                $(e.target).trigger("click");
                                SetMod();
                            }
                          
                        }, 900); 
                        
                    }
                });

              
                try {

                
                    $('div').mouseout(function (e) {
                        // alert("out");
                        if (e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner") {

                            isOver = false;
                          //  console.log(isOver);
                            setTimeout(function () {

                                $("div").css("pointer-events", "auto");
                                if (isOver == false) {
                                    $(".tg-close-button").click();
                                }

                            }, 300);
                           
                           
                            e.stopPropagation();
                        }
                    });
                } catch (e) {
                    alert(e.message);
                }

               function SetMod(){
                $('#'+theId+"_modal").mouseenter(function (e) {
                   
                    isOver = true;
                   // console.log(isOver);
                        e.stopPropagation();
                       
                      
                });

                $('#' + theId + "_modal").mouseleave(function (e) {
                   
                    isOver = false;
                   // console.log(isOver);
                    setTimeout(function () {

                        $("div").css("pointer-events", "auto");
                        if (isOver == false) {
                            $(".tg-close-button").click();
                        }

                    }, 300);

                     e.stopPropagation();


                });
               }
             

                $('div').mousedown(function (e) {
                    if (e.target.className == "timeglider-event-title" || e.target.className == "timeglider-event-spanner" ) {
                       // firstClick = true;
                        var eventId = $(e.target).closest('div[id]').attr("id");
                        eventId = eventId.replace("_modal", "");
                        // alert(eventId);
                        theId = eventId;
                        e.stopPropagation();
                        $("div").css("pointer-events", "none");
                        GetIndividualInfo(theId);
                       // secondClick = false;

                      //  setTimeout(function () {

                       //     $("div").css("pointer-events", "auto");
                      //      if (secondClick == false) {
                                // alert("should of");
                        //        $(e.target).trigger("click");
                       //     }
                        //    firstClick = false;
                      //  }, 501);
                    }
                });
                $(document).dblclick(function (e) {


                    if (firstClick) {
                        //alert(theId);
                        secondClick = true;
                        document.getElementById('hiddenId').value = theId;
                        var tg_instance = tg1.data("timeline");
                        var startDate = tg_instance.getEventByID(theId).startdate;

                        GetIndividualInfo(theId);
                       
                    }

                });


                if(getParameterByName("doc") != "")
                {
                    // alert(getParameterByName("doc"));
                    GetIndividualInfo(getParameterByName("doc"));
                }

            });

          
           

        </script>


        <div id="scripts">


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
        </div>
        <asp:Label ID="labelTime" runat="server" Text="Time"></asp:Label>

        <br />
        <asp:Button ID="buttonCreate" runat="server" OnClick="buttonCreate_Click" Text="Creaza JSON" Visible="False" CssClass="hideButton" />
        <asp:Button ID="buttonSearchId" runat="server" Text="Search Id" OnClick="buttonSearchId_Click" CssClass="hideButton" />

        <asp:HiddenField ID="hiddenId" runat="server" />
        <asp:HiddenField ID="hiddenFieldStartDate" runat="server" />


        <asp:ScriptManager ID="ScriptManagerMain"
            runat="server"
            EnablePageMethods="true"
            ScriptMode="Release"
            LoadScriptsBeforeUI="true">
        </asp:ScriptManager>


        <asp:HiddenField ID="hiddenFieldCriteria" runat="server" />


        <div id="individualInfo" class="hide">


            <div id="divEssentialInfo">

                <asp:Image ID="imageProfile" runat="server" CssClass="profileImage" />
                <div class="imageInline" id="labelsInfo">

                    <asp:Label ID="labelName" runat="server" CssClass="labelName essentialLabels" Width="300px" Font-Size="X-Large"></asp:Label><br />
                    <asp:Label ID="labelDates" runat="server" Text="" CssClass="labelDates essentialLabels" Width="300px"></asp:Label><br />
                    <br />
                    <asp:Label ID="labelNumberOfViews" runat="server" Text="Views" CssClass="labelViews"></asp:Label>
                    <br />
                    <br />
                    <a id="linkVoteUp" class="linkVoteUp">vote up</a>
                    &nbsp;&nbsp; <a id="linkVoteDown" class="linkVoteDown">vote down</a><br />
                    <asp:Label ID="labelVote" runat="server" Text="voting status" CssClass="hide labelVote"></asp:Label>
                    <br />

                    <div id="divTags" runat="server" class="divTags essentialLabels"></div>
                    <div id="divCategories" runat="server" class="divCategories essentialLabels"></div>


                </div>


            </div>
            <br />
            <div id="afterImage">

                <div id="htmlInfo" runat="server">
                </div>

                <div id="additionalResources">
                    <h3>Additional resources</h3>
                    <hr />
                    <a id="images" class="tab">Images</a>
                    <a id="videos" class="tab">Videos</a>
                    <a id="books" class="tab">Books</a>

                    <div id="documentImages" class="Images tab">

                        <div id="divNoImage" runat="server">
                            <h2>Unfortunately there is no image for this document.</h2>
                            <h4>You can suggest some to the editor by sending him a message.<br />
                                Click <a href="#improvePage" class="improvePage">improve page</a> to do so.</h4>
                        </div>



                        <div id="documentSlideshow" class="documentSlideshow" runat="server">
                        </div>
                        <div id="imagesCollection" runat="server">
                        </div>

                    </div>
                    <!-- <a id="videoAdd">add video</a> -->
                    <div id="documentVideos" class="Videos hide tab" runat="server">
                        <div id="videosContainer">
                            <div id="player"></div>
                        </div>


                        <div id="divNoVideo" runat="server">
                            <h2>Unfortunately there is no video for this document.</h2>
                            <h4>You can suggest some to the editor by sending him a message.<br />
                                Click <a href="#improvePage" class="improvePage">improve page</a> to do so.</h4>
                        </div>
                    </div>

                    <div id="documentBooks" class="Books hide tab">

                        <div id="divNoBook" runat="server">
                            <h2>Unfortunately there are no books for this document.</h2>
                            <h4>You can suggest some to the editor by sending him a message.<br />
                                Click <a href="#improvePage" class="improvePage">improve page</a> to do so.</h4>
                        </div>

                        <div id="booksContainer" runat="server"></div>

                        <div id="bookSelectedBook" class="hide">

                            <img id="selectedBookImage" src="#" class="selectedBookInfo" />
                            <input id="hiddenIsbn" type="hidden" />
                            <div id="selectedBookInfo" class="selectedBookInfo">
                                <p id="selectedBookTitle"></p>
                                <p id="selectedBookAuthors"></p>
                                <p id="selectedBookDescription"></p>
                                <p id="selectedBookPages"></p>
                                <a id="selectedBookSelfLink" href="#" target="_blank">Read preview</a>


                            </div>
                            <div id="booksOptions"></div>
                        </div>

                    </div>
                </div>

                <div id="relatedPages">
                    <h3>Related pages</h3>
                    <hr />
                    <div id="additionalLinks" runat="server">
                    </div>
                </div>

                <a id="improvePage" class="improvePage" title="Send feeback to the editor and propose improvements.">Improve this page</a>

                <div id="feedbackMessage">
                    <h2>Help the editor by sending usefull information, tips or resources.</h2>
                    <div id="ckEditorFeedback">
                        <CKEditor:CKEditorControl ID="CKEditorFeedback" BasePath="/ckeditor/" runat="server" Height="200" Width="800"></CKEditor:CKEditorControl>
                        <asp:Button ID="buttonSendFeedback" runat="server" Text="Send" Height="33px" Width="75px" CssClass="buttonSendFeedback" OnClick="buttonSendFeedback_Click" />
                    </div>


                </div>

                <br />
                <br />
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
