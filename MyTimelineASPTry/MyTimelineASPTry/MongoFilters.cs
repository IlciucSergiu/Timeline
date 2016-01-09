using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;



namespace MyTimelineASPTry
{
  
    

    public class DocumentInfo
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public BsonArray name { get; set; }

        public string owner { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string importance { get; set; }
        public BsonArray tags { get; set; }

       
    }

    public class IndividualData
    {
        [BsonId]
        public ObjectId _id { get; set; }

       

        public string owner { get; set; }
        public string id { get; set; }

        public BsonArray events { get; set; }

        public string htmlInformation { get; set; }

        public BsonArray additionalResources { get; set; }

        public BsonArray additionalLinks { get; set; }

        public BsonArray additionalBooks { get; set; }

        private BsonDocument tagsDocument { get; set; }

        public BsonArray tags { get; set; }

        public int timesViewed { get; set; }

        public int votes { get; set; }

        public BsonArray alreadyVoted { get; set; }
    }

    public class UserData
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; } 
        public string gender { get; set; }
        public string salt { get; set; }

        public string image { get; set; }
        public int reputation { get; set; }

        }

    public class TagsCollection
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string tagName { get; set; }
        public string id { get; set; }
        public string owner { get; set; }
        public BsonArray parentTags { get; set; }
        public int relativeImportance { get; set; }
        public string description { get; set; }
        public string tagInfo { get; set; }
        public DateTime dateAdded { get; set; }
        //public BsonArray childrenNames { get; set;}
        public BsonArray documentsBelonging { get; set; }

       
    }

}