using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace PlantATree.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TreeService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TreeService : ITreeService
    {
        /// <summary>
        /// Get all trees from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tree> GetTrees()
        {
            List<Tree> treeList;
            using (var context = new PlantATreeEntities())
            {
                //context.ContextOptions.LazyLoadingEnabled = false;
                treeList = context.Trees.ToList();
            }
            return treeList;
        }


        /// <summary>
        /// Inserts a tree to the database
        /// </summary>
        /// <param name="newTree">New tree object</param>
        /// <returns>True if insert is successfull</returns>
        public int InsertTree(Tree newTree)
        {
            if (newTree == null)
            {
                return 0;
            }

            int newTreeId = 0;
            try
            {
                newTree.TreeId = 0;//ensures that the treeid is new
                newTree.CreationDate = DateTime.UtcNow;
                using (var context = new PlantATreeEntities())
                {
                    context.Trees.AddObject(newTree);
                    context.SaveChanges();

                    newTreeId = newTree.TreeId;
                }
                return newTreeId;
            }
            catch(Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns all trees count
        /// </summary>
        /// <returns></returns>
        public int GetTreesCount()
        {
            int treesCount = 0;
            using (var context = new PlantATreeEntities())
            {
                //context.ContextOptions.LazyLoadingEnabled = false;
                treesCount = context.Trees.Count();
            }
            
            return treesCount;
        }

    }
}
