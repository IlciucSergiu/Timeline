using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web.Services;
using Google.API.Search;


namespace MyTimelineASPTry
{
    public partial class AddData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CKEditorInformation.ResizeEnabled = true;
            CKEditorInformation.ResizeMaxHeight = 500;



            hiddenId.Value = itemId;
            InitializeItem(userId, itemId);



        }
        protected void Page_PreLoad(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["itemId"] = Request.QueryString["itemId"];

            }
            itemId = ViewState["itemId"].ToString();
            userId = Session["userId"].ToString();

        }

        string userId, itemId;

        protected async void InitializeItem(string initUserId, string initItemId)
        {



            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Eq("id", itemId);
            var documents = await collection.Find(filter).FirstAsync();

            string completeName = "";
            foreach (string name in documents.name)
            {
                completeName += name + " ";
            }


            if (!IsPostBack)
            {

                if (documents.startdate.IndexOf("-") == 0)
                {
                    startDateEra.Value = "BC";
                    startDatePicker.Value = documents.startdate.TrimStart('-');
                    ViewState["dateBirth"] = startDatePicker.Value;
                }
                else {
                    startDateEra.Value = "AD";
                    startDatePicker.Value = documents.startdate;
                    ViewState["dateBirth"] = startDatePicker.Value;
                }

                if (documents.startdate.IndexOf("-") == 0)
                {
                    endDateEra.Value = "BC";
                    endDatePicker.Value = documents.enddate.TrimStart('-');
                    ViewState["dateDeath"] = endDatePicker.Value;
                }
                else {
                    endDateEra.Value = "AD";
                    endDatePicker.Value = documents.enddate;
                    ViewState["dateDeath"] = endDatePicker.Value;
                }


                textBoxCompleteName.Text = completeName;
                textBoxLink.Text = documents.link;
                textBoxImage.Text = documents.image;
                textBoxDescription.Text = documents.description;
            }
            imageDocument.ImageUrl = documents.image;

            listBoxTags.Items.Clear();
            if (documents.tags != null)
                foreach (var tag in documents.tags)
                {
                    ListItem tagItem = new ListItem();
                    tagItem.Text = tag[0].ToString() + " " + tag[1].ToString();
                    tagItem.Value = tag[0].ToString() + "-" + tag[1].ToString();
                    listBoxTags.Items.Add(tagItem);

                }

            listBoxCategories.Items.Clear();
            if (documents.categories != null)
                foreach (var category in documents.categories)
                {
                    ListItem categoryItem = new ListItem();
                    categoryItem.Text = category[0].ToString() + " - " + category[1].ToString();
                    categoryItem.Value = category[0].ToString() + "-" + category[1].ToString();
                    listBoxCategories.Items.Add(categoryItem);


                }

            if (documents.owner == initUserId)
            {


                if (ItemExists(itemId))
                {
                    var collection1 = db.GetCollection<IndividualData>("IndividualData");
                    var filter1 = Builders<IndividualData>.Filter.Eq("id", itemId);
                    var item = await collection1.Find(filter1).FirstAsync();


                    listBoxLinks.Items.Clear();
                    if (item.additionalLinks != null)
                        foreach (var links in item.additionalLinks)
                        {
                            listBoxLinks.Items.Add(links.ToString());

                        }

                    listBoxBooks.Items.Clear();
                    if (item.additionalBooks != null)
                        foreach (var book in item.additionalBooks)
                        {
                            if (book["title"] != null)
                            {
                                ListItem listBook = new ListItem();
                                listBook.Value = (book["isbn"].ToString());

                                if (book["authors"] != null)
                                    listBook.Text = (book["title"].ToString() + " - " + book["authors"].ToString());
                                else
                                    listBook.Text = (book["title"].ToString());

                                listBoxBooks.Items.Add(listBook);
                            }
                            else Response.Write("this book has no title");
                        }


                    if (item.imagesLinks != null)
                    {
                        addedImages.Controls.Clear();
                        foreach (string imageLink in item.imagesLinks)
                        {
                            addedImages.Controls.Add(new LiteralControl { Text = "<img   class=\"imageCollection\" src=\"" + imageLink + "\"/>" });

                        }
                    }


                    if (!IsPostBack)
                    {

                        CKEditorInformation.Text = item.htmlInformation;

                        if (item.additionalLinks != null)
                            foreach (var links in item.additionalLinks)
                            {

                                hiddenFieldLinks.Value += links.ToString() + ";";
                            }
                        if (item.documentFeedback != null)
                        {
                            LoadFeedback(item.documentFeedback);
                        }

                        if (item.videoLinks != null)
                        {
                            foreach (string videoId in item.videoLinks)
                                textBoxVideoId.Text += videoId + ";";
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }

        }

        protected async void buttomSaveChanges_Click(object sender, EventArgs e)
        {



            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collectionIndividual = db.GetCollection<IndividualData>("IndividualData");

            BsonArray linksArray = new BsonArray();
            foreach (string link in hiddenFieldLinks.Value.Split(';'))
            {
                if (link.Length > 2)
                    linksArray.Add(link);

            }

            BsonArray videoLinks = new BsonArray();
            foreach (string videoId in textBoxVideoId.Text.Split(';'))
            {
                if (videoId.Length > 4)
                    videoLinks.Add(videoId);
            }

            var filter = Builders<IndividualData>.Filter.Eq("id", itemId);

            var update = Builders<IndividualData>.Update
                .Set("htmlInformation", CKEditorInformation.Text)
                .Set("additionalLinks", linksArray)
                .Set("videoLinks", videoLinks);

            var result = await collectionIndividual.UpdateOneAsync(filter, update);




            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");




            string endDate;



            if (checkBoxContemporary.Checked == true)
            {
                endDate = "contemporary";
            }
            else
            {
                endDate = endDatePicker.Value;
                if (endDateEra.Value == "BC" && endDate != "")
                    endDate = "-" + endDate;
            }

            string startDate = startDatePicker.Value;

            if (startDateEra.Value == "BC")
                startDate = "-" + startDate;




            BsonArray separatedNames = new BsonArray();

            foreach (string name in textBoxCompleteName.Text.Split(' '))
            {
                if (name != "")
                    separatedNames.Add(name);
            }


            var filterDocument = Builders<DocumentInfo>.Filter.Eq("id", itemId);

            var updateDocument = Builders<DocumentInfo>.Update
                .Set("name", separatedNames)
                .Set("title", textBoxCompleteName.Text)
                .Set("startdate", startDate)
                .Set("enddate", endDate)
                .Set("description", textBoxDescription.Text)
                .Set("link", textBoxLink.Text)
                .Set("image", textBoxImage.Text);


            var updateResult = await collection.UpdateOneAsync(filterDocument, updateDocument);

            InitializeItem(Session["userId"].ToString(), itemId);
        }


        bool ItemExists(string id)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<IndividualData>("IndividualData");
            var filter = Builders<IndividualData>.Filter.Eq("id", id);
            var count = collection.Find(filter).CountAsync();


            if (Convert.ToInt32(count.Result) != 0)
                return true;
            else
                return false;
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManaging.aspx?tab=documents", false);
        }





        protected void buttonAddLink_Click(object sender, EventArgs e)
        {
            listBoxLinks.Items.Add(textBoxAddLink.Text);

        }


        public string ReplaceToHTML(string text)
        {
            string[] plainChar = new string[] { "\"", "'", "&" };
            string[] HTMLChar = new string[] { "<q>", "&#39;", "&amp;" };

            for (int i = 0; i < plainChar.Length; i++)
            {
                text = text.Replace(plainChar[i], HTMLChar[i]);
            }

            return text;
        }

        public string ReplaceToText(string text)
        {

            string[] plainChar = new string[] { "\"", "'", "&" };
            string[] HTMLChar = new string[] { "<q>", "&#39;", "&amp;" };

            for (int i = 0; i < plainChar.Length; i++)
            {
                text = text.Replace(HTMLChar[i], plainChar[i]);
            }

            Response.Write(text);
            return text;
        }

        protected void buttonDeleteDocument_Click(object sender, EventArgs e)
        {
            // Response.Write(hiddenId.Value);
            string id = hiddenId.Value;

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collectionDocuments = db.GetCollection<DocumentInfo>("DocumentsCollection");
            var collectionIndividual = db.GetCollection<IndividualData>("IndividualData");
            var collectionCategories = db.GetCollection<CategoriesCollection>("Categories");


            var filterDocuments = Builders<DocumentInfo>.Filter.Eq("id", id);
            var filterIndividual = Builders<IndividualData>.Filter.Eq("id", id);

            collectionDocuments.DeleteOneAsync(filterDocuments).Wait();
            collectionIndividual.DeleteOneAsync(filterIndividual).Wait();

            var updateCategory = Builders<CategoriesCollection>.Update
                       .Pull(p => p.documentsBelonging, new BsonDocument() { { "id", id } });

            collectionCategories.UpdateManyAsync(_ => true, updateCategory).Wait();


            Response.Redirect("UserManaging.aspx?tab=documents", false);
        }

        public void LoadFeedback(BsonArray documentFeedback)
        {
            int count = 0;

            foreach (string feedback in documentFeedback)
            {
                count++;
                feedbackContent.Controls.Add(new LiteralControl { Text = "<div class=\"divDocumentFeedback\" id=\"feedbackMessage_" + count + "\" ><p>" + feedback + "<p /></div>" });

            }
            labelFeedbackNumber.Text = "(" + count + ")";
        }


    }

}