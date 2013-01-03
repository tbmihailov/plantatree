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
using PlantATree.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using PlantATree.Messages;

namespace PlantATree
{
    public partial class NewTreeWindow : ChildWindow
    {
        public NewTreeWindow(TreeViewModel newTree)
        {
            InitializeComponent();

            NewTreeViewModel newTreeViewModel = new NewTreeViewModel();
            newTreeViewModel.Tree = newTree;
            this.DataContext = newTreeViewModel;
            this.RegisterMessages();

            PlantingProcessViewControl.AnimationCompleted += new EventHandler(OnPlantingProcessAnimationCompleted);


        }


        public void OnPlantingProcessAnimationCompleted(object sender, EventArgs e)
        {
            //this.PlantingProcessViewControl.Visibility = System.Windows.Visibility.Collapsed;
            //this.NewTreeViewControl.Visibility = System.Windows.Visibility.Visible;
        }

        public void RegisterMessages()
        {
            Messenger.Default.Register<NewTreeCanceledMessage>(this, OnNewTreeCanceledMessage);
            Messenger.Default.Register<TreeSaveSuccessfullMessage>(this, OnTreeSaveSuccessfullMessage);
            Messenger.Default.Register<TreeSaveUnsuccessfullMessage>(this, OnTreeSaveUnsuccessfullMessage);
        }

        public void OnNewTreeCanceledMessage(NewTreeCanceledMessage msg)
        {
            this.DialogResult = false;
            //this.Close();
        }

        public void OnTreeSaveSuccessfullMessage(TreeSaveSuccessfullMessage msg)
        {
            this.DialogResult = true;
            //this.Close();
        }

        public void OnTreeSaveUnsuccessfullMessage(TreeSaveUnsuccessfullMessage msg)
        {
            this.DialogResult = false;
            //this.Close();
        }

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PlantingProcessViewControl.StartAnimation();
        }
    }

}

