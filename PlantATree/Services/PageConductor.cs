using System;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PlantATree.Views;
using PlantATree.Messages;
using PlantATree.ViewModels;

namespace PlantATree
{
    public class PageConductor : IPageConductor
    {
        protected Frame RootFrame { get; set; }

        public PageConductor()
        {
            Messenger.Default.Register<FrameMessage>(this, OnReceiveFrameMessage);
            Messenger.Default.Register<ShowAboutMessage>(this, OnRecieveShowAboutMessage);
            Messenger.Default.Register<LaunchNewTreeMessage>(this, OnRecieveLaunchNewTreeMessage);
            Messenger.Default.Register<ShowHelpWindowMesage>(this, OnRecieveShowHelpWindowMesage);
        }

        public void OnRecieveShowHelpWindowMesage(ShowHelpWindowMesage msg)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        public void OnRecieveLaunchNewTreeMessage(LaunchNewTreeMessage msg)
        {
            var newMessage = msg.Tree;
            LaunchNewTreeWindow(newMessage);
        }

        public void LaunchNewTreeWindow(TreeViewModel newTree)
        {
            NewTreeWindow newTreeView = new NewTreeWindow(newTree);
            newTreeView.Show();
        }

        public void OnRecieveShowAboutMessage(ShowAboutMessage msg)
        {
            this.ShowAboutWindow();
        }

        private void OnReceiveFrameMessage(FrameMessage msg)
        {
            RootFrame = msg.RootFrame;
        }

        public void ShowAboutWindow()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        public void DisplayError(string origin, Exception e, string details)
        {
            string description = string.Format("Error occured in {0}. {1} {2}", origin, details, e.Message);
            var error = new Error() {Description = description, Title = "Error Occurred"};
            //PushState(ViewTokens.ErrorOverlay, error);
            Messenger.Default.Send(new ErrorMessage() { Error = error });
        }
        
        public void DisplayError(string origin, Exception e)
        {
            DisplayError(origin, e, string.Empty);
        }

        public void GoToView(string viewToken)
        {
            Go(FormatViewPath(viewToken));
        }

        public void GoBack()
        {
            RootFrame.GoBack();
        }

        private void Go(string path)
        {
            RootFrame.Navigate(new Uri(path, UriKind.Relative));
        }

        private string FormatViewPath(string viewToken)
        {
            return string.Format("/{0}/{1}.xaml", ViewTokens.Root, viewToken);
        }

    }
}