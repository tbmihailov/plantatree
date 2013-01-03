#region DesignTreeDataService.cs
using System;
using PlantATree.DesignModel;
using PlantATree.TreeService;
using System.Collections.ObjectModel;

namespace PlantATree.Services
{
    public class DesignTreeDataService : ITreeDataService
    {
            ObservableCollection<Tree> trees;

            public DesignTreeDataService()
            {
                trees = new DesignTrees();
            }

            public void GetTreesList(Action<ObservableCollection<Tree>> getTreesCallback)
            {
                getTreesCallback(trees);
            }

            public void GetTreesCountList(Action<int> getTreesCountCallback)
            {
                int treesCount = trees.Count;
                getTreesCountCallback(treesCount);
            }
            
            public void InsertTrees(Tree newTree, Action<int> insertTreeCallback)
            {
                insertTreeCallback(1);
            }
    }
}

#endregion
