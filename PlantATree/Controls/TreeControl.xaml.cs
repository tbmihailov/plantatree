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

namespace PlantATree.Controls
{
    public partial class TreeControl : UserControl
    {
        public TreeControl()
        {
            InitializeComponent();
            Width = 50;
            Height = 56;
        }

        public TreeControl(TreeViewModel treeInfo)
        {
            InitializeComponent();
            Width = 50;
            Height = 56;
            this.DataContext = treeInfo;
        }

        #region Properties

        private double defaultWidth = 50;
        public double DefaultWidth
        {
            get { return defaultWidth; }
        }

        private double defaultHeight = 56;
        public double DefaultHeight
        {
            get { return defaultHeight; }
        }

        #region Distance Property
        public double Distance
        {
            get
            {
                return (double)GetValue(DistanceProperty);
            }
            set
            {
                SetValue(DistanceProperty, value);
                SetScaleByDistance(value);
            }
        }

        /// <summary>
        /// Set the scale of the control depending on the distace
        /// </summary>
        /// <param name="distance"></param>
        private void SetScaleByDistance(double distance)
        {
            ScaleTransform st = new ScaleTransform();
            st.ScaleX = distance;
            st.ScaleY = distance;

            this.RenderTransform = st;
            //Width = DefaultWidth * distance;
            //Height = DefaultHeight * distance;
        }

        // Using a DependencyProperty as the backing store for Distance.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register("Distance", typeof(double), typeof(TreeControl), new PropertyMetadata(.0, DistanceChangedCallback));

        public static void DistanceChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            double newValue = (double)e.NewValue;

            TreeControl control = obj as TreeControl;
            control.SetScaleByDistance(newValue);
        }
        #endregion


        ///// <summary>
        ///// Represents the  tree root left coordinate
        ///// </summary>
        //public double RootLeft
        //{
        //    get
        //    {
        //        double canvasLeft = Canvas.GetLeft(this);
        //        double rootLeft = canvasLeft + this.ActualWidth / 2;
        //        return rootLeft;
        //    }
        //    set
        //    {
        //        double rootLeft = value;
        //        double canvasLeft = rootLeft - this.ActualWidth / 2;
        //        Canvas.SetLeft(this, canvasLeft);
        //    }
        //}

        ///// <summary>
        ///// Represents the tree root top coordinate
        ///// </summary>
        //public double RootTop
        //{
        //    get
        //    {
        //        double canvasTop = Canvas.GetTop(this);
        //        double rootTop = canvasTop + this.ActualHeight;
        //        return rootTop;
        //    }
        //    set
        //    {
        //        double rootTop = value;
        //        double canvasTop = rootTop - this.ActualHeight;
        //        Canvas.SetTop(this, canvasTop);
        //    }
        //}

        #endregion
    }
}
