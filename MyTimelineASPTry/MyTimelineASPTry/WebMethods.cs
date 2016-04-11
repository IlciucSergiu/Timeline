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

    public class Functions
    {
        [WebMethod]
        public static string FindCategoryOptions(string inputValue)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<CategoriesCollection>("Categories");




            var filter = Builders<CategoriesCollection>.Filter.Regex("categoryName", new BsonRegularExpression("/" + inputValue + "/i"));

            string categoryOptions = "";
            collection.Find(filter).ForEachAsync(d => categoryOptions += d.categoryName.ToString() + "{;}").Wait();

            return categoryOptions;

        }
    }
}