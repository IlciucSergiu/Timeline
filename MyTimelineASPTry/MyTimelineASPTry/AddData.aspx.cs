﻿using System;
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

            if (IsPostBack)
                dateBirth.Value = ViewState["dateBirth"].ToString();
            else
            {
                ViewState["dateBirth"] = "10/12/2012";
                dateBirth.Value = ViewState["dateBirth"].ToString();
            }

        }

        bool setDate = false;
        string gender;
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<BsonDocument>("Persons");

            if (firstName.Text != "" && lastName.Text != "")
            {
                Response.Write("O mers  ");
                Response.Write(inputImportance.Value);



                if (RadioButtonListGender.SelectedIndex != -1)
                {
                    //Response.Write(RadioButtonListGender.SelectedIndex.ToString() + "    ");
                    if (RadioButtonListGender.SelectedValue == "Male")
                    { gender = "male"; }
                    else { gender = "female"; }
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

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            // Response.Write(HttpRequest.RawUrl);
            string urlCurrent = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath + "AddData";

            var dom = CQ.CreateFromUrl(urlCurrent);

            var formEssential = dom["#divAddEssentials"];
            divAddEssentials.Visible = false;


            Response.Write("O mers !?");
        }
        string jsString;


        protected async void Button1_Click(object sender, EventArgs e)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<PersonInfo>.Filter.Eq("id", textBoxId.Text); ;
            var documents = await collection.Find(filter).FirstAsync();
            labelName.Text = documents.name.ToString();
            labelDates.Text = documents.startdate + "-" + documents.enddate;
            labelProfession.Text = documents.profession;
            labelNationality.Text = documents.nationality;
            labelReligion.Text = documents.religion;
            imageProfile.ImageUrl = documents.image;
            //await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += d+",");
            // jsString = "";
            // await collection.Find(new BsonDocument()).ForEachAsync(d => jsString += "{\"id\":\"" + d.id + "\",\"title\" : \"" + d.title + "\",\"startdate\" : \"" + d.startdate + "\",\"enddate\" : \"" + d.enddate + "\",\"importance\" : \"" + d.importance + "\",\"description\" : \"" + d.description + "\",\"link\" : \"" + d.link + "\",\"image\" : \"" + d.image + "\"},");
        }

        protected async void linkButtonEdit_Click(object sender, EventArgs e)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<PersonInfo>.Filter.Eq("id", textBoxId.Text); ;
            var documents = await collection.Find(filter).FirstAsync();
            firstName.Text = documents.name[0].ToString();

            lastName.Text = documents.name[1].ToString();


            dateBirth.Value = documents.startdate;
            ViewState["dateBirth"] = dateBirth.Value;
            dateDeath.Value = documents.enddate;
            ViewState["dateBirth"] = dateDeath.Value;
            Response.Write(dateBirth.Value);
            textBoxDescription.Text = documents.description;
            
                inputImportance.Value = documents.importance;
                textBoxLink.Text = documents.link;
                textBoxImage.Text = documents.image;
                textBoxProfession.Text = documents.profession;
                textBoxNationality.Text = documents.nationality;
                textBoxReligion.Text = documents.religion;

                gender = documents.gender;
               
              
                if (gender == "male")
                {
                    RadioButtonListGender.SelectedIndex = 0;
                }
                else
                    RadioButtonListGender.SelectedIndex = 1;
            
        }
    }
}