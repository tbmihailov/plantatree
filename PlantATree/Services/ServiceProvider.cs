namespace PlantATree.Services
{
    public class ServiceProvider : ServiceProviderBase
    {
        public ServiceProvider()
        {
            PageConductor = new PageConductor();
            
        }

        public override ITreeDataService TreeDataService
        {
            get 
            {
                return new TreeDataService(); 
            }
        }

       
    }
}