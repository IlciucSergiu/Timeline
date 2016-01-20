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
    public partial class EmailVerification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            email = Request.QueryString["email"];
            if (Request.QueryString["key"] != null)
            {
                key = Request.QueryString["key"];

                if(key.Length > 10)
                {
                    MongoClient mclient = new MongoClient();
                    var db = mclient.GetDatabase("Timeline");

                    var user = db.GetCollection<UserData>("Users");
                    var filterUser = Builders<UserData>.Filter.Eq("email", email);

                    bool redirect = false;
                    user.Find(filterUser).ForEachAsync(d =>
                    {
                        if (d.emailVerification == key)
                        {

                            redirect = true;
                        }
                    }).Wait();

                    if (redirect)
                    {
                        var update = Builders<UserData>.Update
                        .Set("emailVerified", true);

                        user.UpdateOneAsync(filterUser, update).Wait();

                        Session["userId"] = email;
                        Session["userLogged"] = "True";

                        Response.Redirect("UserManaging.aspx", false);

                    }
                }
            }
        }
        string email,key;

        protected void buttonCheckKey_Click(object sender, EventArgs e)
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var user = db.GetCollection<UserData>("Users");
            var filterUser = Builders<UserData>.Filter.Eq("email", email);

            bool redirect = false;
            user.Find(filterUser).ForEachAsync(d =>
            {
                if (d.emailVerification == textBoxVerifcation.Text)
                { 
                
                    redirect = true;
                }
            }).Wait();

            if (redirect)
            {
                var update = Builders<UserData>.Update
                .Set("emailVerified", true);

                 user.UpdateOneAsync(filterUser, update).Wait();

                Session["userId"] = email;
                Session["userLogged"] = "True";
                
                Response.Redirect("UserManaging.aspx", false);
                
            }
        }
    }
}