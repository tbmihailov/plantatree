using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;

namespace PlantATree
{
	public partial class TreePlantingProcessView : UserControl
	{
		public TreePlantingProcessView()
		{
			// Required to initialize variables
			InitializeComponent();
            PlantingProcessAnimationStoryboard.Completed += new EventHandler(OnAnimationCompleted);    
		}

        public void StartAnimation()
        {
            PlantingProcessAnimationStoryboard.Begin();
        }


        public void OnAnimationCompleted(object sender, EventArgs e)
        {
            AnimationCompleted.Invoke(sender, e);
        }


        public EventHandler AnimationCompleted { get; set; }
    }
}