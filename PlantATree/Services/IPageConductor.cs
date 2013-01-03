using System;

namespace PlantATree  
{
    public interface IPageConductor
    {
        void DisplayError(string origin, Exception e, string details);
        void DisplayError(string origin, Exception e);
        void GoToView(string viewToken);
        //void GoToCheckoutView(string viewToken, string stateKey, Book book);
        void GoBack();

        //void PushState(string key, object value);
        //T PopState<T>(string key) where T : class;
        //T PeekState<T>(string key) where T : class;
    }
}