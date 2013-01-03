#region  TreeDataService.cs
using System;
using System.Collections.ObjectModel;
using PlantATree.TreeService;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;

namespace PlantATree.Services
{
    public class TreeDataService : ITreeDataService
    {
        public TreeServiceClient Proxy { get; set; }

        public TreeDataService()
        {
            Proxy = new TreeService.TreeServiceClient();
        }

        #region GetTreeList
        private Action<ObservableCollection<Tree>> _getTreesCallBack;

        void OnGetTreesCompleted(object sender, GetTreesCompletedEventArgs e)
        {
            var treeList = e.Result;
            Proxy.GetTreesCompleted -= OnGetTreesCompleted;//remove the connection to avoid memory leaks
            _getTreesCallBack(treeList);
        }

        public void GetTreesList(Action<ObservableCollection<Tree>> getTreesCallback)
        {
            try
            {
                _getTreesCallBack = getTreesCallback;
                Proxy.GetTreesCompleted += OnGetTreesCompleted;
                Proxy.GetTreesAsync();
            }
            catch (Exception e)
            {
                Error error = new Error()
                {
                    Title = "Insert error",
                    Description = e.Message,
                };
                Messenger.Default.Send<ErrorMessage>(new ErrorMessage() { Error = error });
                getTreesCallback(new ObservableCollection<Tree>());
            }
        }
        #endregion

        #region GetTreeCount
        private Action<int> _getTreesCountCallBack;

        void OnGetTreesCountCompleted(object sender, GetTreesCountCompletedEventArgs e)
        {
            var treesCount = e.Result;
            Proxy.GetTreesCountCompleted -= OnGetTreesCountCompleted;//remove the connection to avoid memory leaks
            _getTreesCountCallBack(treesCount);
        }

        public void GetTreesCountList(Action<int> getTreesCountCallback)
        {
            try
            {
                _getTreesCountCallBack = getTreesCountCallback;
                Proxy.GetTreesCountCompleted += OnGetTreesCountCompleted;
                Proxy.GetTreesCountAsync();
            }
            catch (Exception e)
            {
                Error error = new Error()
                {
                    Title = "Insert error",
                    Description = e.Message,
                };
                Messenger.Default.Send<ErrorMessage>(new ErrorMessage() { Error = error });
                getTreesCountCallback(0);
            }
        }
        #endregion

        #region Insert Trees
        private Action<int> _InsertTreeCallBack;

        void OnInsertTreeCompleted(object sender, InsertTreeCompletedEventArgs e)
        {
            var treeId = e.Result;
            Proxy.InsertTreeCompleted -= OnInsertTreeCompleted;//remove the connection to avoid memory leaks
            _InsertTreeCallBack(treeId);
        }

        public void InsertTrees(Tree newTree, Action<int> insertTreeCallback)
        {
            try
            {
                _InsertTreeCallBack = insertTreeCallback;
                Proxy.InsertTreeCompleted += OnInsertTreeCompleted;
                Proxy.InsertTreeAsync(newTree);
            }
            catch(Exception e)
            {
                Error error = new Error()
                {
                    Title = "Insert error",
                    Description = e.Message,
                };
                Messenger.Default.Send<ErrorMessage>(new ErrorMessage(){Error = error});
                insertTreeCallback(0);
            }
        }
        #endregion

    }
}
#endregion 