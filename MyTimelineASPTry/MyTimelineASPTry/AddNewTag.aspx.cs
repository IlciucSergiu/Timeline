﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web.Services;

namespace MyTimelineASPTry
{
    public partial class AddNewTag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void buttonCreateTag_Click(object sender, EventArgs e)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("Tags");

            if (textBoxTagName.Text != "")
            {

                // hiddenFieldParentTagId.Value = textBoxId.Text;
                //ObjectId objectId = ObjectId.Parse(hiddenFieldParentTagId.Value.ToString());
                BsonDocument parentTag = new BsonDocument
                {
                    { "parentName",textBoxParentName.Text},
                    { "id", hiddenFieldParentTagId.Value },
                   // {"_id", objectId}
                   
                };
                BsonArray parentTags = new BsonArray();
                parentTags.Add(parentTag);

                BsonArray tagSynonyms = new BsonArray();
                foreach (string tagSynonym in textBoxSynonyms.Text.Split(';'))
                {
                    if (tagSynonym.Length > 2)
                        tagSynonyms.Add(tagSynonym);
                }

                string id = textBoxTagName.Text.Replace(' ','_') + "_" + Session["userId"].ToString().Substring(0, 5);
                string relativeImportance;

                if (textBoxRelativeImportance.Value.ToString() != "")
                    relativeImportance = textBoxRelativeImportance.Value;
                else
                    relativeImportance = "20";
                BsonDocument document = new BsonDocument
            {
                
                 
                { "tagName",textBoxTagName.Text},
                { "id",id },
                { "owner", Session["userId"].ToString() },
                { "parentTags", parentTags },
                { "relativeImportance", relativeImportance},
                { "description", textBoxTagShortDescription.Text },
                { "tagInfo", CKEditorInformation.Text },
                { "tagSynonyms", tagSynonyms},
                { "dateAdded", DateTime.UtcNow}

            };
                collection.InsertOneAsync(document);
                Response.Redirect("UserManaging.aspx?tab=tags", false);
            }
        }


        [WebMethod]
        public static string FindTagParentOptions(string inputValue)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<TagsCollection>("Tags");

            


            var filter = Builders<TagsCollection>.Filter.Regex(u => u.tagName, new BsonRegularExpression("/" + inputValue + "/i"));

            string tagOptions = "";
            collection.Find(filter).ForEachAsync(d => tagOptions += d.tagName.ToString() + "{0}" + d.id + "{;}").Wait();

            return tagOptions;

        }


        [WebMethod]
        public static bool VerifyTagExistence(string inputValue)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<TagsCollection>("Tags");




            var filter = Builders<TagsCollection>.Filter.Eq("tagName",inputValue);


            bool exists = false;
            collection.Find(filter).Limit(1).ForEachAsync(d => exists = true).Wait();

            return exists;

        }

    }
}