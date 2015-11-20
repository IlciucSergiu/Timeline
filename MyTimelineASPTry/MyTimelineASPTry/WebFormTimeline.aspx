<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormTimeline.aspx.cs" Inherits="MyTimelineASPTry.WebFormTimeline" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <title>MyTry</title>
        <link rel="stylesheet" href="MySecondTry1/css/jquery-ui-1.10.3.custom.css" type="text/css" charset="utf-8"/>
<link rel="stylesheet" href="MySecondTry1/timeglider/Timeglider.css" type="text/css" charset="utf-8"/>
<link rel="stylesheet" href="MySecondTry1/timeglider/timeglider.datepicker.css" type="text/css" charset="utf-8"/>
        <link rel="stylesheet"  type="text/css" href="MySecondTry1/css/MyTimeline.css"   charset="utf-8"/>
        
           

        
    </head>
    <body>
        <script src="MySecondTry1/js/jquery-1.11.3.min.js" type="text/javascript" charset="utf-8"></script> 
        <script src="MySecondTry1/js/testSergiu.js" type="text/javascript" charset="utf-8"></script>
    <h1>MyTry</h1>
        <div id='placement' style="height:400px"></div>
      <form  runat="server">
        <asp:Button ID="buttonLoadTimeline" runat="server" Text="Load timeline" OnClick="buttonLoadTimeline_Click" />
      
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
           <script>

               var jsonString = '<%=jsonData%>';
               
     $(document).ready(function () { 
     
         if (jsonString != "") {
             alert(jsonString);
             var jsonData = JSON.parse(jsonString);

             var tg1 = $("#placement").timeline({

                 "data_source": jsonData, //"MySecondTry1/json/sergiu3.json",
                 "min_zoom": 15,
                 "max_zoom": 60
             }
         );
         }
   });
    </script>
        
         
          <br />
          <asp:Button ID="buttonCreate" runat="server" OnClick="buttonCreate_Click" Text="Creaza JSON" /><br />

          <asp:Button ID="buttonAddData" runat="server" Text="Add data" OnClick="buttonAddData_Click" />

          <p id="info">Nu e nimic aici</p>
          <p>
              
              
              <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
          </p>
        </form>
      
             
    </body>

</html>
