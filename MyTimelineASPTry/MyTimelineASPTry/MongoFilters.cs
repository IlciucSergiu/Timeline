using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;



namespace MyTimelineASPTry
{
    public class PersonBasic
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string id { get; set; }
        public string title { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string importance { get; set; }
       

        
    }

    public class PersonInfo
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

        [BsonIgnoreIfNull]
        public string gender { get; set; }
        [BsonIgnoreIfNull]
        public string profession { get; set; }
        [BsonIgnoreIfNull]
        public string religion { get; set; }
        [BsonIgnoreIfNull]
        public string nationality { get; set; }
    }

    public class IndividualData
    {
        [BsonId]
        public ObjectId _id { get; set; }

       

        //public string owner { get; set; }
        public string id { get; set; }

        public BsonArray events { get; set; }

        public string htmlInformation { get; set; }

        public BsonArray additionalResources { get; set; }

        public BsonArray additionalLinks { get; set; }
    }
}