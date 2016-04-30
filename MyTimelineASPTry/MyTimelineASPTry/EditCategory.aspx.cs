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
    public partial class EditCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //ViewState["itemId"] = Request.QueryString["itemId"];
            if (!IsPostBack)
            {
                categoryId = Request.QueryString["itemId"];
                InitializeCategoryData(categoryId, Session["userId"].ToString());
            }

        }

        public static string categoryId;
        protected async void buttonSaveCategoryChanges_Click(object sender, EventArgs e)
        {

            Response.Write(textBoxCategoryName.Text);
            Response.Write(textBoxCategoryShortDescription.Text);

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<CategoriesCollection>("Categories");

            //string tagId = Request.QueryString["itemId"];
            //string  tagId = Session["tagId"].ToString();
            var filter = Builders<CategoriesCollection>.Filter.Eq("id", categoryId);

            string id = "";


            var filterParent = Builders<CategoriesCollection>.Filter.Eq("categoryName", textBoxParentName.Text);



            long exists = collection.Find(filterParent).CountAsync().Result;

             Response.Write(" nr = " + exists);

            if (exists > 0)
            {

                id = collection.Find(filterParent).FirstAsync().Result.id;
                Response.Write("Am dat id = " + id);

                //ObjectId objectId = ObjectId.Parse(hiddenFieldParentTagId.Value.ToString());

                var filter1 = Builders<CategoriesCollection>.Filter.Eq(d => d.categoryName, textBoxParentName.Text);

                CategoriesCollection parent = collection.Find(filter1).FirstAsync().Result;



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

                string categoryId = "Category_" + textBoxCategoryName.Text.Replace(' ', '_') + "_" + Session["userId"].ToString().Substring(0, 5);

                try
                {
                    var update = Builders<CategoriesCollection>.Update
                   .Set(d => d.categoryName, textBoxCategoryName.Text)
                   .Set(d => d.id, categoryId)
                   .Set(d => d.categoryName, textBoxCategoryName.Text)
                   .Set(d => d.parentCategories, parentCategories)
                   .Set("relativeImportance", textBoxRelativeImportance.Value.ToString())
                   .Set(d => d.description, textBoxCategoryShortDescription.Text)
                   .Set(d => d.categorySynonyms, categorySynonyms)
                   .Set(d => d.categoryInfo, CKEditorCategoryInformation.Text);


                    await collection.UpdateOneAsync(filter, update);
                    Response.Redirect("UserManaging.aspx?tab=categories", false);

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        async void InitializeCategoryData(string initCategoryId, string userId)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<CategoriesCollection>("Categories");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<CategoriesCollection>.Filter.Eq("id", initCategoryId);
            var documents = await collection.Find(filter).FirstAsync();
            if (documents.owner == userId)
            {
                textBoxCategoryName.Text = documents.categoryName.ToString();
                textBoxParentName.Text = documents.parentCategories[0]["parentName"].ToString();
                hiddenFieldParentCategoryId.Value = documents.parentCategories[0]["id"].ToString();
                textBoxRelativeImportance.Value = documents.relativeImportance.ToString();
                textBoxCategoryShortDescription.Text = documents.description;
                CKEditorCategoryInformation.Text = documents.categoryInfo;

                if (documents.categorySynonyms != null)
                    foreach (string synonym in documents.categorySynonyms)
                        textBoxSynonyms.Text += synonym + ";";



                if (documents.documentsBelonging != null)
                    foreach (BsonDocument d in documents.documentsBelonging)
                    {
                        ListItem doc = new ListItem();
                        doc.Value = d["id"].ToString();
                        doc.Text = d["documentName"].ToString();

                        listBoxDocumentsBelongin.Items.Add(doc);

                    }
            }
            else
            {
                Response.Redirect("Error.aspx", false);
            }

        }

        protected async void buttonDeleteCategory_Click(object sender, EventArgs e)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<CategoriesCollection>("Categories");
            var filter = Builders<CategoriesCollection>.Filter.Eq("id", categoryId);

            var result = await collection.DeleteOneAsync(filter);
            Response.Redirect("UserManaging.aspx?tab=categories", false);
        }

        
    }
}