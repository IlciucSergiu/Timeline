﻿using System;
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

            if (setDate)
            {
                dateBirth.Value = ViewState["dateBirth"].ToString();
                dateDeath.Value = ViewState["dateDeath"].ToString();
            }

            if (scope == "modify")
            {
                hiddenId.Value = itemId;
                InitializeItem(userId, itemId);
            }


        }
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            scope = Request.QueryString["scope"];
            // Response.Write(scope);
            if (scope == "create")
            {
                showEssential = true;
                buttonModify.Visible = false;
                cancelRequest = "return";
                // ViewState["userId"] = Request.QueryString["userId"];
                if (ViewState["itemId"] != null)
                {
                    Response.Write("HOHOHPJLADFHPADHFEHPFJNJPAHJDFHPUEOHFLDAHFJPEWHFJADPFJEADHFPEAHFJADLHFP");
                    // Response.Write("am ajuns la viewstate si este: " + ViewState["itemId"].ToString());
                    itemId = ViewState["itemId"].ToString();
                }

            }
            else if (scope == "modify")
            {
                if (!IsPostBack)
                {

                    buttonSubmit.Visible = false;
                    //if (Session["itemId"] != null)
                    // ViewState["itemId"] = Session["itemId"].ToString();
                    ViewState["itemId"] = Request.QueryString["itemId"];


                    //Response.Write("user : "+Session["userId"].ToString());
                    // Response.Write("item : " + Request.QueryString["itemId"]);
                    //ViewState["userId"] = Request.QueryString["userId"];
                    cancelRequest = "hide";
                }

                itemId = ViewState["itemId"].ToString();
                userId = Session["userId"].ToString();


            }
        }

        string userId, itemId;
        public bool setDate = false, showEssential = false;
        string scope, cancelRequest, saveId;


        protected void buttonSubmit_Click(object sender, EventArgs e)
        {

            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("DocumentsCollection");

            if (textBoxCompleteName.Text != "")
            {
                //Response.Write("O mers  ");
                // Response.Write(inputImportance.Value);



                //if (RadioButtonListGender.SelectedIndex != -1)
                //{
                //    //Response.Write(RadioButtonListGender.SelectedIndex.ToString() + "    ");
                //    if (RadioButtonListGender.SelectedValue == "Male")
                //    { gender = "male"; }
                //    else { gender = "female"; }
                //}
                //else { Response.Write("Please select a gender."); }





                string endDate;
                if (checkBoxContemporary.Checked == true)
                {
                    endDate = "contemporary";
                }
                else
                    endDate = dateDeath.Value;
                saveId = textBoxCompleteName.Text.ToLower().Replace(" ", "_");//firstName.Text.ToLower() + "_" + lastName.Text.ToLower();
                //Response.Write(saveId);
                ViewState["itemId"] = saveId;

                BsonArray separatedNames = new BsonArray();

                foreach (string name in textBoxCompleteName.Text.Split(' '))
                {
                    if (name != "")
                        separatedNames.Add(name);
                }

                string[] separateTag;
                BsonArray tagsArray = new BsonArray();
                foreach (string tag in hiddenFieldTags.Value.Split(';'))
                {


                    if (tag.Length > 3)
                    {
                        separateTag = tag.Split('-');

                        BsonDocument tagDocument = new BsonDocument {

                        { "tagName", separateTag[0] },
                        { "tagImportance", separateTag[1] }
                    };


                        tagsArray.Add(tagDocument);
                    }
                }

                BsonDocument document = new BsonDocument
            {

                 { "owner", Session["userId"].ToString() },
                { "id", saveId },
                { "name",separatedNames},
                { "title", textBoxCompleteName.Text},
                { "startdate", dateBirth.Value },
                { "enddate", endDate},
                { "description", textBoxDescription.Text},

                { "link", textBoxLink.Text },
                { "image", textBoxImage.Text },
                { "dateAdded", DateTime.UtcNow },

                { "tags", tagsArray }
            };
                collection.InsertOneAsync(document);

                #region personInfo
                //PersonInfo person1 = new PersonInfo();
                //person1.id = firstName.Text.ToLower() + "_" + lastName.Text.ToLower();
                //person1.name = new BsonArray { firstName.Text, lastName.Text };
                //person1.title = firstName.Text + " " + lastName.Text;
                //person1.startdate = dateBirth.Value;
                //person1.enddate = dateDeath.Value;
                //person1.importance = inputImportance.Value;
                //person1.image = textBoxImage.Text;
                //person1.description = "A very good musician.";
                //person1.link = textBoxLink.Text;

                //if (textBoxNationality.Text != "")
                //person1.nationality = textBoxNationality.Text;
                //if (textBoxProfession.Text != "")
                //person1.profession = textBoxProfession.Text;
                //if (textBoxReligion.Text != "")
                //person1.religion = textBoxReligion.Text;
                //if (gender != "")
                //person1.gender = gender;

                //collection.InsertOneAsync(person1);
                #endregion

                // showEssential = false;

                //InitializeItem(Session["userId"].ToString(), saveId);

                Session["itemId"] = saveId;
                Response.Redirect("AddData.aspx?itemId=" + saveId + "&scope=modify");


            }
            else
            {
                Response.Write("Fill the neccesary fields.");
            }

        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            //// Response.Write(HttpRequest.RawUrl);
            //string urlCurrent = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath + "AddData";

            //var dom = CQ.CreateFromUrl(urlCurrent);

            //var formEssential = dom["#divAddEssentials"];
            //divAddEssentials.Visible = false;


            //Response.Write("O mers !?");
            if (cancelRequest == "return")
            {
                Response.Redirect("UserManaging.aspx", false);
            }
            else
                showEssential = false;
        }





        protected async void linkButtonEdit_Click(object sender, EventArgs e)
        {
            setDate = true;
            showEssential = true;
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();
            // Response.Write("<++"+itemId+"++>");
            var filter = Builders<DocumentInfo>.Filter.Eq("id", itemId);
            var documents = await collection.Find(filter).FirstAsync();

            string completeName = "";
            foreach (string name in documents.name)
            {
                completeName += name + " ";
            }
            textBoxCompleteName.Text = completeName;
            //firstName.Text = documents.name[0].ToString();

            // lastName.Text = documents.name[1].ToString();


            dateBirth.Value = documents.startdate;
            ViewState["dateBirth"] = dateBirth.Value;
            dateDeath.Value = documents.enddate;
            ViewState["dateDeath"] = dateDeath.Value;
            //Response.Write(dateBirth.Value);


            textBoxDescription.Text = documents.description;

            //inputImportance.Value = documents.importance;
            textBoxLink.Text = documents.link;
            textBoxImage.Text = documents.image;
            imageDocument.ImageUrl = documents.image;
            // textBoxProfession.Text = documents.profession;
            //textBoxNationality.Text = documents.nationality;
            // textBoxReligion.Text = documents.religion;

            //  gender = documents.gender;


            //if (gender == "male")
            //{
            //    RadioButtonListGender.SelectedIndex = 0;
            //}
            //else
            //    RadioButtonListGender.SelectedIndex = 1;

            // if (listBoxTags.Items.Count == 0)
            listBoxTags.Items.Clear();
            if (documents.tags != null)
                foreach (var tag in documents.tags)
                {
                    ListItem tagItem = new ListItem();
                    tagItem.Text = tag[0].ToString() + " " + tag[1].ToString();
                    tagItem.Value = tag[0].ToString() + "-" + tag[1].ToString();
                    listBoxTags.Items.Add(tagItem);
                    hiddenFieldTags.Value += tagItem.Value.ToString() + ";";
                }
            //Response.Write(hiddenFieldTags.Value);

        }

        protected async void buttonModify_Click(object sender, EventArgs e)
        {
            // Response.Write(hiddenFieldTags.Value);
            showEssential = false;
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");

            //if (RadioButtonListGender.SelectedIndex != -1)
            //{

            //    if (RadioButtonListGender.SelectedValue == "Male")
            //    { gender = "male"; }
            //    else { gender = "female"; }
            //}
            //else { Response.Write("Please select a gender."); }






            // Response.Write(ReplaceToHTML(textBoxDescription.Text));

            string endDate;
            if (checkBoxContemporary.Checked == true)
            {
                endDate = "contemporary";
            }
            else
                endDate = dateDeath.Value;

            BsonArray separatedNames = new BsonArray();

            foreach (string name in textBoxCompleteName.Text.Split(' '))
            {
                if (name != "")
                    separatedNames.Add(name);
            }

            string[] separateTag;
            BsonArray tagsArray = new BsonArray();
            foreach (string tag in hiddenFieldTags.Value.Split(';'))
            {


                if (tag.Length > 3)
                {
                    separateTag = tag.Split('-');

                    BsonDocument tagDocument = new BsonDocument {

                        { "tagName", separateTag[0] },
                        { "tagImportance", separateTag[1] }
                 };


                    tagsArray.Add(tagDocument);
                }
            }

            var filter = Builders<DocumentInfo>.Filter.Eq("id", itemId);

            var update = Builders<DocumentInfo>.Update
                .Set("name", separatedNames)
                .Set("title", textBoxCompleteName.Text)
                .Set("startdate", dateBirth.Value)
                .Set("enddate", endDate)
                .Set("description", textBoxDescription.Text)
                //.Set("importance", inputImportance.Value)
                .Set("link", textBoxLink.Text)
                .Set("image", textBoxImage.Text)
                // .Set("profession", textBoxProfession.Text)
                //.Set("nationality", textBoxNationality.Text)
                //.Set("religion", textBoxReligion.Text)
                // .Set("gender", gender)
                .Set("tags", tagsArray);
            var result = await collection.UpdateOneAsync(filter, update);
            InitializeItem(userId, itemId);
        }

        protected async void InitializeItem(string initUserId, string initItemId)
        {
            // try
            //{

            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");


            var filter = Builders<DocumentInfo>.Filter.Eq("id", initItemId);
            var documents = await collection.Find(filter).FirstAsync();


            if (documents.owner == initUserId)
            {
                labelName.Text = documents.name.ToString();
                labelDates.Text = documents.startdate + " - " + documents.enddate;

                imageProfile.ImageUrl = documents.image;

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
                                //listBoxLinks.Items.Add(links.ToString());
                                hiddenFieldLinks.Value += links.ToString() + ";";
                            }



                        if (item.documentFeedback != null)
                        {
                            LoadFeedback(item.documentFeedback);
                        }

                        if (item.videoLinks != null)
                        {
                            textBoxVideoId.Text = item.videoLinks[0].ToString();
                        }




                    }


                }

            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }
            // }
            // catch (Exception ex)
            // {
            //     Response.Redirect("Error.aspx?" + ex.Message, false);
            //}
        }

        protected async void buttomSaveChanges_Click(object sender, EventArgs e)
        {


            if (ItemExists(itemId))
            {

                MongoClient mclient = new MongoClient();
                var db = mclient.GetDatabase("Timeline");

                var collection = db.GetCollection<IndividualData>("IndividualData");




                BsonArray linksArray = new BsonArray();
                foreach (string link in hiddenFieldLinks.Value.Split(';'))
                {
                    if (link.Length > 2)
                        linksArray.Add(link);

                }





                BsonArray videoLinks = new BsonArray();
                videoLinks.Add(textBoxVideoId.Text);



                var filter = Builders<IndividualData>.Filter.Eq("id", itemId);

                var update = Builders<IndividualData>.Update
                    .Set("htmlInformation", CKEditorInformation.Text)
                    .Set("additionalLinks", linksArray)
                    .Set("videoLinks", videoLinks);



                var result = await collection.UpdateOneAsync(filter, update);
            }
            else
            {
                MongoClient mgClient = new MongoClient();
                var db = mgClient.GetDatabase("Timeline");
                var collection = db.GetCollection<BsonDocument>("IndividualData");



                BsonArray linksArray = new BsonArray();
                foreach (string link in hiddenFieldLinks.Value.Split(';'))
                {
                    if (link.Length > 3)
                        linksArray.Add(link);

                }

                BsonArray booksArray = new BsonArray();
                foreach (ListItem book in listBoxBooks.Items)
                {
                    booksArray.Add(book.Text);
                    // booksArray.Add(new bso);
                }

                BsonArray videoLinks = new BsonArray();
                videoLinks.Add(textBoxVideoId.Text);



                BsonDocument document = new BsonDocument
            {
                
                 //{ "owner", ViewState["userId"].ToString() },
                { "id", itemId },
                { "owner", userId },
                { "htmlInformation", CKEditorInformation.Text },
                { "additionalLinks", linksArray },
                { "additionalBooks", booksArray},
                {"timesViewed", 0},
                {"videoLinks", videoLinks}



            };
                await collection.InsertOneAsync(document);
            }



            InitializeItem(Session["userId"].ToString(), itemId);
        }


        protected void checkBoxContemporary_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxContemporary.Checked == true)
                dateDeath.Disabled = true;
            else
                dateDeath.Disabled = false;
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

        protected void buttonAddLink_Click(object sender, EventArgs e)
        {
            listBoxLinks.Items.Add(textBoxAddLink.Text);
        }

        protected void buttonAddBook_Click(object sender, EventArgs e)
        {
            listBoxBooks.Items.Add(textBoxAddBooks.Text);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("WebFormTimeline.aspx", false);
        }

        protected void buttonAddTag_Click(object sender, EventArgs e)
        {


            listBoxTags.Items.Add(textBoxTagName.Text + " " + inputImportanceTag.Value);
        }



        protected void buttonSearchTag_Click(object sender, EventArgs e)
        {

            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<TagsCollection>("Tags");




            var filter = Builders<TagsCollection>.Filter.Regex("tagName", new BsonRegularExpression(/*"^"+*/textBoxTagName.Text));


            collection.Find(filter).ForEachAsync(d => Response.Write(d.tagName.ToString()));
        }



        [WebMethod]
        public static string FindTagOptions(string inputValue)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<TagsCollection>("Tags");




            var filter = Builders<TagsCollection>.Filter.Regex("tagName", new BsonRegularExpression(/*"^"+*/inputValue));

            string tagOptions = "";
            collection.Find(filter).ForEachAsync(d => tagOptions += d.tagName.ToString() + "{;}").Wait();

            return tagOptions;

        }

        [WebMethod]
        public static string InsertInTagCollection(string tagName, string documentId, int relativeImportance)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");


            var collectionDocument = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Regex("id", new BsonRegularExpression(documentId));

            DocumentInfo documentInfo = new DocumentInfo();
            collectionDocument.Find(filter).ForEachAsync(d =>
            {
                documentInfo._id = d._id;
                documentInfo.name = d.name;
                documentInfo.id = d.id;

            }).Wait();

            // introduc inregistrarea tagului in document
            BsonDocument tagDocument = new BsonDocument {

                        { "tagName", tagName },
                        { "tagImportance", relativeImportance }
                 };

            var updateDocument = Builders<DocumentInfo>.Update
                    .Push(p => p.tags, tagDocument);

            collectionDocument.UpdateOneAsync(filter, updateDocument).Wait();



            var collectionTags = db.GetCollection<TagsCollection>("Tags");

            BsonDocument documentsBelonging = new BsonDocument()
            {

                { "_id", documentInfo._id },
                { "id", documentInfo.id },
                { "documentName", documentInfo.name },
                { "relativeImportance", relativeImportance}

            };





            var filterTag = Builders<TagsCollection>.Filter.Regex("tagName", tagName);
            var updateTag = Builders<TagsCollection>.Update
                    // .Inc("votes", 1)
                    .Push(p => p.documentsBelonging, documentsBelonging);

            collectionTags.UpdateOneAsync(filterTag, updateTag).Wait();



            return "True";

        }

        protected void buttonSearchBook_Click(object sender, EventArgs e)
        {
            GbookSearchClient client = new GbookSearchClient("www.timetrail.com");
            IList<IBookResult> results = client.Search(textBoxAddBooks.Text, 2);
            // IList<IBookResult> results = client.Search(TextBox1.Text, 30);

            foreach (IBookResult book in results)
            {
                Response.Write(book.Title);
                Response.Write(book.BookId);
                Response.Write(book.Authors);
                // imageBookCover.ImageUrl = book.TbImage.Url;
            }
        }

        [WebMethod]
        public static string RemoveInTagCollection(string tagName, string documentId)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");


            var collectionDocument = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Regex("id", new BsonRegularExpression(documentId));

            DocumentInfo documentInfo = new DocumentInfo();
            collectionDocument.Find(filter).ForEachAsync(d =>
            {
                documentInfo._id = d._id;
                documentInfo.name = d.name;
                documentInfo.id = d.id;

            }).Wait();


            // sterg inregistrarea tagului din document
            var updateDocument = Builders<DocumentInfo>.Update
                       .Pull(p => p.tags, new BsonDocument(){
    { "tagName", tagName } });

            collectionDocument.UpdateOneAsync(filter, updateDocument).Wait();


            // sterg inregistrarea din  tag
            var collectionTags = db.GetCollection<TagsCollection>("Tags");


            var filterTag = Builders<TagsCollection>.Filter.Regex("tagName", tagName);
            var updateTag = Builders<TagsCollection>.Update

                    .Pull(p => p.documentsBelonging, new BsonDocument(){
    { "_id", documentInfo._id } });

            collectionTags.UpdateOneAsync(filterTag, updateTag).Wait();



            return "Deleted";

        }

        [WebMethod]
        public static string AddSelectedBook(string title, string authors, string isbn, string imageUrl, string documentId)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<IndividualData>("IndividualData");

            BsonDocument book = new BsonDocument()
            {
                { "title", title },
                { "authors", authors },
                { "isbn", isbn },
                { "imageUrl", imageUrl },
            };


            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                    // .Inc("votes", 1)
                    .Push(p => p.additionalBooks, book);

            collection.UpdateOneAsync(filter, update).Wait();

            return "Inserted";

        }


        [WebMethod]
        public static string RemoveSelectedBook(string title, string isbn, string documentId)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<IndividualData>("IndividualData");

            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                    .Pull(p => p.additionalBooks, new BsonDocument(){
    { "isbn", isbn } });

            collection.UpdateOneAsync(filter, update).Wait();

            return "Deleted";

        }


        [WebMethod]
        public static string AddAdditionalImage(string imageUrl, string documentId)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<IndividualData>("IndividualData");




            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                  .Push(p => p.imagesLinks, imageUrl);

            collection.UpdateOneAsync(filter, update).Wait();

            return "Inserted";

        }


        [WebMethod]
        public static string DeleteAdditionalImage(string imageUrl, string documentId)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<IndividualData>("IndividualData");




            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                  .Pull(p => p.imagesLinks, imageUrl);

            collection.UpdateOneAsync(filter, update).Wait();

            return "Deleted";

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