using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web.Services;

namespace MyTimelineASPTry
{
    public partial class WebMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }




        // Tag functions aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        #region tagFunctions

        [WebMethod]
        public static string FindTagOptions(string inputValue)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<TagsCollection>("Tags");




            var filter = Builders<TagsCollection>.Filter.Regex("tagName", new BsonRegularExpression("/" + inputValue + "/i"));

            string tagOptions = "";
            collection.Find(filter).ForEachAsync(d => tagOptions += d.tagName.ToString() + "{;}").Wait();

            return tagOptions;

        }



        [WebMethod]
        public static string InsertInTagCollection(string tagName, string documentId, int relativeImportance)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collectionDocument = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Regex("id", new BsonRegularExpression(documentId));

            DocumentInfo documentInfo = new DocumentInfo();
            collectionDocument.Find(filter).ForEachAsync(d =>
            {
                documentInfo._id = d._id;
                documentInfo.name = d.name;
                documentInfo.id = d.id;

            }).Wait();

            // introduc inregistrarea tagului in document
            BsonDocument tagDocument = new BsonDocument {

                        { "tagName", tagName },
                        { "tagImportance", relativeImportance }
                 };

            var updateDocument = Builders<DocumentInfo>.Update
                    .Push(p => p.tags, tagDocument);

            collectionDocument.UpdateOneAsync(filter, updateDocument).Wait();



            var collectionTags = db.GetCollection<TagsCollection>("Tags");

            BsonDocument documentsBelonging = new BsonDocument()
            {

                { "_id", documentInfo._id },
                { "id", documentInfo.id },
                { "documentName", documentInfo.name },
                { "relativeImportance", relativeImportance}

            };





            var filterTag = Builders<TagsCollection>.Filter.Regex("tagName", tagName);
            var updateTag = Builders<TagsCollection>.Update
                    // .Inc("votes", 1)
                    .Push(p => p.documentsBelonging, documentsBelonging);

            collectionTags.UpdateOneAsync(filterTag, updateTag).Wait();



            return "True";

        }



        [WebMethod]
        public static string RemoveInTagCollection(string tagName, string documentId)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);


            var collectionDocument = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Regex("id", new BsonRegularExpression(documentId));

            DocumentInfo documentInfo = new DocumentInfo();
            collectionDocument.Find(filter).ForEachAsync(d =>
            {
                documentInfo._id = d._id;
                documentInfo.name = d.name;
                documentInfo.id = d.id;

            }).Wait();


            // sterg inregistrarea tagului din document
            var updateDocument = Builders<DocumentInfo>.Update
                       .Pull(p => p.tags, new BsonDocument(){
    { "tagName", tagName } });

            collectionDocument.UpdateOneAsync(filter, updateDocument).Wait();


            // sterg inregistrarea din  tag
            var collectionTags = db.GetCollection<TagsCollection>("Tags");


            var filterTag = Builders<TagsCollection>.Filter.Regex("tagName", tagName);
            var updateTag = Builders<TagsCollection>.Update

                    .Pull(p => p.documentsBelonging, new BsonDocument(){
    { "_id", documentInfo._id } });

            collectionTags.UpdateOneAsync(filterTag, updateTag).Wait();



            return "Deleted";

        }

        #endregion

        // Category functinos aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa


        [WebMethod]
        public static string FindCategoryOptions(string inputValue)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);
            var collection = db.GetCollection<CategoriesCollection>("Categories");




            var filter = Builders<CategoriesCollection>.Filter.Regex("categoryName", new BsonRegularExpression("/" + inputValue + "/i"));

            string categoryOptions = "";
            collection.Find(filter).ForEachAsync(d => categoryOptions += d.categoryName.ToString() + "{;}").Wait();

            return categoryOptions;

        }

        [WebMethod]
        public static string InsertInCategoryCollection(string categoryName, string documentId, int relativeImportance)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);


            var collectionDocument = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Regex("id", new BsonRegularExpression(documentId));

            DocumentInfo documentInfo = new DocumentInfo();
            collectionDocument.Find(filter).ForEachAsync(d =>
            {
                documentInfo._id = d._id;
                documentInfo.name = d.name;
                documentInfo.id = d.id;

            }).Wait();

            // introduc inregistrarea categoriei in document
            BsonDocument categoryDocument = new BsonDocument {

                        { "categoryName", categoryName },
                        { "categoryImportance", relativeImportance }
                 };

            var updateDocument = Builders<DocumentInfo>.Update
                    .Push(p => p.categories, categoryDocument);

            collectionDocument.UpdateOneAsync(filter, updateDocument).Wait();




            var collectionCategories = db.GetCollection<CategoriesCollection>("Categories");

            BsonDocument documentsBelonging = new BsonDocument()
            {

                { "_id", documentInfo._id },
                { "id", documentInfo.id },
                { "documentName", documentInfo.name },
                { "relativeImportance", relativeImportance}

            };





            var filterCategory = Builders<CategoriesCollection>.Filter.Eq(p => p.categoryName, categoryName);
            var updateCategory = Builders<CategoriesCollection>.Update
                    .Push(p => p.documentsBelonging, documentsBelonging);

           collectionCategories.UpdateOneAsync(filterCategory, updateCategory).Wait();



            return "True";

        }


        [WebMethod]
        public static string RemoveInCategoryCollection(string categoryName, string documentId)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);


            var collectionDocument = db.GetCollection<DocumentInfo>("DocumentsCollection");

            var filter = Builders<DocumentInfo>.Filter.Eq("id", documentId);

            DocumentInfo documentInfo = new DocumentInfo();
            collectionDocument.Find(filter).ForEachAsync(d =>
            {
                documentInfo._id = d._id;
                documentInfo.name = d.name;
                documentInfo.id = d.id;

            }).Wait();


            // sterg inregistrarea tagului din document
            var updateDocument = Builders<DocumentInfo>.Update
                       .Pull(p => p.categories, new BsonDocument(){
    { "categoryName", categoryName } });

            collectionDocument.UpdateOneAsync(filter, updateDocument).Wait();


            // sterg inregistrarea din  tag
            var collectionCategories = db.GetCollection<CategoriesCollection>("Categories");


            var filterCategory = Builders<CategoriesCollection>.Filter.Eq("categoryName", categoryName);
            var updateCategory = Builders<CategoriesCollection>.Update
            .Pull(p => p.documentsBelonging, new BsonDocument(){
             { "_id", documentInfo._id } });

            collectionCategories.UpdateOneAsync(filterCategory, updateCategory).Wait();



            return "Deleted";

        }


        //Books function aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        [WebMethod]
        public static string AddSelectedBook(string title, string authors, string isbn, string imageUrl, string documentId)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<IndividualData>("IndividualData");

            BsonDocument book = new BsonDocument()
            {
                { "title", title },
                { "authors", authors },
                { "isbn", isbn },
                { "imageUrl", imageUrl },
            };


            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                    // .Inc("votes", 1)
                    .Push(p => p.additionalBooks, book);

            collection.UpdateOneAsync(filter, update).Wait();

            return "Inserted";

        }


        [WebMethod]
        public static string RemoveSelectedBook(string title, string isbn, string documentId)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<IndividualData>("IndividualData");

            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                    .Pull(p => p.additionalBooks, new BsonDocument(){
    { "isbn", isbn } });

            collection.UpdateOneAsync(filter, update).Wait();

            return "Deleted";

        }


        //Image function aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

        [WebMethod]
        public static string AddAdditionalImage(string imageUrl, string documentId)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<IndividualData>("IndividualData");




            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                  .Push(p => p.imagesLinks, imageUrl);

            collection.UpdateOneAsync(filter, update).Wait();

            return "Inserted";

        }

        

        [WebMethod]
        public static string DeleteAdditionalImage(string imageUrl, string documentId)
        {
            MongoClient mclient = new MongoClient(GlobalVariables.mongolabConection);
            var db = mclient.GetDatabase(GlobalVariables.mongoDatabase);

            var collection = db.GetCollection<IndividualData>("IndividualData");




            var filter = Builders<IndividualData>.Filter.Regex("id", documentId);

            var update = Builders<IndividualData>.Update
                  .Pull(p => p.imagesLinks, imageUrl);

            collection.UpdateOneAsync(filter, update).Wait();

            return "Deleted";

        }

    }
}