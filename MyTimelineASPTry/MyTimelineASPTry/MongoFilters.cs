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

        public string id { get; set; }
        public string title { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string importance { get; set; }

        public string gender { get; set; }
        public string profesion { get; set; }
        public string religion { get; set; }
        public string nationality { get; set; }
    }
}