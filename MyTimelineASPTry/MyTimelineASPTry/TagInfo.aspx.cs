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
    public partial class TagInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          searchTag = Request.QueryString["tagName"];
          LoadTagData(searchTag);
        }
        string searchTag;
        void LoadTagData(string tagName)
        {


            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<TagsCollection>("Tags");

            //inputValue = "ev";
            var filter = Builders<TagsCollection>.Filter.Eq(u => u.tagName, tagName);


            

            collection.Find(filter).ForEachAsync(d =>
            {
                labelTag.Text = d.tagName;
                containerTagDescription.InnerText = d.description;
                containerTagInfo.InnerHtml = d.tagInfo;
            }).Wait();


        

        }
    }
}