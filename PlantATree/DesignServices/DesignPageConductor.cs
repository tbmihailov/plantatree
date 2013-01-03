using System;
using System.Collections.Generic;

namespace PlantATree
{
    public class DesignPageConductor : IPageConductor
    {
        protected Dictionary<string, object> State = new Dictionary<string, object>();

        public DesignPageConductor()
        {
        }

        public void DisplayError(string origin, Exception e, string details)
        {
            return;
        }

        public void DisplayError(string origin, Exception e)
        {
            return;
        }

        public void GoToView(string viewToken)
        {
            return;
        }


        public void GoBack()
        {
            return;
        }

    }
}