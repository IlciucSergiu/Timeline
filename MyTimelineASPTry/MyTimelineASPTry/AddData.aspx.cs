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
    public partial class AddData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("Personalities");

            if(firstName.Text != "" && lastName.Text != ""){
                Response.Write("O mers");
                Response.Write(inputImportance.Value + "adsasdg");
                
                BsonDocument document = new BsonDocument
            {
                { "_id", firstName.Text.ToLower() +"_"+lastName.Text.ToLower() },
                { "id", firstName.Text.ToLower() +"_"+lastName.Text.ToLower() },
                { "name",new BsonArray{ firstName.Text,lastName.Text} },
                { "title", firstName.Text+" "+lastName.Text },
                { "startdate", dateBirth.Value },
                { "enddate", dateDeath.Value },
                { "description", textBoxDescription.Text },
                { "importance", inputImportance.Value },
                { "link", textBoxLink.Text },
                { "image", textBoxImage.Text },
            };

                collection.InsertOneAsync(document);
                Response.Redirect("WebFormTimeline.aspx");

            }
            else
            {
                Response.Write("Fill the neccesary fields.");
            }
        }
    }
}