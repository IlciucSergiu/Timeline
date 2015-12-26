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
    public partial class AddNewTag : System.Web.UI.Page
    {
        protected  void Page_Load(object sender, EventArgs e)
        {
          
        }
       

        protected void buttonCreateTag_Click(object sender, EventArgs e)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("Tags");

            if (textBoxTagName.Text != "")
            {
                 BsonDocument document = new BsonDocument
            {
                //{ "_id", firstName.Text.ToLower() +"_"+lastName.Text.ToLower() },
                 
                { "tagName",textBoxTagName.Text},
                { "id", textBoxTagName.Text+"_"+Session["userId"].ToString().Substring(0,5) },
                { "owner", Session["userId"].ToString() },
                { "parentName", textBoxParentName.Text },
                { "relativeImportance", textBoxRelativeImportance.Value},
                { "description", textBoxTagShortDescription.Text },
                { "tagInfo", CKEditorInformation.Text },
                { "dateAdded", DateTime.UtcNow}
                
            };
                collection.InsertOneAsync(document);
                Response.Redirect("UserManaging.aspx",false);
            }
        }

       
       
    }
}