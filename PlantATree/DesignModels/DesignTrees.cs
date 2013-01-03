#region DesignTrees.cs
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using PlantATree.TreeService;

namespace PlantATree.DesignModel
{
    public class DesignTrees : ObservableCollection<Tree>
    {
        private int entitiesCount = 20;
        public DesignTrees()
        {
            var treesList = GenerateDesignTreesList();
            foreach (var tree in treesList)
            {
                this.Add(tree);
            }
        }

        private IList<Tree> GenerateDesignTreesList()
        {
            IList<Tree> generatedSource = new List<Tree>();

            for (int i = 2; i < entitiesCount; i++)
            {
                var tree =
                    new Tree()
                    {
                        TreeId = i,
                        CreatorName = "Tree Creator " + i,
                        CreatorEmail = "TreeCreator" + i +"@live.com",
                        CreationDate = DateTime.Now,
                        CoordinateX = 100+i*20,
                        CoordinateY = 0 + i*30,
                        Message = "Plant a tree - save the world!"

                    };
                generatedSource.Add(tree);
            }

            return generatedSource;
        }

    }

}
#endregion 