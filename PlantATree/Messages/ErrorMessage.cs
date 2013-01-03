using GalaSoft.MvvmLight.Messaging;

namespace PlantATree
{
    internal class ErrorMessage : MessageBase
    {
        public Error Error { get; set; }
    }
}