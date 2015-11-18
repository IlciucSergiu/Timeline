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
        protected void Page_Load(object sender, EventArgs e)
        {
            //MongoClient mclient = new MongoClient();
            //var db = mclient.GetDatabase("test");

            //var collection = db.GetCollection<Persons>("people");
            //var nume = "Adi";
            //var data = collection.Find();
            //Label1.Text = data.ToString();
        }

        public string jsonData,jsString; //{get;set;}

        protected async void buttonLoadTimeline_Click(object sender, EventArgs e)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<BsonDocument>("Personalities");

            BsonDocument document = new BsonDocument
            {
                { "name", "Gigel" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
              {
                  { "x", 203 },
                  { "y", 102 }
              }}
            };

            //collection.InsertOneAsync(document);

            // var doc = collection.Find( _=> true);
            //var data = collection.FindAll();
            var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<BsonDocument>.Filter.Eq("name", "gigel");

            //await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += d+",");
            await collection.Find(_ => true).ForEachAsync(d => jsString += d+",");
            // await collection.Find(filter).ForEachAsync(d => jsString += d+",");

            jsonData = "[{" +
         "\"id\": \"important_personalities\"," +
         "\"title\": \"Important Personalities\"," +
         "\"initial_zoom\": \"40\"," +
                //"\"focus_date\": \"1998-03-11 12:00:00\","+
         "\"image_lane_height\": 50," +
         "\"events\":[" + jsString.TrimEnd(',') + "]" +
     "}]";

            Label1.Text = jsonData;

            
            // ViewState["keep"] += Label1.Text;
            // Label2.Text = (string)ViewState["keep"];
        }
        

        protected void buttonCreate_Click(object sender, EventArgs e)
        {
            Persons person1 = new Persons();
            person1.id = "beethoven";
            person1.title = "L V Beethoven";
            person1.startdate = "1770-12-17 ";
            person1.enddate = "1827-3-26 12:00:00";
            person1.importance = "70";
            person1.image = "https://upload.wikimedia.org/wikipedia/commons/6/6f/Beethoven.jpg";
            person1.description = "A very good musician.";
            person1.link = "https://en.wikipedia.org/wiki/Ludwig_van_Beethoven";
            person1.profesion = "Dentist";

            Persons person2 = new Persons();
            person2.id = "mozart";
            person2.title = "W A Mozart";
            person2.startdate = "1756-1-27 12:00:00";
            person2.enddate = "1791-12-5 12:00:00";
            person2.importance = "70";
            person2.image = "https://upload.wikimedia.org/wikipedia/commons/1/1e/Wolfgang-amadeus-mozart_1.jpg";
            person2.description = "A verry good musician.";
            person2.link = "https://en.wikipedia.org/wiki/Wolfgang_Amadeus_Mozart";

            List<Persons> listPersons = new List<Persons>();
            listPersons.Add(person1);
            listPersons.Add(person2);

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            string jsString = jsSerializer.Serialize(listPersons);

            jsonData = "[{" +
         "\"id\": \"sergiu-hystory\"," +
         "\"title\": \"My story\"," +
         "\"initial_zoom\": \"39\"," +
                //"\"focus_date\": \"1998-03-11 12:00:00\","+
         "\"image_lane_height\": 50," +
         "\"events\":" + jsString + "" +
     "}]";
            
            Label1.Text = jsonData;

        }

        protected void buttonAddData_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddData.aspx");
        }
    }
}