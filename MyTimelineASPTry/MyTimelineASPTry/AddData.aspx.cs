using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using HtmlAgilityPack;
using CsQuery;

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
                    // Response.Write("am ajuns la viewstate si este: " + ViewState["itemId"].ToString());
                    itemId = ViewState["itemId"].ToString();
                }

            }
            else if (scope == "modify")
            {
                if (!IsPostBack)
                {

                    buttonSubmit.Visible = false;
                    if (Session["itemId"] != null)
                        ViewState["itemId"] = Session["itemId"].ToString();
                    // ViewState["itemId"] = Request.QueryString["itemId"];
                    //Response.Write("user : "+Session["userId"].ToString());
                    // Response.Write("item : " + Request.QueryString["itemId"]);
                    //ViewState["userId"] = Request.QueryString["userId"];
                    cancelRequest = "hide";
                }

                itemId = ViewState["itemId"].ToString();
                userId = Session["userId"].ToString();

                InitializeItem(userId, itemId);
            }
        }

        string userId, itemId;
        public bool setDate = false, showEssential = false;
        string gender, scope, cancelRequest, saveId;


        protected void buttonSubmit_Click(object sender, EventArgs e)
        {

            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("Persons");

            if (textBoxCompleteName.Text != "")
            {
                //Response.Write("O mers  ");
                // Response.Write(inputImportance.Value);



                if (RadioButtonListGender.SelectedIndex != -1)
                {
                    //Response.Write(RadioButtonListGender.SelectedIndex.ToString() + "    ");
                    if (RadioButtonListGender.SelectedValue == "Male")
                    { gender = "male"; }
                    else { gender = "female"; }
                }
                else { Response.Write("Please select a gender."); }

                string endDate;
                if (checkBoxContemporary.Checked == true)
                {
                    endDate = "contemporary";
                }
                else
                    endDate = dateDeath.Value;
                saveId = textBoxCompleteName.Text.ToLower().Replace(" ", "_");//firstName.Text.ToLower() + "_" + lastName.Text.ToLower();
                Response.Write(saveId);
                ViewState["itemId"] = saveId;

                BsonArray separatedNames = new BsonArray();

                foreach (string name in textBoxCompleteName.Text.Split(' '))
                {
                    if (name != "")
                        separatedNames.Add(name);
                }

                string[] separateTag;
                BsonArray tagsArray = new BsonArray();
                foreach (ListItem tag in listBoxTags.Items)
                {



                    separateTag = tag.Text.Split(' ');

                    BsonDocument tagDocument = new BsonDocument {

                        { "tagName", separateTag[0] },
                        { "tagImportance", separateTag[1] }
                 };


                    tagsArray.Add(tagDocument);
                }

                BsonDocument document = new BsonDocument
            {
                //{ "_id", firstName.Text.ToLower() +"_"+lastName.Text.ToLower() },
                 { "owner", Session["userId"].ToString() },
                { "id", saveId },
                { "name",separatedNames},
                { "title", textBoxCompleteName.Text},
                { "startdate", dateBirth.Value },
                { "enddate", endDate},
                { "description", textBoxDescription.Text },
                { "importance", inputImportance.Value },
                { "link", textBoxLink.Text },
                { "image", textBoxImage.Text },
                { "profession", textBoxProfession.Text },
                { "nationality", textBoxNationality.Text },
                { "religion", textBoxReligion.Text },
                { "gender", gender },
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



        protected async void Button1_Click(object sender, EventArgs e)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<PersonInfo>.Filter.Eq("id", textBoxId.Text); ;
            var documents = await collection.Find(filter).FirstAsync();
            labelName.Text = documents.name.ToString();
            labelDates.Text = documents.startdate + " - " + documents.enddate;
            labelProfession.Text = documents.profession;
            labelNationality.Text = documents.nationality;
            labelReligion.Text = documents.religion;
            imageProfile.ImageUrl = documents.image;
            //await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += d+",");
            // jsString = "";
            // await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += "{\"id\":\"" + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate + "\",\"enddate\" : \"" + d.enddate + "\",\"importance\" : \"" + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \"" + d.link + "\",\"image\" : \"" + d.image + "\"},");
        }

        protected async void linkButtonEdit_Click(object sender, EventArgs e)
        {
            setDate = true;
            showEssential = true;
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();
            // Response.Write("<++"+itemId+"++>");
            var filter = Builders<PersonInfo>.Filter.Eq("id", itemId);
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
            Response.Write(dateBirth.Value);
            textBoxDescription.Text = documents.description;

            inputImportance.Value = documents.importance;
            textBoxLink.Text = documents.link;
            textBoxImage.Text = documents.image;
            textBoxProfession.Text = documents.profession;
            textBoxNationality.Text = documents.nationality;
            textBoxReligion.Text = documents.religion;

            gender = documents.gender;


            if (gender == "male")
            {
                RadioButtonListGender.SelectedIndex = 0;
            }
            else
                RadioButtonListGender.SelectedIndex = 1;

            if(listBoxTags.Items.Count == 0)
            if (documents.tags != null)
                foreach (var tag in documents.tags)
                {
                    listBoxTags.Items.Add(tag[0].ToString() + " " + tag[1].ToString());
                }

        }

        protected async void buttonModify_Click(object sender, EventArgs e)
        {
            showEssential = false;
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");

            if (RadioButtonListGender.SelectedIndex != -1)
            {
                
                if (RadioButtonListGender.SelectedValue == "Male")
                { gender = "male"; }
                else { gender = "female"; }
            }
            else { Response.Write("Please select a gender."); }

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
            foreach (ListItem tag in listBoxTags.Items)
            {



                separateTag = tag.Text.Split(' ');

                BsonDocument tagDocument = new BsonDocument {

                        { "tagName", separateTag[0] },
                        { "tagImportance", separateTag[1] }
                 };


                tagsArray.Add(tagDocument);
            }

            var filter = Builders<PersonInfo>.Filter.Eq("id", itemId);

            var update = Builders<PersonInfo>.Update
                .Set("name", separatedNames)
                .Set("title", textBoxCompleteName.Text)
                .Set("startdate", dateBirth.Value)
                .Set("enddate", endDate)
                .Set("description", textBoxDescription.Text)
                .Set("importance", inputImportance.Value)
                .Set("link", textBoxLink.Text)
                .Set("image", textBoxImage.Text)
                .Set("profession", textBoxProfession.Text)
                .Set("nationality", textBoxNationality.Text)
                .Set("religion", textBoxReligion.Text)
                .Set("gender", gender)
                .Set("tags", tagsArray);
            var result = await collection.UpdateOneAsync(filter, update);
            InitializeItem(userId, itemId);
        }

        protected async void InitializeItem(string initUserId, string initItemId)
        {
            {

                MongoClient mclient = new MongoClient();
                var db = mclient.GetDatabase("Timeline");

                var collection = db.GetCollection<PersonInfo>("Persons");
                //var documents = await collection.Find(new BsonDocument()).FirstAsync();

                var filter = Builders<PersonInfo>.Filter.Eq("id", initItemId);
                var documents = await collection.Find(filter).FirstAsync();
                labelName.Text = documents.name.ToString();
                labelDates.Text = documents.startdate + " - " + documents.enddate;
                labelProfession.Text = documents.profession;
                labelNationality.Text = documents.nationality;
                labelReligion.Text = documents.religion;
                imageProfile.ImageUrl = documents.image;
                //await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += d+",");
                // jsString = "";
                // await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += "{\"id\":\"" + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate + "\",\"enddate\" : \"" + d.enddate + "\",\"importance\" : \"" + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \"" + d.link + "\",\"image\" : \"" + d.image + "\"},");

                //MongoClient mgClient = new MongoClient();
                //var db1 = mgClient.GetDatabase("Timeline");
                if (ItemExists(itemId))
                {
                    var collection1 = db.GetCollection<IndividualData>("IndividualData");
                    var filter1 = Builders<IndividualData>.Filter.Eq("id", itemId);
                    var item = await collection1.Find(filter1).FirstAsync();




                    // listBoxLinks.Items.Clear();
                    if (!IsPostBack)
                    {

                        CKEditorInformation.Text = item.htmlInformation;

                        if (item.additionalLinks != null)
                            foreach (var links in item.additionalLinks)
                            {
                                listBoxLinks.Items.Add(links.ToString());
                            }

                        if (item.additionalBooks != null)
                            foreach (var book in item.additionalBooks)
                            {
                                listBoxBooks.Items.Add(book.ToString());
                            }

                       
                    }

                }
            }
        }

        protected async void buttomSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBoxTags.Items.Count != 0)
            {
                MongoClient mclient = new MongoClient();
                var db = mclient.GetDatabase("Timeline");

                var collection = db.GetCollection<PersonInfo>("Person");

                string[] separateTag;
                BsonArray tagsArray = new BsonArray();
                foreach (ListItem tag in listBoxTags.Items)
                {



                    separateTag = tag.Text.Split(' ');

                    BsonDocument tagDocument = new BsonDocument {

                        { "tagName", separateTag[0] },
                        { "tagImportance", separateTag[1] }
                    };


                    tagsArray.Add(tagDocument);

                    var filter = Builders<PersonInfo>.Filter.Eq("id", itemId);
                    
                    var update = Builders<PersonInfo>.Update
                        .Set("tags", tagsArray);

                   
                    var result = await collection.UpdateOneAsync(filter, update);
                }


            }


            if (ItemExists(itemId))
            {

                MongoClient mclient = new MongoClient();
                var db = mclient.GetDatabase("Timeline");

                var collection = db.GetCollection<IndividualData>("IndividualData");

                BsonArray linksArray = new BsonArray();
                foreach (ListItem link in listBoxLinks.Items)
                {
                    linksArray.Add(link.Text);
                }


                BsonArray booksArray = new BsonArray();
                foreach (ListItem book in listBoxBooks.Items)
                {
                    booksArray.Add(book.Text);
                    // booksArray.Add(new bso);
                }

                

                var filter = Builders<IndividualData>.Filter.Eq("id", itemId);
                
                var update = Builders<IndividualData>.Update
                    .Set("htmlInformation", CKEditorInformation.Text)
                    .Set("additionalLinks", linksArray)
                    .Set("additionalBooks", booksArray);

               
                var result = await collection.UpdateOneAsync(filter, update);
            }
            else
            {
                MongoClient mgClient = new MongoClient();
                var db = mgClient.GetDatabase("Timeline");
                var collection = db.GetCollection<BsonDocument>("IndividualData");


                BsonArray linksArray = new BsonArray();
                foreach (ListItem link in listBoxLinks.Items)
                {
                    linksArray.Add(link.Text);
                }

                BsonArray booksArray = new BsonArray();
                foreach (ListItem book in listBoxBooks.Items)
                {
                    booksArray.Add(book.Text);
                    // booksArray.Add(new bso);
                }

               

                BsonDocument document = new BsonDocument
            {
                
                 //{ "owner", ViewState["userId"].ToString() },
                { "id", itemId },
                { "owner", userId },
                { "htmlInformation", CKEditorInformation.Text },
                { "additionalLinks", linksArray },
                { "additionalBooks", booksArray},
               

            };
                await collection.InsertOneAsync(document);
            }

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
            //TableCell tagName = new TableCell();
            //tagName.Text = textBoxTagName.Text;

            // TableCell tagImportance = new TableCell();
            //tagImportance.Text = inputImportance.ToString();

            //TableRow tagRow = new TableRow();
            //tagRow.Controls.Add(tagName);
            //tagRow.Controls.Add(tagImportance);

            //tableTags.Controls.Add(tagRow);

            listBoxTags.Items.Add(textBoxTagName.Text + " " + inputImportanceTag.Value);
        }




    }

}