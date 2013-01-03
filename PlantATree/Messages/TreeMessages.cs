//TreeMessages.cs
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using PlantATree.TreeService;
using PlantATree.ViewModels;

namespace PlantATree.Messages
{
    /// <summary>
    /// Lanuches a new edit message window
    /// </summary>
    internal class LaunchEditTreeMessage : MessageBase
    {
        public LaunchEditTreeMessage()
            : base()
        { }

        public LaunchEditTreeMessage(TreeViewModel tree)
        {
            this.Tree = tree;
        }

        public TreeViewModel Tree { get; set; }
    }

    /// <summary>
    /// Tells that tree is updated
    /// </summary>
    public class UpdatedTreeMessage : MessageBase
    {
        public UpdatedTreeMessage()
            : base()
        { }

        public UpdatedTreeMessage(TreeViewModel tree)
        {
            this.Tree = tree;
        }

        public TreeViewModel Tree { get; set; }
    }

    /// <summary>
    /// Shows a dialog when tree is saved
    /// </summary>
    public class SavedTreeDialogMessage : DialogMessage
    {

        public SavedTreeDialogMessage(string content, string title)
            : base(content, null)
        {
            Button = MessageBoxButton.OK;
            Caption = title;
        }
    }

    /// <summary>
    /// Tells the conductor to launch new tree
    /// </summary>
    public class LaunchNewTreeMessage : MessageBase
    {
        public LaunchNewTreeMessage()
            : base()
        { }

        public LaunchNewTreeMessage(TreeViewModel tree)
        {
            this.Tree = tree;
        }

        public TreeViewModel Tree { get; set; }
    }

    /// <summary>
    /// Tells the ForestNature to render new message on the hill
    /// </summary>
    public class RenderTreeMessage : MessageBase
    {
        public RenderTreeMessage()
            : base()
        { }

        public RenderTreeMessage(TreeViewModel tree)
            : base()
        {
            this.Tree = tree;
        }

        public TreeViewModel Tree { get; set; }
    }

    /// <summary>
    /// Tells that treesave is successfull
    /// </summary>
    public class TreeSaveSuccessfullMessage : MessageBase
    {
        public TreeViewModel Tree { get; set; }

        public TreeSaveSuccessfullMessage()
            : base()
        {
        }

        public TreeSaveSuccessfullMessage(TreeViewModel tree)
            : base()
        {
            this.Tree = tree;
        }

    }

    /// <summary>
    /// Tells that tree save is UNsuccessfull
    /// </summary>
    public class TreeSaveUnsuccessfullMessage : MessageBase
    {
        public TreeViewModel Tree { get; set; }

        public TreeSaveUnsuccessfullMessage()
            : base()
        {
        }

        public TreeSaveUnsuccessfullMessage(TreeViewModel tree)
            : base()
        {
            this.Tree = tree;
        }
    }

    /// <summary>
    /// Tells the NewTreeWindow that NewTree is canceled
    /// </summary>
    public class NewTreeCanceledMessage : MessageBase
    {
        public NewTreeCanceledMessage()
            : base()
        { }
    }

    /// <summary>
    /// Adds a tree to the trees collection in the ForestNatureViewModel
    /// </summary>
    public class AddTreeToTreesListMessage : MessageBase
    {
        public TreeViewModel Tree { get; set; }

        public AddTreeToTreesListMessage()
            : base()
        {
        }

        public AddTreeToTreesListMessage(TreeViewModel treeToAdd)
            : base()
        {
            this.Tree = treeToAdd;
        }
    }


}
