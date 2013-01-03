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
using PlantATree.Controls;
using GalaSoft.MvvmLight.Messaging;
using PlantATree.Messages;
using PlantATree.ViewModels;
using System.ServiceModel.Channels;

namespace PlantATree.Views
{
    public partial class ForestNature : UserControl
    {
        //PlantTreeCursorControl TreeCursor;
        bool IsPlanting { get; set; }

        public ForestNature()
        {
            InitializeComponent();
            
            this.InitCursor();
            this.RegisterMessages();

            Hill.MouseLeftButtonDown +=new MouseButtonEventHandler(Hill_MouseLeftButtonDown);

            this.Loaded +=new RoutedEventHandler(ForestNature_Loaded);
            //Show about on start
            
        }

        public void ForestNature_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<ShowAboutMessage>(new ShowAboutMessage());
            BeginAnimations();
        }

        private void BeginAnimations()
        {
            //Cloud1Storyboard.Begin();
            Cloud3Storyboard.Begin();
            Cloud4Storyboard.Begin();
            Cloud5StoryBoard.Begin();
            TurtleStoryboard.Begin();
        }

        private void InitCursor()
        {
            //cursor = new PlantTreeCursorControl();
            //cursor.Visibility = Visibility.Collapsed;
            TreeCursor.Width = 40;
            TreeCursor.Height = 110;
            //NatureRootCanvas.Children.Add(TreeCursor);
            Hill.MouseMove += new MouseEventHandler(Hill_MouseMove);
            Hill.MouseLeave += new MouseEventHandler(Hill_MouseLeave);
        }

