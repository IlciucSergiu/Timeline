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

        protected  void buttonCreateCategory_Click(object sender, EventArgs e)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<CategoriesCollection>("Categories");

           


            if (textBoxCategoryName.Text != "")
            {

                // hiddenFieldParentTagId.Value = textBoxId.Text;
                //ObjectId objectId = ObjectId.Parse(hiddenFieldParentTagId.Value.ToString());


                var filter = Builders<CategoriesCollection>.Filter.Eq(d => d.categoryName, textBoxParentName.Text);

               CategoriesCollection parent = collection.Find(filter).FirstAsync().Result;



                BsonDocument parentCategory = new BsonDocument
                {
                    { "parentName",parent.categoryName},
                    { "id", parent.id },
                    {"_id", parent._id}
                   
                };
                BsonArray parentCategories = new BsonArray();
                parentCategories.Add(parentCategory);

                BsonArray categorySynonyms = new BsonArray();
                foreach (string categorySynonym in textBoxSynonyms.Text.Split(';'))
                {
                    if (categorySynonym.Length > 2)
                        categorySynonyms.Add(categorySynonym);
                }

                string id = "Category_"+textBoxCategoryName.Text.Replace(' ', '_') + "_" + Session["userId"].ToString().Substring(0, 5);
                string relativeImportance;
                if (textBoxRelativeImportance.Value.ToString() != "")
                    relativeImportance = textBoxRelativeImportance.Value;
                else
                    relativeImportance = "20";


                CategoriesCollection document = new CategoriesCollection();

                document.categoryName = textBoxCategoryName.Text;
                document.id = id;
                document.owner = Session["userId"].ToString();
                document.parentCategories = parentCategories;
                document.relativeImportance = Convert.ToInt32(relativeImportance);
                document.description = textBoxCategoryShortDescription.Text;
                document.categoryInfo = CKEditorCategoryInformation.Text;
                document.categorySynonyms = categorySynonyms;
                document.dateAdded = DateTime.UtcNow;

                if(textBoxParentName.Text != "")
                {
                    document.parentCategories = parentCategories;
                }

            //    {


            //    //{ "categoryName",textBoxCategoryName.Text},
            //    //{ "id",id },
            //    //{ "owner", Session["userId"].ToString() },
            //    //{ "parentCategories", parentCategories },
            //    //{ "relativeImportance", relativeImportance},
            //    //{ "description", textBoxCategoryShortDescription.Text },
            //    //{ "categoryInfo", CKEditorCategoryInformation.Text },
            //    //{ "categorySynonyms", categorySynonyms},
            //    //{ "dateAdded", DateTime.UtcNow}

            //};
                collection.InsertOneAsync(document);
                Response.Redirect("UserManaging.aspx?tab=categories", false);
            }
        }


        [WebMethod]
        public static string FindCategoryParentOptions(string inputValue)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<CategoriesCollection>("Categories");




            var filter = Builders<CategoriesCollection>.Filter.Regex(u => u.categoryName, new BsonRegularExpression("/" + inputValue + "/i"));

            string categoryOptions = "";
            collection.Find(filter).ForEachAsync(d => categoryOptions += d.categoryName.ToString() + "{0}" + d.id + "{;}").Wait();

            return categoryOptions;

        }


        [WebMethod]
        public static bool VerifyCategoryExistence(string inputValue)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<CategoriesCollection>("Categories");




            var filter = Builders<CategoriesCollection>.Filter.Eq(d => d.categoryName, inputValue);


            bool exists = false;
            collection.Find(filter).ForEachAsync(d => exists = true).Wait();

            return exists;

        }
    }
}