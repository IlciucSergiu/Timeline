using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;


// codu asta e praff tre facut reparat mult
namespace MyTimelineASPTry
{
    public partial class CategoriesMap : System.Web.UI.Page
    {

        CategoryElements[] categoryMap = new CategoryElements[100];

        // IEnumerable<TagElements> tagMapEnum = new Enumerable();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTags();
            SetHierarhicalPosition(Array.Find(categoryMap, p => p.categoryName == "Main"), 0);
            treeViewCategoriesMap.Nodes.Clear();
            TreeNode main = new TreeNode();
            main.Text = "Main";
            treeViewCategoriesMap.Nodes.Add(main);
            PopulateTreeView(Array.Find(categoryMap, p => p.categoryName == "Main"), 0, main);
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
                CategoryElements categoryElement = new CategoryElements();

                categoryElement.categoryName = d.tagName;
                categoryElement.parentName = d.parentTags[0]["parentName"].ToString();
               categoryMap[i] = categoryElement;
                i++;

            }).Wait();


        }

        void ShowTagsArray()
        {

            int j = 0;
            foreach (CategoryElements tag in categoryMap)
            {
                if (tag == null)
                    break;
                j++;

                Response.Write(tag.categoryName + "   " + tag.parentName + "   " + tag.hierarchicalPosition.ToString() + "<br />");
            }
            Response.Write(j.ToString() + "<br />");
            Response.Write(categoryMap.Length + "<br />");
        }

        int SetHierarhicalPosition(CategoryElements curentTag, int curentPosition)
        {
            curentTag.hierarchicalPosition = curentPosition;


            int j = 0;
            foreach (CategoryElements tag in categoryMap)
            {
                if (tag == null) break;

                if (tag.parentName == curentTag.categoryName)
                {
                    SetHierarhicalPosition(tag, curentPosition + 1);
                }

                j++;

                // Response.Write(tag.tagName + "   " + tag.parentName + "<br />");
            }

            return 0;
        }

        int PopulateTreeView(CategoryElements parentTag, int curentPosition, TreeNode parentNode)
        {
            // parentTag.hierarchicalPosition = curentPosition;


            int j = 0;
            foreach (CategoryElements tag in categoryMap)
            {
                if (tag == null) break;

                if (tag.parentName == parentTag.categoryName)
                {
                    TreeNode curentNode = new TreeNode();
                    curentNode.Text = tag.categoryName;
                    curentNode.NavigateUrl = "TagInfo.aspx?tagName=" + tag.categoryName;
                    curentNode.Collapse();
                    parentNode.ChildNodes.Add(curentNode);
                    PopulateTreeView(tag, curentPosition + 1, curentNode);
                }

                j++;

                // Response.Write(tag.tagName + "   " + tag.parentName + "<br />");
            }

            return 0;
        }
        public class CategoryClass
        {

            public IEnumerable<CategoryElements> Tag { get; set; }
        }

        public class CategoryElements
        {

            public string id { get; set; }
            public string categoryName { get; set; }
            public string parentName { get; set; }
            public int hierarchicalPosition { get; set; }
        }


        void PopulateTreeView()
        {
            TreeNode node = new TreeNode();

            node.Text = "Geo";
            node.NavigateUrl = "www.google.com";


            treeViewCategoriesMap.Nodes.Add(node);
        }


       
    }
}