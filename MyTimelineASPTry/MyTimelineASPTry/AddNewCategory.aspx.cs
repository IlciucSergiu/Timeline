using System;
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
    public partial class AddNewCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonCreateCategory_Click(object sender, EventArgs e)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("Categories");

            if (textBoxCategoryName.Text != "")
            {

                // hiddenFieldParentTagId.Value = textBoxId.Text;
                //ObjectId objectId = ObjectId.Parse(hiddenFieldParentTagId.Value.ToString());
                BsonDocument parentTag = new BsonDocument
                {
                    { "parentName",textBoxParentName.Text},
                    { "id", hiddenFieldParentCategoryId.Value },
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

                string id = "Category_"+textBoxCategoryName.Text.Replace(' ', '_') + "_" + Session["userId"].ToString().Substring(0, 5);
                string relativeImportance;
                if (textBoxRelativeImportance.Value.ToString() != "")
                    relativeImportance = textBoxRelativeImportance.Value;
                else
                    relativeImportance = "20";
                BsonDocument document = new BsonDocument
            {


                { "categoryName",textBoxCategoryName.Text},
                { "id",id },
                { "owner", Session["userId"].ToString() },
                { "parentCategories", parentTags },
                { "relativeImportance", relativeImportance},
                { "description", textBoxCategoryShortDescription.Text },
                { "categoryInfo", CKEditorCategoryInformation.Text },
                { "categorySynonyms", tagSynonyms},
                { "dateAdded", DateTime.UtcNow}

            };
                collection.InsertOneAsync(document);
                Response.Redirect("UserManaging.aspx?tab=categories", false);
            }
        }


        [WebMethod]
        public static string FindCategoryParentOptions(string inputValue)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<CategoriesCollection>("Categories");




            var filter = Builders<CategoriesCollection>.Filter.Regex(u => u.categoryName, new BsonRegularExpression("/" + inputValue + "/i"));

            string categoryOptions = "";
            collection.Find(filter).ForEachAsync(d => categoryOptions += d.categoryName.ToString() + "{0}" + d.id + "{;}").Wait();

            return categoryOptions;

        }


        [WebMethod]
        public static bool VerifyCategoryExistence(string inputValue)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<CategoriesCollection>("Categories");




            var filter = Builders<CategoriesCollection>.Filter.Eq(d => d.categoryName, inputValue);


            bool exists = false;
            collection.Find(filter).ForEachAsync(d => exists = true).Wait();

            return exists;

        }
    }
}