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
        protected void Page_Load(object sender, EventArgs e)
        {

            //ViewState["itemId"] = Request.QueryString["itemId"];
            if (!IsPostBack)
            {
                tagId = Request.QueryString["itemId"];
                InitializeTagData(tagId, Session["userId"].ToString());
            }

        }

        public static string tagId;
        protected async void buttonSaveTagChanges_Click(object sender, EventArgs e)
        {

            // Response.Write(textBoxTagName.Text);
            // Response.Write(textBoxTagShortDescription.Text);

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<TagsCollection>("Tags");

            //string tagId = Request.QueryString["itemId"];
            //string  tagId = Session["tagId"].ToString();

           

            string id = "";


            var filterParent = Builders<TagsCollection>.Filter.Eq("tagName", textBoxParentName.Text);



            long exists = collection.Find(filterParent).CountAsync().Result;

            // Response.Write(" nr = " + exists);

            if (exists > 0)
            {

                //id = collection.Find(filterParent).FirstAsync().Result.id;
                //Response.Write("Am dat id = " + id);

                var filter = Builders<TagsCollection>.Filter.Eq("id", tagId);



                //ObjectId objectId = ObjectId.Parse(hiddenFieldParentTagId.Value.ToString());

                var filter1 = Builders<TagsCollection>.Filter.Eq(d => d.tagName, textBoxParentName.Text);

                TagsCollection parent = collection.Find(filter1).FirstAsync().Result;



                BsonDocument parentTag = new BsonDocument
                {
                    { "parentName",parent.tagName},
                    { "id", parent.id },
                    {"_id", parent._id}

                };


                BsonArray parentTags = new BsonArray();
                parentTags.Add(parentTag);

                BsonArray tagSynonyms = new BsonArray();
                foreach (string tagSynonym in textBoxSynonyms.Text.Split(';'))
                {
                    if (tagSynonym.Length > 2)
                        tagSynonyms.Add(tagSynonym);
                }


                try
                {
                    var update = Builders<TagsCollection>.Update
                   .Set("tagName", textBoxTagName.Text)
                   .Set("parentTags", parentTags)
                   .Set("relativeImportance", textBoxRelativeImportance.Value.ToString())
                   .Set("description", textBoxTagShortDescription.Text)
                   .Set("tagSynonyms", tagSynonyms)
                   .Set("tagInfo", CKEditorInformation.Text);


                    await collection.UpdateOneAsync(filter, update);
                    Response.Redirect("UserManaging.aspx?tab=tags", false);




                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
                Response.Write("Parent does not exist!");
        }

        async void InitializeTagData(string initTagId, string userId)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<TagsCollection>("Tags");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<TagsCollection>.Filter.Eq("id", initTagId);
            var documents = await collection.Find(filter).FirstAsync();
            if (documents.owner == userId)
            {
                textBoxTagName.Text = documents.tagName.ToString();
                textBoxParentName.Text = documents.parentTags[0]["parentName"].ToString();
                hiddenFieldParentTagId.Value = documents.parentTags[0]["id"].ToString();
                textBoxRelativeImportance.Value = documents.relativeImportance.ToString();
                textBoxTagShortDescription.Text = documents.description;
                CKEditorInformation.Text = documents.tagInfo;

                if (documents.tagSynonyms != null)
                    foreach (string synonym in documents.tagSynonyms)
                        textBoxSynonyms.Text += synonym + ";";

            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }

        }

        protected async void buttonDeleteTag_Click(object sender, EventArgs e)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<TagsCollection>("Tags");
            var filter = Builders<TagsCollection>.Filter.Eq("id", tagId);

            var result = await collection.DeleteOneAsync(filter);
            Response.Redirect("UserManaging.aspx?tab=tags", false);
        }


    }
}