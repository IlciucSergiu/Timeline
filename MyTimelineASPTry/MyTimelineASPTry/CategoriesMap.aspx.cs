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
            LoadCategories();
            SetHierarhicalPosition(Array.Find(categoryMap, p => p.categoryName == "Main"), 0);
            treeViewCategoriesMap.Nodes.Clear();
            TreeNode main = new TreeNode();
            main.Text = "Main";
            treeViewCategoriesMap.Nodes.Add(main);
            PopulateTreeView(Array.Find(categoryMap, p => p.categoryName == "Main"), 0, main);
            //ShowCategoriesArray();

        }

        void LoadCategories()
        {
            MongoClient mclient = new MongoClient();
            var db = mclient.GetDatabase("Timeline");

            var collection = db.GetCollection<CategoriesCollection>("Categories");

            //inputValue = "ev";
            // var filter = Builders<IndividualData>.Filter.Eq("id", id);


            int i = 0;

            collection.Find(_ => true).ForEachAsync(d =>
            {
                CategoryElements categoryElement = new CategoryElements();

                categoryElement.categoryName = d.categoryName;
                categoryElement.parentName = d.parentCategories[0]["parentName"].ToString();
               categoryMap[i] = categoryElement;
                i++;

            }).Wait();


        }

        void ShowCategoriesArray()
        {

            int j = 0;
            foreach (CategoryElements category in categoryMap)
            {
                if (category == null)
                    break;
                j++;

                Response.Write(category.categoryName + "   " + category.parentName + "   " + category.hierarchicalPosition.ToString() + "<br />");
            }
            Response.Write(j.ToString() + "<br />");
            Response.Write(categoryMap.Length + "<br />");
        }

        int SetHierarhicalPosition(CategoryElements curentCategory, int curentPosition)
        {
           
            curentCategory.hierarchicalPosition = curentPosition;


            int j = 0;
            foreach (CategoryElements category in categoryMap)
            {
                if (category  == null) break;

                if (category.parentName == curentCategory.categoryName)
                {
                    SetHierarhicalPosition(category, curentPosition + 1);
                }

                j++;

                // Response.Write(tag.tagName + "   " + tag.parentName + "<br />");
            }

            return 0;
        }

        int PopulateTreeView(CategoryElements parentCategory, int curentPosition, TreeNode parentNode)
        {
            // parentTag.hierarchicalPosition = curentPosition;


            int j = 0;
            foreach (CategoryElements category in categoryMap)
            {
                if (category == null) break;

                if (category.parentName == parentCategory.categoryName)
                {
                    TreeNode curentNode = new TreeNode();
                    curentNode.Text = category.categoryName;
                    curentNode.NavigateUrl = "TagInfo.aspx?tagName=" + category.categoryName;
                    curentNode.Collapse();
                    parentNode.ChildNodes.Add(curentNode);
                    PopulateTreeView(category, curentPosition + 1, curentNode);
                }

                j++;

                // Response.Write(tag.tagName + "   " + tag.parentName + "<br />");
            }

            return 0;
        }
        public class CategoryClass
        {

            public IEnumerable<CategoryElements> Category { get; set; }
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