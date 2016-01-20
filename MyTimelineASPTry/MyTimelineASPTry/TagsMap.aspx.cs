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
    public partial class TagsMap : System.Web.UI.Page
    {

        TagElements[] tagMap = new TagElements[100];

        // IEnumerable<TagElements> tagMapEnum = new Enumerable();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTags();
            SetHierarhicalPosition(Array.Find(tagMap, p => p.tagName == "main"), 0);
            treeViewTagsMap.Nodes.Clear();
            TreeNode main = new TreeNode();
            main.Text = "main";
           treeViewTagsMap.Nodes.Add(main);
            PopulateTreeView(Array.Find(tagMap, p => p.tagName == "main"), 0, main);
           // ShowTagsArray();
           
        }

        void LoadTags()
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<TagsCollection>("Tags");

            //inputValue = "ev";
            // var filter = Builders<IndividualData>.Filter.Eq("id", id);


            int i = 0;

            collection.Find(_ => true).ForEachAsync(d =>
            {
                TagElements tagElement = new TagElements();

                tagElement.tagName = d.tagName;
                tagElement.parentName = d.parentTags[0]["parentName"].ToString();
                tagMap[i] = tagElement;
                i++;

            }).Wait();


        }

        void ShowTagsArray()
        {

            int j = 0;
            foreach (TagElements tag in tagMap)
            {
                if (tag == null)
                    break;
                j++;

                Response.Write(tag.tagName + "   " + tag.parentName + "   " + tag.hierarchicalPosition.ToString() + "<br />");
            }
            Response.Write(j.ToString() + "<br />");
            Response.Write(tagMap.Length + "<br />");
        }

        int SetHierarhicalPosition(TagElements curentTag, int curentPosition)
        {
            curentTag.hierarchicalPosition = curentPosition;


            int j = 0;
            foreach (TagElements tag in tagMap)
            {
                if (tag == null) break;

                if (tag.parentName == curentTag.tagName)
                {
                    SetHierarhicalPosition(tag, curentPosition + 1);
                }

                j++;

                // Response.Write(tag.tagName + "   " + tag.parentName + "<br />");
            }

            return 0;
        }

        int PopulateTreeView(TagElements parentTag, int curentPosition, TreeNode parentNode)
        {
           // parentTag.hierarchicalPosition = curentPosition;


            int j = 0;
            foreach (TagElements tag in tagMap)
            {
                if (tag == null) break;

                if (tag.parentName == parentTag.tagName)
                {
                    TreeNode curentNode = new TreeNode();
                    curentNode.Text = tag.tagName;
                    curentNode.NavigateUrl = "TagInfo.aspx?tagName=" + tag.tagName;
                    curentNode.Collapse();
                    parentNode.ChildNodes.Add(curentNode);
                    PopulateTreeView(tag, curentPosition + 1, curentNode);
                }

                j++;

                // Response.Write(tag.tagName + "   " + tag.parentName + "<br />");
            }

            return 0;
        }
        public class TagClass
        {

            public IEnumerable<TagElements> Tag { get; set; }
        }

        public class TagElements
        {

            public string id { get; set; }
            public string tagName { get; set; }
            public string parentName { get; set; }
            public int hierarchicalPosition { get; set; }
        }


        void PopulateTreeView()
        {
            TreeNode node = new TreeNode();

            node.Text = "Geo";
            node.NavigateUrl = "www.google.com";

            
            treeViewTagsMap.Nodes.Add(node);
        }


        void PopulateTreeViewOnDemand()
        {
            //treeViewTagsMap.TreeNodeExpanded
        }
    }
}