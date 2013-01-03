using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;

namespace PlantATree.Messages
{
    /// <summary>
    /// Starts planting mode
    /// </summary>
    public class StartPlantingModeMessage : MessageBase
    {

    }

    public class EndPlantingModeMessage : MessageBase
    {

    }

    public class IsPlantingModePropertyChangedMessage : PropertyChangedMessageBase
    {
        public bool OldValue { get; set; }
        public bool NewValue { get; set; }

        public IsPlantingModePropertyChangedMessage() : base("IsPlantingMode")
        {
        }
    }


}
