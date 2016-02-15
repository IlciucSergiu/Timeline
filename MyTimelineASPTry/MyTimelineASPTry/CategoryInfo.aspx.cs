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
    public partial class CategoryInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            searchCategory = Request.QueryString["categoryName"];
            LoadTagData(searchCategory);
        }
        string searchCategory;
        void LoadTagData(string categoryName)
        {


            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<CategoriesCollection>("Categories");

           
            var filter = Builders<CategoriesCollection>.Filter.Eq(u => u.categoryName, categoryName);

             collection.Find(filter).ForEachAsync(d =>
            {
                labelCategory.Text = d.categoryName;
                containerCategoryDescription.InnerText = d.description;
                containerCategoryInfo.InnerHtml = d.categoryInfo;
            }).Wait();

        }
    }
}