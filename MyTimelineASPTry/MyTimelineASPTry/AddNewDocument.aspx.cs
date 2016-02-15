using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Web.Services;


namespace MyTimelineASPTry
{
    public partial class AddNewDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




        }


        
        public bool setDate = false, showEssential = false;
        string saveId;


        protected void buttonSubmit_Click(object sender, EventArgs e)
        {


            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<BsonDocument>("DocumentsCollection");
            var individualData = db.GetCollection<BsonDocument>("IndividualData");

            if (textBoxCompleteName.Text != "")
            {



                

                saveId = textBoxCompleteName.Text.Replace(" ", "_");
                //Response.Write(saveId);
                ViewState["itemId"] = saveId;

                BsonArray separatedNames = new BsonArray();

                foreach (string name in textBoxCompleteName.Text.Split(' '))
                {
                    if (name != "")
                        separatedNames.Add(name);
                }

                string endDate;
                if (checkBoxContemporary.Checked == true)
                {
                    endDate = "contemporary";
                }
                else
                {
                    endDate = endDatePicker.Value;
                    if (endDateEra.Value == "BC" && endDate != "")
                        endDate = "-" + endDate;
                }
                string startDate = startDatePicker.Value;

                if (startDateEra.Value == "BC")
                    startDate = "-" + startDate;

                    
               

                BsonDocument document = new BsonDocument
            {

                 { "owner", Session["userId"].ToString() },
                { "id", saveId },
                { "name",separatedNames},
                { "title", textBoxCompleteName.Text},
                { "startdate", startDate},
               // { "enddate", endDate},
                { "description", textBoxDescription.Text},
                { "link", textBoxLink.Text },
                { "image", textBoxImage.Text },
                { "dateAdded", DateTime.UtcNow }


            };
                if(endDate != "")
                document.Add( "enddate", endDate);

                collection.InsertOneAsync(document);


                // add individual data


                BsonDocument individualDocument = new BsonDocument
            {
                { "owner", Session["userId"].ToString() },
                { "id", saveId },
                { "dateAdded", DateTime.UtcNow }
            };

                individualData.InsertOneAsync(individualDocument);


                Session["itemId"] = saveId;
                Response.Redirect("EditDocument.aspx?itemId=" + saveId + "&scope=modify");


            }
            else
            {
                Response.Write("Fill the neccesary fields.");
            }

        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManaging.aspx?tab=documents", false);
        }



    }
}