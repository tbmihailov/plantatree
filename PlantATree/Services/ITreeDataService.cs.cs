#region ITreeDataService.cs
using System;
using System.Collections.ObjectModel;
using PlantATree.TreeService;

namespace PlantATree.Services
{
    public interface ITreeDataService
    {
        void GetTreesList(Action<ObservableCollection<Tree>> getTreesCallback);
        void GetTreesCountList(Action<int> getTreesCountCallback);
        void InsertTrees(Tree newTree, Action<int> insertTreeCallback);
    }
}

#endregion