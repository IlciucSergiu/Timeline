using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using HtmlAgilityPack;
using CsQuery;

namespace MyTimelineASPTry
{
    public partial class AddData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CKEditor1.ResizeEnabled = true;
            CKEditor1.ResizeMaxHeight = 500;
           
        }
        string gender;
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("Personalities");

            if(firstName.Text != "" && lastName.Text != ""){
                Response.Write("O mers  ");
                Response.Write(inputImportance.Value );

                

                if(RadioButtonListGender.SelectedIndex != -1)
                {
                    //Response.Write(RadioButtonListGender.SelectedIndex.ToString() + "    ");
                if(RadioButtonListGender.SelectedValue == "Male")
                {gender = "male"; }
                else { gender="female"; }
                }
                else { Response.Write("Please select a gender."); }


                BsonDocument document = new BsonDocument
            {
                //{ "_id", firstName.Text.ToLower() +"_"+lastName.Text.ToLower() },
                { "id", firstName.Text.ToLower() +"_"+lastName.Text.ToLower() },
                { "name",new BsonArray{ firstName.Text,lastName.Text} },
                { "title", firstName.Text+" "+lastName.Text },
                { "startdate", dateBirth.Value },
                { "enddate", dateDeath.Value },
                { "description", textBoxDescription.Text },
                { "importance", inputImportance.Value },
                { "link", textBoxLink.Text },
                { "image", textBoxImage.Text },
                { "profession", textBoxProfession.Text },
                { "nationality", textBoxNationality.Text },
                { "religion", textBoxReligion.Text },
                { "gender", gender }
            };

                collection.InsertOneAsync(document);

                

                
                 Response.Write("O mers !?");
               //Response.Redirect("WebFormTimeline.aspx", false);

            }
            else
            {
                Response.Write("Fill the neccesary fields.");
            }
        }

        protected  void buttonCancel_Click(object sender, EventArgs e)
        {
           // Response.Write(HttpRequest.RawUrl);
           string urlCurrent =  Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath + "AddData";

            var dom = CQ.CreateFromUrl(urlCurrent);
            
            var formEssential = dom["#divAddEssentials"];
            divAddEssentials.Visible = false;

            
            Response.Write("O mers !?");
        }
    }
}