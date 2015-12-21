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
using System.Diagnostics;
using System.Web.Services;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;



namespace MyTimelineASPTry
{
    public partial class WebFormTimeline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["userLogged"] != null)
                if (Session["userLogged"].ToString() == "True")
                {
                    linkButtonLogout.Visible = true;
                    buttonLogin.Visible = false;
                    buttonWorkspace.Visible = true;
                }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            LoadTimelineConcat();
            sw.Stop();
            labelTime.Text = "Page Load : " + sw.Elapsed.TotalMilliseconds.ToString();


        }

        public static string jsonData { get; set; }

        public string jsString;

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginUser.aspx", false);
        }

        protected void buttonLoadTimeline_Click(object sender, EventArgs e)
        {


            LoadTimelineConcat();

        }

        protected async void LoadTimeline()
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<BsonDocument>("Personalities");



            var filter = Builders<BsonDocument>.Filter.Eq("name", "gigel");


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

            var filter = Builders<PersonInfo>.Filter.Eq("name", "Ilciuc"); ;

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

        static string endDate(string date)
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



        public bool showIndividual = false;
        //string eventId;
        protected void buttonSearchId_Click(object sender, EventArgs e)
        {
            showIndividual = true;
            InitializeData();

        }

        async void InitializeData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();


            string itemId = hiddenId.Value;
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<PersonInfo>.Filter.Eq("id", itemId); ;
            var documents = await collection.Find(filter).FirstAsync();
            labelName.Text = documents.name.ToString().Replace(',', ' ').Replace('[', ' ').Replace(']', ' '); ;
            labelDates.Text = documents.startdate + " - " + documents.enddate;
            labelProfession.Text = documents.profession;
            labelNationality.Text = documents.nationality;
            labelReligion.Text = documents.religion;
            imageProfile.ImageUrl = documents.image;

            divTags.InnerHtml = "";
            if (documents.tags != null)
                foreach (BsonDocument tag in documents.tags)
                {
                    divTags.InnerHtml += "<a class=\"tagLinks\" runat=\"server\" >" + tag[0] + "</a>  ";

                }

            htmlInfo.InnerHtml = "";
            additionalLinks.InnerHtml = "";
            additionalResources.InnerHtml = "";

            if (ItemExists(itemId))
            {
                var collection1 = db.GetCollection<IndividualData>("IndividualData");
                var filter1 = Builders<IndividualData>.Filter.Eq("id", itemId);
                var item = await collection1.Find(filter1).FirstAsync();

                htmlInfo.InnerHtml = item.htmlInformation;
                // listBoxLinks.Items.Clear();

               // additionalResources.InnerHtml = "Additional resources";
                if (item.additionalBooks != null)
                    foreach (var book in item.additionalBooks)
                    {
                        additionalResources.InnerHtml += "<br />" + book.ToString();
                    }
                additionalResources.InnerHtml += "<br /><br />";

               // additionalLinks.InnerHtml = "Additional links";
                if (item.additionalLinks != null)
                    foreach (var links in item.additionalLinks)
                    {
                        additionalLinks.InnerHtml += "<br /><a href=" + links.ToString() + ">" + links.ToString() + "<a/>";

                    }
                additionalLinks.InnerHtml += "<br /><br />";

            }
            sw.Stop();
            labelTime.Text += "\r\n InitializeData :" + sw.Elapsed.TotalMilliseconds.ToString();
        }

        bool ItemExists(string id)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<IndividualData>("IndividualData");
            var filter = Builders<IndividualData>.Filter.Eq("id", id);
            var count = collection.Find(filter).CountAsync();

            sw.Stop();
            labelTime.Text += "\r\n Item Exists :" + sw.Elapsed.TotalMilliseconds.ToString();
            // Response.Write(count.Result);
            if (Convert.ToInt32(count.Result) != 0)
                return true;
            else
                return false;


        }

        protected void buttonSearchQuery_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string searchQuery = "";
            if (hiddenFieldCriteria.Value.ToString() != "")
            {
                searchQuery = hiddenFieldCriteria.Value.ToString();
                SearchQueryByTag(searchQuery);
            }
            else
            {
                if (textBoxSearchQuery.Text != "")
                {
                    searchQuery = textBoxSearchQuery.Text;
                    if (searchQuery.Contains(':'))
                        SearchQueryByCriteria(searchQuery);
                    else
                    {

                        SearchQueryByTag(searchQuery);
                    }
                }
            }
            sw.Stop();
            labelTime.Text += "\r\n Search Query :" + sw.Elapsed.TotalMilliseconds.ToString();


        }

        async void SearchQueryByCriteria(string searchQuery)
        {
            if (searchQuery.Contains(':'))
            {
                MongoClient mclient = new MongoClient();
                var db = mclient.GetDatabase("Timeline");
                var collection = db.GetCollection<PersonInfo>("Persons");
                var filter = Builders<PersonInfo>.Filter.Eq("name", "Mozart");




                string[] category = searchQuery.Split(':');
                filter = Builders<PersonInfo>.Filter.Eq(category[0], category[1]);


                jsString = "";
                await collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\""
                    + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate
                    + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \""
                    + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \""
                    + d.link + "\",\"image\" : \"" + d.image + "\"},");



                jsonData = "[{" +
             "\"id\": \"" + searchQuery + "\"," +
             "\"title\": \"Important Personalities\"," +
             "\"initial_zoom\": \"40\"," +
                    //"\"focus_date\": \"1998-03-11 12:00:00\","+
             "\"image_lane_height\": 50," +
             "\"events\":[" + jsString.TrimEnd(',') + "]" +
         "}]";
            }

        }

        async void SearchQueryByTag(string searchQuery)
        {

            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");
            var collection = db.GetCollection<PersonInfo>("Persons");
            var filter = Builders<PersonInfo>.Filter.Eq("name", "Mozart");


            filter = Builders<PersonInfo>.Filter.Eq("tags.tagName", searchQuery);



            jsString = "";
            await collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\""
                + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate
                + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \""
                + GetImportance(d.tags, searchQuery) + "\",\"description\" : \"" + d.description + "\",\"link\" : \""
                + d.link + "\",\"image\" : \"" + d.image + "\"},");

            // await collection.Find(filter).ForEachAsync(d => jsString += GetImportance(d.tags,searchQuery));


            //  Response.Write(jsString);

            jsonData = "[{" +
         "\"id\": \"" + searchQuery + "\"," +
         "\"title\": \"" + searchQuery.First().ToString().ToUpper() + searchQuery.Substring(1) + "\"," +
         "\"initial_zoom\": \"40\"," +
                //"\"focus_date\": \"1998-03-11 12:00:00\","+
         "\"image_lane_height\": 50," +
         "\"events\":[" + jsString.TrimEnd(',') + "]" +
     "}]";


            hiddenFieldCriteria.Value = "";
            // sw.Stop();
            //labelTime.Text += "\r\n Search Query :" + sw.Elapsed.TotalMilliseconds.ToString();

        }

       static string GetImportance(BsonArray tags, string searchQuery)
        {
            string theReturn = "";
            foreach (BsonDocument tagDocument in tags)
            {
                var obj = JObject.Parse(tagDocument.ToString());
                //var url = (string)obj["data"]["img_url"];
                if ((string)obj["tagName"] == searchQuery)
                {

                    theReturn = (string)obj["tagImportance"];
                }

            }
            return theReturn;

        }
        //string jsonData;
        //[WebMethod]
        ////[ScriptMethod(UseHttpGet = true)]
        //public static async Task<string> SearchByCriteria(string criteria)
        //{
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    MongoClient mclient = new MongoClient();
        //    var db = mclient.GetDatabase("Timeline");
        //    var collection = db.GetCollection<PersonInfo>("Persons");
        //    var filter = Builders<PersonInfo>.Filter.Eq("name", "Mozart");

            

        //    string jsString = "";
        //    try
        //    {
        //        await collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\""
        //            + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate
        //            + "\",\"enddate\" : \"" + /*endDate(d.enddate)*/"" + "\",\"importance\" : \""
        //            + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \""
        //            + d.link + "\",\"image\" : \"" + d.image + "\"},");


                
        //        //var result =  collection.Find(filter);

        //    //    BsonDocument document = new BsonDocument
        //    //{
                
        //    //     //{ "owner", ViewState["userId"].ToString() },
        //    //    { "id", "Fanica" },
        //    //    { "owner", "Geo" }
              
               

        //    //};
        //    //   // await collection.InsertOneAsync(document);
        //    //    // collection.InsertOneAsync(document);

        //        return jsString;
        //    }
        //     catch (Exception e)
        //    {
        //        return e.ToString();
        //    }

        //    string jsonData1 = "[{" +
        //    "\"id\": \"important_personalities\"," +
        //    "\"title\": \"Important Personalities\"," +
        //    "\"initial_zoom\": \"40\"," +
        //        //"\"focus_date\": \"1998-03-11 12:00:00\","+
        //    "\"image_lane_height\": 50," +
        //    "\"events\":[" + jsString.TrimEnd(',') + "]" +
        //"}]";


        //    sw.Stop();
        //    //jsonData = sw.Elapsed.ToString();
        //    return jsonData1;

        //}






        [WebMethod]
       
        public static  string SearchByCriteria(string criteria)
        {

            string searchQuery = criteria;
            Stopwatch sw = new Stopwatch();
            sw.Start();

           MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");
            var collection = db.GetCollection<PersonInfo>("Persons");
            var filter = Builders<PersonInfo>.Filter.Eq("name", "Mozart");


            filter = Builders<PersonInfo>.Filter.Eq("tags.tagName", searchQuery);



            string jsString = "";
             collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\""
                + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate
                + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \""
                + GetImportance(d.tags, searchQuery) + "\",\"description\" : \"" + d.description + "\",\"link\" : \""
                + d.link + "\",\"image\" : \"" + d.image + "\"},").Wait();



            jsonData = "[{" +
           "\"id\": \"" + searchQuery + "\"," +
           "\"title\": \"" + searchQuery.First().ToString().ToUpper() + searchQuery.Substring(1) + "\"," +
           "\"initial_zoom\": \"40\"," +
                //"\"focus_date\": \"1998-03-11 12:00:00\","+
           "\"image_lane_height\": 50," +
           "\"events\":[" + jsString.TrimEnd(',') + "]" +
       "}]";

            sw.Stop();
            //jsonData = sw.Elapsed.ToString();
            return jsonData;

        }



       




        [WebMethod]
        public static string SearchPersonalInfo(string personId)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");
            var collection = db.GetCollection<PersonInfo>("Persons");


            // var filter = Builders<PersonInfo>.Filter.Eq("name", "Mozart");

            var filter = Builders<PersonInfo>.Filter.Eq("id", personId);

            PersonInfo personData = new PersonInfo();
            string jsString = "";
            try
            {
                collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\""
                   + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate
                   + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \""
                   + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \""
                   + d.link + "\",\"image\" : \"" + d.image + "\"},").Wait();

                //collection.Find(filter).ForEachAsync(d => { personData.id = d.id; 
                //    personData.name = d.name; 
                //    personData.nationality = d.nationality;
                //    personData.profession = d.profession;
                //    personData.religion = d.religion;
                //    personData.image = d.image;
                //}).Wait();




                //return jsString;
            }
            catch (Exception e)
            {
                return e.ToString();
            }

          


            //sw.Stop();
            //jsonData = sw.Elapsed.ToString();
            //JObject jsonData = JObject.Parse(jsString.TrimEnd(','));
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(jsString.TrimEnd(','));
            return json;

        }





        protected void linkButtonLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("userLogged");
            Response.Redirect("WebFormTimeline.aspx", false);
        }

        [System.Web.Services.WebMethod]
        public static string LoadTimelineQuery(string query)
        {
            return "sergiu e amecher";

        }




    }
}