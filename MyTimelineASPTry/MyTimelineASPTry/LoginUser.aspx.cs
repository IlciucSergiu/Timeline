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
            

            if (ItemExists(textBoxSearchId.Text))
            {

                MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
                var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

                var collection = db.GetCollection<UserData>("Users");
                //var documents = await collection.Find(new BsonDocument()).FirstAsync();

                var filter = Builders<UserData>.Filter.Eq("email", textBoxSearchId.Text);
                //await collection.Find(filter).ForEachAsync(d => listBoxOwns.Items.Add(d.id.ToString()));

                var document = await collection.Find(filter).FirstAsync();


                if (BCryptHelper.CheckPassword(textBoxPassword.Text, document.password))
                {
                    if (document.emailVerified) { 
                    Session["userId"] = document.email;
                    Session["userLogged"] = "True";
                    //Response.Write("Te-ai logat cu username " + Session["userId"].ToString() +"  si parola " +document.password);
                    Response.Redirect("UserManaging.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("EmailVerification.aspx?email="+document.email, false);
                    }
                }
                else
                {
                    labelEmailVerification.Visible = true;
                    labelEmailVerification.Text = "Email or password is incorect";
                }
            }
            else
            {
                labelEmailVerification.Visible = true;
                labelEmailVerification.Text = "Email or password is incorect";
            }


           

        }
        bool ItemExists(string insert)
        {

            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
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