        public void Hill_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPlanting)
            {
                return;
            }

            UIElement senderAsUI = sender as UIElement;
            double hillTop = Canvas.GetTop(senderAsUI);
            double hillLeft = Canvas.GetLeft(senderAsUI);

            Point mousePosition = e.GetPosition(senderAsUI);
            double mouseX = mousePosition.X;
            double mouseY = mousePosition.Y;

            double cursorLeft = hillLeft + mouseX;
            double cursorTop = hillTop + mouseY;

            Point cursorPostion = new Point(cursorLeft, cursorTop);
            //this.Cursor = Cursors.None;
            TreeCursor.MoveTo(cursorPostion);
            TreeCursor.Visibility = Visibility.Visible;
        }

        public void Hill_MouseLeave(object sender, MouseEventArgs e)
        {
            //this.Cursor = Cursors.Arrow;
            TreeCursor.Hide();
        }

        /// <summary>
        /// Registers messages
        /// </summary>
        public void RegisterMessages()
        {
            Messenger.Default.Register<RenderTreeMessage>(this, OnRenderTreeMessage);
            Messenger.Default.Register<IsPlantingModePropertyChangedMessage>(this,  OnIsPlantingPropertyChanged);
            //Messenger.Default.Register<StartPlantingModeMessage>(this, OnStartPlantingModeMesssage);
            //Messenger.Default.Register<EndPlantingModeMessage>(this, OnEndPlantingModeMesssage);
        }

        public void OnEndPlantingModeMesssage(EndPlantingModeMessage msg)
        {
            IsPlanting = false;
            TreeCursor.Visibility = System.Windows.Visibility.Collapsed;
        }


        public void OnStartPlantingModeMesssage(StartPlantingModeMessage msg)
        {
            IsPlanting = true;
        }

        public void OnIsPlantingPropertyChanged(IsPlantingModePropertyChangedMessage msg)
        {
            IsPlanting = msg.NewValue;
        }


        /// <summary>
        /// Fires on new RenderTreeMessage recieved
        /// </summary>
        /// <param name="msg"></param>
        public void OnRenderTreeMessage(RenderTreeMessage msg)
        {
            var treeToRender = msg.Tree;
            double treeToRenderCoordinateX = treeToRender.CoordinateX;
            double treeToRenderCoordinateY = treeToRender.CoordinateY;

            PlantTreeOnTheHill(treeToRender);
            //PlantTreeOnTheHill(treeToRenderCoordinateX, treeToRenderCoordinateY);
        }

       
        /// <summary>
        /// Choose where to place the tree and launches planting dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hill_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsPlanting)
            {
                return;
            }

            UIElement senerUIElement = sender as UIElement;
            Point mousePosition = e.GetPosition(senerUIElement);
            double mouseX = mousePosition.X;
            double mouseY = mousePosition.Y;

            TreeViewModel newTree = new TreeViewModel()
                {
                    CoordinateX = mouseX,
                    CoordinateY = mouseY,
                };

            //Messenger.Default.Send<RenderTreeMessage>(new RenderTreeMessage(newTree));
            Messenger.Default.Send<LaunchNewTreeMessage>(new LaunchNewTreeMessage(newTree));
            Messenger.Default.Send<EndPlantingModeMessage>(new EndPlantingModeMessage());

            TreeCursor.Hide();
            
        }

        /// <summary>
        /// Puts a tree on the hill
        /// </summary>
        /// <param name="treeX">Tree X - left from the Hill</param>
        /// <param name="treeY">Tree Y - top from the Hill</param>
        public void PlantTreeOnTheHill(double treeX, double treeY)
        {
            var hill = Hill;

            double hillY = Canvas.GetTop(hill);
            double hillX = Canvas.GetLeft(hill);

            double newTreeX = hillX + treeX;//newTreeX is the X(left) referred to whole Nature (RootNatureCanvas)
            double newTreeY = hillY + treeY;//newTreeY is the Y(top) referred to whole Nature (RootNatureCanvas)

            double treeWidth = 50;
            double treeHeight = 56;

            PlantATree.Controls.TreeControl newTree = new PlantATree.Controls.TreeControl()
            {
                Height = treeWidth,
                Width = treeHeight,
            };

            //Calculates the scale
            double hillHeight = Hill.ActualHeight;
            double senderTopToHill = newTreeY - hillY;

            double minScaleCoef = 0.25;
            double scaleCoef = minScaleCoef + (senderTopToHill / hillHeight) * (1 - minScaleCoef); ;

            this.NatureRootCanvas.Children.Add(newTree);

            CompositeTransform composite = new CompositeTransform();
            composite.TranslateX = newTreeX - treeWidth / 2;
            composite.TranslateY = newTreeY - treeHeight;

            composite.CenterX = 0.5;
            composite.CenterY = 1.0;
            composite.ScaleX = scaleCoef;
            composite.ScaleY = scaleCoef;

            newTree.RenderTransform = composite;
        }

        /// <summary>
        /// Puts a tree on the hill
        /// </summary>
        /// <param name="treeInfo">TreeViewModel with the tree information</param>
        public void PlantTreeOnTheHill(TreeViewModel treeInfo)
        {
            double treeX = treeInfo.CoordinateX;
            double treeY = treeInfo.CoordinateY;
            var hill = Hill;

            double hillY = Canvas.GetTop(hill);
            double hillX = Canvas.GetLeft(hill);

            double newTreeX = hillX + treeX;//newTreeX is the X(left) referred to whole Nature (RootNatureCanvas)
            double newTreeY = hillY + treeY;//newTreeY is the Y(top) referred to whole Nature (RootNatureCanvas)

            double treeWidth = 50;
            double treeHeight = 56;

            PlantATree.Controls.TreeControl newTree = new PlantATree.Controls.TreeControl()
            {
                Height = treeWidth,
                Width = treeHeight,
                DataContext = treeInfo,
            };

            //Calculates the scale
            double hillHeight = Hill.ActualHeight;
            double senderTopToHill = newTreeY - hillY;

            double minScaleCoef = 0.25;
            double scaleCoef = minScaleCoef + (senderTopToHill / hillHeight) * (1 - minScaleCoef); ;

            this.NatureRootCanvas.Children.Add(newTree);

            CompositeTransform composite = new CompositeTransform();
            composite.TranslateX = newTreeX - treeWidth / 2;
            composite.TranslateY = newTreeY - treeHeight;

            composite.CenterX = 0.5;
            composite.CenterY = 1.0;
            composite.ScaleX = scaleCoef;
            composite.ScaleY = scaleCoef;

            newTree.RenderTransform = composite;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlantATree.ForestChildWindow window = new PlantATree.ForestChildWindow();
            window.Show();
        }

    }
}
