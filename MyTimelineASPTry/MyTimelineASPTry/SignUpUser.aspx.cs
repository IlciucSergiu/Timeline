using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using DevOne.Security.Cryptography.BCrypt;


namespace MyTimelineASPTry
{
    public partial class SignUpUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        string gender;

        protected void Button1_Click(object sender, EventArgs e)
        {
            HideLabels();
            if (InputIsValid())
            {
                if (!ItemExists(textBoxEmail.Text))
                { 
                MongoClient mgClient = new MongoClient();
                var db = mgClient.GetDatabase("Timeline");
                var collection = db.GetCollection<BsonDocument>("Users");




                if (radioButtonListGender.SelectedIndex != -1)
                {
                    //Response.Write(RadioButtonListGender.SelectedIndex.ToString() + "    ");
                    if (radioButtonListGender.SelectedValue == "Male")
                    { gender = "male"; }
                    else { gender = "female"; }
                }

                    string userInput = textBoxPassword.Text;
                string salt = BCryptHelper.GenerateSalt();
                //Response.Write(salt +"<br />");
                var hashedPassword = BCryptHelper.HashPassword(userInput, salt);

               // Response.Write(hashedPassword + "<br />");


                //Response.Write(BCryptHelper.CheckPassword("mere",hashedPassword));
                BsonDocument document = new BsonDocument
            {
               
                { "firstName", textBoxFirstName.Text },
                { "lastName", textBoxLastName.Text },
                { "password", hashedPassword },
                { "salt", salt },
                { "email", textBoxEmail.Text },
                { "gender", gender }
            };
               collection.InsertOneAsync(document);
                Response.Redirect("LoginUser.aspx", false);
                }
                else
                {
                    labelEmailValidation.Visible = true;
                    labelEmailValidation.Text = "This email is already registred.";
                }
            }
            else
            {
                labelInvalid.Visible = true;
                labelInvalid.Text = "Complete all fields.";
            }

            
        }

        bool InputIsValid()
        {
            bool valid = true;

            if (textBoxFirstName.Text == "")
                valid = false;

            if (textBoxLastName.Text == "")
                valid = false;

            if (textBoxPassword.Text == "")
            {
                
                valid = false;
            }
            else if (textBoxPassword.Text != textBoxPasswordVerify.Text)
            {
                valid = false;
                labelPasswordValidation.Visible = true;
                labelPasswordValidation.Text = "The passwords do not match.";
            }
            else if (textBoxPassword.Text.Length < 7)
            {
                valid = false;
                labelPasswordValidation.Visible = true;
                labelPasswordValidation.Text = "The password is to short.";
            }

            if (textBoxEmail.Text == "" )
            {
                //if(!textBoxEmail.Text.Contains('@'))
                valid = false;
            }
            else if(!textBoxEmail.Text.Contains('@'))
            {
                valid = false;
                labelEmailValidation.Visible = true;
                labelEmailValidation.Text = "Email must contain at least a \"@\" character";
            }

            if (radioButtonListGender.SelectedIndex == -1)
                valid = false;

            return valid;
        }

        void HideLabels()
        {
            labelEmailValidation.Visible = false;
            labelInvalid.Visible = false;
            labelPasswordValidation.Visible = false;
        }

        bool ItemExists(string insert)
        {
            MongoClient mgClient = new MongoClient();
            var db = mgClient.GetDatabase("Timeline");
            var collection = db.GetCollection<UserData>("Users");
            var filter = Builders<UserData>.Filter.Eq("email", insert);
            var count = collection.Find(filter).CountAsync();

            //Response.Write(count.Result);
            if (Convert.ToInt32(count.Result) != 0)
                return true;
            else
                return false;
        }

    }
}