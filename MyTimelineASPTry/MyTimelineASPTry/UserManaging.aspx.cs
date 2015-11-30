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
    public partial class UserManaging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void buttonSearchId_Click(object sender, EventArgs e)
        {
            listBoxOwns.Items.Clear();
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<PersonInfo>("Persons");
            //var documents = await collection.Find(new BsonDocument()).FirstAsync();

            var filter = Builders<PersonInfo>.Filter.Eq("owner", textBoxSearchId.Text);
            await collection.Find(filter).ForEachAsync(d => listBoxOwns.Items.Add(d.id.ToString()));

            //var documents = await collection.Find(filter).FirstAsync();
            //labelName.Text = documents.name.ToString();
            //labelDates.Text = documents.startdate + "-" + documents.enddate;
            //labelProfession.Text = documents.profession;
            //labelNationality.Text = documents.nationality;
            //labelReligion.Text = documents.religion;
            //imageProfile.ImageUrl = documents.image;
            Session["userId"] = textBoxSearchId.Text;

            buttonCreate.Enabled = true;
        }

        protected void listBoxOwns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxOwns.SelectedIndex.ToString() != "")
                buttonEdit.Enabled = true;
            else
                buttonEdit.Enabled = false;
        }

        protected void buttonEdit_Click(object sender, EventArgs e)
        {
            Session["userId"] = textBoxSearchId.Text;
            Session["itemId"] = listBoxOwns.SelectedValue;
            Response.Redirect("AddData.aspx?itemId=" + listBoxOwns.SelectedValue + "&scope=modify");
        }

        protected void buttonCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddData.aspx?scope=create");
        }
    }
}