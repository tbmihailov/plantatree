using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace PlantATree
{
    internal class FrameMessage : MessageBase
    {
        public Frame RootFrame { get; set; }
    }
}