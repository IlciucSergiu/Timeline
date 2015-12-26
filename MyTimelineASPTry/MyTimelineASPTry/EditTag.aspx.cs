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
    public partial class EditTag : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            InitializaTagData(Session["tagId"].ToString(), Session["userId"].ToString());
        }

        protected async void buttonSaveTagChanges_Click(object sender, EventArgs e)
        {

            Response.Write(textBoxTagName.Text);
            Response.Write(textBoxTagShortDescription.Text);
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<TagsCollection>("Tags");
           string  tagId = Session["tagId"].ToString();
             var filter = Builders<TagsCollection>.Filter.Eq("id",tagId);

             try {
                 var update = Builders<TagsCollection>.Update
                .Set("tagName", textBoxTagName.Text)
                .Set("parentName", textBoxParentName.Text)
                .Set("relativeImportance", textBoxRelativeImportance.Value.ToString())
                .Set("description", textBoxTagShortDescription.Text)
                .Set("tagInfo", CKEditorInformation.Text);


            await collection.UpdateOneAsync(filter, update);
           
                 }
            catch(Exception ex)
             {
                 Response.Write(ex.Message);
             }

        }

        async void InitializaTagData(string tagId, string userId)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<TagsCollection>("Tags");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<TagsCollection>.Filter.Eq("id", tagId);
            var documents = await collection.Find(filter).FirstAsync();
            if (documents.owner == userId)
            {
                textBoxTagName.Text = documents.tagName.ToString();
                textBoxParentName.Text = documents.parentName;
                textBoxRelativeImportance.Value = documents.relativeImportance.ToString();
                textBoxTagShortDescription.Text = documents.description;
                CKEditorInformation.Text = documents.tagInfo;

            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }

        }

    }
}