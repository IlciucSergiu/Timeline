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
using System.Text.RegularExpressions;

namespace MyTimelineASPTry
{
    public partial class WebFormTimeline : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            CKEditorFeedback.Toolbar = CKEditorFeedback.ToolbarBasic;

            if (Session["userLogged"] != null)
                if (Session["userLogged"].ToString() == "True")
                {
                    linkButtonLogout.Visible = true;
                    buttonLogin.Visible = false;
                    buttonWorkspace.Visible = true;
                }
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string searchQuery = Request.QueryString["q"];

            if(searchQuery ==  "all")
                LoadTimelineConcat();

            if (searchQuery == null)
            {
                isFront = true;
                searchQuery = "category:Important events";
            }

            if (!IsPostBack)
                QueryInterpretation(searchQuery);
               // LoadTimelineConcat();

            sw.Stop();
            labelTime.Text = "Page Load : " + sw.Elapsed.TotalMilliseconds.ToString();


        }

        bool isFront = false;
        public static string jsonData { get; set; }

        public string jsString, videoId;

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginUser.aspx", false);
        }

        protected void buttonLoadTimeline_Click(object sender, EventArgs e)
        {


            LoadTimelineConcat();

        }





        protected void LoadTimelineConcat()
        {
            // Response.Write("start    ");

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");


            var filter = Builders<DocumentInfo>.Filter.Eq("name", "Ilciuc"); 

            //await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += d+",");
            jsString = "";
            collection.Find(new BsonDocument()).ForEachAsync(d => jsString += "{\"id\":\""
               + ReplaceToHTML(d.id) + "\",\"title\" : \"" + ReplaceToHTML(d.title) + "\",\"startdate\" : \"" + d.startdate
               + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \""
               + 50 + "\",\"description\" : \"" + ReplaceToHTML(d.description) + "\",\"link\" : \""
               + ReplaceToHTML(d.link)  + "\",\"image\" : \"" + ReplaceToHTML(d.image)  + "\"},").Wait();
            //await collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\"" + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate + "\",\"enddate\" : \"" + d.enddate + "\",\"importance\" : \"" + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \"" + d.link + "\",\"image\" : \"" + d.image + "\"},");


            jsonData = "[{" +
         "\"id\": \"main_data\"," +
         "\"title\": \"TimeTrail\"," +
         "\"initial_zoom\": \"40\"," +
         //"\"focus_date\": \"1998-03-11 12:00:00\","+
         "\"image_lane_height\": 50," +
         "\"events\":[" + jsString.TrimEnd(',') + "]" +
     "}]";

            //Response.Write("    end");
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

        protected void buttonSearchId_Click(object sender, EventArgs e)
        {
            showIndividual = true;
            InitializeData();

        }

        public bool setDate = false;
        async void InitializeData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            setDate = true;
            string itemId = hiddenId.Value;
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<DocumentInfo>.Filter.Eq("id", itemId); ;
            var documents = await collection.Find(filter).FirstAsync();
            labelName.Text = documents.name.ToString().Replace(',', ' ').Replace('[', ' ').Replace(']', ' '); 
            labelDates.Text = documents.startdate + " - " + documents.enddate;
            // labelProfession.Text = documents.profession;
            // labelNationality.Text = documents.nationality;
            // labelReligion.Text = documents.religion;
            imageProfile.ImageUrl = documents.image;

            divTags.InnerHtml = "";
            if (documents.tags != null)
                foreach (BsonDocument tag in documents.tags)
                {
                    divTags.InnerHtml += "<a class=\"tagLinks\" runat=\"server\" >" + tag[0] + "</a>  ";

                }

            htmlInfo.InnerHtml = "";
            additionalLinks.InnerHtml = "";


            if (ItemExists(itemId))
            {
                var collection1 = db.GetCollection<IndividualData>("IndividualData");
                var filter1 = Builders<IndividualData>.Filter.Eq("id", itemId);
                var item = await collection1.Find(filter1).FirstAsync();

                // Aici incrementez numarul vizualizarilor
                var update = Builders<IndividualData>.Update
                   .Inc("timesViewed", 1);
                var result = await collection1.UpdateOneAsync(filter1, update);


                htmlInfo.InnerHtml = item.htmlInformation;

                labelNumberOfViews.Text = "viewed " + (item.timesViewed + 1).ToString() + " times";



                if (item.additionalLinks != null)
                    foreach (var links in item.additionalLinks)
                    {
                        additionalLinks.InnerHtml += "<br /><a href=" + links.ToString() + ">" + links.ToString() + "<a/>";

                    }
                additionalLinks.InnerHtml += "<br /><br />";

                if (item.videoLinks != null)
                {
                    bool noVideo = true;

                    foreach (string videoLink in item.videoLinks)
                    {
                        if (videoLink != "")
                        {
                            noVideo = false;
                            documentVideos.Controls.Add(new LiteralControl { Text = "<div  id=\"player\"></div>" });
                            videoId = item.videoLinks[0].ToString();
                        }
                    }

                    if (noVideo)
                        divNoVideo.Style.Add("display", "block");
                    else
                    {
                        divNoVideo.Style.Add("display", "none");
                    }

                }
                else {

                    divNoVideo.Style.Add("display", "block");
                }

                if (item.imagesLinks != null)
                {
                    bool noImage = true;

                    foreach (string image in item.imagesLinks)
                    {
                        if (image != "")
                        {
                            noImage = false;
                            documentSlideshow.Controls.Add(new LiteralControl { Text = "<img  class=\"slideImage\" src=\"" + image + "\"/>" });
                            imagesCollection.Controls.Add(new LiteralControl { Text = "<img   class=\"imageCollection\" src=\"" + image + "\"/>" });
                        }
                    }
                    //Response.Write("Are images");
                    if (noImage)
                    {
                        // Response.Write("Still no image " + item.imagesLinks.Count);
                        divNoImage.Style.Add("display", "block");
                    }
                    else
                    {
                        divNoImage.Style.Add("display", "none");
                    }

                }
                else {
                   
                    divNoImage.Style.Add("display", "block");
                }




                if (item.additionalBooks != null)
                {
                    bool noBook = true;

                    foreach (BsonDocument book in item.additionalBooks)
                    {
                        if (book["isbn"] != "")
                        {
                            noBook = false;
                            booksContainer.Controls.Add(new LiteralControl { Text = "<img  id=\"" + book["isbn"] + "\"  class=\"documentBook\" src=\"" + book["imageUrl"] + "\"/>" });
                        }
                    }
                    if (noBook)
                    {
                        divNoBook.Style.Add("display", "block");
                    }
                    else
                    {
                        divNoBook.Style.Add("display", "none");
                    }
                }
                else {
                    divNoBook.Style.Add("display", "block");
                }

            }
            sw.Stop();
            labelTime.Text += "\r\n InitializeData :" + sw.Elapsed.TotalMilliseconds.ToString();
        }

        bool ItemExists(string id)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
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

            Response.Redirect("Home.aspx?q="+textBoxSearchQuery.Text+"");
            //Stopwatch sw = new Stopwatch();
            //sw.Start();


            //if (textBoxSearchQuery.Text != "")
            //    QueryInterpretation(textBoxSearchQuery.Text);

            //sw.Stop();
            //labelTime.Text += "\r\n Search Query :" + sw.Elapsed.TotalMilliseconds.ToString();


        }

        void QueryInterpretation(string searchQuery)
        {
            if (searchQuery.Contains(":"))
            {
                SearchSpecificQuery(searchQuery);
            }
            else
            {
                SearchPlainTextQuery(searchQuery);
            }

        }



        void SearchSpecificQuery(string searchQuery)
        {
            string criteria = searchQuery.Split(':')[0];
            string value = searchQuery.Split(':')[1];

            criteria = criteria.TrimEnd().TrimStart().ToLower();
            value = value.TrimStart();
            if (criteria.Contains(' ')) criteria = criteria.Substring(criteria.LastIndexOf(' ') + 1);

            //Response.Write("x"+criteria+"-------"+value+"x");

            if (criteria == "tag")
            {
                if (value.Contains(' ')) value = value.Substring(0, value.IndexOf(' '));
                value = value.ToLower();
                List<DocumentInfo> tagList = SearchQueryByTag(value);
                ConcatenateList(tagList, value);
            }
            else if (criteria == "description")
            {
                //Response.Write("describe");
                List<DocumentInfo> responseList = SearchQueryByDescription(value);
                ConcatenateList(responseList, value);
            }
            else if (criteria == "name")
            {
                List<DocumentInfo> responseList = SearchQueryByName(value);
                ConcatenateList(responseList, value);

            }
            else if (criteria == "info")
            {
                List<DocumentInfo> responseList = SearchQueryByInfo(value);
                ConcatenateList(responseList, value);
            }
            else if (criteria == "category")
            {
                //Response.Write("category");
                List<DocumentInfo> responseList = SearchQueryByCategory(value);
                ConcatenateList(responseList, value);
            }

        }

         void  SearchPlainTextQuery(string searchQuery)
        {
            searchQuery = searchQuery.Trim();
            //if (!searchQuery.Contains(' '))
            //{
            //    List<DocumentInfo> tagList = SearchQueryByTag(searchQuery);
            //    ConcatenateList(tagList, searchQuery);
            //}

            Stopwatch sw = new Stopwatch();

            sw.Start();

            List<DocumentInfo> searchResponse = new List<DocumentInfo>();

           
            Stopwatch taskWatch = new Stopwatch();
            taskWatch.Start();

            Task taskName = Task.Run(() => searchResponse.AddRange(SearchQueryByName(searchQuery)));
            Task taskTag = Task.Run(() => searchResponse.AddRange(SearchQueryByTag(searchQuery)));
            Task taskDescription = Task.Run(() => searchResponse.AddRange(SearchQueryByDescription(searchQuery)));
            Task taskInfo = Task.Run(() => searchResponse.AddRange(SearchQueryByInfo(searchQuery)));
            Task taskCategory = Task.Run(() => searchResponse.AddRange(SearchQueryByCategory(searchQuery)));

          
           // Response.Write( "   <p> tasks sent in   " + taskWatch.Elapsed.TotalMilliseconds +"<p>");

            taskName.Wait();
           // Response.Write("   <p> tasks name   " + taskWatch.Elapsed.TotalMilliseconds + "<p>");
            taskTag.Wait();
           // Response.Write("   <p> tasks tag   " + taskWatch.Elapsed.TotalMilliseconds + "<p>");
            taskDescription.Wait();
           // Response.Write("   <p> tasks description   " + taskWatch.Elapsed.TotalMilliseconds + "<p>");
            taskInfo.Wait();
           // Response.Write("   <p> tasks info   " + taskWatch.Elapsed.TotalMilliseconds + "<p>");
            taskCategory.Wait();
           // Response.Write("   <p> tasks category  " + taskWatch.Elapsed.TotalMilliseconds + "<p>");

            searchResponse = searchResponse.GroupBy(item => item.id)
                   .Select(g => g.OrderByDescending(i => i.importance)
                   .First()).ToList();
           // Response.Write("   <p> Selected  " + taskWatch.Elapsed.TotalMilliseconds + "<p>");

            taskWatch.Stop();
            sw.Stop();
           // Response.Write(searchResponse.Count + "   and took   "+ sw.Elapsed.TotalMilliseconds);

            ConcatenateList(searchResponse.Take(100).ToList(),searchQuery);
        }

        string ConcatenateList(List<DocumentInfo> theList, string title)
        {
            if (theList.Count > 0)
            {
                theList.ForEach(d => jsString += "{\"id\":\""
            + ReplaceToHTML(d.id) + "\",\"title\" : \"" + FormatTitle(ReplaceToHTML(d.title),20) + "\",\"startdate\" : \"" + d.startdate
            + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \""
            + d.importance + "\",\"description\" : \"" + ReplaceToHTML(d.description) + "\",\"link\" : \""
            + ReplaceToHTML(d.link) + "\",\"image\" : \"" + ReplaceToHTML(d.image)  + "\"},");

                if (title.Length > 20)
                    title = title.Substring(0, 20);

                var first =new DocumentInfo();

                int initZoom = 20;
                if (isFront)
                {
                    initZoom = 20;
                   first = theList.OrderByDescending(o => o.startdate).First();
                }
                else
                 first = theList.First();

                jsonData = "[{" +
             "\"id\": \"" + title.Replace(' ', '_') + "\"," +
             "\"title\": \"" + title.First().ToString().ToUpper() + title.Substring(1) + "\"," +
             "\"initial_zoom\": \""+initZoom+"\"," +
             "\"focus_date\": \"" + first.startdate + " 12:00:00\"," +
             "\"image_lane_height\": 50," +
             "\"events\":[" + jsString.TrimEnd(',') + "]" +
             "}]";

                //Response.Write(jsonData);
                return jsonData;
            }
            else return "None";
        }

       

        string FormatTitle(string theString, int lenght)
        {
            if (theString.Length > lenght)
                theString = theString.Substring(0, lenght-3) + "...";

            return theString;
        }



        List<DocumentInfo> SearchQueryByTag(string tagName)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Eq("tags.tagName", tagName);

            List<DocumentInfo> documentResponse = collection.Find(filter).ToListAsync().Result;

            documentResponse.ForEach(d =>
            {
                d.importance = GetImportance(d.tags, tagName);

            });

            return documentResponse.OrderByDescending(o => o.importance).Take(100).ToList();
        }

        List<DocumentInfo> SearchQueryByCategory(string categoryName)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Regex("categories.categoryName", new BsonRegularExpression("/" + categoryName + "/i"));
           // var filter = Builders<DocumentInfo>.Filter.Eq("categories.categoryName", categoryName);

            List<DocumentInfo> documentResponse = collection.Find(filter).ToListAsync().Result;

            documentResponse.ForEach(d =>
            {
                //Response.Write(categoryName);
                d.importance = GetImportanceCategory(d.categories, categoryName);

            });

            return documentResponse.OrderByDescending(o => o.importance).Take(100).ToList();
        }

        List<DocumentInfo> SearchQueryByDescription(string keyWords)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");


            var filter = Builders<DocumentInfo>.Filter.Regex("description", new BsonRegularExpression("/" + keyWords + "/i"));

            List<DocumentInfo> documentResponse = collection.Find(filter).ToListAsync().Result;
            documentResponse.ForEach(d =>
            {
                d.importance = "60";

            });


            //Response.Write("---- going with list"+ documentResponse.Count);

            return documentResponse.OrderByDescending(o => o.importance).Take(100).ToList();

        }


        List<DocumentInfo> SearchQueryByName(string name)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");

            name = Regex.Replace(name, @"\s+", " ");
            // Response.Write(name+"  :");
            string[] names = name.Split(' ').Select(p => p.Trim()).ToArray();
            string fullName = name;

            var filter = Builders<DocumentInfo>.Filter.Regex("name", new BsonRegularExpression("/" + string.Join("|", names) + "/i"));

            List<DocumentInfo> response = collection.Find(filter).ToListAsync().Result;

            response.ForEach(d =>
           {
               d.importance = (40 + Convert.ToInt32(50 * HowManyMaches(d.name.ToString(), names))).ToString();

           });

            return response.OrderByDescending(o => o.importance).ToList().Take(100).ToList();
        }


        List<DocumentInfo> SearchQueryByInfo(string infoText)
        {

            infoText = Regex.Replace(infoText, @"\s+", " ");

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collectionIndividual = db.GetCollection<IndividualData>("IndividualData");
            var collectionInfo = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<IndividualData>.Filter.Regex("htmlInformation", new BsonRegularExpression("/" + infoText /*string.Join("|", )*/ + "/i"));

            var filterDocument = Builders<DocumentInfo>.Filter.Regex("id", "gigi_becali");

            Stopwatch sw = new Stopwatch();

            sw.Start();

            

            List<string> listOfIds = new List<string>();

            collectionIndividual.Find(filter).ForEachAsync(d => listOfIds.Add(d.id)).Wait();

            filterDocument = Builders<DocumentInfo>.Filter.In(d => d.id, listOfIds);

            var response = collectionInfo.Find(filterDocument).ToListAsync().Result;

            sw.Stop();
           // Response.Write("  This operation  long array " + sw.Elapsed.TotalMilliseconds);

            response.ForEach(d =>
                        {
                            d.importance = "60";/*(40 + Convert.ToInt32(50 * HowManyMaches(d.name.ToString(), names))).ToString()*/

                        });

            return response.OrderByDescending(o => o.importance).Take(100).ToList();
        }


        float HowManyMaches(string theString, string[] theArray)
        {

            theArray = theArray.Select(s => s.ToLowerInvariant()).ToArray();
            theString = theString.ToLower();
            float matched = 0;
            foreach (string checkValue in theArray)
            {
                if (theString.Contains(checkValue))
                    matched++;
            }

            //  Response.Write(" \n "+(matched / theArray.Length) + "  \n");
            return matched / theArray.Length;
        }

        bool ContainsAll(string theString, string[] theArray)
        {
            foreach (string checkValue in theArray)
            {
                if (!theString.ToLower().Contains(checkValue.ToLower()))
                    return false;
            }
            return true;
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

        static string GetImportanceCategory(BsonArray categories, string searchQuery)
        {

           
            string theReturn = "40";
            foreach (BsonDocument categoryDocument in categories)
            {
                var obj = JObject.Parse(categoryDocument.ToString());
                //var url = (string)obj["data"]["img_url"];
                if ((string)obj["categoryName"] == searchQuery)
                {

                    theReturn = (string)obj["categoryImportance"];
                }

            }
            return theReturn;

        }



        public string ReplaceToHTML(string text)
        {
            string[] plainChar = new string[] {  "'" , "\"" };
            string[] HTMLChar = new string[] { "", "" };

            for (int i = 0; i < plainChar.Length; i++)
            {
                text = text.Replace(plainChar[i], HTMLChar[i]);
            }

            return text;
        }


        [WebMethod]

        public static string SearchByCriteria(string criteria)
        {

            string searchQuery = criteria;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");
            var filter = Builders<DocumentInfo>.Filter.Eq("name", "Mozart");


            filter = Builders<DocumentInfo>.Filter.Eq("tags.tagName", searchQuery);



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

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");


            // var filter = Builders<PersonInfo>.Filter.Eq("name", "Mozart");

            var filter = Builders<DocumentInfo>.Filter.Eq("id", personId);

            DocumentInfo personData = new DocumentInfo();
            string jsString = "";
            try
            {
                collection.Find(filter).ForEachAsync(d => jsString += "{\"id\":\""
                   + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate
                   + "\",\"enddate\" : \"" + endDate(d.enddate) + "\",\"importance\" : \""
                   + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \""
                   + d.link + "\",\"image\" : \"" + d.image + "\"},").Wait();

                


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
            Response.Redirect("Home.aspx", false);
        }

     


       

        protected void buttonSearchTag_Click(object sender, EventArgs e)
        {
            if (hiddenFieldCriteria.Value.ToString() != "")
            {
                string tagName = hiddenFieldCriteria.Value.ToString();
                List<DocumentInfo> tagList = SearchQueryByTag(tagName);
                ConcatenateList(tagList, tagName);

                hiddenFieldCriteria.Value = "";
            }
        }



        protected void buttonSendFeedback_Click(object sender, EventArgs e)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<IndividualData>("IndividualData");

            var filter = Builders<IndividualData>.Filter.Eq("id", hiddenId.Value);

            var update = Builders<IndividualData>.Update
               .Push(p => p.documentFeedback, CKEditorFeedback.Text);

            collection.UpdateOneAsync(filter, update);
        }
    }
}