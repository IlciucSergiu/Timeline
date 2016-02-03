using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MyTimelineASPTry
{
    public partial class UserManaging : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {

            CKEditorProfileInfo.Toolbar = CKEditorProfileInfo.ToolbarBasic;
            CKEditorProfileInfo.Width = 700;
            CKEditorProfileInfo.Height = 250;
            //CKEditorProfileInfo.ResizeMaxHeight = 400;

            // Response.Write(listBoxOwns.Items.Count);
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var user = db.GetCollection<UserData>("Users");
            var filterUser = Builders<UserData>.Filter.Eq("email", Session["userId"].ToString());
            await user.Find(filterUser).ForEachAsync(d =>
            {
                LabelUsername.Text = d.firstName.ToString() + " " + d.lastName.ToString();

                if (!IsPostBack)
                {
                    CKEditorProfileInfo.Text = d.profileInfo;
                    textBoxProfileImage.Text = d.image;
                }

                    if (d.image != null)
                {
                  
                    profileImage.Src = d.image;
                    profileImageEdit.Src = d.image;
                }
                else
                {
                    profileImage.Src = "https://cdn4.iconfinder.com/data/icons/linecon/512/photo-256.png";
                  
                    profileImageEdit.Src = "https://cdn4.iconfinder.com/data/icons/linecon/512/photo-256.png";
                }
            });

            GetUserReputation(Session["userId"].ToString());
            

            var collection = db.GetCollection<DocumentInfo>("DocumentsCollection");


            var filter = Builders<DocumentInfo>.Filter.Eq("owner", Session["userId"].ToString());

            int numberOfDocuments = 0;
            await collection.Find(filter).ForEachAsync(d =>
            {
                numberOfDocuments++;
                documentsContainer.Controls.Add(new LiteralControl { Text = "<hr><div class=\"documentsListElement\" > <a class=\"editDocumentLink\" href = \"EditDocument.aspx?itemId=" + d.id.ToString() + "&scope=modify\" >" + d.title.ToString() + "</a></div>" });
                //documentsContainer.Controls.Add(new LiteralControl { Text = "<div class=\"documentsListElement\" value=\"" + d.id.ToString() + "\">" + d.title.ToString() + "</div>" });
                //documentsContainer.InnerHtml +="<div class=\"documentElement\" value=\"" + d.id.ToString() + "\">" + d.title.ToString() + "</div>";
            });

            labelNumeberOfDocuments.Text = "(" + numberOfDocuments + ")";






            var collectionTags = db.GetCollection<TagsCollection>("Tags");

            int numberOfTags = 0;
            var filterTags = Builders<TagsCollection>.Filter.Eq("owner", Session["userId"].ToString());
            await collectionTags.Find(filterTags).ForEachAsync(d =>
            {
                numberOfTags++;
                tagsContainer.Controls.Add(new LiteralControl { Text = "<hr><div class=\"tagsListElement\" > <a class=\"editTagLink\" href=\"EditTag.aspx?itemId=" + d.id.ToString() + " \">" + d.tagName + "</a></div>" });

                // listBoxTags.Items.Add(d.id.ToString());
            }
                );
            labelNumberOfTags.Text = "(" + numberOfTags + ")";




            var collectionCategories = db.GetCollection<CategoriesCollection>("Categories");

            int numberOfCategories = 0;
            var filterCategories = Builders<CategoriesCollection>.Filter.Eq("owner", Session["userId"].ToString());
            await collectionCategories.Find(filterCategories).ForEachAsync(d =>
            {
                numberOfCategories++;
                categoriesContainer.Controls.Add(new LiteralControl { Text = "<hr><div class=\"tagsListElement\" > <a class=\"editTagLink\" href=\"EditCategory.aspx?itemId=" + d.id.ToString() + " \">" + d.categoryName + "</a></div>" });

                // listBoxTags.Items.Add(d.id.ToString());
            }
                );
            labelNumberOfCategories.Text = "(" + numberOfCategories + ")";





        }


       async void GetUserReputation(string userId)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var document = db.GetCollection<IndividualData>("IndividualData");
            var filterDocument = Builders<IndividualData>.Filter.Eq("owner", userId);

            int totalNumberOfViews = 0;
            int totalNumberOfVotes = 0;
            await document.Find(filterDocument).ForEachAsync(d =>
            {
                //if(d.timesViewed != null)
               // Response.Write(d.timesViewed.ToString());
                totalNumberOfViews += d.timesViewed;
                totalNumberOfVotes += d.votes;
            });


            int reputation = (totalNumberOfViews / 10) + (totalNumberOfVotes * 2);
           
           labelReputation.Text = "Reputation " + reputation.ToString();

            var userCollection = db.GetCollection<UserData>("Users");
            var filterUser = Builders<UserData>.Filter.Eq("email", userId);

            var update = Builders<UserData>.Update
                    .Set("reputation", reputation);
            var result = await userCollection.UpdateOneAsync(filterUser, update);
        }

        //protected void buttonEdit_Click(object sender, EventArgs e)
        //{
        //    Response.Write("ai selectat :" + listBoxOwns.SelectedIndex.ToString());
        //   // Session["userId"] = textBoxSearchId.Text;

        //    if (listBoxOwns.SelectedIndex >= 0) { 
        //   Session["itemId"] = listBoxOwns.SelectedValue;
        //    Response.Redirect("AddData.aspx?scope=modify",false);
        //    }
        //}

        protected void buttonCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewDocument.aspx", false);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }

      

        protected void buttonCreateTag_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewTag.aspx", false);
        }

        protected void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var user = db.GetCollection<UserData>("Users");
            var filterUser = Builders<UserData>.Filter.Eq("email", Session["userId"].ToString());

            string imageLink = textBoxProfileImage.Text;
            var update = Builders<UserData>.Update
               .Set(d => d.profileInfo, CKEditorProfileInfo.Text)
                .Set(d => d.image, imageLink);

            profileImage.Src = imageLink;
            profileImageEdit.Src = imageLink;

            user.UpdateOneAsync(filterUser, update).Wait();
        }

        protected void buttonCreateCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewCategory.aspx", false);
        }
    }
}