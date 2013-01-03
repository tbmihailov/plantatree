using System.ComponentModel;

namespace PlantATree.Services
{
    public abstract class ServiceProviderBase
    {
        public virtual ITreeDataService TreeDataService { get; protected set; }
        public virtual IPageConductor PageConductor { get; protected set; }

        private static ServiceProviderBase _instance;
        public static ServiceProviderBase Instance
        {
            get 
            {
                if (_instance == null)
                {
                    CreateInstance(); 
                }
                return _instance;
            }
        }

        static ServiceProviderBase CreateInstance()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                _instance = new DesignServiceProvider();
            }
            else
            {
                _instance = new ServiceProvider();
            }
            return _instance;

        }
    }
}