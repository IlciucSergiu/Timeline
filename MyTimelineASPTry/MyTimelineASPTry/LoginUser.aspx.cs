using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using MongoDB.Bson;
using DevOne.Security.Cryptography.BCrypt;

namespace MyTimelineASPTry
{
    public partial class SignUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void buttonSearchId_Click(object sender, EventArgs e)
        {
            labelEmailVerification.Visible = false;
            labelPasswordVerification.Visible = false;

            if (ItemExists(textBoxSearchId.Text))
            {
                MongoClient mclient = new MongoClient();
                var db = mclient.GetDatabase("Timeline"); 

                var collection = db.GetCollection<UserData>("Users");
                //var documents = await collection.Find(new BsonDocument()).FirstAsync();

                var filter = Builders<UserData>.Filter.Eq("email", textBoxSearchId.Text);
                //await collection.Find(filter).ForEachAsync(d => listBoxOwns.Items.Add(d.id.ToString()));

                var document = await collection.Find(filter).FirstAsync();


                if (BCryptHelper.CheckPassword(textBoxPassword.Text, document.password))
                {
                    Session["userId"] = document.email;
                    //Response.Write("Te-ai logat cu username " + Session["userId"].ToString() +"  si parola " +document.password);
                    Response.Redirect("UserManaging.aspx", false);
                }
                else
                {
                    labelPasswordVerification.Visible = true;
                    labelPasswordVerification.Text = "Password is incorect";
                }
            }
            else
            {
                labelEmailVerification.Visible = true;
                labelEmailVerification.Text = "This email is not registred";
            }


           

        }
        bool ItemExists(string insert)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<UserData>("Users");
            var filter = Builders<UserData>.Filter.Eq("email", insert);
            var count = collection.Find(filter).CountAsync();

            Response.Write(count.Result);
            if (Convert.ToInt32(count.Result) != 0)
                return true;
            else
                return false;
        }

        
    }
}