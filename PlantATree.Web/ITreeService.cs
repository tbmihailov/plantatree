using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace PlantATree.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITreeService" in both code and config file together.
    [ServiceContract]
    public interface ITreeService
    {

        [OperationContract]
        IEnumerable<Tree> GetTrees();

        [OperationContract]
        int InsertTree(Tree newTree);

        [OperationContract]
        int GetTreesCount();

    }
}
