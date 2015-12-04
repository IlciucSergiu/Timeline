using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;


namespace MyTimelineASPTry
{
    public partial class WebFormTimeline : System.Web.UI.Page
    {
        protected  void Page_Load(object sender, EventArgs e)
        {
            Page prevPage = Page.PreviousPage;
            
            LoadTimelineConcat();

            //if(IsPostBack)
            //{
            //    if (hiddenId.Value != null)
            //    { 
            //        eventId = hiddenId.Value;
            //        Response.Write(eventId);
                
            //    }
            //}
            
        }

        public string jsonData {get;set;}
        
        public string  jsString;

        protected  void buttonLoadTimeline_Click(object sender, EventArgs e)
        {
            
          
            LoadTimelineConcat();
            
 }
        
         protected async void LoadTimeline ()
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<BsonDocument>("Personalities");

             //collection.InsertOneAsync(document);

            // var doc = collection.Find( _=> true);
            //var data = collection.FindAll();
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<BsonDocument>.Filter.Eq("name", "gigel");

            //await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += d+",");
            jsString = "";
            await collection.Find(_ => true).ForEachAsync(d => jsString += d + ",");
            // await collection.Find(filter).ForEachAsync(d => jsString += d+",");

            jsonData = "[{" +
         "\"id\": \"important_personalities\"," +
         "\"title\": \"Important Personalities\"," +
         "\"initial_zoom\": \"40\"," +
                //"\"focus_date\": \"1998-03-11 12:00:00\","+
         "\"image_lane_height\": 50," +
         "\"events\":[" + jsString.TrimEnd(',') + "]" +
     "}]";

  }

         protected async void LoadTimelineConcat()
         {
             MongoClient mclient = new MongoClient();
             var db = mclient.GetDatabase("Timeline");

             var collection = db.GetCollection<PersonInfo>("Persons");
              
            

            

             
             //var documents = await collection.Find(new BsonDocument()).FirstAsync();

             var filter = Builders<PersonInfo>.Filter.Eq("name", "Ilciuc");;

             //await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += d+",");
             jsString = ""; 
             await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += "{\"id\":\"" 
                 + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate 
                 + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \"" 
                 + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \"" 
                 + d.link + "\",\"image\" : \"" + d.image + "\"},");
             //await collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\"" + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate + "\",\"enddate\" : \"" + d.enddate + "\",\"importance\" : \"" + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \"" + d.link + "\",\"image\" : \"" + d.image + "\"},");
             

             jsonData = "[{" +
          "\"id\": \"important_personalities\"," +
          "\"title\": \"Important Personalities\"," +
          "\"initial_zoom\": \"40\"," +
                 //"\"focus_date\": \"1998-03-11 12:00:00\","+
          "\"image_lane_height\": 50," +
          "\"events\":[" + jsString.TrimEnd(',') + "]" +
      "}]";

         }

         string endDate(string date)
             {
                 if (date == "contemporary" || date == "now")
                     
                     return DateTime.Now.ToString("yyyy-MM-dd");
                     return date;
             }
       protected void buttonCreate_Click(object sender, EventArgs e)
        {
     //       Persons person1 = new Persons();
     //       person1.id = "beethoven";
     //       person1.title = "L V Beethoven";
     //       person1.startdate = "1770-12-17 ";
     //       person1.enddate = "1827-3-26 12:00:00";
     //       person1.importance = "70";
     //       person1.image = "https://upload.wikimedia.org/wikipedia/commons/6/6f/Beethoven.jpg";
     //       person1.description = "A very good musician.";
     //       person1.link = "https://en.wikipedia.org/wiki/Ludwig_van_Beethoven";
     //       person1.profesion = "Dentist";

     //       Persons person2 = new Persons();
     //       person2.id = "mozart";
     //       person2.title = "W A Mozart";
     //       person2.startdate = "1756-1-27 12:00:00";
     //       person2.enddate = "1791-12-5 12:00:00";
     //       person2.importance = "70";
     //       person2.image = "https://upload.wikimedia.org/wikipedia/commons/1/1e/Wolfgang-amadeus-mozart_1.jpg";
     //       person2.description = "A verry good musician.";
     //       person2.link = "https://en.wikipedia.org/wiki/Wolfgang_Amadeus_Mozart";

     //       List<Persons> listPersons = new List<Persons>();
     //       listPersons.Add(person1);
     //       listPersons.Add(person2);

     //       JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

     //       string jsString = jsSerializer.Serialize(listPersons);

     //       jsonData = "[{" +
     //    "\"id\": \"sergiu-hystory\"," +
     //    "\"title\": \"My story\"," +
     //    "\"initial_zoom\": \"39\"," +
     //           //"\"focus_date\": \"1998-03-11 12:00:00\","+
     //    "\"image_lane_height\": 50," +
     //    "\"events\":" + jsString + "" +
     //"}]";
            
            

        }

        protected void buttonAddData_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginUser.aspx",false);
           
        }

        public bool showIndividual = false;
        //string eventId;
        protected  void buttonSearchId_Click(object sender, EventArgs e)
        {
            showIndividual = true;
            InitializeData();
           
        }

        async void InitializeData()
        {
         string itemId = hiddenId.Value;
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<PersonInfo>.Filter.Eq("id", itemId); ;
            var documents = await collection.Find(filter).FirstAsync();
            labelName.Text = documents.name.ToString();
            labelDates.Text = documents.startdate + " - " + documents.enddate;
            labelProfession.Text = documents.profession;
            labelNationality.Text = documents.nationality;
            labelReligion.Text = documents.religion;
            imageProfile.ImageUrl = documents.image;

            if (ItemExists(itemId))
            {
                var collection1 = db.GetCollection<IndividualData>("IndividualData");
                var filter1 = Builders<IndividualData>.Filter.Eq("id", itemId);
                var item = await collection1.Find(filter1).FirstAsync();

                htmlInfo.InnerHtml = item.htmlInformation;
                // listBoxLinks.Items.Clear();
               
                    additionalResources.InnerHtml = "Additional resources";
                    if (item.additionalBooks != null)
                        foreach (var book in item.additionalBooks)
                        {
                            additionalResources.InnerHtml +="<br />"+ book.ToString();
                        }

                    additionalLinks.InnerHtml = "Additional links";
                    if (item.additionalLinks != null)
                        foreach (var links in item.additionalLinks)
                        {
                            additionalLinks.InnerHtml += "<br /><a href=" + links.ToString() + ">" + links.ToString() + "<a/>";

                        }

            }
        }

        bool ItemExists(string id)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<IndividualData>("IndividualData");
            var filter = Builders<IndividualData>.Filter.Eq("id", id);
            var count = collection.Find(filter).CountAsync();

           // Response.Write(count.Result);
            if (Convert.ToInt32(count.Result) != 0)
                return true;
            else
                return false;
        }
    }
}