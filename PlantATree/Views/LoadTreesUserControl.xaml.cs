using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PlantATree.Views
{
    public partial class LoadTreesUserControl : UserControl
    {
        public LoadTreesUserControl()
        {
            InitializeComponent();
            
            TreeProxy = new TreeService.TreeServiceClient();
            TreeProxy.GetTreesCompleted += new EventHandler<TreeService.GetTreesCompletedEventArgs>(treeProxy_GetTreesCompleted);
            TreeProxy.InsertTreeCompleted+=new EventHandler<TreeService.InsertTreeCompletedEventArgs>(TreeProxy_InsertTreeCompleted);
        }

        void TreeProxy_InsertTreeCompleted(object sender, TreeService.InsertTreeCompletedEventArgs e)
        {
            int result = e.Result;

            if (result>0)
            {
                MessageBox.Show("Message saved successfully!");
            }
            else
            {
                MessageBox.Show("Message was not save!");
            }
        }

        public TreeService.TreeServiceClient TreeProxy{get;set;}

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TreeProxy.GetTreesAsync();
        }

        public ObservableCollection<TreeService.Tree> treeList { get; private set; }

               
        void treeProxy_GetTreesCompleted(object sender, TreeService.GetTreesCompletedEventArgs e)
        {
            this.treeList = e.Result;
            dataGrid1.ItemsSource = treeList;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            TreeService.Tree newTree = new TreeService.Tree()
            {
                CreatorName = NameTextBox.Text,
                Message = MessageTextBox.Text
            };

            TreeProxy.InsertTreeAsync(newTree);
        }



    }
}
